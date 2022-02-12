using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Crux;

public partial class MotViewWindow : Window
{
    public MotViewWindow(string motSpec, MotList ml)
        {
            InitializeComponent();
            revisedSpecification = originalSpecification = motSpec;
            pword = new Mot() { Specification = motSpec };
            pool = ml;
        }
        private readonly Mot pword;
        private MotList pool; // so that the reference can be passed on to the edit window 
        private string originalSpecification;
        private string revisedSpecification;

        public string RevisedSpecification { get => revisedSpecification; }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            buttonReset.IsEnabled = false;
            FeaturesListBox.Items.Clear();
            BuildList();
        }

        private void BuildList()
        {
            foreach (string a in pword.Aliases)
            {
                FeaturesListBox.Items.Add(new ListBoxItem() { IsHitTestVisible = false, Content = new Border() {Background= Brushes.Ivory, BorderBrush = Brushes.SaddleBrown, BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(4), Margin = new Thickness(6, 2, 6,2), Child = new TextBlock() { Background = Brushes.Ivory, Text = a, FontSize = 14, FontFamily = new FontFamily("Consolas"), Foreground = Brushes.SaddleBrown, Margin = new Thickness(6, 4, 6, 4) } } });
            }
            for (int i = 0; i < pword.ElementCount; i++)
            {
                MotElement elem = pword.Element[i];
                DockPanel dp = new DockPanel();
                Button buttonCopy = new Button
                {
                    Content = elem.IsLink ? "Link" : "Copy",
                    Foreground = elem.IsLink ? Brushes.Magenta : Brushes.DarkSlateGray,Background=Brushes.Ivory,
                    Tag = i,
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 80,
                    Padding = new Thickness(4, 2, 4, 2),
                    Margin = new Thickness(4, 2, 4, 2)
                };
                buttonCopy.Click += ItemButton_Click; // add click event handler
                dp.Children.Add(buttonCopy);
                dp.Children.Add(new TextBlock
                {
                    Text = pword.Element[i].Caption,
                    Foreground = new SolidColorBrush(Colors.MidnightBlue),
                    FontFamily = new FontFamily("Lucida Console"),
                    FontSize = 12,
                    FontWeight = FontWeights.Medium,
                    VerticalAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(4, 2, 4, 2),
                    Margin = new Thickness(4, 2, 4, 2),
                    IsHitTestVisible = false
                });
                dp.Children.Add(new TextBlock
                {
                    Text = pword.Element[i].Content,
                    Foreground = new SolidColorBrush(Colors.DarkGreen),
                    FontFamily = new FontFamily("Lucida Console"),
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(4, 2, 4, 2),
                    Margin = new Thickness(4, 2, 4, 2),
                    IsHitTestVisible = false
                });
                FeaturesListBox.Items.Add(new ListBoxItem() { Content = dp });
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i=0; i<FeaturesListBox.Items.Count; i++)
            {
                if (FeaturesListBox.Items[i] is ListBoxItem lbi)
                {
                    if (lbi.Content is DockPanel dp)
                    {
                        foreach(object v in dp.Children)
                        {
                            if (v is Button b)
                            {
                                b.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
            }
            buttonReset.IsEnabled = false;
        }

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button q = sender as Button;
            int i = (int)q.Tag;
            if (q.Content.ToString() == "Copy")
            {
                Clipboard.SetText(pword.Element[i].Content);
            }
            else
            {
                try
                {
                    System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                    myProcess.StartInfo.FileName = pword.Element[i].Content;
                    myProcess.Start();
                }
                catch (Exception ex)
                {
                    if (ex.Message == "The system cannot find the file specified")
                    {
                        MessageBox.Show("Windows thinks you are trying to open a file.\n\nYou need to ensure the web address is fully specified.", pword.Element[i].Content, MessageBoxButton.OK, MessageBoxImage.Error); 
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, pword.Element[i].Content, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            q.Visibility = Visibility.Hidden;
            buttonReset.IsEnabled = true;
            buttonReset.Focus();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = revisedSpecification != originalSpecification;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
          MotEditor w = new MotEditor(originalSpecification, pool)
            {
                Owner = this
            };
            if (w.ShowDialog() == true) 
            {
                revisedSpecification = w.EditedSpecification;
                pword.Specification = revisedSpecification; 
                RefreshView();
            }
        }
}