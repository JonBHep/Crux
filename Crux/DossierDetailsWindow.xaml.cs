using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Crux;

public partial class DossierDetailsWindow : Window
{
    public DossierDetailsWindow(PortfolioDossier paramDossier, List<string> paramGroupList)
    {
        InitializeComponent();
        _groupList = paramGroupList;
        _permdossier = paramDossier;
        _tempDossier = new PortfolioDossier(_permdossier.DossierType);
        _tempDossier.Mirror(_permdossier);
        if (_permdossier.DossierType == PortfolioDossier.DossierTypeConstants.AccountDossier) { Title = "Account details"; } else { Title = "Service details"; }
    }
    
    private readonly PortfolioDossier _permdossier;
        private readonly PortfolioDossier _tempDossier;
        private readonly List<string> _groupList;
        private bool allowEdits = false;

        private class AlertDisplayItem
        {
            public string DateForDisplay { get; set; }
            public string Caption { get; set; }
            public string Amount { get; set; }
            public bool Overdue { get; set; }
        }


        private readonly System.Collections.ObjectModel.ObservableCollection<AlertDisplayItem> AlertDisplayList=new System.Collections.ObjectModel.ObservableCollection<AlertDisplayItem>();

        private void CheckOk()
        {
            if (!allowEdits) { return; }
            
            buttonOK.IsEnabled = (!(string.IsNullOrWhiteSpace(comboboxGroupName.Text) || string.IsNullOrWhiteSpace(textboxTitleSpecifics.Text)));
           
            _tempDossier.Title = comboboxGroupName.Text + ":" + textboxTitleSpecifics.Text;

            _tempDossier.ProviderOrganisation = textboxProviderOrganisation.Text;

            if (checkboxEnableDate.IsChecked == true)
            { if (datepickerSumDate.SelectedDate.HasValue) { _tempDossier.LastDate = datepickerSumDate.SelectedDate.Value; } }
            else
            { _tempDossier.LastDate = new DateTime(1954, 1, 1); }

            if (checkboxDocument.IsChecked.HasValue) { _tempDossier.IncludeInDocument = checkboxDocument.IsChecked.Value; }
            if (checkboxEuros.IsChecked.HasValue) { _tempDossier.CurrencyEuro = checkboxEuros.IsChecked.Value; }
            if (checkboxObsolete.IsChecked.HasValue) { _tempDossier.Obsolete = checkboxObsolete.IsChecked.Value; }
            if (checkboxOption.IsChecked.HasValue) { _tempDossier.Option = checkboxOption.IsChecked.Value; }

            Single s;
            if (Single.TryParse(textboxAmount.Text, out s)) { _tempDossier.Amount = s; }
            
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            comboboxGroupName.Items.Clear();
            foreach (string s in _groupList) { comboboxGroupName.Items.Add(s); }
            comboboxGroupName.SelectedIndex = 0;
            datepickerSumDate.DisplayDateEnd = DateTime.Today;
            if (_permdossier.DossierType== PortfolioDossier.DossierTypeConstants.AccountDossier)
            {
                textblockTitle.Text = "Account";
                textblockAmount.Text = "Balance";
                textblockDateCaption.Text = "As at";
                checkboxOption.Content = "Account operated online";
                checkboxObsolete.Content = "Obsolete account";
                checkboxDocument.IsChecked = true;
                checkboxDocument.IsEnabled = false;
                comboboxGroupName.ToolTip="Account category: If this is 'Bank' then the balance date will show red in the list if more than 7 days old.";
            }
            else
            {
                textblockTitle.Text = "Service";
                textblockAmount.Text = "Last payment";
                textblockDateCaption.Text = "Last payment date";
                checkboxOption.Content = "Paid by direct debit";
                checkboxObsolete.Content = "Obsolete service";
                comboboxGroupName.ToolTip = "Service category";
            }
            
            comboboxGroupName.Text = _tempDossier.GroupName;
            textboxTitleSpecifics.Text = _tempDossier.TitleSpecifics;
            textboxProviderOrganisation.Text = _tempDossier.ProviderOrganisation;
            checkboxObsolete.IsChecked = _tempDossier.Obsolete;
            checkboxEuros.IsChecked = _tempDossier.CurrencyEuro;
            checkboxOption.IsChecked = _tempDossier.Option;
            checkboxDocument.IsChecked = _tempDossier.IncludeInDocument;
            textboxAmount.Text = _tempDossier.Amount.ToString();
            if (_tempDossier.LastDate<new DateTime(2000,1,1))
            {
                checkboxEnableDate.IsChecked = false;
                datepickerSumDate.IsEnabled = false;
                datepickerSumDate.SelectedDate = DateTime.Today;
            }
            else
            {
                checkboxEnableDate.IsChecked = true;
                datepickerSumDate.IsEnabled = true;
                datepickerSumDate.SelectedDate = _tempDossier.LastDate;
            }
            VerifyTextBoxEntrySingle(textboxAmount);
            buttonOK.IsEnabled = false;
            RefreshAlertsList();
            RefreshReferencesList();
            listviewAlerts.ItemsSource = AlertDisplayList;
            //listboxReferences.ItemsSource = ReferenceDisplayList;
            allowEdits = true;
        }

