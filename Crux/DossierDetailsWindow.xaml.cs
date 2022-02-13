using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Crux;

public partial class DossierDetailsWindow
{
    public DossierDetailsWindow(PortfolioDossier paramDossier, List<string> paramGroupList)
    {
        InitializeComponent();
        _groupList = paramGroupList;
        _permdossier = paramDossier;
        _tempDossier = new PortfolioDossier(_permdossier.DossierType);
        _tempDossier.Mirror(_permdossier);
        Title = _permdossier.DossierType == PortfolioDossier.DossierTypeConstants.AccountDossier ? "Account details" : "Service details";
    }
    
    private readonly PortfolioDossier _permdossier;
        private readonly PortfolioDossier _tempDossier;
        private readonly List<string> _groupList;
        private bool _allowEdits;

        private class AlertDisplayItem
        {
            public string? DateForDisplay { get; set; }
            public string? Caption { get; set; }
            public string? Amount { get; set; }
            public bool Overdue { get; set; }
        }


        private readonly System.Collections.ObjectModel.ObservableCollection<AlertDisplayItem> _alertDisplayList=new System.Collections.ObjectModel.ObservableCollection<AlertDisplayItem>();

        private void CheckOk()
        {
            if (!_allowEdits) { return; }
            
            ButtonOk.IsEnabled = (!(string.IsNullOrWhiteSpace(ComboboxGroupName.Text) || string.IsNullOrWhiteSpace(TextboxTitleSpecifics.Text)));
           
            _tempDossier.Title = ComboboxGroupName.Text + ":" + TextboxTitleSpecifics.Text;

            _tempDossier.ProviderOrganisation = TextboxProviderOrganisation.Text;

            if (CheckboxEnableDate.IsChecked == true)
            { if (DatepickerSumDate.SelectedDate.HasValue) { _tempDossier.LastDate = DatepickerSumDate.SelectedDate.Value; } }
            else
            { _tempDossier.LastDate = new DateTime(1954, 1, 1); }

            if (CheckboxDocument.IsChecked.HasValue) { _tempDossier.IncludeInDocument = CheckboxDocument.IsChecked.Value; }
            if (CheckboxEuros.IsChecked.HasValue) { _tempDossier.CurrencyEuro = CheckboxEuros.IsChecked.Value; }
            if (CheckboxObsolete.IsChecked.HasValue) { _tempDossier.Obsolete = CheckboxObsolete.IsChecked.Value; }
            if (CheckboxOption.IsChecked.HasValue) { _tempDossier.Option = CheckboxOption.IsChecked.Value; }

            if (Single.TryParse(TextboxAmount.Text, out var s)) { _tempDossier.Amount = s; }
            
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            ComboboxGroupName.Items.Clear();
            foreach (string s in _groupList) { ComboboxGroupName.Items.Add(s); }
            ComboboxGroupName.SelectedIndex = 0;
            DatepickerSumDate.DisplayDateEnd = DateTime.Today;
            if (_permdossier.DossierType== PortfolioDossier.DossierTypeConstants.AccountDossier)
            {
                TextblockTitle.Text = "Account";
                TextblockAmount.Text = "Balance";
                TextblockDateCaption.Text = "As at";
                CheckboxOption.Content = "Account operated online";
                CheckboxObsolete.Content = "Obsolete account";
                CheckboxDocument.IsChecked = true;
                CheckboxDocument.IsEnabled = false;
                ComboboxGroupName.ToolTip="Account category: If this is 'Bank' then the balance date will show red in the list if more than 7 days old.";
            }
            else
            {
                TextblockTitle.Text = "Service";
                TextblockAmount.Text = "Last payment";
                TextblockDateCaption.Text = "Last payment date";
                CheckboxOption.Content = "Paid by direct debit";
                CheckboxObsolete.Content = "Obsolete service";
                ComboboxGroupName.ToolTip = "Service category";
            }
            
            ComboboxGroupName.Text = _tempDossier.GroupName;
            TextboxTitleSpecifics.Text = _tempDossier.TitleSpecifics;
            TextboxProviderOrganisation.Text = _tempDossier.ProviderOrganisation;
            CheckboxObsolete.IsChecked = _tempDossier.Obsolete;
            CheckboxEuros.IsChecked = _tempDossier.CurrencyEuro;
            CheckboxOption.IsChecked = _tempDossier.Option;
            CheckboxDocument.IsChecked = _tempDossier.IncludeInDocument;
            TextboxAmount.Text = _tempDossier.Amount.ToString(CultureInfo.CurrentCulture);
            if (_tempDossier.LastDate<new DateTime(2000,1,1))
            {
                CheckboxEnableDate.IsChecked = false;
                DatepickerSumDate.IsEnabled = false;
                DatepickerSumDate.SelectedDate = DateTime.Today;
            }
            else
            {
                CheckboxEnableDate.IsChecked = true;
                DatepickerSumDate.IsEnabled = true;
                DatepickerSumDate.SelectedDate = _tempDossier.LastDate;
            }
            VerifyTextBoxEntrySingle(TextboxAmount);
            ButtonOk.IsEnabled = false;
            RefreshAlertsList();
            RefreshReferencesList();
            ListviewAlerts.ItemsSource = _alertDisplayList;
            //listboxReferences.ItemsSource = ReferenceDisplayList;
            _allowEdits = true;
        }

