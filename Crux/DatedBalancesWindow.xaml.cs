using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Crux;

public partial class DatedBalancesWindow
{
    public DatedBalancesWindow(List<PortfolioCore.ClassDatedBalances> bals)
    {
        InitializeComponent();
        _balances = bals;
    }
    private readonly List<PortfolioCore.ClassDatedBalances> _balances;
        private bool _loaded;
    
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            RefreshList();
            _loaded = true;

            // get min and max values
            int maxPounds = 0;
            int maxEuros = 0;
            foreach (var t in _balances)
            {
                if (t.BalancePounds > maxPounds) { maxPounds = t.BalancePounds; }
                if (t.BalanceEuros > maxEuros) { maxEuros = t.BalanceEuros; }
            }

            // get min and max dates
            long minDateTicks = _balances[0].BalanceDate.Ticks;
            long maxDateTicks = _balances[^1].BalanceDate.Ticks;

            // get horizontal and vertical spans
            int maxCurrency = Math.Max(maxPounds, maxEuros);
            double verticalValueSpan = maxCurrency;
            double horizontalValueSpan = maxDateTicks - minDateTicks;
            int graphMargin = 20;
            int graphWidth = (int)CanvasGraph.ActualWidth - (2 * graphMargin);
            int graphHeight = (int)CanvasGraph.ActualHeight - (2 * graphMargin);

            // Plot month and year markers
            DateTime earliest = _balances[0].BalanceDate;
            DateTime marker = new DateTime(earliest.Year, earliest.Month, 1);
            while (marker < DateTime.Today)
            {
                SolidColorBrush pinceau = marker.Month == 1 ? Brushes.Gray : Brushes.Gainsboro;
                double thik = marker.Month == 1 ? 1 : 0.8;
                long tickDiff = marker.Ticks - minDateTicks;
                double leftvalue = graphMargin + graphWidth * (tickDiff / horizontalValueSpan);
                Line m = new Line() { X1 = leftvalue, Y1 = graphMargin, X2 = leftvalue, Y2 = graphHeight + graphMargin, Stroke = pinceau, StrokeThickness = thik };
                CanvasGraph.Children.Add(m);

                if (marker.Month == 6)
                {
                    TextBlock t = new TextBlock() { Text = $"{marker:yyyy}", FontWeight = FontWeights.Medium, FontSize = 16, Foreground = Brushes.CadetBlue };
                    Canvas.SetLeft(t, leftvalue);
                    Canvas.SetBottom(t, 300);
                    CanvasGraph.Children.Add(t);
                }

                marker = marker.AddMonths(1);
            }

            // Plot amount markers
            // 100K
            int amountOffset = maxCurrency - 100000;
            double ypos = graphMargin + graphHeight * (amountOffset / verticalValueSpan);
            Line n = new Line() { X1 = graphMargin + 48, Y1 = ypos, X2 = graphMargin + graphWidth, Y2 = ypos, Stroke = Brushes.Orange, StrokeThickness = 1 };
            CanvasGraph.Children.Add(n);
            TextBlock tCent = new TextBlock() { Text = "£100K", FontWeight = FontWeights.Medium, FontSize = 16, Foreground = Brushes.Orange };
            Canvas.SetLeft(tCent, graphMargin);
            Canvas.SetTop(tCent, ypos - 12);
            CanvasGraph.Children.Add(tCent);
            // 50K
            amountOffset = maxCurrency - 50000;
            ypos = graphMargin + graphHeight * (amountOffset / verticalValueSpan);
            n = new Line() { X1 = graphMargin + 48, Y1 = ypos, X2 = graphMargin + graphWidth, Y2 = ypos, Stroke = Brushes.Orange, StrokeThickness = 1 };
            CanvasGraph.Children.Add(n);
            tCent = new TextBlock() { Text = "£50K", FontWeight = FontWeights.Medium, FontSize = 16, Foreground = Brushes.Orange };
            Canvas.SetLeft(tCent, graphMargin);
            Canvas.SetTop(tCent, ypos - 12);
            CanvasGraph.Children.Add(tCent);

            for (int p = 10000; p < maxCurrency; p += 10000)
            {
                if ((p != 50000) && (p != 100000))
                {
                    amountOffset = maxCurrency - p;
                    ypos = graphMargin + graphHeight * (amountOffset / verticalValueSpan);
                    n = new Line() { X1 = graphMargin, Y1 = ypos, X2 = graphMargin + graphWidth, Y2 = ypos, Stroke = Brushes.Orange, StrokeThickness = 0.5 };
                    CanvasGraph.Children.Add(n);
                }
            }

