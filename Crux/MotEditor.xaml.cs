using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Crux;

public partial class MotEditor : Window
{
   internal MotEditor(string passwordSpec, MotList ml)
        {
            InitializeComponent();
            editedSpec = passwordSpec;
            _tempPasswordFile = new Mot { Specification = editedSpec };
            pool = ml;
        }
        private readonly Mot _tempPasswordFile;
        private bool _somethingAltered = false;
        private string editedSpec;
        private MotList pool; // for checking uniqueness of titles
        public string EditedSpecification { get { return editedSpec; } }
        
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            comboboxKey.Items.Clear();
            comboboxKey.Items.Add("Account number");
            comboboxKey.Items.Add("Email");
            comboboxKey.Items.Add("Login name");
            comboboxKey.Items.Add("Password");
            comboboxKey.Items.Add("User ID");
            comboboxKey.Items.Add("Website");
            _somethingAltered = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double scrX = System.Windows.SystemParameters.PrimaryScreenWidth;
            double scrY = System.Windows.SystemParameters.PrimaryScreenHeight;
            double winX = scrX * .8;
            double winY = scrY * .94;
            double Xm = (scrX - winX) / 2;
            double Ym = (scrY - winY) / 4;
            Width = winX; Height = winY;
            Left = Xm;
            Top = Ym;

            textboxTitle.Focus();
            textboxTitle.SelectAll();
            checkboxFavourite.IsChecked = _tempPasswordFile.Favourite;

