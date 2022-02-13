using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux;

public class PortfolioCore
{
    public const char EuroSymbol =(char)8364;
        public const char PoundSymbol ='£';

        private readonly List<PortfolioDossier> _accountList;
        private readonly List<PortfolioDossier> _serviceList;
        private readonly List<ClassDatedBalances> _datedBalances;

        public DateTime LastDocumentExport { get; set; }

        public PortfolioCore() // constructor
        {
            _accountList = new List<PortfolioDossier>();
            _serviceList = new List<PortfolioDossier>();
            _datedBalances = new List<ClassDatedBalances>();
        }

        public class ClassDueDate : IComparable
        {
            public DateTime Due { get; set; }
            public string PortfolioDescription { get; set; }
            public string AlertDescription { get; set; }
            public string Category { get; set; }
            public string Amount { get; set; }
            public ClassDueDate(DateTime paramDue, string paramPortfolio, string paramAlert, string paramCategory, string paramAmount) // constructor
            {
                Due = paramDue;
                PortfolioDescription = paramPortfolio;
                AlertDescription = paramAlert;
                Category = paramCategory;
                Amount = paramAmount;
            }

            public int CompareTo (object obj)
            {
                if (obj == null) return 1;
                ClassDueDate other = (ClassDueDate) obj;
                return this.Due.CompareTo(other.Due);
            }

            public int DaysAway
            {
                get
                {
                    TimeSpan ts = DateTime.Today - Due;
                    return (int) ts.TotalDays;
                }
            }
        }

        public class ClassDatedBalances
        {
            private readonly DateTime _dateZero = new DateTime(2015, 1, 1);
            public DateTime BalanceDate { get { return _dateZero.AddDays(BalanceDateIndex); } }
            public int BalanceDateIndex { get; set; }
            public int BalancePounds{get;set;}
            public int BalanceEuros{get;set;}
            public bool UnwantedFlag { get; set; }
            public string Specification
            {
                get
                {
                    return BalanceDateIndex.ToString() + "#" + BalancePounds.ToString() + "#" + BalanceEuros.ToString();
                }
            }
            public ClassDatedBalances(string paramSpecification)
            {
                string[] u = paramSpecification.Split('#');
                BalanceDateIndex = int.Parse(u[0]);
                BalancePounds = int.Parse(u[1]);
                BalanceEuros = int.Parse(u[2]);
            }
            public ClassDatedBalances(DateTime paramDate, int paramPounds, int paramEuros)
            {
                BalanceDateIndex = (int)(paramDate - _dateZero).TotalDays;
                BalanceEuros = paramEuros;
                BalancePounds = paramPounds;
            }
        }

        public void PurgeDatedBalances()
        {
            if (_datedBalances.Count > 1)
            {
                for (int i = 1; i < _datedBalances.Count; i++)
                {
                    _datedBalances[i].UnwantedFlag = false;
                    if (_datedBalances[i].BalanceEuros == _datedBalances[i - 1].BalanceEuros)
                    {
                        if (_datedBalances[i].BalancePounds == _datedBalances[i - 1].BalancePounds)
                        {
                            // balances have not been updated since the previous time, so do not remember this occasion
                            _datedBalances[i].UnwantedFlag = true;
                        }
                    }
                }
            }
        }

        public int AccountCount { get { return _accountList.Count; } }

        public int ServiceCount { get { return _serviceList.Count; } }

        public bool DocumentExportOverdue
        {
            get
            {
                TimeSpan ts = DateTime.Today - LastDocumentExport;
                return (ts.TotalDays > 28);
            }
        }

        public int AccountCountObsolete
        {
            get
            {
                int ct = 0;
                foreach (PortfolioDossier pd in _accountList) { if (pd.Obsolete) { ct++; } }
                return ct;
            }
        }

        public int ServiceCountObsolete
        {
            get
            {
                int ct = 0;
                foreach (PortfolioDossier pd in _serviceList) { if (pd.Obsolete) { ct++; } }
                return ct;
            }
        }

        public int AccountCountOverdue(out List<string> items)
        {
            items = new List<string>();
            foreach (PortfolioDossier pd in _accountList)
            {
                if (!pd.Obsolete)
                {
                    if (pd.AnyOverdueAlerts) { items.Add($"{pd.GroupName} - {pd.Title} - {pd.TitleSpecifics}"); }
                }
            }
            return items.Count();
        }

        public int ServiceCountOverdue(out List<string> items)
        {
            items = new List<string>();
            foreach (PortfolioDossier pd in _serviceList)
            {
                if (!pd.Obsolete)
                {
                    if (pd.AnyOverdueAlerts) { items.Add($"{pd.GroupName} - {pd.Title} - {pd.TitleSpecifics}"); }
                }
            }
            return items.Count();
        }

        public PortfolioDossier Account(int index) { return _accountList[index]; }

        public PortfolioDossier Service(int index) { return _serviceList[index]; }

        public List<ClassDatedBalances> DatedBalances { get { return _datedBalances; } }

        public void ClearAllData()
        {
            _accountList.Clear();
            _serviceList.Clear();
        }

        public PortfolioDossier NewAccount
        {
            get
            {
                PortfolioDossier nu = new PortfolioDossier(PortfolioDossier.DossierTypeConstants.AccountDossier);
                _accountList.Add(nu);
                return nu;
            }
        }

        public PortfolioDossier NewService
        {
            get
            {
                PortfolioDossier nu = new PortfolioDossier(PortfolioDossier.DossierTypeConstants.ServiceDossier);
                _serviceList.Add(nu);
                return nu;
            }
        }