        private void RefreshAlertsList()
        {
            AlertDisplayList.Clear();
            for(int a =0;a<_tempDossier.AlertCount;a++)
            { 
                AlertDisplayItem al = new AlertDisplayItem();
                al.Caption = _tempDossier.Alert(a).Caption;
                al.DateForDisplay = _tempDossier.Alert(a).AlertDate.ToString("dd MMM yyyy");
                al.Overdue = (_tempDossier.Alert(a).AlertDate < DateTime.Today);
                if (_tempDossier.Alert(a).ShowAmount) { al.Amount = _tempDossier.AmountString; } else { al.Amount = string.Empty; }
                AlertDisplayList.Add(al);
            }
        }

        private void RefreshReferencesList()
        {
            listboxReferences.Items.Clear();
            for (int r = 0; r < _tempDossier.ReferenceCount; r++)
            {
                Grid gd = new Grid();
                RowDefinition rdCaption = new RowDefinition();
                RowDefinition rdText = new RowDefinition();
                rdText.Height = GridLength.Auto;
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
                tbText.Width = listboxReferences.ActualWidth;
                tbText.TextWrapping = TextWrapping.Wrap;
                tbText.FontFamily = new FontFamily("Lucida Console");
                tbCaption.Text = _tempDossier.Reference(r).Caption;
                tbText.Text = _tempDossier.Reference(r).TextWithReturns;
                tbCaption.FontWeight = FontWeights.Bold;
                if (_tempDossier.Reference(r).Highlighted) { tbCaption.Background =tbText.Background= Brushes.Yellow;  }
                listboxReferences.Items.Add(gd);
            }
        }

        private void checkboxEnableDate_Checked(object sender, RoutedEventArgs e)
        {
            datepickerSumDate.IsEnabled = true;
            buttonToday.Visibility = Visibility.Visible;
            CheckOk();
        }

        private void checkboxEnableDate_Unchecked(object sender, RoutedEventArgs e)
        {
            datepickerSumDate.IsEnabled = false;
            buttonToday.Visibility = Visibility.Hidden;
            CheckOk();
        }

        private void buttonToday_Click(object sender, RoutedEventArgs e)
        {
            checkboxEnableDate.IsChecked = true;
            datepickerSumDate.SelectedDate = DateTime.Today;
        }

        private static void VerifyTextBoxEntrySingle(TextBox tb)
        {
            Single sng;
            if (Single.TryParse(tb.Text, out sng))
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
            textblockSymbol.Text = PortfolioCore.EuroSymbol.ToString();
            CheckOk();
        }

        private void checkboxEuros_Unchecked(object sender, RoutedEventArgs e)
        {
            textblockSymbol.Text = PortfolioCore.PoundSymbol.ToString();
            CheckOk();
        }