            RefreshLists();
            SaveButton.IsEnabled = false;
            AddTitleButton.IsEnabled = false;
        }

        private void RefreshTitles()
        {
            FontFamily ff = new FontFamily("Lucida Console");
            double listwidth = TitlesListBox.ActualWidth;
            double textwidth = (listwidth > 120) ? listwidth - 120 : 200;
            TitlesListBox.Items.Clear();
            foreach (string nom in _tempPasswordFile.Aliases)
            {
                TextBlock tbk = new TextBlock() { FontFamily = ff, Text = nom, Foreground = Brushes.SteelBlue };
                TitlesListBox.Items.Add(new ListBoxItem() { Tag = nom, Content = new TextBlock() { FontFamily = ff, Text = nom, Foreground = Brushes.SteelBlue } });
            }
            RemoveTitleButton.IsEnabled = false;
        }
        private void RefreshLists()
        {
            RefreshTitles();
            FontFamily ff = new FontFamily("Lucida Console");
            double listwidth = PasswordListBox.ActualWidth;
            double textwidth = (listwidth > 120) ? listwidth - 120 : 200;
            PasswordListBox.Items.Clear();
            for (int i = 0; i < _tempPasswordFile.ElementCount; i++)
            {
                TextBlock tbk = new TextBlock() { FontFamily = ff, Text = _tempPasswordFile.Element[i].Caption, Foreground = Brushes.SteelBlue };
                if (_tempPasswordFile.Element[i].IsLink) { tbk.Inlines.Add(new Run() { Foreground = Brushes.Crimson, Text = " (weblink)" }); }
                TextBlock tbv = new TextBlock() { FontFamily = ff, Text = _tempPasswordFile.Element[i].Content, Foreground = Brushes.SaddleBrown, TextWrapping = TextWrapping.Wrap, MaxWidth = textwidth, Margin = new Thickness(12, 0, 0, 0) };
                StackPanel panel = new StackPanel() { };
                panel.Children.Add(tbk);
                panel.Children.Add(tbv);
                ListBoxItem item = new ListBoxItem() { Content = panel };
                PasswordListBox.Items.Add(item);
            }
            comboboxKey.Text = string.Empty;
            textboxValue.Clear();
            Enablement();
            if (PasswordListBox.Items.Count > 0) { comboboxKey.Focus(); } else { textboxTitle.Focus(); }
        }
        private void Enablement()
        {
            bool itemselected = PasswordListBox.SelectedItem != null;
            bool highitemselected = false;
            if (itemselected) { highitemselected = PasswordListBox.SelectedIndex > 0; }
            bool textinboxes = true;
            if (string.IsNullOrWhiteSpace(comboboxKey.Text)) { textinboxes = false; }
            if (string.IsNullOrWhiteSpace(textboxValue.Text)) { textinboxes = false; }
            buttonAdd.IsEnabled = textinboxes;
            buttonDelete.IsEnabled = itemselected;
            buttonEdit.IsEnabled = itemselected;
            buttonMoveUp.IsEnabled = highitemselected;
            SaveButton.IsEnabled = (TitlesListBox.Items.Count>0) && (PasswordListBox.Items.Count > 0) && _somethingAltered;
        }

        private void PasswordListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string addcaption = (string)buttonAdd.Content;
            if (addcaption == "Change")
            {
                // cancel edit if selected item changes
                buttonAdd.Content = "Add";
                comboboxKey.Text = string.Empty;
                textboxValue.Text = string.Empty;
            }
            Enablement();
        }

        private void ComboboxKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboReaction();
        }

        private void ComboReaction()
        {
            Enablement();
        }

        private void textboxValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            Enablement();
        }

        private void comboboxKey_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            ComboReaction();
        }

        private void textboxTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddTitleButton.IsEnabled = textboxTitle.Text.Trim().Length > 0;
        }

        private void buttonGeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            MotMaker mmkr=new MotMaker()
            {
                Owner = this
            };
            if (mmkr.ShowDialog() == true) { textboxValue.Text = mmkr.GeneratedPassword; }
        }

        private void checkboxFavourite_Checked(object sender, RoutedEventArgs e)
        {
            _somethingAltered = true;
            Enablement();
        }

        private void checkboxFavourite_Unchecked(object sender, RoutedEventArgs e)
        {
            _somethingAltered = true;
            Enablement();
        }

        private void ButtonHotmail_Click(object sender, RoutedEventArgs e)
        {
            comboboxKey.Text = "Email";
            ComboReaction();
            textboxValue.Text = "jonathanhepworth@hotmail.co.uk";
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string keytext = comboboxKey.Text.Trim();
            string valtext = textboxValue.Text.Trim();
            bool ok = true;
            if (!MotList.IsPermittedString(keytext)) { ok = false; }
            if (!MotList.IsPermittedString(valtext)) { ok = false; }
            if (!ok) { MessageBox.Show("Invalid characters in input strings", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning); return; }

            if ((string)buttonAdd.Content == "Add")
            {
                _tempPasswordFile.AddElement(keytext, valtext, LinkCheckBox.IsChecked.Value);
            }
            else
            {
                if (PasswordListBox.SelectedItem != null)
                {
                    int i = PasswordListBox.SelectedIndex;
                    _tempPasswordFile.AmendElement(i, keytext, valtext, LinkCheckBox.IsChecked.Value);
                    buttonAdd.Content = "Add";
                }
            }
            _somethingAltered = true;
            LinkCheckBox.IsChecked = false;
            RefreshLists();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordListBox.SelectedItem is null) { return; }
            int i = PasswordListBox.SelectedIndex;
            if (MessageBox.Show(_tempPasswordFile.Element[i].Caption + "\n\n" + _tempPasswordFile.Element[i].Content + "\n\nDelete this?", "Delete item", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel) { return; }
            _tempPasswordFile.DeleteElement(i);
            _somethingAltered = true;
            RefreshLists();
        }

        private void ButtonMoveUp_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordListBox.SelectedItem == null) { return; }
            int i = PasswordListBox.SelectedIndex;
            _tempPasswordFile.MoveUp(i);
            _somethingAltered = true;
            RefreshLists();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answ = MessageBox.Show("Has the password been altered?", "Password file", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (answ == MessageBoxResult.Cancel) { return; }
            if (answ == MessageBoxResult.Yes) { _tempPasswordFile.PasswordChanged = DateTime.Today; }
            _tempPasswordFile.Updated = DateTime.Today.ToString("dd MMM yyyy");
            _tempPasswordFile.Favourite = checkboxFavourite.IsChecked.Value;
            _tempPasswordFile.Accessed = DateTime.Now;
            editedSpec = _tempPasswordFile.Specification;
            DialogResult = true;
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordListBox.SelectedItem is null) { return; }
            int i = PasswordListBox.SelectedIndex;
            comboboxKey.Text = _tempPasswordFile.Element[i].Caption;
            textboxValue.Text = _tempPasswordFile.Element[i].Content;
            LinkCheckBox.IsChecked = _tempPasswordFile.Element[i].IsLink;
            buttonAdd.Content = "Change";
        }

        private void AddTitleButton_Click(object sender, RoutedEventArgs e)
        {
            string nu = textboxTitle.Text.Trim();
            if (pool.MotDictionary.ContainsKey(nu))
            {
                MessageBox.Show("Cannot add this caption as it is already in use", Jbh.AppManager.AppName, MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            _somethingAltered = true;
            _tempPasswordFile.Aliases.Add(textboxTitle.Text.Trim());
            _tempPasswordFile.Aliases.Remove("New password file");  // remove placeholder title
            _tempPasswordFile.Aliases.Sort();
            textboxTitle.Clear();
            RefreshLists();
        }

        private void RemoveTitleButton_Click(object sender, RoutedEventArgs e)
        {
            if (TitlesListBox.SelectedItem is ListBoxItem item)
            {
                if (item.Tag is string nom)
                {
                    if (TitlesListBox.Items.Count < 2)
                    {
                        MessageBox.Show("Do not remove the only caption", Jbh.AppManager.AppName, MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                    else
                    {
                        _tempPasswordFile.Aliases.Remove(nom);
                        _somethingAltered = true;
                        RefreshLists();
                    }
                }
            }
        }

        private void TitlesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveTitleButton.IsEnabled = TitlesListBox.SelectedIndex >= 0;
        }
        
}