using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Crux;

public partial class GiftsWindow
{
    public GiftsWindow()
    {
        InitializeComponent();
        _dons = new GiftList();
    }
    private struct Donation
        {
            public string GDate { get;set;}
            public string GAge { get;set;}
            public string GDetail { get; set; }
            public string GAmount { get; set; }
            public Brush Tint { get; set; }
        }

        private struct TaxYearSummary
        {
            public string YDates { get; set; }
            public string YCount { get; set; }
            public string YGross { get; set; }
            public string YNet { get; set; }
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

            
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            RebuildListViewSources();
        }

        private void RebuildListViewSources()
        {
            Donation d = new Donation();
            TaxYearSummary ty;

            _giftsList.Clear();
            foreach (GiftList.GiftGiven g in _dons.Gifts)
            {
                d = new Donation() { GAmount = g.Amount.ToString("#0.00"), GDate = g.GiftDate.ToShortDateString(), GDetail = g.Detail, GAge = g.AgeString };
                if (g.LessThanSevenYearsOld) { d.Tint = Brushes.Black; } else { d.Tint = Brushes.Gray; }
                _giftsList.Add(d);
            }
            GiftsListView.ItemsSource = _giftsList;
            // scroll to last item
            if (!string.IsNullOrWhiteSpace(d.GDetail))
            {
                GiftsListView.ScrollIntoView(d);
            }

            AssumptionTextBlock.Text = $"Assumes an annual exemption of £{GiftList.AnnualExemption}, and only includes gifts dated within 7 years of today.";

            _taxYearsList.Clear();
            for (int yr=DateTime.Today.Year-8; yr <= DateTime.Today.Year; yr++)
            {
                ty = new TaxYearSummary
                {
                    YDates = $"{yr}-{yr + 1}",
                    YGross ="£"+ _dons.TaxYearTotal(yr, out int ycount).ToString("#,0.00"),
                    YNet ="£"+ _dons.TaxYearTotalLessAnnualExemption(yr).ToString("#,0.00"),
                    YCount = ycount.ToString()
                };
                _taxYearsList.Add(ty);
            }
            YearsListView.ItemsSource = _taxYearsList;

            // summary 
            AllTimeTotalTextBlock.Text ="£"+ _dons.AllTimeTotal(out int allCount).ToString("#,0.00");
            AllTimeCountTextBlock.Text = $"{allCount} gifts";

            SevenYearTotalTextBlock.Text = "£" + _dons.SevenYearTimeTotal(out int sevenCount).ToString("#,0.00");
            SevenYearCountTextBlock.Text =$"{sevenCount} gifts";

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
                ButtonDelete.IsEnabled = false;
                ButtonEdit.IsEnabled = false;
            }
            else
            {
                ButtonDelete.IsEnabled = true;
                ButtonEdit.IsEnabled = true;
                Donation don = (Donation)GiftsListView.SelectedItem;
                GiftAmountBox.Text = don.GAmount;
                GiftDatePicker.SelectedDate = DateTime.Parse(don.GDate);
                GiftDetailBox.Text = don.GDetail;
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
            if (!EnteredDataIsOk()) { MessageBox.Show("Invalid data entered", Jbh.AppManager.AppName, MessageBoxButton.OK, MessageBoxImage.Asterisk); return; }

            DateTime when =GiftDatePicker.SelectedDate ?? DateTime.Today; // in fact will not be null - already checked
            double howMuch =double.Parse(GiftAmountBox.Text);
            string what = GiftDetailBox.Text.Trim();

            GiftList.GiftGiven g = new GiftList.GiftGiven
            {
                Amount = howMuch,
                Detail = what,
                GiftDate = when
            };

            _dons.AddItem(g);
            RebuildListViewSources();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!EnteredDataIsOk()) { MessageBox.Show("Invalid data entered", Jbh.AppManager.AppName, MessageBoxButton.OK, MessageBoxImage.Asterisk); return; }

            MessageBoxResult answ = MessageBox.Show("Do you want to amend the details of the selected gift?", Jbh.AppManager.AppName, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answ == MessageBoxResult.Yes)
            {
                DateTime when =GiftDatePicker.SelectedDate ?? DateTime.Today; // in fact will not be null - already checked
                double howMuch = double.Parse(GiftAmountBox.Text);
                string what = GiftDetailBox.Text.Trim();

                int s = GiftsListView.SelectedIndex;
                _dons.Gifts[s].Amount = howMuch;
                _dons.Gifts[s].Detail=what;
                _dons.Gifts[s].GiftDate = when;
                RebuildListViewSources();
            }
        }

        private bool EnteredDataIsOk()
        {
            bool ok = true;
            DateTime when = DateTime.MinValue;
            string what = string.Empty;
            if (!double.TryParse(GiftAmountBox.Text, out double howmuch))
            {
                ok = false;
            }

            if (!GiftDatePicker.SelectedDate.HasValue)
            {
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(GiftDetailBox.Text))
            {
                ok = false;
            }

            return ok;
        }

}