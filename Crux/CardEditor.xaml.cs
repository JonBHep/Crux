using System;
using System.Windows;

namespace Crux;

public partial class CardEditor : Window
{
        private readonly CarteBancaire _carte;
        
        public CardEditor(string cardspec)
        {
            InitializeComponent();
            _carte = new CarteBancaire() { Specification=cardspec};
        }

        public string CardSpecification
        {
            get
            {
                return _carte.Specification;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string errmsg = string.Empty;
            _carte.Caption = CaptionBox.Text.Trim();
            if (string.IsNullOrEmpty(_carte.Caption)) { errmsg = "Give the card a caption"; }
            _carte.CardNumber =NumberBox.Text.Trim();
            if (string.IsNullOrEmpty(_carte.CardNumber)) { errmsg = "Enter the card number"; }
            if (DebitRadio.IsChecked.Value)
            {
                _carte.CreditLimit = -1;
            }
            else
            {
                if (int.TryParse( CreditLimitBox.Text, out int limit))
                {
                    _carte.CreditLimit = limit;
                }
                else
                {
                    _carte.CreditLimit = 0;
                    errmsg = "Enter the card credit limit";
                }
            }

            _carte.Cvv = CvvBox.Text.Trim();

            if (int.TryParse(PinBox.Text, out int ib))
            {
                _carte.Epingle = ib;
            }
            else
            {
                _carte.Epingle = 0;
                errmsg = "Enter the card PIN";
            }
            if (int.TryParse(MonthFromBox.Text, out int ic))
            {
                _carte.FromMonth = ic;
            }
            else
            {
                _carte.FromMonth = 0;
                errmsg = "From month error";
            }
            if (int.TryParse(MonthToBox.Text, out int id))
            {
                _carte.ToMonth = id;
            }
            else
            {
                _carte.ToMonth = 0;
                errmsg = "To month error";
            }
            if (int.TryParse(YearFromBox.Text, out int ie))
            {
                _carte.FromYear = ie;
            }
            else
            {
                _carte.FromYear = 0;
                errmsg = "From year error";
            }
            if (int.TryParse(YearToBox.Text, out int i))
            {
                _carte.ToYear = i;
            }
            else
            {
                _carte.ToYear = 0;
                errmsg = "To year error";
            }
            _carte.NameOnCard = NameBox.Text.Trim();
            if (string.IsNullOrEmpty(_carte.NameOnCard)) { errmsg = "Enter the name on the card"; }
            
            _carte.Notes = NotesBox.Text.Trim();
            
            _carte.VerificationMessage = VMessageBox.Text.Trim();
            
            _carte.VerificationPassword = VWordBox.Text.Trim();
            if (!string.IsNullOrEmpty(errmsg))
            {
                MessageBox.Show(errmsg, Jbh.AppManager.AppName, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            CaptionBox.Text = _carte.Caption;
            NumberBox.Text = _carte.CardNumber;
            if (_carte.CreditLimit < 0)
            {
                DebitRadio.IsChecked = true;
            }
            else
            {
                CreditRadio.IsChecked = true;
                CreditLimitBox.Text = _carte.CreditLimit.ToString();
            }
            CvvBox.Text = _carte.Cvv;
            PinBox.Text = _carte.Epingle.ToString();
            MonthFromBox.Text = _carte.FromMonth.ToString("00");
            MonthToBox.Text = _carte.ToMonth.ToString("00");
            YearFromBox.Text = _carte.FromYear.ToString("0000");
            YearToBox.Text = _carte.ToYear.ToString("0000");
            NameBox.Text = _carte.NameOnCard;
            NotesBox.Text = _carte.Notes;
            VMessageBox.Text = _carte.VerificationMessage;
            VWordBox.Text = _carte.VerificationPassword;
            CaptionBox.Focus();
        }

        private void DebitRadio_Checked(object sender, RoutedEventArgs e)
        {
            CreditLimitCaption.Visibility = Visibility.Hidden;
            CreditLimitBox.Visibility = Visibility.Hidden;
        }

        private void CreditRadio_Checked(object sender, RoutedEventArgs e)
        {
            CreditLimitCaption.Visibility = Visibility.Visible;
            CreditLimitBox.Visibility = Visibility.Visible;
        }
}