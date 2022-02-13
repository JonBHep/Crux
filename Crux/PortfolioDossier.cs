using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux;

public class PortfolioDossier : IComparable
{
    public enum DossierTypeConstants { NullDossier = 0, AccountDossier = 1, ServiceDossier = 2 };
        
        public class ClassReference
        {
            // TODO when finished, rename 
            private string _refText;
            public string Caption { get; set; }
            //public string Value { get; set; }
            public bool Highlighted { get; set; }
            
            public ClassReference()
            {
                Caption = string.Empty;
                TextWithReturns = string.Empty;
                Highlighted = false;
                _refText = string.Empty;
            }

            public string TextWithReturns
            {
                get => _refText.Replace("_", Environment.NewLine);
                set => _refText = value.Replace(Environment.NewLine, "_");
            }

            public string TextWithoutReturns
            {
                get => _refText;
                set => _refText = value;
            }

        }

        public class ClassAlert
        {
            // TODO when finished, rename 
            public string Caption { get; set; }
            public DateTime AlertDate { get; set; }
            public bool ShowAmount { get; set; }
            public ClassAlert()
            {
                Caption = string.Empty;
                AlertDate=DateTime.Today;
                ShowAmount=true;
            }
        }

        public DossierTypeConstants DossierType { get; private set; }
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
            set => _includeInDocument = value;
        }

        private bool _includeInDocument;

        private readonly List<ClassAlert> _alertList;
        
        private readonly List<ClassReference> _referenceList;
        
        public List<ClassAlert> Alerts
        {
            get => _alertList;
        }
        public List<ClassReference> References
        {
            get => _referenceList;
        }
        
        // public int AlertCount => _alertList.Count();
        //
        // public int ReferenceCount => _referenceList.Count();

        // public ClassAlert? Alert(int index)
        // {
        //     if ((index<0)||(index>=_alertList.Count())) {return null;}
        //     return _alertList[index];
        // }

        // public ClassReference? Reference(int index)
        // {
        //     if ((index < 0) || (index >= _referenceList.Count())) { return null; }
        //     return _referenceList[index];
        // }

        public void PromoteReference(int index)
        {
            if (index<1) {return;}
            if (index>=_referenceList.Count) {return;}
            ClassReference temp = _referenceList[index];
            _referenceList.RemoveAt(index);
            _referenceList.Insert(index - 1, temp);
        }

        public ClassAlert NewAlert // used in loading data
        {
            get
            {
                ClassAlert nouveau = new ClassAlert();
                _alertList.Add(nouveau);
                return nouveau;
            }
        }

        public void AddAlert (ClassAlert nova) // user-added
        {
            _alertList.Add(nova);
        }

        public ClassReference NewReference  // used in loading data
        {
            get
            {
                ClassReference nouveau = new ClassReference();
                _referenceList.Add(nouveau);
                return nouveau;
            }
        }
        
        public void AddReference(ClassReference nova) // user-added
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
                foreach (ClassAlert a in _alertList) { if (a.AlertDate < DateTime.Today) { allOk = false; } }
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
            _alertList = new List<ClassAlert>();
            _referenceList = new List<ClassReference>();
        }

        public string GroupName
        {
            get
            {
                int p = Title.IndexOf(':');
                var grup = p>0 ? Title.Substring(0, p) : "Default";
                return grup;
            }
        }

        public string TitleSpecifics
        {
            get
            {
                int p = Title.IndexOf(':');
                var species = p>0 ? Title.Substring(p + 1) : Title;
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
            foreach(ClassAlert al in other._alertList)
            {
                ClassAlert nova = new ClassAlert
                {
                    AlertDate = al.AlertDate,
                    Caption = al.Caption
                    ,
                    ShowAmount = al.ShowAmount
                };
                this._alertList.Add(nova);
            }

            this._referenceList.Clear();
            foreach (ClassReference rf in other._referenceList)
            {
                ClassReference nova = new ClassReference
                {
                    Caption = rf.Caption,
                    Highlighted = rf.Highlighted
                    ,
                    TextWithReturns = rf.TextWithReturns
                };
                this._referenceList.Add(nova);
            }
        }
       
}