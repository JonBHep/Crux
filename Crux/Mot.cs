using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux;

internal class Mot
{
    internal string Updated { get; set; }
    internal DateTime PasswordChanged { get; set; }
    internal DateTime Accessed { get; set; }
    internal bool Favourite { get; set; }

        private readonly List<MotElement> _elements;

        private List<string> _aliases;
        internal List<string> Aliases => _aliases;
        internal List<MotElement> Element => _elements;

        internal void MoveUp(int index)
        {
            (_elements[index], _elements[index - 1]) = (_elements[index - 1], _elements[index]);
        }

        internal int ElementCount => _elements.Count;

        internal string Specification
        {
            get
            {
                string rv = string.Empty;
                foreach (string a in _aliases)
                {
                    rv += $"{MotList.Pairconnector}{a}";
                }
                rv = rv.Substring(1) + MotList.Mainconnector;
                foreach(MotElement elem in _elements)
                {
                    rv += elem.Specification + MotList.Mainconnector;
                }
        
                if (Favourite) { rv += "F"; } else { rv += "f"; }
                if (string.IsNullOrWhiteSpace(Updated)) { Updated = DateTime.Today.ToString("dd MMM yyyy"); }
                rv += Updated + MotList.Mainconnector;
                rv += $"{Accessed.ToBinary()}{MotList.Mainconnector}";
                rv += $"{PasswordChanged.ToBinary()}";
                return rv;
            }
            set
            {
                string[] parts = value.Split(MotList.Mainconnector);

                string[] noms = parts[0].Split(MotList.Pairconnector);
                _aliases = noms.ToList();
                
                int ubound = parts.GetUpperBound(0);
                int penult = ubound - 1;
                int antepenult = ubound - 2;
                
                _elements.Clear();
                for (int z = 1; z < antepenult; z++) {_elements.Add(new MotElement(parts[z])); }

                string updpart = parts[antepenult];
                Favourite = updpart.First() == 'F';
                Updated = updpart.Substring(1);
                
                string accesspart = parts[penult];
                long ad = long.Parse(accesspart);
                DateTime access = DateTime.FromBinary(ad);
                Accessed = access;
                
                string pwdchangedpart = parts[ubound];
                long ch = long.Parse(pwdchangedpart);
                DateTime changee = DateTime.FromBinary(ch);
                
                PasswordChanged = changee;
            }
        }

        internal void DeleteElement(int index)
        {
            _elements.RemoveAt(index); 
        }

        internal void AddElement(string key, string password, bool lien)
        {
            MotElement elem = new MotElement(key, password, lien);
            _elements.Add(elem);
        }

        internal void AmendElement(int index, string key, string password, bool lien)
        {
            key = key.Trim();
            password = password.Trim();
            _elements[index].Caption = key;
            _elements[index].Content = password;
            _elements[index].IsLink = lien;
        }

        internal Mot() // constructor
        {
            _aliases = new List<string>() { "New password file" };
            Accessed = DateTime.Now;
            PasswordChanged = DateTime.Now;
            _elements = new List<MotElement>();
            Updated = string.Empty;
        }

}