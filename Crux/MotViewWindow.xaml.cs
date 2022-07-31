using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Crux;

public partial class MotViewWindow
{
    public MotViewWindow(string motSpec, MotList ml)
        {
            InitializeComponent();
            _revisedSpecification = _originalSpecification = motSpec;
            _pword = new Mot() { Specification = motSpec };
            _pool = ml;
        }
        private readonly Mot _pword;
        private readonly MotList _pool; // so that the reference can be passed on to the edit window 
        private readonly string _originalSpecification;
        private string _revisedSpecification;

        public string RevisedSpecification => _revisedSpecification;

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            ButtonReset.IsEnabled = false;
            FeaturesListBox.Items.Clear();
            BuildList();
        }

        private void BuildList()
        {
            foreach (string a in _pword.Aliases)
            {
                FeaturesListBox.Items.Add(new ListBoxItem() { IsHitTestVisible = false, Content = new Border() {Background= Brushes.Ivory, BorderBrush = Brushes.SaddleBrown, BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(4), Margin = new Thickness(6, 2, 6,2), Child = new TextBlock() { Background = Brushes.Ivory, Text = a, FontSize = 14, FontFamily = new FontFamily("Consolas"), Foreground = Brushes.SaddleBrown, Margin = new Thickness(6, 4, 6, 4) } } });
            }
            for (int i = 0; i < _pword.ElementCount; i++)
            {
                MotElement elem = _pword.Element[i];
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
                    Text = _pword.Element[i].Caption,
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
                    Text = _pword.Element[i].Content,
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
            foreach (var t in FeaturesListBox.Items)
            {
                if (t is ListBoxItem {Content: DockPanel dp})
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

            ButtonReset.IsEnabled = false;
        }

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button q)
            {
                int i = (int) q.Tag;
                if (q.Content.ToString() == "Copy")
                {
                    Clipboard.SetText(_pword.Element[i].Content);
                }
                else
                {
                    try
                    {
                        Process myProcess = new Process();
                        myProcess.StartInfo.UseShellExecute = true;
                        myProcess.StartInfo.FileName = _pword.Element[i].Content;
                        myProcess.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message == "The system cannot find the file specified"
                                ? "Windows thinks you are trying to open a file.\n\nYou need to ensure the web address is fully specified."
                                : ex.Message
                            , _pword.Element[i].Content, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                q.Visibility = Visibility.Hidden;
                ButtonReset.IsEnabled = true;
                ButtonReset.Focus();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = _revisedSpecification != _originalSpecification;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MotEditor w = new MotEditor(_revisedSpecification, _pool, false)
            {
                Owner = this
            };
            bool? sd = w.ShowDialog();
            if (sd?? false) 
            {
                _pword.Specification =_revisedSpecification = w.EditedSpecification;
                RefreshView();
            }
        }
}