            // Plot pound balance
            long tickOffset = _balances[0].BalanceDate.Ticks - minDateTicks;
            int valueOffset = maxCurrency - _balances[0].BalancePounds;
            double startX = graphMargin + graphWidth * (tickOffset / horizontalValueSpan);
            double startY = graphMargin + graphHeight * (valueOffset / verticalValueSpan);

            for (int x = 1; x < _balances.Count; x++)
            {
                tickOffset = _balances[x].BalanceDate.Ticks - minDateTicks;
                valueOffset = maxCurrency - _balances[x].BalancePounds;
                double leftvalue = graphMargin + graphWidth * (tickOffset / horizontalValueSpan);
                double topvalue = graphMargin + graphHeight * (valueOffset / verticalValueSpan);
                Line m = new Line() { X1 = startX, Y1 = startY, X2 = leftvalue, Y2 = topvalue, Stroke = Brushes.SaddleBrown, StrokeThickness = 1 };
                CanvasGraph.Children.Add(m);
                startX = leftvalue;
                startY = topvalue;
            }

            // Plot euro balance
            tickOffset = _balances[0].BalanceDate.Ticks - minDateTicks;
            valueOffset = maxCurrency - _balances[0].BalanceEuros;
            startX = graphMargin + graphWidth * (tickOffset / horizontalValueSpan);
            startY = graphMargin + graphHeight * (valueOffset / verticalValueSpan);

            for (int x = 1; x < _balances.Count; x++)
            {
                tickOffset = _balances[x].BalanceDate.Ticks - minDateTicks;
                valueOffset = maxCurrency - _balances[x].BalanceEuros;
                double leftvalue = graphMargin + graphWidth * (tickOffset / horizontalValueSpan);
                double topvalue = graphMargin + graphHeight * (valueOffset / verticalValueSpan);
                Line m = new Line() { X1 = startX, Y1 = startY, X2 = leftvalue, Y2 = topvalue, Stroke = Brushes.DarkOliveGreen, StrokeThickness = 1 };
                CanvasGraph.Children.Add(m);
                startX = leftvalue;
                startY = topvalue;
            }
        }

        private void RefreshList()
        {
            BalancesListBox.Items.Clear();
            bool just2Years = ListLimitedTick.IsChecked == true;
            foreach (var t in _balances)
            {
                bool show = true;
                if (just2Years)
                {
                    TimeSpan ts = DateTime.Today - t.BalanceDate;
                    if (ts.Days > 731) { show = false; }
                }
                if (show)
                {
                    StackPanel sp = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };
                    TextBlock tbD = new TextBlock()
                    {
                        Text = t.BalanceDate.ToString("dd MMM yyyy"),
                        Width = 80,
                        Foreground = new SolidColorBrush(Colors.DarkMagenta)
                    };
                    sp.Children.Add(tbD);
                    TextBlock tbP = new TextBlock()
                    {
                        Text = PortfolioCore.PoundSymbol + t.BalancePounds.ToString("#,0"),
                        Width = 80,
                        Foreground = new SolidColorBrush(Colors.SaddleBrown),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        TextAlignment = TextAlignment.Right
                    };
                    sp.Children.Add(tbP);
                    TextBlock tbE = new TextBlock()
                    {
                        Text = PortfolioCore.EuroSymbol + t.BalanceEuros.ToString("#,0"),
                        Width = 80,
                        Foreground = new SolidColorBrush(Colors.DarkOliveGreen),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        TextAlignment = TextAlignment.Right
                    };
                    sp.Children.Add(tbE);
                    BalancesListBox.Items.Add(new ListBoxItem() { Content = sp });
                }
            }
            int last = BalancesListBox.Items.Count - 1;
            BalancesListBox.ScrollIntoView(BalancesListBox.Items[last]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double scrX = SystemParameters.PrimaryScreenWidth;
            double scrY = SystemParameters.PrimaryScreenHeight;
            double winX = scrX * .98;
            double winY = scrY * .94;
            double xm = (scrX - winX) / 2;
            double ym = (scrY - winY) / 4;
            Width = winX;
            Height = winY;
            Left = xm;
            Top = ym;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ListLimitedTick_Checked(object sender, RoutedEventArgs e)
        {
            if (_loaded) { RefreshList(); }
        }
}