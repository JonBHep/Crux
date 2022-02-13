using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Crux;

public partial class MotsStartWindow
{
    public MotsStartWindow()
    {
        InitializeComponent();
    }
    private readonly MotList _motList = new MotList();
        private string _chosenLetter = "ALL";

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UiServices.SetBusyState();
            _motList.SaveData();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            RefreshList();
            TextboxFilter.Focus();
        }

        private void RefreshList()
        {
            string filterText = TextboxFilter.Text.Trim().ToLower();
            int countrecents = 0;
            _motList.RefreshDictionary();
            List<string> captions = _motList.Names;
            captions.Sort();

            RecentListBox.Items.Clear();
            EntireListBox.Items.Clear();
            FavouriteListBox.Items.Clear();
            List<string> recently = _motList.Top20();
            foreach (string nom in captions)
            {
                Mot m = _motList.MotForName(nom);
                var showEnt = true;
                var showFav = true;
                var showRec = true;
                DateTime recentdate = DateTime.Now.AddMonths(-2);
                if (!string.IsNullOrWhiteSpace(filterText)) { showEnt = showFav = showRec = nom.ToLower().Contains(filterText); }
                if (_chosenLetter.Length == 1)
                {
                    if (!nom.StartsWith(_chosenLetter, StringComparison.CurrentCultureIgnoreCase)) { showEnt = false; }
                }
                if (!m.Favourite) { showFav = false; }
                if (m.Accessed < recentdate) { showRec = false; }
                if (!recently.Contains(nom)) { showRec = false; }

                if (showEnt)
                {
                    EntireListBox.Items.Add(PasswordLbItem(nom, m));
                }
                if (showFav)
                {
                    FavouriteListBox.Items.Add(PasswordLbItem(nom, m));
                }
                if (showRec)
                {
                    countrecents++;
                    RecentListBox.Items.Add(PasswordLbItem(nom, m));
                }
            }
            ButtonDelete.IsEnabled = false;
            ButtonView.IsEnabled = false;
            TextblockCount.Text = $"{_motList.Count} items, {_motList.FavouritesCount} favourites, {countrecents} recent";
        }

        private ListBoxItem PasswordLbItem(string caption,Mot pword )
        {
            double datewidth = 90;
            double itemwidth = 60;
            double listWidth = PasswordTabControl.ActualWidth;
            if (listWidth < 500) { listWidth = 500; }
            
            StackPanel pnl = new StackPanel() { Orientation = Orientation.Horizontal };
            ListBoxItem item = new ListBoxItem() { Content = pnl, FontFamily = new FontFamily("Consolas") };
            TextBlock nomBloc = new TextBlock() { Text = caption, Foreground = Brushes.MidnightBlue, Width = listWidth - (itemwidth + datewidth + datewidth + datewidth + 40) };
            if (pword.Favourite) { nomBloc.Foreground = Brushes.Blue; }
            TextBlock entBloc = new TextBlock() { Text = pword.ElementCount.ToString(System.Globalization.CultureInfo.CurrentCulture), Width = itemwidth };
            TextBlock updBloc = new TextBlock() { Text = pword.Updated, ToolTip = "Updated", MinWidth = datewidth, Foreground = Brushes.Teal };

            DateTime yearago = DateTime.Today.AddYears(-1);
            Brush pwdbrush = (pword.PasswordChanged < yearago) ? Brushes.Red : Brushes.ForestGreen;
            TextBlock pwdBloc = new TextBlock() { Text = pword.PasswordChanged.ToString("dd MMM yyyy"), ToolTip = "Password changed", MinWidth = datewidth, Foreground = pwdbrush };

            string accessstring = (pword.Accessed > new DateTime(1954, 1, 3)) ? pword.Accessed.ToString("dd MMM yyyy") : string.Empty;
            TextBlock accBloc = new TextBlock() { Text = accessstring, ToolTip = "Accessed", MinWidth = datewidth, Foreground = Brushes.Firebrick };

            pnl.Children.Add(nomBloc);
            pnl.Children.Add(entBloc);
            pnl.Children.Add(updBloc);
            pnl.Children.Add(pwdBloc);
            pnl.Children.Add(accBloc);
            item.Tag = caption;
            return item;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TextboxFilter.Clear();
            RefreshList();
            TextboxFilter.Focus();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox box)
            {
                bool enab = (box.SelectedItem != null);
                ButtonDelete.IsEnabled = enab;
                ButtonView.IsEnabled = enab;
            }
        }

        private void TextboxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonClear.IsEnabled = (!string.IsNullOrWhiteSpace(TextboxFilter.Text));
            RefreshList();
        }

        private string SelectedName
        {
            get
            {
                ListBox box = RecentListBox;
                string s = string.Empty;
                switch (PasswordTabControl.SelectedIndex)
                {
                    case 1:
                        { box = RecentListBox; break; }
                    case 0:
                        { box = FavouriteListBox; break; }
                    case 2:
                        { box = EntireListBox; break; }
                }
                if (box.SelectedItem is ListBoxItem {Tag: string q})
                { s = q; }

                return s;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Mot newpwd = new Mot();
            MotEditor ed=new MotEditor(newpwd.Specification, _motList)
            {
                Owner = this
            };
            if (ed.ShowDialog() == true)
            {
                string revisedSpecification = ed.EditedSpecification;
                newpwd.Specification = revisedSpecification;
                _motList.AddItem(newpwd);
                RefreshList();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            string i = SelectedName;
            if (MessageBox.Show("Delete this item?\n\n" + i, "Confirm deletion", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                _motList.DeleteItem(i);
                RefreshList();
            } 
        }

        private void ButtonView_Click(object sender, RoutedEventArgs e)
        {
            DoView();
        }

        private void DoView()
        {
            string s = SelectedName;
            Mot q = _motList.MotForName(s);
            q.Accessed = DateTime.Now;
            MotViewWindow w = new MotViewWindow(q.Specification, _motList)
            {
                Owner = this
        };
            w.ShowDialog();
            _motList.MotForName(s).Specification = w.RevisedSpecification;
            RefreshList(); // because at least the 'accessed' date has changed
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DoView();
        }

        private void LetterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button {Tag: string l})
            {
                _chosenLetter = l;
                RefreshList();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double scrX = SystemParameters.PrimaryScreenWidth;
            double scrY = SystemParameters.PrimaryScreenHeight;
            double winX = scrX * .98;
            double winY = scrY * .94;
            double xm = (scrX - winX) / 2;
            double ym = (scrY - winY) / 4;
            Width = winX; Height = winY;
            Left = xm;
            Top = ym;
        }
}