        private void RefreshAlertsList()
        {
            _alertDisplayList.Clear();
            foreach (PortfolioDossier.ClassAlert alert in _tempDossier.Alerts)
            {
                AlertDisplayItem al = new AlertDisplayItem
                {
                    Caption = alert.Caption,
                    DateForDisplay =alert.AlertDate.ToString("dd MMM yyyy")
                    ,
                    Overdue = alert.AlertDate < DateTime.Today
                    ,
                    Amount =alert.ShowAmount ? _tempDossier.AmountString : string.Empty
                };
                _alertDisplayList.Add(al);
            }
        }

        private void RefreshReferencesList()
        {
            ListboxReferences.Items.Clear();
            foreach (var reference in _tempDossier.References)
            {
                Grid gd = new Grid();
                RowDefinition rdCaption = new RowDefinition();
                RowDefinition rdText = new RowDefinition
                {
                    Height = GridLength.Auto
                };
                gd.RowDefinitions.Add(rdCaption);
                gd.RowDefinitions.Add(rdText);
                TextBlock tbCaption = new TextBlock();
                TextBlock tbText = new TextBlock();
                Grid.SetRow(tbCaption, 0);
                Grid.SetRow(tbText, 1);
                gd.Children.Add(tbCaption);
                gd.Children.Add(tbText);
                tbCaption.Foreground = Brushes.ForestGreen;
                tbText.Foreground = Brushes.ForestGreen;
                tbText.Width = ListboxReferences.ActualWidth;
                tbText.TextWrapping = TextWrapping.Wrap;
                tbText.FontFamily = new FontFamily("Lucida Console");
                tbCaption.Text =reference.Caption;
                tbText.Text =reference.TextWithReturns;
                tbCaption.FontWeight = FontWeights.Bold;
                if (reference.Highlighted)
                {
                    tbCaption.Background = tbText.Background = Brushes.Yellow;
                }

                ListboxReferences.Items.Add(gd);              
            }
        }

        private void checkboxEnableDate_Checked(object sender, RoutedEventArgs e)
        {
            DatepickerSumDate.IsEnabled = true;
            ButtonToday.Visibility = Visibility.Visible;
            CheckOk();
        }

        private void checkboxEnableDate_Unchecked(object sender, RoutedEventArgs e)
        {
            DatepickerSumDate.IsEnabled = false;
            ButtonToday.Visibility = Visibility.Hidden;
            CheckOk();
        }

        private void buttonToday_Click(object sender, RoutedEventArgs e)
        {
            CheckboxEnableDate.IsChecked = true;
            DatepickerSumDate.SelectedDate = DateTime.Today;
        }

        private static void VerifyTextBoxEntrySingle(TextBox tb)
        {
            if (Single.TryParse(tb.Text, out var sng))
            {
                tb.Text = sng.ToString("#,0.00");
                tb.Foreground = new SolidColorBrush(Colors.ForestGreen);
            }
            else
            {
                tb.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void checkboxEuros_Checked(object sender, RoutedEventArgs e)
        {
            TextblockSymbol.Text = PortfolioCore.EuroSymbol.ToString();
            CheckOk();
        }

        private void checkboxEuros_Unchecked(object sender, RoutedEventArgs e)
        {
            TextblockSymbol.Text = PortfolioCore.PoundSymbol.ToString();
            CheckOk();
        }

        private void listviewAlerts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var enab = ListviewAlerts.SelectedItem != null;
            ButtonAlertDelete.IsEnabled = enab;
            ButtonAlertEdit.IsEnabled = enab;
        }

        private void comboboxGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckOk();
        }

