using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux;

public class MotList
{
    public const char Mainconnector = '|';
        public const char Pairconnector = '¬';

        private readonly List<Mot> _jFiles = new List<Mot>();
        internal readonly Dictionary<string, int> MotDictionary = new Dictionary<string, int>();
        internal List<string> Names => MotDictionary.Keys.ToList();
        internal Mot MotForName(string nom) { return _jFiles[ MotDictionary[nom]]; }
        internal int Count => _jFiles.Count;

        internal int FavouritesCount
        {
            get
            {
                int f = 0;
                foreach (Mot pwf in _jFiles) { if (pwf.Favourite) { f++; } }
                return f;
            }
        }

        internal void RefreshDictionary()
        {
            MotDictionary.Clear();
            for(int i=0; i < _jFiles.Count; i++)
            {
                foreach(string titl in _jFiles[i].Aliases)
                {
                    MotDictionary.Add(titl, i);
                }
            }
        }

        public MotList()
        {
            LoadData();
        }

        public static bool IsPermittedString(string proposed)
        {
            bool rv = !proposed.Contains(Mainconnector);
            if (proposed.Contains(Pairconnector)) { rv = false; }
            return rv;
        }

        private static void BackupData()
        {
            // Purge oldest backup files if there are more than 99
            const string filespec = "Mots_*.txt";
            string dataFolder = Jbh.AppManager.DataPath;
            string[] foundfiles = System.IO.Directory.GetFiles(dataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
            while (foundfiles.Count() > 100) // current data plus 99 backups
            {
                // identify and delete the oldest file
                DateTime oldestdate = DateTime.Now;
                string oldestfile = string.Empty;
                foreach (string f in foundfiles)
                {
                    var foundfileinfo = new System.IO.FileInfo(f);
                    if (foundfileinfo.LastWriteTimeUtc < oldestdate)
                    {
                        oldestdate = foundfileinfo.LastWriteTimeUtc;
                        oldestfile = f;
                    }
                }
                if (!string.IsNullOrWhiteSpace(oldestfile)) { System.IO.File.Delete(oldestfile); }
                foundfiles = System.IO.Directory.GetFiles(dataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
            }
            // Create a backup file by renaming the data file which is about to be overwritten
            string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "Mots.txt");
            if (System.IO.File.Exists(filepath))
            {
                System.IO.FileInfo finfo = new System.IO.FileInfo(filepath);
                string backupname = "Mots_" + finfo.LastWriteTimeUtc.ToString("yyyy-MM-dd_hh-mm") + ".txt";
                string backuppath = System.IO.Path.Combine(Jbh.AppManager.DataPath, backupname);
                if (System.IO.File.Exists(backuppath)) { System.IO.File.Delete(backuppath); }
                System.IO.File.Move(filepath, backuppath); // rename
            }
        }
        public void SaveData()
        {
            BackupData();
            try
            {
                string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "Mots.txt");
                using System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath);
                foreach (Mot jf in _jFiles)
                { string p = jf.Specification; string c = Solus.PolyAlphabetic.Encoded(p); sw.WriteLine(c); }
            }
            catch (Exception ex)
            { System.Windows.MessageBox.Show("Data not saved\n\n" + ex.Message, "Crucial", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning); }
        }

        private void LoadData()
        {
            _jFiles.Clear();
            string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "Mots.txt");
            if (!System.IO.File.Exists(filepath)) { return; }
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    string? rd = sr.ReadLine();
                    if (rd is { })
                    {
                        string p = Solus.PolyAlphabetic.Decoded(rd);
                        Mot jf = new Mot()
                        {
                            Specification = p
                        };
                        _jFiles.Add(jf);
                    }
                }
            }
            RefreshDictionary();
        }


        internal void AddItem(Mot newItem)
        {
            _jFiles.Add(newItem);
            RefreshDictionary();
        }

        public void DeleteItem(string caption)
        {
            int y= MotDictionary[caption];
            _jFiles.RemoveAt(y);
            RefreshDictionary();
        }

        public List<string> Top20()
        {
            List<DateTime> datelist = new List<DateTime>();
            foreach (Mot pwf in _jFiles) { datelist.Add(pwf.Accessed); }
            datelist.Sort();
            datelist.Reverse();
            int pointer = Math.Min(25, _jFiles.Count);
            DateTime criterion = datelist[pointer - 1];
            List<string> recently = new List<string>();
            foreach (var t1 in _jFiles)
            {
                if (t1.Accessed >= criterion) 
                {
                    foreach(string n in t1.Aliases)
                    {
                        recently.Add(n);
                    }
                }
            }
            return recently;
        }
}