using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Crux;

public partial class GiftsWindow : Window
{
    public GiftsWindow()
    {
        InitializeComponent();
    }
    private struct Donation
        {
            public string G_date { get;set;}
            public string G_age { get;set;}
            public string G_detail { get; set; }
            public string G_amount { get; set; }
            public Brush Tint { get; set; }
        }

        private struct TaxYearSummary
        {
            public string Y_dates { get; set; }
            public string Y_count { get; set; }
            public string Y_gross { get; set; }
            public string Y_net { get; set; }
        }

        private readonly System.Collections.ObjectModel.ObservableCollection<Donation> _giftsList = new System.Collections.ObjectModel.ObservableCollection<Donation>();
        private readonly System.Collections.ObjectModel.ObservableCollection<TaxYearSummary> _taxYearsList = new System.Collections.ObjectModel.ObservableCollection<TaxYearSummary>();

        private GiftList _dons;


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

            _dons = new GiftList();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            RebuildListViewSources();
        }

        private void RebuildListViewSources()
        {
            Donation d = new Donation();
            TaxYearSummary ty = new TaxYearSummary();

            _giftsList.Clear();
            foreach (GiftList.GiftGiven g in _dons.Gifts)
            {
                d = new Donation() { G_amount = g.Amount.ToString("#0.00"), G_date = g.GiftDate.ToShortDateString(), G_detail = g.Detail, G_age = g.AgeString };
                if (g.LessThanSevenYearsOld) { d.Tint = Brushes.Black; } else { d.Tint = Brushes.Gray; }
                _giftsList.Add(d);
            }
            GiftsListView.ItemsSource = _giftsList;
            // scroll to last item
            if (!string.IsNullOrWhiteSpace(d.G_detail))
            {
                GiftsListView.ScrollIntoView(d);
            }

            AssumptionTextBlock.Text = $"Assumes an annual exemption of £{GiftList.AnnualExemption}, and only includes gifts dated within 7 years of today.";

            _taxYearsList.Clear();
            for (int yr=DateTime.Today.Year-8; yr <= DateTime.Today.Year; yr++)
            {
                ty = new TaxYearSummary
                {
                    Y_dates = $"{yr}-{yr + 1}",
                    Y_gross ="£"+ _dons.TaxYearTotal(yr, out int ycount).ToString("#,0.00"),
                    Y_net ="£"+ _dons.TaxYearTotalLessAnnualExemption(yr).ToString("#,0.00"),
                    Y_count = ycount.ToString()
                };
                _taxYearsList.Add(ty);
            }
            YearsListView.ItemsSource = _taxYearsList;

            // summary 
            AllTimeTotalTextBlock.Text ="£"+ _dons.AllTimeTotal(out int AllCount).ToString("#,0.00");
            AllTimeCountTextBlock.Text = $"{AllCount} gifts";

            SevenYearTotalTextBlock.Text = "£" + _dons.SevenYearTimeTotal(out int SevenCount).ToString("#,0.00");
            SevenYearCountTextBlock.Text =$"{SevenCount} gifts";

            PotentiallyChargeableTextBlock.Text = "£" + _dons.MyCalculationOfTotalPotentiallyChargeableGifts().ToString("#,0.00");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _dons.SaveData();
        }

        private void GiftsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int s = GiftsListView.SelectedIndex;
            if (s < 0)
            {
                buttonDelete.IsEnabled = false;
                buttonEdit.IsEnabled = false;
            }
            else
            {
                buttonDelete.IsEnabled = true;
                buttonEdit.IsEnabled = true;
                Donation don = (Donation)GiftsListView.SelectedItem;
                GiftAmountBox.Text = don.G_amount;
                GiftDatePicker.SelectedDate = DateTime.Parse(don.G_date);
                GiftDetailBox.Text = don.G_detail;
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answ = MessageBox.Show("Do you want to delete the selected gift?", Jbh.AppManager.AppName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answ == MessageBoxResult.Yes)
            {
                int s = GiftsListView.SelectedIndex;
                _dons.DeleteItem(s);
                RebuildListViewSources();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!EnteredDataIsOK()) { MessageBox.Show("Invalid data entered", Jbh.AppManager.AppName, MessageBoxButton.OK, MessageBoxImage.Asterisk); return; }

            DateTime when = GiftDatePicker.SelectedDate.Value;
            double howmuch =double.Parse(GiftAmountBox.Text);
            string what = GiftDetailBox.Text.Trim();

            GiftList.GiftGiven g = new GiftList.GiftGiven
            {
                Amount = howmuch,
                Detail = what,
                GiftDate = when
            };

            _dons.AddItem(g);
            RebuildListViewSources();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!EnteredDataIsOK()) { MessageBox.Show("Invalid data entered", Jbh.AppManager.AppName, MessageBoxButton.OK, MessageBoxImage.Asterisk); return; }

            MessageBoxResult answ = MessageBox.Show("Do you want to amend the details of the selected gift?", Jbh.AppManager.AppName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answ == MessageBoxResult.Yes)
            {
                DateTime when = GiftDatePicker.SelectedDate.Value;
                double howmuch = double.Parse(GiftAmountBox.Text);
                string what = GiftDetailBox.Text.Trim();

                int s = GiftsListView.SelectedIndex;
                _dons.Gifts[s].Amount = howmuch;
                _dons.Gifts[s].Detail=what;
                _dons.Gifts[s].GiftDate = when;
                RebuildListViewSources();
            }
        }

        private bool EnteredDataIsOK()
        {
            bool OK = true;
        DateTime when = DateTime.MinValue;
        string what = string.Empty;
            if (!double.TryParse(GiftAmountBox.Text, out double howmuch))
            {
                OK = false;
            }
            if (!GiftDatePicker.SelectedDate.HasValue)
            {
                OK = false;
            }
            if (string.IsNullOrWhiteSpace(GiftDetailBox.Text))
            {
                OK = false;
            }
            return OK;
        }

}