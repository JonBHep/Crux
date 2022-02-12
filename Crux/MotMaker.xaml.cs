using System;
using System.Windows;
using System.Windows.Controls;

namespace Crux;

public partial class MotMaker : Window
{
    public MotMaker()
    {
        InitializeComponent();
    }
    private int lengthValue;

        public string GeneratedPassword        { get { return PasswordTextBlock.Text; } }

        private int PasswordLength
        {
            get => lengthValue;
            set
            {
                lengthValue = value;
                NumTextBox.Text = lengthValue.ToString();
            }
        }

        private void NumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(NumTextBox.Text, out lengthValue))
            {
                NumTextBox.Text = lengthValue.ToString();
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordLength++;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordLength--;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            PasswordTextBlock.Text = string.Empty;
            PasswordLength = 10;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(PasswordTextBlock.Text);
            CopyButton.IsEnabled = false;
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            string selection = string.Empty;
            if (UpperCaseCheckBox.IsChecked == true) { selection += "ACEFGHJKLMNPQRTUVWXYACEFGHJKLMNPQRTUVWXY"; } // BDIOSZ excluded for confusion with 801052
            if (LowerCaseCheckBox.IsChecked == true) { selection += "acdefghijkmnprstuvwxyacdefghijkmnprstuvwxy"; } // bloqsz excluded
            if (NumberCheckBox.IsChecked == true) { selection += "347347"; } // 012568 excluded
            if (SymbolCheckBox.IsChecked == true) { selection += "!?£$%^&*-+=_@#'~{}[]<>()"; }

            if (string.IsNullOrWhiteSpace(selection))
            {
                MessageBox.Show("No types of characters are included\n\nPlease tick at least one of the boxes", "Cannot generate password", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Random Q = new Random();
            string pwd = string.Empty;
            for (int x = 0; x < lengthValue; x++)
            {
                int c = Q.Next(selection.Length);
                pwd += selection.Substring(c, 1);
            }
            PasswordTextBlock.Text = pwd;
            CopyButton.IsEnabled = true;
            UseButton.IsEnabled = true;
        }
}