        private void listviewAlerts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool enab = true;
            if (listviewAlerts.SelectedItem == null) { enab = false; }
            buttonAlertDelete.IsEnabled = enab;
            buttonAlertEdit.IsEnabled = enab;
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
            DateTime? Q = datepickerSumDate.SelectedDate;
            buttonToday.IsEnabled = !Q.HasValue || Q.Value != DateTime.Today;
            CheckOk();
        }

        private void textboxAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifyTextBoxEntrySingle(textboxAmount);
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            buttonCopy.IsEnabled = false;
            string copied = _tempDossier.GroupName + ":" + _tempDossier.TitleSpecifics;
            copied += Environment.NewLine + "Provider: " + _tempDossier.ProviderOrganisation;
            copied += Environment.NewLine + "Amount: " + _tempDossier.AmountString;
            if (checkboxEnableDate.IsChecked==true) { copied += " as at " + _tempDossier.LastDate.ToLongDateString(); }
            for (int x=0;x<_tempDossier.AlertCount;x++)
            { copied += Environment.NewLine + "Alert: " + _tempDossier.Alert(x).AlertDate.ToShortDateString() + ": " + _tempDossier.Alert(x).Caption; }
            for (int x = 0; x < _tempDossier.ReferenceCount; x++)
            { copied += Environment.NewLine + "Reference: " + _tempDossier.Reference(x).Caption + ": " + _tempDossier.Reference(x).TextWithReturns; }
            Clipboard.SetText(copied);
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            _permdossier.Mirror(_tempDossier);
            DialogResult = true;
        }

        private void ButtonAlertAdd_Click(object sender, RoutedEventArgs e)
        {
            PortfolioDossier.classAlert al = new PortfolioDossier.classAlert();
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
            PortfolioDossier.classReference rf = new PortfolioDossier.classReference();
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
            int index = listviewAlerts.SelectedIndex;
            if (MessageBox.Show(_tempDossier.Alert(index).AlertDate.ToShortDateString() + " " + _tempDossier.Alert(index).Caption + "\n\nDelete this alert?", "Delete alert", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) { return; }
            _tempDossier.DeleteAlert(index);
            RefreshAlertsList();
            CheckOk();
        }

        private void buttonReferenceDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = listboxReferences.SelectedIndex;
            if (MessageBox.Show(_tempDossier.Reference(index).Caption + ": " + _tempDossier.Reference(index).TextWithReturns + "\n\nDelete this reference?", "Delete reference", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) { return; }
            _tempDossier.DeleteReference(index);
            RefreshReferencesList();
            CheckOk();
        }

        private void ButtonAlertEdit_Click(object sender, RoutedEventArgs e)
        {
            AlertEdit(listviewAlerts.SelectedIndex);
        }

        private void AlertEdit(int index)
        {
            DossierSubDetailsWindow w = new DossierSubDetailsWindow(1);
            w.SetAlert(_tempDossier.Alert(index));
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
            ReferenceEdit(listboxReferences.SelectedIndex);
        }

        private void ReferenceEdit(int index)
        {
            DossierSubDetailsWindow w = new DossierSubDetailsWindow(2);
            w.SetReference(_tempDossier.Reference(index));
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
            if (buttonAlertEdit.IsEnabled) { AlertEdit(listviewAlerts.SelectedIndex); }
        }

        private void buttonReferencePromote_Click(object sender, RoutedEventArgs e)
        {
            _tempDossier.PromoteReference(listboxReferences.SelectedIndex);
            RefreshReferencesList();
            CheckOk();
        }

        private void listboxReferences_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (buttonReferenceEdit.IsEnabled) { ReferenceEdit(listboxReferences.SelectedIndex); }
        }

        private void listboxReferences_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool enab = true;
            if (listboxReferences.SelectedItem == null) { enab = false; }
            buttonReferenceDelete.IsEnabled = enab;
            buttonReferenceEdit.IsEnabled = enab;
            buttonReferencePromote.IsEnabled = enab && (listboxReferences.SelectedIndex > 0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double scrX = SystemParameters.PrimaryScreenWidth;
            double scrY = SystemParameters.PrimaryScreenHeight;
            double winX = scrX * .98;
            double winY = scrY * .94;
            double Xm = (scrX - winX) / 2;
            double Ym = (scrY - winY) / 4;
            Width = winX;
            Height = winY;
            Left = Xm;
            Top = Ym;
        }
}