        private void textboxTitleSpecifics_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckOk();
        }

        private void textboxProviderOrganisation_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckOk();
        }

        private void textboxAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckOk();
        }

        private void checkboxObsolete_Checked(object sender, RoutedEventArgs e)
        {
            CheckOk();
        }

        private void checkboxObsolete_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckOk();
        }

        private void checkboxOption_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckOk();
        }

        private void checkboxDocument_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckOk();
        }

        private void checkboxOption_Checked(object sender, RoutedEventArgs e)
        {
            CheckOk();
        }

        private void checkboxDocument_Checked(object sender, RoutedEventArgs e)
        {
            CheckOk();
        }

        private void datepickerSumDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? q = DatepickerSumDate.SelectedDate;
            ButtonToday.IsEnabled = !q.HasValue || q.Value != DateTime.Today;
            CheckOk();
        }

        private void textboxAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifyTextBoxEntrySingle(TextboxAmount);
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            ButtonCopy.IsEnabled = false;
            string copied = _tempDossier.GroupName + ":" + _tempDossier.TitleSpecifics;
            copied += Environment.NewLine + "Provider: " + _tempDossier.ProviderOrganisation;
            copied += Environment.NewLine + "Amount: " + _tempDossier.AmountString;
            if (CheckboxEnableDate.IsChecked==true) { copied += " as at " + _tempDossier.LastDate.ToLongDateString(); }
            foreach (var alert in _tempDossier.Alerts)
            {
                copied += Environment.NewLine + "Alert: " + alert.AlertDate.ToShortDateString() + ": " + alert.Caption;
            }

            foreach (var reference in _tempDossier.References)
            {
                copied += Environment.NewLine + "Reference: " + reference.Caption + ": " + reference.TextWithReturns;  
            }
            
            Clipboard.SetText(copied);
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            _permdossier.Mirror(_tempDossier);
            DialogResult = true;
        }

        private void ButtonAlertAdd_Click(object sender, RoutedEventArgs e)
        {
            PortfolioDossier.ClassAlert al = new PortfolioDossier.ClassAlert();
            DossierSubDetailsWindow w = new DossierSubDetailsWindow(1);
            w.SetAlert(al);
            w.Owner = this;
            bool? answ = w.ShowDialog();
            if (answ == true)
            {
                _tempDossier.AddAlert(al); // al will have been modified by windowPortfolioSubDetails
                RefreshAlertsList();
                CheckOk();
            }
        }

        private void ButtonReferenceAdd_Click(object sender, RoutedEventArgs e)
        {
            PortfolioDossier.ClassReference rf = new PortfolioDossier.ClassReference();
            DossierSubDetailsWindow w = new DossierSubDetailsWindow(2);
            w.SetReference(rf);
            w.Owner = this;
            bool? answ = w.ShowDialog();
            if (answ==true)
            {
                _tempDossier.AddReference(rf); // rf will have been modified by windowPortfolioSubDetails
                RefreshReferencesList();
                CheckOk();
            }
        }

        private void ButtonAlertDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = ListviewAlerts.SelectedIndex;
            if (MessageBox.Show(_tempDossier.Alerts[index].AlertDate.ToShortDateString() + " " + _tempDossier.Alerts[index].Caption + "\n\nDelete this alert?", "Delete alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) { return; }
            _tempDossier.DeleteAlert(index);
            RefreshAlertsList();
            CheckOk();
        }

        private void buttonReferenceDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = ListboxReferences.SelectedIndex;
            if (MessageBox.Show(_tempDossier.References[index].Caption + ": " + _tempDossier.References[index].TextWithReturns + "\n\nDelete this reference?", "Delete reference", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) { return; }
            _tempDossier.DeleteReference(index);
            RefreshReferencesList();
            CheckOk();
        }

        private void ButtonAlertEdit_Click(object sender, RoutedEventArgs e)
        {
            AlertEdit(ListviewAlerts.SelectedIndex);
        }

        private void AlertEdit(int index)
        {
            DossierSubDetailsWindow w = new DossierSubDetailsWindow(1);
            w.SetAlert(_tempDossier.Alerts[index]);
            w.Owner = this;
            bool? answ = w.ShowDialog();
            if (answ == true)
            {
                // the selected alert will have been modified by windowPortfolioSubDetails
                RefreshAlertsList();
                CheckOk();
            }
        }

        private void buttonReferenceEdit_Click(object sender, RoutedEventArgs e)
        {
            ReferenceEdit(ListboxReferences.SelectedIndex);
        }

        private void ReferenceEdit(int index)
        {
            DossierSubDetailsWindow w = new DossierSubDetailsWindow(2);
            w.SetReference(_tempDossier.References[index]);
            w.Owner = this;
            bool? answ = w.ShowDialog();
            if (answ == true)
            {
                // the selected reference will have been modified by windowPortfolioSubDetails
                RefreshReferencesList();
                CheckOk();
            }
        }
        
        private void listviewAlerts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ButtonAlertEdit.IsEnabled) { AlertEdit(ListviewAlerts.SelectedIndex); }
        }

        private void buttonReferencePromote_Click(object sender, RoutedEventArgs e)
        {
            _tempDossier.PromoteReference(ListboxReferences.SelectedIndex);
            RefreshReferencesList();
            CheckOk();
        }

        private void listboxReferences_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ButtonReferenceEdit.IsEnabled) { ReferenceEdit(ListboxReferences.SelectedIndex); }
        }

        private void listboxReferences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var enab = ListboxReferences.SelectedItem != null;
            ButtonReferenceDelete.IsEnabled = enab;
            ButtonReferenceEdit.IsEnabled = enab;
            ButtonReferencePromote.IsEnabled = enab && (ListboxReferences.SelectedIndex > 0);
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
}