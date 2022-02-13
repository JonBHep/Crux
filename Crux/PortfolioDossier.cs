using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux;

public class PortfolioDossier : IComparable
{
    public enum DossierTypeConstants { NullDossier = 0, AccountDossier = 1, ServiceDossier = 2 };
        
        public class classReference
        {
            // TODO when finished, rename 
            private string _RefText;
            public string Caption { get; set; }
            //public string Value { get; set; }
            public bool Highlighted { get; set; }
            
            public classReference()
            {
                Caption = string.Empty;
                TextWithReturns = string.Empty;
                Highlighted = false;
            }

            public string TextWithReturns
            {
                get
                {
                    return _RefText.Replace("_", Environment.NewLine);
                }
                set
                {
                    _RefText = value.Replace(Environment.NewLine, "_");
                }
            }

            public string TextWithoutReturns
            {
                get
                {
                    return _RefText;
                }
                set
                {
                    _RefText = value;
                }
            }

        }

        public class classAlert
        {
            // TODO when finished, rename 
            public string Caption { get; set; }
            public DateTime AlertDate { get; set; }
            public bool ShowAmount { get; set; }
            public classAlert()
            {
                Caption = string.Empty;
                AlertDate=DateTime.Today;
                ShowAmount=true;
            }
        }

        public DossierTypeConstants DossierType { get; set; }
        public bool Obsolete { get; set; }
        public bool Option { get; set; }
        public bool CurrencyEuro { get; set; }
        public string Title { get; set; }
        public string ProviderOrganisation { get; set; }
        public Single Amount { get; set; }
        public DateTime LastDate { get; set; }

        public bool IncludeInDocument
        {
            get
            {
                if (DossierType == DossierTypeConstants.AccountDossier) { if (Obsolete) { return false; } else { return true; } } // Accounts, unless obsolete, are always included in document
                if (Obsolete) { return false; } else { return _includeInDocument; } // Services, unless obsolete, will be included if their flag is set 
            }
            set
            {
                _includeInDocument = value;
            }
        }

        private bool _includeInDocument;

        private readonly List<classAlert> _alertList;
        
        private readonly List<classReference> _referenceList;
        
        public int AlertCount { get { return _alertList.Count(); } }
        
        public int ReferenceCount { get { return _referenceList.Count(); } }
        
        public classAlert Alert(int index)
        {
            if ((index<0)||(index>=_alertList.Count())) {return null;}
            return _alertList[index];
        }

        public classReference Reference(int index)
        {
            if ((index < 0) || (index >= _referenceList.Count())) { return null; }
            return _referenceList[index];
        }

        public void PromoteReference(int index)
        {
            if (index<1) {return;}
            if (index>=_referenceList.Count) {return;}
            classReference temp = _referenceList[index];
            _referenceList.RemoveAt(index);
            _referenceList.Insert(index - 1, temp);
        }

        public classAlert NewAlert // used in loading data
        {
            get
            {
                classAlert nouveau = new classAlert();
                _alertList.Add(nouveau);
                return nouveau;
            }
        }

        public void AddAlert (classAlert nova) // user-added
        {
            _alertList.Add(nova);
        }

        public classReference NewReference  // used in loading data
        {
            get
            {
                classReference nouveau = new classReference();
                _referenceList.Add(nouveau);
                return nouveau;
            }
        }
        
        public void AddReference(classReference nova) // user-added
        {
            _referenceList.Add(nova);
        }

        public void DeleteAlert(int index)
        {
            if ((index<0)||(index>=_alertList.Count())) {return;}
            _alertList.RemoveAt(index);
        }

        public void DeleteReference(int index)
        {
            if ((index < 0) || (index >= _referenceList.Count())) { return; }
            _referenceList.RemoveAt(index);
        }

        public bool AnyOverdueAlerts
        {
            get
            {
                bool allOk = true;
                foreach (classAlert a in _alertList) { if (a.AlertDate < DateTime.Today) { allOk = false; } }
                return !allOk;
            }
        }

        public PortfolioDossier(DossierTypeConstants type) // constructor
        {
            DossierType = type;
            Obsolete = false;
            Amount = 0;
            LastDate = new DateTime(1954, 1, 1);
            Title = string.Empty;
            CurrencyEuro = false;
            Option = false;
            IncludeInDocument = false;
            ProviderOrganisation = string.Empty;
            _alertList = new List<classAlert>();
            _referenceList = new List<classReference>();
        }

        public string GroupName
        {
            get
            {
                string grup;
                int p = Title.IndexOf(':');
                if (p>0)
                { grup = Title.Substring(0, p); }
                else
                { grup = "Default"; }
                return grup;
            }
        }

        public string TitleSpecifics
        {
            get
            {
                string species;
                int p = Title.IndexOf(':');
                if (p>0)
                { species = Title.Substring(p + 1); }
                else { species = Title; }
                return species.Trim();
            }
        }

        public string AmountString
        {
            get
            {
                if (Amount == 0) { return string.Empty; }
                if (CurrencyEuro) { return PortfolioCore.EuroSymbol + Amount.ToString("n"); } else { return PortfolioCore.PoundSymbol + Amount.ToString("n"); }
            }
        }

        public int CompareTo(object? obj)
        {
            if (obj is PortfolioDossier dossier)
            {
                return String.Compare(Title, dossier.Title, StringComparison.Ordinal);    
            }

            return 0;
        }

        public void Mirror(PortfolioDossier other)
        {
            this.Amount = other.Amount;
            this.CurrencyEuro = other.CurrencyEuro;
            this.DossierType = other.DossierType;
            this.IncludeInDocument = other.IncludeInDocument;
            this.LastDate = other.LastDate;
            this.Obsolete = other.Obsolete;
            this.Option = other.Option;
            this.ProviderOrganisation = other.ProviderOrganisation;
            this.Title = other.Title;
         
            this._alertList.Clear();
            foreach(classAlert al in other._alertList)
            {
                classAlert nova = new classAlert();
                nova.AlertDate=al.AlertDate;
                nova.Caption=al.Caption;
                nova.ShowAmount=al.ShowAmount;
                this._alertList.Add(nova);
            }

            this._referenceList.Clear();
            foreach (classReference rf in other._referenceList)
            {
                classReference nova = new classReference();
                nova.Caption = rf.Caption;
                nova.Highlighted = rf.Highlighted;
                nova.TextWithReturns = rf.TextWithReturns;
                this._referenceList.Add(nova);
            }
        }
       
}