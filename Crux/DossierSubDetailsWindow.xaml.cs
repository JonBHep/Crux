using System;
using System.Windows;
using System.Windows.Controls;

namespace Crux;

public partial class DossierSubDetailsWindow
{
    public DossierSubDetailsWindow(int paramKind)
    {
        InitializeComponent();
        _whatKind = paramKind;
        _alertEdited = new PortfolioDossier.ClassAlert();
        _alertOriginal = new PortfolioDossier.ClassAlert();
        _referenceEdited = new PortfolioDossier.ClassReference();
        _referenceOriginal = new PortfolioDossier.ClassReference();
    }

    private readonly int _whatKind; // alert = 1, reference = 2
    private PortfolioDossier.ClassAlert _alertEdited;
    private PortfolioDossier.ClassAlert _alertOriginal;
    private PortfolioDossier.ClassReference _referenceEdited;
    private PortfolioDossier.ClassReference _referenceOriginal;

    public void SetAlert(PortfolioDossier.ClassAlert original)
    {
        _alertOriginal = original;
        _alertEdited = new PortfolioDossier.ClassAlert
        {
            AlertDate = original.AlertDate,
            Caption = original.Caption
            ,
            ShowAmount = original.ShowAmount
        };
        DatepickerAlertDate.SelectedDate = _alertEdited.AlertDate;
        TextBoxAlertReferenceValue.Text = _alertEdited.Caption;
        CheckboxReferenceHighlightAlertShowAmount.IsChecked = _alertEdited.ShowAmount;
    }

    public void SetReference(PortfolioDossier.ClassReference original)
    {
        _referenceOriginal = original;
        _referenceEdited = new PortfolioDossier.ClassReference
        {
            Caption = original.Caption,
            Highlighted = original.Highlighted
            ,
            TextWithReturns = original.TextWithReturns
        };
        TextBoxReferenceCaption.Text = _referenceEdited.Caption;
        TextBoxAlertReferenceValue.Text = _referenceEdited.TextWithReturns;
        CheckboxReferenceHighlightAlertShowAmount.IsChecked = _referenceEdited.Highlighted;
    }

    private void Window_ContentRendered(object sender, EventArgs e)
    {
        switch (_whatKind)
        {
            case 1:
            {
                Title = "Alert details";
                TextBlockLabelOne.Text = "Alert date";
                TextBlockLabelTwo.Text = "Caption";
                TextBlockLabelThree.Text = "Show amount in alert";
                TextBoxReferenceCaption.Visibility = Visibility.Hidden;
                DatepickerAlertDate.Visibility = Visibility.Visible;
                break;
            }
            case 2:
            {
                Title = "Reference details";
                TextBlockLabelOne.Text = "Caption";
                TextBlockLabelTwo.Text = "Details";
                TextBlockLabelThree.Text = "Highlight this reference";
                TextBoxReferenceCaption.Visibility = Visibility.Visible;
                TextBoxAlertReferenceValue.Height = 100;
                TextBoxAlertReferenceValue.AcceptsReturn = true;
                TextBoxAlertReferenceValue.TextWrapping = TextWrapping.Wrap;
                DatepickerAlertDate.Visibility = Visibility.Hidden;
                break;
            }
        }
    }

    private void buttonCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void textBoxAlertReferenceValue_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_whatKind == 1)
        {
            _alertEdited.Caption = TextBoxAlertReferenceValue.Text;
        }
        else
        {
            _referenceEdited.TextWithReturns = TextBoxAlertReferenceValue.Text;
        }
    }

    private void TextBoxReferenceCaption_TextChanged(object sender, TextChangedEventArgs e)
    {
        _referenceEdited.Caption = TextBoxReferenceCaption.Text;
    }

    private void checkboxReferenceHighlightAlertShowAmount_Checked(object sender, RoutedEventArgs e)
    {
        if (_whatKind == 1)
        {
            _alertEdited.ShowAmount = true;
        }
        else
        {
            _referenceEdited.Highlighted = true;
        }
    }

    private void checkboxReferenceHighlightAlertShowAmount_Unchecked(object sender, RoutedEventArgs e)
    {
        if (_whatKind == 1)
        {
            _alertEdited.ShowAmount = false;
        }
        else
        {
            _referenceEdited.Highlighted = false;
        }
    }

    private void datepickerAlertDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DatepickerAlertDate.SelectedDate.HasValue)
        {
            _alertEdited.AlertDate = DatepickerAlertDate.SelectedDate.Value;
        }
    }

    private void buttonOK_Click(object sender, RoutedEventArgs e)
    {
        string problem = string.Empty;
        switch (_whatKind)
        {

            case 1: // alert
            {
                if (!DatepickerAlertDate.SelectedDate.HasValue)
                {
                    problem = "Please set a date";
                }

                if (string.IsNullOrWhiteSpace(TextBoxAlertReferenceValue.Text))
                {
                    problem = "Please enter the alert text";
                }

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
            case 2: // reference
            {
                if (string.IsNullOrWhiteSpace(TextBoxReferenceCaption.Text))
                {
                    problem = "Please enter the reference caption";
                }

                if (string.IsNullOrWhiteSpace(TextBoxAlertReferenceValue.Text))
                {
                    problem = "Please enter the reference text";
                }

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