using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Crux;

public partial class PortfolioListWindow
{
    public PortfolioListWindow(PortfolioDossier.DossierTypeConstants paramDossierType, PortfolioCore paramPortfolio)
    {
        InitializeComponent();
        _dossierType = paramDossierType;
        _portfolio = paramPortfolio;
    }

    private readonly PortfolioDossier.DossierTypeConstants _dossierType;
    private readonly PortfolioCore _portfolio;
    private bool _firstDisplay = true;
    readonly ContextMenu _cmnu = new ContextMenu();
    readonly MenuItem _cmnuAdd = new MenuItem();
    readonly MenuItem _cmnuEdit = new MenuItem();
    readonly MenuItem _cmnuDelete = new MenuItem();
    readonly MenuItem _cmnuObsolete = new MenuItem();
    readonly MenuItem _cmnuBalances = new MenuItem();
    readonly MenuItem _cmnuClose = new MenuItem();
    readonly MenuItem _cmnuCancel = new MenuItem();
    string _thing = "thing";
    string _things = "things";

    private readonly System.Collections.ObjectModel.ObservableCollection<ListedItem> _displayList
        = new System.Collections.ObjectModel.ObservableCollection<ListedItem>();

    private class ListedItem
    {
        public string? GroupName { get; set; }
        public string? DossierName { get; set; }
        public string? Provider { get; set; }
        public string? Amount { get; set; }
        public string? AmountDate { get; set; }
        public string? Alerts { get; set; }
        public string? Online { get; set; }
        public string? InDocument { get; set; }

        public bool Obsolete { get; set; }

        // for triggers
        public bool BankBalanceOld { get; set; }
        public bool BankBalanceToday { get; set; }
        public bool AlertOverdue { get; set; }
        public int Index { get; init; }
    }

    private void Window_ContentRendered(object sender, EventArgs e)
    {
        SetUpContextMenu();
        SetListLiewColumnHeadings();
        BuildList();
    }

    private void SetUpContextMenu()
    {
        if (_dossierType == PortfolioDossier.DossierTypeConstants.AccountDossier)
        {
            _thing = "account";
            _things = "accounts";
        }

        else
        {
            _thing = "service";
            _things = "services";
            _cmnuBalances.Visibility = Visibility.Collapsed;
        }

        Title = "My " + _things;
        _cmnuAdd.Header = "Add a new " + _thing;
        _cmnuBalances.Header = "Display dated balances";
        _cmnuBalances.FontWeight = FontWeights.SemiBold;
        _cmnuBalances.Foreground = new SolidColorBrush(Colors.Blue);
        _cmnuEdit.Header = "Edit the selected " + _thing;
        _cmnuEdit.IsEnabled = false;
        _cmnuDelete.Header = "Delete the selected " + _thing;
        _cmnuDelete.IsEnabled = false;
        _cmnuObsolete.Header = "Show obsolete " + _things;
        _cmnuClose.Header = "Close this window";
        _cmnuCancel.Header = "Cancel";
        _cmnuObsolete.IsCheckable = true;
        _cmnuObsolete.IsChecked = false;

        _cmnuAdd.Click += MenuItemAdd_Click;
        _cmnuBalances.Click += MenuItemBalances_Click;
        _cmnuClose.Click += MenuItemClose_Click;
        _cmnuDelete.Click += MenuItemDelete_Click;
        _cmnuEdit.Click += MenuItemEdit_Click;
        _cmnuObsolete.Click += MenuItemObsolete_Click;

        _cmnu.Items.Add(_cmnuAdd);
        _cmnu.Items.Add(_cmnuEdit);
        _cmnu.Items.Add(_cmnuDelete);
        _cmnu.Items.Add(new Separator());
        _cmnu.Items.Add(_cmnuObsolete);
        _cmnu.Items.Add(_cmnuBalances);
        _cmnu.Items.Add(new Separator());
        _cmnu.Items.Add(_cmnuClose);
        _cmnu.Items.Add(_cmnuCancel);
    }

    private void SetListLiewColumnHeadings()
    {
        switch (_dossierType)
        {
            case PortfolioDossier.DossierTypeConstants.AccountDossier:
            {
                ColumnDossier.Header = "Account";
                ColumnProvider.Header = "Provider";
                ColumnAmount.Header = "Balance";
                ColumnAmountDate.Header = "Balance date";
                ColumnAmountDate.Width = 90;
                ColumnAlerts.Header = "Alerts";
                ColumnOnline.Header = "Online operation";
                ColumnDocument.Header = "In document";
                ColumnObsolete.Header = "Obsolete";
                break;
            }
            case PortfolioDossier.DossierTypeConstants.ServiceDossier:
            {
                ColumnDossier.Header = "Service";
                ColumnProvider.Header = "Provider";
                ColumnAmount.Header = "Last payment";
                ColumnAmountDate.Header = "Last payment date";
                ColumnAmountDate.Width = 90;
                ColumnAlerts.Header = "Alerts";
                ColumnOnline.Header = "Direct debit";
                ColumnDocument.Header = "In document";
                ColumnObsolete.Header = "Obsolete";
                break;
            }
            case PortfolioDossier.DossierTypeConstants.NullDossier:
            default:
            {
                break;
            }
        }
    }