        public void DeleteAccount(int index)
        {
            _accountList.RemoveAt(index);
        }

        public void DeleteService(int index)
        {
            _serviceList.RemoveAt(index);
        }

        private Single LiveEuroBalance()
        {
            Single tot = 0;
            foreach (PortfolioDossier pd in _accountList)
            {
                if (pd.CurrencyEuro)
                {
                    if (!pd.Obsolete)
                    { tot += pd.Amount; }
                }
            }
            return tot;
        }

        private Single LivePoundBalance()
        {
            Single tot = 0;
            foreach (PortfolioDossier pd in _accountList)
            {
                if (!pd.CurrencyEuro)
                {
                    if (!pd.Obsolete)
                    { tot += pd.Amount; }
                }
            }
            return tot;
        }

        public string TotalBalanceOfLiveAccountsEuro()
        {
            Single tot = LiveEuroBalance();
            return EuroSymbol + tot.ToString("n");
        }

        public string TotalBalanceOfLiveAccountsPound()
        {
            Single tot = LivePoundBalance();
            return PoundSymbol + tot.ToString("n");
        }
        
        public void AddTodaysBalances()
        {
            _datedBalances.Add(new ClassDatedBalances(DateTime.Today, (int)Math.Round(LivePoundBalance(), MidpointRounding.ToEven), (int)Math.Round(LiveEuroBalance(), MidpointRounding.ToEven)));
            // TODO clean list by (a) deleting all but the last entry for a given date (b) deleting any balance whose amount is unchanged from the previous. We may already have implemented this
            List<ClassDatedBalances> reverseList = new List<ClassDatedBalances>();
            int previousDateIndex = 0;
            for (int z = _datedBalances.Count-1; z>=0;z--)
            {
                if (_datedBalances[z].BalanceDateIndex != previousDateIndex) { reverseList.Add(_datedBalances[z]); previousDateIndex = _datedBalances[z].BalanceDateIndex; }
            }
            _datedBalances.Clear();
            for (int z = reverseList.Count-1; z >= 0; z--){ _datedBalances.Add(reverseList[z]); }
        }

        public void SortAccounts() { _accountList.Sort(); }

        public void SortServices() { _serviceList.Sort(); }

        public List<ClassDueDate> DueDates()
        {
            string defaultAmount;
            string displayedAmount;
            List<ClassDueDate> rv = new List<ClassDueDate>();
            foreach(PortfolioDossier pd in _accountList)
            {
                if (!pd.Obsolete)
                {
                    if (pd.CurrencyEuro) { defaultAmount = EuroSymbol.ToString(); } else { defaultAmount = PoundSymbol.ToString(); }
                    defaultAmount += pd.Amount.ToString("#,0.00");
                    for (int a=0;a<pd.AlertCount;a++)
                    { 
                        if (pd.Alert(a).ShowAmount) { displayedAmount = defaultAmount; } else { displayedAmount = string.Empty; }
                        rv.Add(new ClassDueDate(pd.Alert(a).AlertDate,pd.Title,pd.Alert(a).Caption,"Account",displayedAmount));
                    }
                }
            }
            foreach(PortfolioDossier pd in _serviceList)
            {
                if (!pd.Obsolete)
                {
                    if (pd.CurrencyEuro) { defaultAmount = EuroSymbol.ToString(); } else { defaultAmount = PoundSymbol.ToString(); }
                    defaultAmount += pd.Amount.ToString("#,0.00");
                    for (int a=0;a<pd.AlertCount;a++)
                    { 
                        if (pd.Alert(a).ShowAmount) { displayedAmount = defaultAmount; } else { displayedAmount = string.Empty; }
                        rv.Add(new ClassDueDate(pd.Alert(a).AlertDate,pd.Title,pd.Alert(a).Caption,"Service",displayedAmount));
                    }
                }
            }
            // add an item representing today
            rv.Add(new ClassDueDate(DateTime.Today, "", "", "TODAY", ""));
            rv.Sort();
            return rv;
        }

        public List<string> GroupsList(PortfolioDossier.DossierTypeConstants typ)
        {
            List<string> lst = new List<string>();
            List<PortfolioDossier> pfos;
            if (typ == PortfolioDossier.DossierTypeConstants.AccountDossier) { pfos = _accountList; } else { pfos = _serviceList; }
            foreach (PortfolioDossier d in pfos)
            {
                if (!lst.Contains(d.GroupName)) { lst.Add(d.GroupName); }
            }
            lst.Sort();
            return lst;
        }

        public void ExportAlertsList()
        {
            List<ClassDueDate> alertDates = DueDates();
            string path =Jbh.AppManager.DataPath;
            path = System.IO.Path.GetDirectoryName(path);
            path = System.IO.Path.Combine(path, "Souvenir");
            if (!System.IO.Directory.Exists(path))
            {
                System.Windows.MessageBox.Show("Attempting to export alerts to Souvenir program\n\nCannot locate the directory\n\n" + path, "Directory not found", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }
            path = System.IO.Path.Combine(path, "CrucialAlerts.txt");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
            {
                foreach (ClassDueDate dd in alertDates)
                {
                    if (!dd.Category.Equals("TODAY"))
                    {
                        sw.WriteLine(dd.Due.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        sw.WriteLine($"{dd.PortfolioDescription} - {dd.AlertDescription}");
                    }
                }
            }
        }
}