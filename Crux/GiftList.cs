using System;
using System.Collections.Generic;
using System.Linq;

namespace Crux;

public class GiftList
{
    private const char SpecConnector = '|';
    private const char SafeConnector = '-';
    public const double AnnualExemption = 3000;

    private readonly List<GiftGiven> _gifts = new List<GiftGiven>();

    public List<GiftGiven> Gifts
    {
        get { return _gifts; }
    }

    public GiftList() // constructor
    {
        LoadData();
    }

    public static string PermittedString(string proposed)
    {
        return proposed.Replace(SpecConnector, SafeConnector);
    }

    private static void BackupData()
    {
        // Purge oldest backup files if there are more than 99
        const string filespec = "Gifts_*.txt";
        System.IO.FileInfo foundfileinfo;
        string DataFolder = Jbh.AppManager.DataPath;
        string[] foundfiles
            = System.IO.Directory.GetFiles(DataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
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

            if (!string.IsNullOrWhiteSpace(oldestfile))
            {
                System.IO.File.Delete(oldestfile);
            }

            foundfiles = System.IO.Directory.GetFiles(DataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
        }

        // Create a backup file by renaming the data file which is about to be overwritten
        string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "Gifts.txt");
        if (System.IO.File.Exists(filepath))
        {
            System.IO.FileInfo finfo = new System.IO.FileInfo(filepath);
            string backupname = "Gifts_" + finfo.LastWriteTimeUtc.ToString("yyyy-MM-dd_hh-mm") + ".txt";
            string backuppath = System.IO.Path.Combine(Jbh.AppManager.DataPath, backupname);
            if (System.IO.File.Exists(backuppath))
            {
                System.IO.File.Delete(backuppath);
            }

            System.IO.File.Move(filepath, backuppath); // rename
        }
    }

    public void SaveData()
    {
        BackupData();
        try
        {
            string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "Gifts.txt");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath))
            {
                foreach (GiftGiven jf in _gifts)
                {
                    string p = jf.Specification;
                    string c = Solus.PolyAlphabetic.Encoded(p);
                    sw.WriteLine(c);
                }
            }
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show("Data not saved\n\n" + ex.Message, "Crucial"
                , System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }
    }

    private void LoadData()
    {
        _gifts.Clear();
        string filepath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "Gifts.txt");
        if (!System.IO.File.Exists(filepath))
        {
            return;
        }

        using (System.IO.StreamReader sr = new System.IO.StreamReader(filepath))
        {
            while (!sr.EndOfStream)
            {
                string c = sr.ReadLine();
                string p = Solus.PolyAlphabetic.Decoded(c);
                GiftGiven jf = new GiftGiven
                {
                    Specification = p
                };
                _gifts.Add(jf);
            }
        }

        _gifts.Sort();
    }

    public void AddItem(GiftGiven newItem)
    {
        _gifts.Add(newItem);
        _gifts.Sort();
    }

    public void DeleteItem(int index)
    {
        _gifts.RemoveAt(index);
    }

    public double AllTimeTotal(out int count)
    {
        double tot = 0;
        int ct = 0;
        foreach (GiftGiven g in _gifts)
        {
            tot += g.Amount;
            ct++;
        }

        count = ct;
        return tot;
    }

    public double SevenYearTimeTotal(out int count)
    {
        double tot = 0;
        int ct = 0;
        foreach (GiftGiven g in _gifts)
        {
            if (g.LessThanSevenYearsOld)
            {
                tot += g.Amount;
                ct++;
            }
        }

        count = ct;
        return tot;
    }

    public double TaxYearTotal(int StartYear, out int count)
    {
        double tot = 0;
        int ct = 0;
        foreach (GiftGiven g in _gifts)
        {
            if (TaxYearStartYear(g.GiftDate) == StartYear)
            {
                if (g.LessThanSevenYearsOld)
                {
                    tot += g.Amount;
                    ct++;
                }
            }
        }

        count = ct;
        return tot;
    }

    public double TaxYearTotalLessAnnualExemption(int StartYear)
    {
        double tot = TaxYearTotal(StartYear, out int ct);
        tot = Math.Max(0, tot - AnnualExemption);
        return tot;
    }

    public double MyCalculationOfTotalPotentiallyChargeableGifts()
    {
        double tot = 0;
        for (int yr = DateTime.Today.Year - 8; yr <= DateTime.Today.Year; yr++)
        {
            tot += TaxYearTotalLessAnnualExemption(yr);
        }

        return tot;
    }

    private static int TaxYearStartYear(DateTime d)
    {
        if (d.Month < 4)
        {
            return d.Year - 1;
        }
        else if (d.Month > 4)
        {
            return d.Year;
        }
        else
        {
            // April
            if (d.Day < 6)
            {
                return d.Year - 1;
            }
            else
            {
                return d.Year;
            }
        }
    }

    public class GiftGiven : IComparable<GiftGiven>
    {
        public DateTime GiftDate { get; set; }
        public double Amount { get; set; }

        private string _detail;

        public string Specification
        {
            get
            {
                string rv = DateToCode(GiftDate) + SpecConnector;
                rv += Amount.ToString() + SpecConnector;
                rv += Detail;
                return rv;
            }
            set
            {
                string[] parts = value.Split(SpecConnector);
                GiftDate = DateFromCode(parts[0]);
                Amount = double.Parse(parts[1]);
                Detail = parts[2];
            }
        }

        public GiftGiven() // constructor
        {
            Detail = "New gift";
            GiftDate = DateTime.MinValue;
            Amount = 0;
        }

        private static string DateToCode(DateTime dat)
        {
            return dat.ToString("yyyyMMdd");
        }

        private static DateTime DateFromCode(string code)
        {
            string ystring = code.Substring(0, 4);
            string mstring = code.Substring(4, 2);
            string dstring = code.Substring(6, 2);
            int yint = int.Parse(ystring);
            int mint = int.Parse(mstring);
            int dint = int.Parse(dstring);
            DateTime d = new DateTime(yint, mint, dint);
            return d;
        }

        int IComparable<GiftGiven>.CompareTo(GiftGiven other)
        {
            return this.GiftDate.CompareTo(other.GiftDate);
        }

        public bool LessThanSevenYearsOld
        {
            get
            {
                DateTime SevenYearsAgo = DateTime.Today.AddYears(-7);
                return (GiftDate > SevenYearsAgo);
            }
        }

        public string AgeString
        {
            get
            {
                int yrs = 0;
                int mths = 0;
                DateTime pt = DateTime.Today;
                while (pt > GiftDate)
                {
                    // go back a year until reached or overshot the gift date
                    yrs++;
                    pt = pt.AddYears(-1);
                }

                if (pt < GiftDate)
                {
                    // if overshot then come forward one year
                    yrs--;
                    pt = pt.AddYears(1);

                    while (pt > GiftDate)
                    {
                        // go back a month until reached or overshot the gift date
                        mths++;
                        pt = pt.AddMonths(-1);
                    }

                    if (pt < GiftDate)
                    {
                        // if overshot then come forward one month
                        mths--;
                        pt = pt.AddMonths(1);
                    }
                }

                return $"{yrs} yr {mths} mth";
            }
        }

        public string Detail
        {
            get { return _detail; }
            set { _detail = PermittedString(value); }
        }
    }
}