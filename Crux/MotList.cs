using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux;

public class MotList
{
    public const char Mainconnector = '|';
        public const char Pairconnector = '¬';

        private List<Mot> jFiles = new List<Mot>();
        public Dictionary<string, int> MotDictionary = new Dictionary<string, int>();
        public List<string> Names { get { return MotDictionary.Keys.ToList(); } }
        public Mot MotForName(string nom) { return jFiles[ MotDictionary[nom]]; }
        public int Count
        {
            get { return jFiles.Count; }
        }
        public int FavouritesCount
        {
            get
            {
                int f = 0;
                foreach (Mot pwf in jFiles) { if (pwf.Favourite) { f++; } }
                return f;
            }
        }

        public void RefreshDictionary()
        {
            MotDictionary.Clear();
            for(int i=0; i < jFiles.Count; i++)
            {
                foreach(string titl in jFiles[i].Aliases)
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
            bool rv = true;
            if (proposed.Contains(Mainconnector)) { rv = false; }
            if (proposed.Contains(Pairconnector)) { rv = false; }
            return rv;
        }

        private static void BackupData()
        {
            // Purge oldest backup files if there are more than 99
            const string filespec = "Mots_*.txt";
            System.IO.FileInfo foundfileinfo;
            string DataFolder = Jbh.AppManager.DataPath;
            string[] foundfiles = System.IO.Directory.GetFiles(DataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
            while (foundfiles.Count() > 100) // current data plus 99 backups
            {
                // identify and delete the oldest file
                DateTime oldestdate = DateTime.Now;
                string oldestfile = string.Empty;
                foreach (string f in foundfiles)
                {
                    foundfileinfo = new System.IO.FileInfo(f);
                    if (foundfileinfo.LastWriteTimeUtc < oldestdate)
                    {
                        oldestdate = foundfileinfo.LastWriteTimeUtc;
                        oldestfile = f;
                    }
                }
                if (!string.IsNullOrWhiteSpace(oldestfile)) { System.IO.File.Delete(oldestfile); }
                foundfiles = System.IO.Directory.GetFiles(DataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
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
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath))
                {
                    foreach (Mot jf in jFiles)
                    { string p = jf.Specification; string c = Solus.PolyAlphabetic.Encoded(p); sw.WriteLine(c); }
                }
            }
            catch (Exception ex)
            { System.Windows.MessageBox.Show("Data not saved\n\n" + ex.Message, "Crucial", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning); }
        }

        public void LoadData()
        {
            jFiles.Clear();
            string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "Mots.txt");
            if (!System.IO.File.Exists(filepath)) { return; }
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    string c = sr.ReadLine();
                    string p = Solus.PolyAlphabetic.Decoded(c);
                    Mot jf = new Mot()
                    {
                        Specification = p
                    };
                    jFiles.Add(jf);
                }
            }
            RefreshDictionary();
        }

        
        public void AddItem(Mot newItem)
        {
            jFiles.Add(newItem);
            RefreshDictionary();
        }

        public void DeleteItem(string caption)
        {
            int y= MotDictionary[caption];
            jFiles.RemoveAt(y);
            RefreshDictionary();
        }

        public List<string> Top20()
        {
            List<DateTime> datelist = new List<DateTime>();
            foreach (Mot pwf in jFiles) { datelist.Add(pwf.Accessed); }
            datelist.Sort();
            datelist.Reverse();
            int pointer = Math.Min(25, jFiles.Count);
            DateTime criterion = datelist[pointer - 1];
            List<string> recently = new List<string>();
            for (int t = 0; t < jFiles.Count; t++)
            {
                if (jFiles[t].Accessed >= criterion) 
                {
                    foreach(string n in jFiles[t].Aliases)
                    {
                        recently.Add(n);
                    }
                }
            }
            return recently;
        }
}