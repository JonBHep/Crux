using System;
using System.Windows;
using System.Windows.Controls;

namespace Crux;

public partial class DossierSubDetailsWindow : Window
{
    public DossierSubDetailsWindow(int paramKind)
    {
        InitializeComponent();
        whatKind = paramKind;
    }
    private readonly int whatKind; // alert = 1, reference = 2
        private PortfolioDossier.classAlert _alertEdited;
        private PortfolioDossier.classAlert _alertOriginal;
        private PortfolioDossier.classReference _referenceEdited;
        private PortfolioDossier.classReference _referenceOriginal;
        
        public void SetAlert(PortfolioDossier.classAlert original)
        {
            _alertOriginal=original;
            _alertEdited=new PortfolioDossier.classAlert();
            _alertEdited.AlertDate=original.AlertDate;
            _alertEdited.Caption=original.Caption;
            _alertEdited.ShowAmount=original.ShowAmount;
            datepickerAlertDate.SelectedDate = _alertEdited.AlertDate;
            textboxAlertReferenceValue.Text = _alertEdited.Caption;
            checkboxReferenceHighlightAlertShowAmount.IsChecked = _alertEdited.ShowAmount;
        }

        public void SetReference(PortfolioDossier.classReference original)
        {
            _referenceOriginal = original;
            _referenceEdited = new PortfolioDossier.classReference();
            _referenceEdited.Caption = original.Caption;
            _referenceEdited.Highlighted = original.Highlighted;
            _referenceEdited.TextWithReturns = original.TextWithReturns;
            textboxReferenceCaption.Text = _referenceEdited.Caption;
            textboxAlertReferenceValue.Text = _referenceEdited.TextWithReturns;
            checkboxReferenceHighlightAlertShowAmount.IsChecked = _referenceEdited.Highlighted;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            switch (whatKind)
            {
                case 1:
                    {
                        Title = "Alert details";
                        textblockLabelOne.Text = "Alert date";
                        textblockLabelTwo.Text = "Caption";
                        textblockLabelThree.Text = "Show amount in alert";
                        textboxReferenceCaption.Visibility = Visibility.Hidden;
                        datepickerAlertDate.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        Title = "Reference details";
                        textblockLabelOne.Text = "Caption";
                        textblockLabelTwo.Text = "Details";
                        textblockLabelThree.Text = "Highlight this reference";
                        textboxReferenceCaption.Visibility = Visibility.Visible;
                        textboxAlertReferenceValue.Height = 100;
                        textboxAlertReferenceValue.AcceptsReturn = true;
                        textboxAlertReferenceValue.TextWrapping = TextWrapping.Wrap;
                        datepickerAlertDate.Visibility = Visibility.Hidden;
                        break;
                    }
                default:
                    { break; }
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void textboxAlertReferenceValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (whatKind==1)
            { _alertEdited.Caption = textboxAlertReferenceValue.Text; }
            else
            { _referenceEdited.TextWithReturns = textboxAlertReferenceValue.Text; }
        }

        private void textboxReferenceCaption_TextChanged(object sender, TextChangedEventArgs e)
        {
            _referenceEdited.Caption = textboxReferenceCaption.Text;
        }

        private void checkboxReferenceHighlightAlertShowAmount_Checked(object sender, RoutedEventArgs e)
        {
            if (whatKind==1)
            { _alertEdited.ShowAmount = true; }
            else
            { _referenceEdited.Highlighted = true; }
        }

        private void checkboxReferenceHighlightAlertShowAmount_Unchecked(object sender, RoutedEventArgs e)
        {
            if (whatKind == 1)
            { _alertEdited.ShowAmount = false; }
            else
            { _referenceEdited.Highlighted = false; }
        }

        private void datepickerAlertDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datepickerAlertDate.SelectedDate.HasValue)
            { _alertEdited.AlertDate = datepickerAlertDate.SelectedDate.Value; }
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            string problem = null;
            switch(whatKind)
            {
                    
                case 1:// alert
                    {
                        if (!datepickerAlertDate.SelectedDate.HasValue) { problem = "Please set a date"; }
                        if (string.IsNullOrWhiteSpace(textboxAlertReferenceValue.Text)) { problem = "Please enter the alert text"; }
                        if (!string.IsNullOrWhiteSpace(problem))
                        {
                            MessageBox.Show(problem, "Data problem", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        _alertOriginal.AlertDate = _alertEdited.AlertDate;
                        _alertOriginal.Caption = _alertEdited.Caption;
                        _alertOriginal.ShowAmount = _alertEdited.ShowAmount;
                        break;
                    }
                case 2:// reference
                    {
                        if (string.IsNullOrWhiteSpace(textboxReferenceCaption.Text)) { problem = "Please enter the reference caption"; }
                        if (string.IsNullOrWhiteSpace(textboxAlertReferenceValue.Text)) { problem = "Please enter the reference text"; }
                        if (!string.IsNullOrWhiteSpace(problem))
                        {
                            MessageBox.Show(problem, "Data problem", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        _referenceOriginal.Caption = _referenceEdited.Caption;
                        _referenceOriginal.Highlighted = _referenceEdited.Highlighted;
                        _referenceOriginal.TextWithReturns = _referenceEdited.TextWithReturns;
                        break;
                    }
            }
            DialogResult = true;
        }
}