    private void BuildList()
    {
        _displayList.Clear();
        switch (_dossierType)
        {
            case PortfolioDossier.DossierTypeConstants.AccountDossier:
            {
                for (int x = 0; x < _portfolio.AccountCount; x++)
                {
                    if ((_cmnuObsolete.IsChecked) || (!_portfolio.Account(x).Obsolete))
                    {
                        ListedItem itm = new ListedItem
                        {
                            Index = x, Alerts = _portfolio.Account(x).Alerts.Count.ToString()
                            , Amount = _portfolio.Account(x).AmountString
                            , AmountDate = _portfolio.Account(x).LastDate.ToShortDateString()
                            , DossierName = _portfolio.Account(x).TitleSpecifics
                            , GroupName = _portfolio.Account(x).GroupName
                            , InDocument = _portfolio.Account(x).IncludeInDocument.ToString()
                            , Obsolete = _portfolio.Account(x).Obsolete
                            , Online = _portfolio.Account(x).Option.ToString()
                            , Provider = _portfolio.Account(x).ProviderOrganisation,
                            // triggering properties
                            BankBalanceOld = ((DateTime.Today - _portfolio.Account(x).LastDate).TotalDays > 7)
                            , BankBalanceToday = (DateTime.Today == _portfolio.Account(x).LastDate.Date)
                            , AlertOverdue = _portfolio.Account(x).AnyOverdueAlerts
                        };
                        _displayList.Add(itm);
                    }
                }

                TextblockDossierTotalCount.Text = _portfolio.AccountCount.ToString() + " accounts";
                TextblockDossierLiveCount.Text
                    = (_portfolio.AccountCount - _portfolio.AccountCountObsolete).ToString() + " live";
                TextblockDossierObsoleteCount.Text = _portfolio.AccountCountObsolete.ToString() + " obsolete";
                TextblockTotalPounds.Text = _portfolio.TotalBalanceOfLiveAccountsPound();
                TextblockTotalEuros.Text = _portfolio.TotalBalanceOfLiveAccountsEuro();
                break;
            }
            case PortfolioDossier.DossierTypeConstants.ServiceDossier:
            {
                for (int x = 0; x < _portfolio.ServiceCount; x++)
                {
                    if ((_cmnuObsolete.IsChecked) || (!_portfolio.Service(x).Obsolete))
                    {
                        ListedItem itm = new ListedItem
                        {
                            Index = x, Alerts = _portfolio.Service(x).Alerts.Count.ToString()
                            , Amount = _portfolio.Service(x).AmountString
                            , AmountDate = _portfolio.Service(x).LastDate.ToShortDateString()
                        };
                        if (_portfolio.Service(x).LastDate < new DateTime(2000, 1, 1))
                        {
                            itm.AmountDate = string.Empty;
                        }

                        itm.DossierName = _portfolio.Service(x).TitleSpecifics;
                        itm.GroupName = _portfolio.Service(x).GroupName;
                        itm.InDocument = _portfolio.Service(x).IncludeInDocument.ToString();
                        itm.Obsolete = _portfolio.Service(x).Obsolete;
                        itm.Online = _portfolio.Service(x).Option.ToString();
                        itm.Provider = _portfolio.Service(x).ProviderOrganisation;
                        // triggering properties
                        itm.BankBalanceOld = false;
                        itm.AlertOverdue = _portfolio.Service(x).AnyOverdueAlerts;
                        _displayList.Add(itm);
                    }
                }

                TextblockDossierTotalCount.Text = _portfolio.ServiceCount.ToString() + " services";
                TextblockDossierLiveCount.Text
                    = (_portfolio.ServiceCount - _portfolio.ServiceCountObsolete).ToString() + " live";
                TextblockDossierObsoleteCount.Text = _portfolio.ServiceCountObsolete.ToString() + " obsolete";
                TextblockTotalPounds.Text = string.Empty;
                TextblockTotalEuros.Text = string.Empty;
                break;
            }
            case PortfolioDossier.DossierTypeConstants.NullDossier:
            default:
            {
                break;
            }
        }

        if (_firstDisplay)
        {
            ListviewDossiers.ItemsSource = _displayList;
            CollectionView view = (CollectionView) CollectionViewSource.GetDefaultView(ListviewDossiers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("GroupName");
            if (view.GroupDescriptions != null) view.GroupDescriptions.Add(groupDescription);

            ListviewDossiers.ContextMenu = _cmnu;
            _firstDisplay = false;
        }

    }

    private void MenuItemAdd_Click(object sender, RoutedEventArgs e)
    {
        PortfolioDossier pd = new PortfolioDossier(_dossierType);
        DossierDetailsWindow w = new DossierDetailsWindow(pd, _portfolio.GroupsList(_dossierType))
        {
            Owner = this
        };
        if (w.ShowDialog() == true)
        {
            switch (_dossierType)
            {
                case PortfolioDossier.DossierTypeConstants.AccountDossier:
                {
                    PortfolioDossier nw = _portfolio.NewAccount;
                    nw.Mirror(pd);
                    BuildList();
                    break;
                }
                case PortfolioDossier.DossierTypeConstants.ServiceDossier:
                {
                    PortfolioDossier nw = _portfolio.NewService;
                    nw.Mirror(pd);
                    BuildList();
                    break;
                }
            }
        }
    }

    private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
    {
        switch (_dossierType)
        {
            case PortfolioDossier.DossierTypeConstants.AccountDossier:
            {
                ListedItem it = (ListedItem) ListviewDossiers.SelectedItem;
                int p = it.Index;
                DossierDetailsWindow w
                    = new DossierDetailsWindow(_portfolio.Account(p), _portfolio.GroupsList(_dossierType))
                    {
                        Owner = this
                    };
                if (w.ShowDialog() == true)
                {
                    BuildList();
                }

                break;
            }
            case PortfolioDossier.DossierTypeConstants.ServiceDossier:
            {
                ListedItem it = (ListedItem) ListviewDossiers.SelectedItem;
                int p = it.Index;
                DossierDetailsWindow w
                    = new DossierDetailsWindow(_portfolio.Service(p), _portfolio.GroupsList(_dossierType))
                    {
                        Owner = this
                    };
                if (w.ShowDialog() == true)
                {
                    BuildList();
                }

                break;
            }
        }
    }

    private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
    {
        ListedItem it = (ListedItem) ListviewDossiers.SelectedItem;
        int p = it.Index;
        MessageBoxResult mbr;
        switch (_dossierType)
        {
            case PortfolioDossier.DossierTypeConstants.AccountDossier:
            {
                mbr = _portfolio.Account(p).Obsolete
                    ? MessageBox.Show("Delete the selected obsolete account?\n\n" + _portfolio.Account(p).Title
                        , "Delete account", MessageBoxButton.YesNo, MessageBoxImage.Question)
                    : MessageBox.Show(
                        "Delete the selected account?\n\n" + _portfolio.Account(p).Title +
                        "\n\nRemember you have the option to mark the account as obsolete", "Delete account"
                        , MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mbr == MessageBoxResult.Yes)
                {
                    _portfolio.DeleteAccount(p);
                    BuildList();
                }

                break;
            }
            case PortfolioDossier.DossierTypeConstants.ServiceDossier:
            {
                mbr = _portfolio.Service(p).Obsolete
                    ? MessageBox.Show("Delete the selected obsolete service?\n\n" + _portfolio.Service(p).Title
                        , "Delete service", MessageBoxButton.YesNo, MessageBoxImage.Question)
                    : MessageBox.Show(
                        "Delete the selected service?\n\n" + _portfolio.Service(p).Title +
                        "\n\nRemember you have the option to mark the service as obsolete", "Delete service"
                        , MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mbr == MessageBoxResult.Yes)
                {
                    _portfolio.DeleteService(p);
                    BuildList();
                }

                break;
            }
        }

    }

    private void MenuItemBalances_Click(object sender, RoutedEventArgs e)
    {
        ShowHistory();
    }

    private void ShowHistory()
    {
        _portfolio.AddTodaysBalances();
        DatedBalancesWindow w = new DatedBalancesWindow(_portfolio.DatedBalances)
        {
            Owner = this
        };
        w.ShowDialog();
    }

    private void MenuItemObsolete_Click(object sender, RoutedEventArgs e)
    {
        BuildList();
    }

    private void MenuItemClose_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void ListviewDossiers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        bool sel = ListviewDossiers.SelectedItem != null;
        _cmnuDelete.IsEnabled = sel;
        _cmnuEdit.IsEnabled = sel;
    }

    private void ListviewDossiers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (_cmnuEdit.IsEnabled)
        {
            MenuItemEdit_Click(sender, e);
        }
    }

    private void ButtonClose_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void BalancesButton_Click(object sender, RoutedEventArgs e)
    {
        ShowHistory();
    }

    private void UpdateBalancesButton_Click(object sender, RoutedEventArgs e)
    {
        MiseAJourWindow majw = new(_portfolio) {Owner = this};
        bool? rv = majw.ShowDialog();
        if (rv.HasValue && rv.Value)
        {
            foreach (var key in majw.MiseAJour.Keys)
            {
                _portfolio.Account(key).Amount = majw.MiseAJour[key];
                _portfolio.Account(key).LastDate = DateTime.Today;
                BuildList();
            }
        }
    }

    private void PortfolioListWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_dossierType != PortfolioDossier.DossierTypeConstants.AccountDossier)
        {
            BalancesButton.Visibility = UpdateBalancesButton.Visibility = Visibility.Hidden;
        }
    }
}