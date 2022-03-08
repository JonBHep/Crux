using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Crux;

public partial class MiseAJourWindow
{
    public MiseAJourWindow(PortfolioCore folio)
    {
        InitializeComponent();
        _portfolio = folio;
    }

    private Dictionary<int, float> _today = new();
    private readonly PortfolioCore _portfolio;
    public Dictionary<int, float> MiseAJour => _today;

    private void MiseAJourWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        for (int x = 0; x < _portfolio.AccountCount; x++)
        {
            if (!_portfolio.Account(x).Obsolete)
            {
                StackPanel panel = new() {Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 2)};
                panel.Children.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center, Text = _portfolio.Account(x).GroupName
                    , FontWeight = FontWeights.Medium, Width = 100
                });
                panel.Children.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center, Text = _portfolio.Account(x).TitleSpecifics
                    , FontWeight = FontWeights.Bold, Width = 256
                });
                panel.Children.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center, Text = _portfolio.Account(x).AmountString
                    , TextAlignment = TextAlignment.Right, FontWeight = FontWeights.Bold, Width = 80
                });
                panel.Children.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center
                    , Text = _portfolio.Account(x).LastDate.ToShortDateString(), Width = 72
                    , Margin = new Thickness(6, 0, 0, 0)
                });
                panel.Children.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center, Text = "Today's balance:"
                    , Margin = new Thickness(0, 0, 4, 0)
                });
                TextBox boite = new TextBox()
                    {Tag = x, Width = 80, VerticalAlignment = VerticalAlignment.Center};
                boite.TextChanged += AmountBox_OnTextChanged;
                panel.Children.Add(boite);
                
                // last child for display of difference
                panel.Children.Add(new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(8, 0, 0, 0)
                });
                
                AccountsListBox.Items.Add(new ListBoxItem() {Content = panel});
            }
        }
        UpdateProgress();
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void AmountBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox box)
        {
            if (box.Tag is int y)
            {
                if (box.Parent is StackPanel panel)
                {
                    if (panel.Children[panel.Children.Count - 1] is TextBlock balanceBlock)
                    {
                        string what = box.Text.Trim();
                        if (string.IsNullOrWhiteSpace(what))
                        {
                            ClearEntry(y);
                            balanceBlock.Text = string.Empty;
                            box.Background = Brushes.White;
                        }
                        else
                        {
                            if (float.TryParse(what, out float dub))
                            {
                                SetEntry(y, dub);
                                float balance = dub - _portfolio.Account(y).Amount;
                                balanceBlock.Foreground = balance == 0 ? Brushes.Blue : balance>0 ? Brushes.Green : Brushes.Red;
                                balanceBlock.Text =$"{balance:+#,0.00;-#,0.00;0.00}";
                                box.Background = Brushes.PaleGreen;
                            }
                            else
                            {
                                ClearEntry(y);
                                balanceBlock.Text = string.Empty;
                                box.Background = Brushes.Pink;
                            }
                        }
                    }
                }
            }
        }
    }

    private void SetEntry(int index, float value)
    {
        if (_today.ContainsKey(index))
        {
            _today[index] = value;
        }
        else
        {
            _today.Add(index, value);
        }
        UpdateProgress();
    }
    
    private void ClearEntry(int index)
    {
        if (_today.ContainsKey(index))
        {
            _today.Remove(index);
            UpdateProgress();
        }
    }

    private void UpdateProgress()
    {
        DoneProgressBar.Maximum = AccountsListBox.Items.Count;
        DoneProgressBar.Value = _today.Count;
        ProgressTextBlock.Text = $"{_today.Count} of {AccountsListBox.Items.Count}";
    }
}
