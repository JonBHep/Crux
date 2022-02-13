using System;
using System.Collections.Generic;

namespace Crux;

public class Cartes
{
    public Cartes()
        {
            LoadData();
        }

        public List<CarteBancaire> Cards = new List<CarteBancaire>();

        private static void BackupData()
        {
            // Purge oldest backup files if there are more than 99
            const string filespec = "CBs_*.txt";
            System.IO.FileInfo foundfileinfo;
            string DataFolder = Jbh.AppManager.DataPath;
            string[] foundfiles = System.IO.Directory.GetFiles(DataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
            while (foundfiles.Length > 100) // current data plus 99 backups
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
            string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "CBs.txt");
            if (System.IO.File.Exists(filepath))
            {
                System.IO.FileInfo finfo = new System.IO.FileInfo(filepath);
                string backupname = "CBs_" + finfo.LastWriteTimeUtc.ToString("yyyy-MM-dd_hh-mm") + ".txt";
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
                string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "CBs.txt");
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath))
                {
                    foreach (CarteBancaire cb in Cards)
                    { string p =cb.Specification; string c = Solus.PolyAlphabetic.Encoded(p); sw.WriteLine(c); }
                }
            }
            catch (Exception ex)
            { System.Windows.MessageBox.Show("Data not saved\n\n" + ex.Message, "Crucial", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning); }
        }

        private void LoadData()
        {
           Cards.Clear();
            string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "CBs.txt");
            if (!System.IO.File.Exists(filepath)) { return; }
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    string c = sr.ReadLine();
                    string p = Solus.PolyAlphabetic.Decoded(c);
                    CarteBancaire cb = new CarteBancaire() { Specification = p };
                    Cards.Add(cb);
                }
            }
            Cards.Sort();
        }

}