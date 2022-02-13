using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Crux;

public partial class CardsWindow : Window
{
    private readonly Cartes _cartes;

    public CardsWindow()
    {
        InitializeComponent();
        _cartes = new Cartes();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        _cartes.SaveData();
        DialogResult = true;
    }

    private void NewButton_Click(object sender, RoutedEventArgs e)
    {
        CardEditor ed = new CardEditor(string.Empty) {Owner = this};
        bool? response = ed.ShowDialog();
        if (response.HasValue && response.Value)
        {
            CarteBancaire cb = new CarteBancaire() {Specification = ed.CardSpecification};
            _cartes.Cards.Add(cb);
            DisplayList();
        }
    }

    private void DisplayList()
    {
        EditButton.IsEnabled = false;
        DeleteButton.IsEnabled = false;
        CardsListBox.Items.Clear();
        _cartes.Cards.Sort();
        foreach (CarteBancaire cb in _cartes.Cards)
        {
            TextBlock bloc = new TextBlock()
            {
                Text = cb.Caption, FontFamily = new FontFamily("Lucida Console"), FontSize = 12
                , Foreground = Brushes.SaddleBrown
            };
            ListBoxItem item = new ListBoxItem() {Content = bloc, Tag = cb.Specification};
            CardsListBox.Items.Add(item);
        }
    }

    private void Window_ContentRendered(object sender, EventArgs e)
    {
        DisplayList();
    }

    private void CardsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        bool enab = CardsListBox.SelectedIndex >= 0;
        EditButton.IsEnabled = DeleteButton.IsEnabled = enab;
        CopyNameButton.IsEnabled = CopyNumberButton.IsEnabled = CopyCcvButton.IsEnabled = enab;
        if (enab)
        {
            int i = CardsListBox.SelectedIndex;
            CardCaptionBlock.Text = _cartes.Cards[i].CardDetailDisplay;
        }
        else
        {
            CardCaptionBlock.Text = string.Empty;
        }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        int i = CardsListBox.SelectedIndex;
        ListBoxItem l = CardsListBox.SelectedItem as ListBoxItem;
        string s = (l.Tag.ToString());
        CardEditor w = new CardEditor(s) {Owner = this};
        bool? response = w.ShowDialog();
        if (response.HasValue && response.Value)
        {
            _cartes.Cards[i].Specification = w.CardSpecification;
            DisplayList();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        int i = CardsListBox.SelectedIndex;
        string cap = _cartes.Cards[i].Caption;
        MessageBoxResult ans = MessageBox.Show($"Delete the card {cap}", Jbh.AppManager.AppName, MessageBoxButton.YesNo
            , MessageBoxImage.Question);
        if (ans != MessageBoxResult.Yes)
        {
            return;
        }

        _cartes.Cards.RemoveAt(i);
        DisplayList();
    }

    private void CopyNameButton_Click(object sender, RoutedEventArgs e)
    {
        int i = CardsListBox.SelectedIndex;
        string source = _cartes.Cards[i].NameOnCard;
        Clipboard.SetText(source);
        CopyNameButton.IsEnabled = false;
    }

    private void CopyNumberButton_Click(object sender, RoutedEventArgs e)
    {
        int i = CardsListBox.SelectedIndex;
        string source = _cartes.Cards[i].CardNumber;
        Clipboard.SetText(source);
        CopyNumberButton.IsEnabled = false;
    }

    private void CopyCcvButton_Click(object sender, RoutedEventArgs e)
    {
        int i = CardsListBox.SelectedIndex;
        string source = _cartes.Cards[i].Cvv;
        Clipboard.SetText(source);
        CopyCcvButton.IsEnabled = false;
    }
}