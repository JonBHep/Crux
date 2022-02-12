using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux;

public class Mot
{
    public string Updated { get; set; }
        public DateTime PasswordChanged { get; set; }
        public DateTime Accessed { get; set; }
        public bool Favourite { get; set; }

        private readonly List<MotElement> elements;

        private List<string> aliases;
        public List<string> Aliases { get { return aliases; } }
        public List<MotElement> Element { get { return elements; } }
        
        public void MoveUp(int index)
        {
            MotElement m = elements[index];
            elements[index] = elements[index - 1];
            elements[index - 1] = m;
        }

        public int ElementCount { get { return elements.Count; } }

        public string Specification
        {
            get
            {
                string rv = string.Empty;
                foreach (string a in aliases)
                {
                    rv += $"{MotList.Pairconnector}{a}";
                }
                rv = rv.Substring(1) + MotList.Mainconnector;
                foreach(MotElement elem in elements)
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
                aliases = noms.ToList();
                
                int ubound = parts.GetUpperBound(0);
                int penult = ubound - 1;
                int antepenult = ubound - 2;
                
                elements.Clear();
                for (int z = 1; z < antepenult; z++) {elements.Add(new MotElement(parts[z])); }

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

        public void DeleteElement(int index)
        {
            elements.RemoveAt(index); 
        }

        public void AddElement(string key, string password, bool lien)
        {
            MotElement elem = new MotElement(key, password, lien);
            elements.Add(elem);
        }

        public void AmendElement(int index, string key, string password, bool lien)
        {
            key = key.Trim();
            password = password.Trim();
            elements[index].Caption = key;
            elements[index].Content = password;
            elements[index].IsLink = lien;
        }

        public Mot() // constructor
        {
            aliases = new List<string>() { "New password file" };
            Accessed = DateTime.Now;
            PasswordChanged = DateTime.Now;
            elements = new List<MotElement>();
        }

}