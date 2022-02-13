using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Crux;

public partial class PortfolioStartWindow
{
    private bool _dataWasLoaded;
    private readonly string _dataFilePath = System.IO.Path.Combine(Jbh.AppManager.DataPath, "Portfolio.xml");
    private readonly PortfolioXml _xmlMachine;
    private PortfolioCore _portfolio = new();

    public PortfolioStartWindow()
    {
        InitializeComponent();

        string path = Jbh.AppManager.DataPath;
        path = System.IO.Path.Combine(path, "JohnWells1947.bmp");
        Uri uri = new Uri(path, UriKind.Absolute);
        BitmapImage bmi = new BitmapImage();
        bmi.BeginInit();
        bmi.UriSource = uri;
        bmi.EndInit();
        ImagePicture.Source = bmi;
        ImagePicture.Stretch = Stretch.Uniform;

        _xmlMachine = new PortfolioXml(_dataFilePath, _portfolio);
        // TODO Research embedding images
    }

    private void ButtonClose_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void ShowCounts()
    {
        if (_dataWasLoaded)
        {
            TextblockAccountsCount.Foreground = new SolidColorBrush(Colors.DarkBlue);
            TextblockServicesCount.Foreground = new SolidColorBrush(Colors.DarkGreen);
            TextblockAccountsCount.Text = _portfolio.AccountCount.ToString() + " accounts";
            TextblockServicesCount.Text = _portfolio.ServiceCount.ToString() + " services";

            int aco = _portfolio.AccountCountOverdue(out List<string> acSoucis);
            int svo = _portfolio.ServiceCountOverdue(out List<string> svSoucis);
            if (aco + svo == 0)
            {
                TextblockOverdue.Visibility = Visibility.Hidden;
                TextblockAccountsOverdueCount.Visibility = Visibility.Hidden;
                TextblockServicesOverdueCount.Visibility = Visibility.Hidden;
            }
            else
            {
                TextblockOverdue.Visibility = Visibility.Visible;
                if (aco == 0)
                {
                    TextblockAccountsOverdueCount.Visibility = Visibility.Collapsed;
                }
                else
                {
                    System.Windows.Controls.StackPanel spnl = new System.Windows.Controls.StackPanel();
                    foreach (string s in acSoucis)
                    {
                        System.Windows.Controls.TextBlock tblk = new System.Windows.Controls.TextBlock()
                            {Text = s, Padding = new Thickness(2, 2, 2, 2)};
                        spnl.Children.Add(tblk);
                    }

                    TextblockAccountsOverdueCount.Inlines.Clear();
                    TextblockAccountsOverdueCount.Inlines.Add(new Run()
                        {Text = "Accounts: " + aco.ToString(), Foreground = Brushes.Red});
                    TextblockAccountsOverdueCount.Inlines.Add(new Run()
                        {Text = " (hover for list)", Foreground = Brushes.Gray});
                    TextblockAccountsOverdueCount.Visibility = Visibility.Visible;
                    TextblockAccountsOverdueCount.ToolTip = spnl;
                }

                if (svo == 0)
                {
                    TextblockServicesOverdueCount.Visibility = Visibility.Collapsed;
                }
                else
                {
                    System.Windows.Controls.StackPanel spnl = new System.Windows.Controls.StackPanel();
                    foreach (string s in svSoucis)
                    {
                        System.Windows.Controls.TextBlock tblk = new System.Windows.Controls.TextBlock()
                            {Text = s, Padding = new Thickness(2, 2, 2, 2)};
                        spnl.Children.Add(tblk);
                    }

                    TextblockServicesOverdueCount.Inlines.Clear();
                    TextblockServicesOverdueCount.Inlines.Add(new Run()
                        {Text = "Services: " + svo.ToString(), Foreground = Brushes.Red});
                    TextblockServicesOverdueCount.Inlines.Add(new Run()
                        {Text = " (hover for list)", Foreground = Brushes.Gray});
                    TextblockServicesOverdueCount.Visibility = Visibility.Visible;
                    TextblockServicesOverdueCount.ToolTip = spnl;
                }
            }

            TextblockDocumentExported.Text = _portfolio.LastDocumentExport.ToLongDateString();
            TextblockDocumentExported.Foreground = _portfolio.DocumentExportOverdue
                ? new SolidColorBrush(Colors.Red)
                : new SolidColorBrush(Colors.Black);

        }
    }

    private void Window_ContentRendered(object sender, EventArgs e)
    {
        TextblockAccountsCount.Text = "Data not loaded";
        TextblockServicesCount.Text = "";
        _dataWasLoaded = true;
        // _portfolio = new PortfolioCore();
        if (System.IO.File.Exists(_dataFilePath))
        {
            _xmlMachine.ReadXmlFile();
            _portfolio.SortAccounts();
            _portfolio.SortServices();
        }

        ShowCounts();
    }

    private void BackUpData()
    {
        // Purge the oldest backup files if there are more than 99
        const string filespec = "Portfolio_*.xml";
        string dataFolder = Jbh.AppManager.DataPath;
        string[] foundfiles
            = System.IO.Directory.GetFiles(dataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
        while (foundfiles.Count() > 100) // current data plus 99 backups
        {
            // identify and delete the oldest file
            DateTime oldestDate = DateTime.Now;
            string oldestFile = string.Empty;
            foreach (string f in foundfiles)
            {
                var foundFileInfo = new System.IO.FileInfo(f);
                if (foundFileInfo.LastWriteTimeUtc < oldestDate)
                {
                    oldestDate = foundFileInfo.LastWriteTimeUtc;
                    oldestFile = f;
                }
            }

            if (!string.IsNullOrWhiteSpace(oldestFile))
            {
                System.IO.File.Delete(oldestFile);
            }

            foundfiles = System.IO.Directory.GetFiles(dataFolder, filespec, System.IO.SearchOption.TopDirectoryOnly);
        }

        // Create a backup copy of the data file which is about to be overwritten
        if (System.IO.File.Exists(_dataFilePath))
        {
            System.IO.FileInfo finfo = new System.IO.FileInfo(_dataFilePath);
            string backupName = "Portfolio_" + finfo.LastWriteTimeUtc.ToString("yyyy-MM-dd_hh-mm") + ".xml";
            string backupPath = System.IO.Path.Combine(Jbh.AppManager.DataPath, backupName);
            if (System.IO.File.Exists(backupPath))
            {
                System.IO.File.Delete(backupPath);
            }

            System.IO.File.Copy(_dataFilePath, backupPath);
        }

        // Add a new entry to the log of dated balances
        _portfolio.AddTodaysBalances();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (!_dataWasLoaded)
        {
            return;
        }

        UiServices.SetBusyState();
        BackUpData();
        _xmlMachine.WriteXmlFile();
        _portfolio.ExportAlertsList();
    }

    private static string GetLocationForExportedDocument()
    {
        string targetDirectory = string.Empty;
        System.IO.DriveInfo[] myDrives = System.IO.DriveInfo.GetDrives();
        foreach (System.IO.DriveInfo di in myDrives)
        {
            if (di.IsReady)
            {
                string rootPath = di.RootDirectory.FullName;
                string foundPath = System.IO.Path.Combine(rootPath, "Jbh.Original");
                //foundPath = System.IO.Path.Combine(foundPath, "Jbh.Info");
                foundPath = System.IO.Path.Combine(foundPath, "Business");
                foundPath = System.IO.Path.Combine(foundPath, "If I have died");
                if (System.IO.Directory.Exists(foundPath))
                {
                    targetDirectory = foundPath;
                }
            }

            if (!string.IsNullOrWhiteSpace(targetDirectory))
            {
                break;
            }
        }

        return targetDirectory;
    }



    private static Paragraph FreshPara(int fSize, bool grayed = false, bool bolded = false, bool @fixed = false)
    {
        Paragraph para = new Paragraph
        {
            FontFamily = @fixed ? new FontFamily("Lucida Console") : new FontFamily("Arial"), FontSize = fSize
            , Foreground = grayed ? Brushes.Gray : Brushes.Black
            , FontWeight = bolded ? FontWeights.Bold : FontWeights.Normal
        };

        return para;
    }

    private static InlineUIContainer FreshLineContainer(double lineThickness)
    {
        System.Windows.Shapes.Line ruler = new System.Windows.Shapes.Line
        {
            X1 = 0, X2 = 10, Y1 = 0, Y2 = 0, Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = lineThickness
            , Stretch = Stretch.Fill
        };
        InlineUIContainer container = new InlineUIContainer(ruler);
        return container;
    }

    private FlowDocument CreateFlowDocument(double pageWidth, double pageHeight)
    {
        FlowDocument flowDocument = new FlowDocument
        {
            PageWidth = pageWidth, PageHeight = pageHeight, PagePadding = new Thickness(76, 76, 76, 76)
            , // approx 2 cm margin around page
            IsOptimalParagraphEnabled = true
            , IsHyphenationEnabled = true, IsColumnWidthFlexible = true, ColumnWidth = pageWidth
        };

        Paragraph para = FreshPara(16, bolded: true);
        para.Inlines.Add(new Run("JONATHAN HEPWORTH: PROPERTY, ACCOUNTS, POLICIES, UTILITIES ETC."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(10, grayed: true);
        para.FontWeight = FontWeights.Thin;
        para.Inlines.Add(new Run("Document created: "));
        para.Inlines.Add(new Bold(new Run(DateTime.Today.ToLongDateString())));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run("This document was produced by a homemade application named "));
        para.Inlines.Add(new Bold(new Run("Crucial")));
        para.Inlines.Add(new Run(" which exists on my computer. To access my computer, log in with the PIN number "));
        para.Inlines.Add(new Bold(new Run("5864.")));

        para.Inlines.Add(new Run(" Another password "));
        para.Inlines.Add(new Bold(new Run("Avalon2285")));
        para.Inlines.Add(new Run(
            " will unlock some of my portable external USB drives. The most important is a black SanDisk Extreme solid state drive which will probably be found plugged in to"));
        para.Inlines.Add(new Run(
            " my desktop computer and which can be unlocked using the SanDisk security software (on my Start menu, or downloadable) and the password "));
        para.Inlines.Add(new Bold(new Run("Avalon2285")));
        para.Inlines.Add(new Run(
            ". This contains the original of my confidential data (under 'Jbh.Original\\Jbh.Business') including my financial and property information which is summarised in this document. It also contains a recent backup of my less confidential data, applications etc. under 'Jbh.Portable\\Jbh.Info'"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("MY WILL & MY EXECUTORS"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Bold(new Run("My Will")));
        para.Inlines.Add(new Run(
            " dated December 2016 is held by GA Solicitors of Gill Akaster House, 25 Lockyer Street, Plymouth PL1 2QW (tel 01752 203500). There is a photocopy in my fireproof box, to which a key should be found in a drawer in my top bedroom. The executors of my Will are GA Solicitors and my brother."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "My Will gives a right of residence at Rose Cottage for a period after my death to Rhona Jane Skilton. This provision was drawn up while we were living together. As she has now established her principal and permanent residence in France I assume that she would not wish to take up this right and that she will in any case have forfeited the right by virtue of clause 5.4.1.3 of my Will. We have not lived together, and she has not lived at Rose Cottage, since the end of May 2018."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Bold(new Run(
            "Information useful to my executors will be found on two small USB sticks stored in the fireproof box along with my Will. ")));
        para.Inlines.Add(new Run(
            "The two drives are not password protected and should contain identical information - there are two copies in case one drive malfunctions."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(
            new Run("The latest version of this document should be found on the USB sticks for my executors under "));
        para.Inlines.Add(new Bold(new Run("Jbh.Original\\Jbh.Business\\If I have died")));
        para.Inlines.Add(new Run(
            " or a copy can be created by running the Crucial application and clicking on 'Accounts, policies and services' and then the 'Document' button. This saves the document in Microsoft XPS format or sends it to print where 'Print to PDF' can be selected as the printer. There are backup copies of my personal data under "));
        para.Inlines.Add(new Bold(new Run("Jbh.Backup")));
        para.Inlines.Add(new Run(
            " on a couple of other removable disk drives. The business data on my SanDisk drive is backed up on my Samsung T3 portable drive and elsewhere."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(
            new Run("The homemade applications on my computer can be accessed through a launcher application "));
        para.Inlines.Add(new Bold(new Run("Launchpad")));
        para.Inlines.Add(new Run("  which is run by clicking on the yellow "));
        para.Inlines.Add(new Bold(new Run("JH")));
        para.Inlines.Add(new Run(" icon on the Windows desktop taskbar at the bottom of the screen. The "));
        para.Inlines.Add(new Bold(new Run("Crucial")));
        para.Inlines.Add(new Run(" application (which can also be launched using the "));
        para.Inlines.Add(new Bold(new Run("PERSONAL INFO")));
        para.Inlines.Add(new Run(
            " button in the launcher) contains details of my houses, bank and savings accounts, policies, utilities and other services, gifts that may attract inheritance tax and my passwords for many applications and websites. There is information in two sections of this application: "));
        para.Inlines.Add(new Bold(new Run("Accounts, policies and services")));
        para.Inlines.Add(new Run(" and "));
        para.Inlines.Add(new Bold(new Run("Passwords")));
        para.Inlines.Add(new Run("."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("Financial and property records"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run("My National Insurance Number is "));
        para.Inlines.Add(new Bold(new Run("YY 37 56 02 A")));
        para.Inlines.Add(new Run("."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(
            new Run("My most up-to-date business, financial &c. information will be on my Samsung T3 drive under "));
        para.Inlines.Add(new Bold(new Run("Jbh.Original\\Jbh.Business")));
        para.Inlines.Add(new Run(" as well as probably the most recent backup of my general personal data under "));
        para.Inlines.Add(new Bold(new Run("Jbh.Backup\\Jbh.Info")));
        para.Inlines.Add(
            new Run(" and the original of my general personal data will be on my computer C: drive under "));
        para.Inlines.Add(new Bold(new Run("C:\\Jbh.Original\\Jbh.Info")));
        para.Inlines.Add(new Run("."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("Family history"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "My family history information including scanned photographs and old documents, as well as documents I have written, is contained on my computer under "));
        para.Inlines.Add(new Bold(new Run("C:\\Jbh.Original\\Jbh.Info\\Family")));
        para.Inlines.Add(new Run(
            "  where can also be found all my records of the Arthur Jackson archive and website, photographs of his paintings etc. Duplicate copies of all the data in 'Family' should also be found on at least two external drives which are not password protected and which should be labelled 'Family History'. At the time of writing these are desktop hard disk drives which require their own power supply plugs which should be found with them."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("Other data"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Bold(new Run("Devon County Council ")));
        para.Inlines.Add(new Run(
            "A couple of encrypted disk drives contain backups of data held in connection with my work for Devon County Council and will not open with my usual password. They can simply be re-formatted and reused, with the exception of a USB stick with a long red cord attached which is the property of Devon County Council although they may not want it as it is probably obsolete technology."));

        para.Inlines.Add(new Run(
            " What ought to be of interest to Devon County Council is the source code of the applications I developed for the County Council and Adopt South West which they will need in order to maintain and further develop their adoption information systems. The source code is contained on the USB sticks prepared for my executors. My contacts in Devon County Council and the Adopt South West regional adoption agency hosted by Devon County Council are Kath Drescher and Mark Berry (Adoption Service - 'Adopt South-West') and Jon Prout (Devon CC ICT Services)."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("MY PROPERTY"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12, bolded: true);
        para.Inlines.Add(new Run("P1: Main residence (WHOLLY OWNED)"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12, bolded: true);
        para.Inlines.Add(new Run("Rose Cottage, Old Road, Harbertonford, Totnes, Devon, TQ9 7TA"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "This property is owned exclusively by me, without a mortgage and no other person or body has a financial interest in the property. The deeds are currently with GA Solicitors in Plymouth (Steven Leigh has been my solicitor). The mortgage account (reference 097 645741 08) with Alliance & Leicester (now part of Santander) was repaid in 2003. The approximate value of Rose Cottage is £260,000."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run("Planning Permission: ") {FontWeight = FontWeights.Bold});
        para.Inlines.Add(new Run(
            "I was granted Planning Approval for two different schemes involving building an extension incorporating a new kitchen and remodelling of the lower staircase. Both consents are still valid. Copies of the relevant plans and approvals are included on the USB drives prepared for my executors. These approvals may enhance the attractiveness of Rose Cottage to potential purchasers."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12, bolded: true);
        para.Inlines.Add(new Run("P2: Rental property (WHOLLY OWNED)"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12, bolded: true);
        para.Inlines.Add(new Run("96 Helston Road, Penryn, Cornwall TR10 8NG"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run("With effect from 4 March 2019 I am the sole owner of the property."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "This property was owned from 2005 partly by me and partly by Rhona Jane Skilton, without a mortgage or other financial encumbrance. The Declaration of Trust establishing the agreement between us was drawn up by Steven Leigh of Gill Akaster, now 'GA Solicitors', Plymouth. Our ownership was originally in the ratio of two to one, my share being two thirds and Rhona’s share one third, until October 2018. From then on, the ratio was my share 75% and Rhona's share 25% as I purchased an extra one-twelfth share from Rhona on 16 October 2018 for £16,667. This valued the house at £200,000.  Until February 2019, all income and costs were shared between me and Rhona in proportion (originally 2 to 1, then 3 to 1). On 4th March 2019 I purchased Rhona's remaining 25% share for £50,000 and I am now the sole owner of the property. This has been recorded at the Land Registry and Steven Leigh or GA Solicitors will be in possession of all relevant documents."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run("Listed Building Consent: ") {FontWeight = FontWeights.Bold});
        para.Inlines.Add(new Run(
            "This property is a Grade II Listed building. I was granted Listed Building Consent for the installation of gas central heating (completed) and for installation of extractor fans and vents (work not yet undertaken). Copies of the relevant documents are included on the USB drives prepared for my executors."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "The property is let through Townsend's property services, 92 Trevethan Road, Falmouth, TR11 2AX. (Telephone: 01326 315000 and email: enquiry@townsends.co [sic])."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12, bolded: true);
        para.Inlines.Add(new Run("P3: Holiday home (INFO ONLY - NO LONGER OWNED)"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12, bolded: true);
        para.Inlines.Add(
            new Run("La Grange de Viaud (a.k.a. Les Chabossauds), Courlac, 16210 Chalais, Charente, France"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "This property was owned exclusively by me, without a mortgage and no other person or body had a financial interest in the property."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "The property was sold on 8 February 2019, my purchase in 2008 and subsequent sale of the property being arranged by a notaire, Maitre Alexandre Desautel, Place du Chateau, BP9, 16390 Aubeterre-Sur-Dronne, France (Tel 00 33 (0)5 45 98 58 80, email alexandre.desautel@notaires.fr). I remained responsible for the payment of property taxes for the calendar year 2019, namely taxe d'habitation and taxe fonciere, which I paid in full and on time. Further details can be obtained from the estate agent Euro-Immobilier Chalais, 7 Avenue de la Gare, 16210 Chalais, France email contact@euro-immo.com (Richard and Catherine Dannreuther)."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12, bolded: true);
        para.Inlines.Add(new Run("P4: Works of Art"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "See the catalogue of Arthur Jackson’s works in 'Jbh.Info\\Family\\Arthur Jackson Hepworth' on my computer."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "Paintings and drawings by my father Arthur Jackson Hepworth (known as Arthur Jackson) which belong to me are 4/87; 10/87; 3/92."));

        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "Paintings that I inherited from my father but which I have since sold are 2/37; 3/92; 1/93; 1/94 and 1/97."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "Works of art of which I share ownership equally with my brother and sister are a painting of Portloe by our father, a plaster relief by Barbara Hepworth of my father’s sisters Jill and Peggy, and a drawing by Barbara Hepworth of my grandparents. These works are currently in the keeping of my brother (the painting), The Hepworth gallery, Wakefield (the plaster), and my sister (the drawing)."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("BANK AND SAVINGS ACCOUNTS"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "Bank statements for my current accounts going back several years will be found on my Samsung T3 drive under Jbh.Original\\Jbh.Business\\Finance."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12, bolded: true);
        para.Inlines.Add(new Run("SUMMARY"));
        flowDocument.Blocks.Add(para);

        float totalpounds = 0;
        float totaleuros = 0;
        for (int a = 0; a < _portfolio.AccountCount; a++)
        {
            if (_portfolio.Account(a).IncludeInDocument) // will automatically be true unless account is obsolete
            {
                para = FreshPara(12);
                para.Inlines.Add(new Run(_portfolio.Account(a).GroupName + ": "));
                para.Inlines.Add(new Bold(new Run(_portfolio.Account(a).TitleSpecifics)));

                para.Inlines.Add(new Run("   balance: "));
                string currency = PortfolioCore.PoundSymbol.ToString();
                if (_portfolio.Account(a).CurrencyEuro)
                {
                    totaleuros += _portfolio.Account(a).Amount;
                    currency = PortfolioCore.EuroSymbol.ToString();
                }
                else
                {
                    totalpounds += _portfolio.Account(a).Amount;
                }

                para.Inlines.Add(new Bold(new Run(currency)));
                para.Inlines.Add(new Bold(new Run(_portfolio.Account(a).Amount.ToString("#,0.00"))));
                para.Inlines.Add(new Run(" as at " + _portfolio.Account(a).LastDate.ToLongDateString()));
                flowDocument.Blocks.Add(para);
            }
        }

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("Totals"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14);
        para.Inlines.Add(new Run("Pounds"));
        para.Inlines.Add(new Bold(new Run($" {PortfolioCore.PoundSymbol}{totalpounds:#,0.00}")));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14);
        para.Inlines.Add(new Run("Euros"));
        para.Inlines.Add(new Bold(new Run($" {PortfolioCore.EuroSymbol}{totaleuros:#,0.00}")));
        flowDocument.Blocks.Add(para);

        InlineUIContainer container = FreshLineContainer(2);
        flowDocument.Blocks.Add(new Paragraph(container));

        int serial = 0;

        for (int a = 0; a < _portfolio.AccountCount; a++)
        {
            if (_portfolio.Account(a).IncludeInDocument) // will automatically be true unless account is obsolete
            {
                serial++;
                para = FreshPara(14);
                para.Inlines.Add(new Run($"A{serial}:"));
                flowDocument.Blocks.Add(para);

                para = FreshPara(14);
                para.Inlines.Add(new Run(_portfolio.Account(a).GroupName + ": "));
                para.Inlines.Add(new Bold(new Run(_portfolio.Account(a).TitleSpecifics)));
                flowDocument.Blocks.Add(para);

                para = FreshPara(12);
                para.Inlines.Add(new Run("Provider: "));
                para.Inlines.Add(new Bold(new Run(_portfolio.Account(a).ProviderOrganisation)));
                flowDocument.Blocks.Add(para);

                para = FreshPara(12);
                para.Inlines.Add(new Run("Balance: "));
                string currency = PortfolioCore.PoundSymbol.ToString();
                if (_portfolio.Account(a).CurrencyEuro)
                {
                    currency = PortfolioCore.EuroSymbol.ToString();
                }

                para.Inlines.Add(new Bold(new Run(currency)));
                para.Inlines.Add(new Bold(new Run(_portfolio.Account(a).Amount.ToString("#,0.00"))));
                para.Inlines.Add(new Run(" as at " + _portfolio.Account(a).LastDate.ToLongDateString()));
                flowDocument.Blocks.Add(para);

                para = FreshPara(12);
                para.Inlines.Add(new Run("Operated on-line: "));
                para.Inlines.Add(_portfolio.Account(a).Option ? new Bold(new Run("Yes")) : new Bold(new Run("No")));
                flowDocument.Blocks.Add(para);

                foreach (PortfolioDossier.ClassAlert alert in _portfolio.Account(a).Alerts)
                {
                    para = FreshPara(12);
                    para.Inlines.Add(new Run(alert.AlertDate.ToShortDateString() + ": "));
                    para.Inlines.Add(new Bold(new Run(alert.Caption)));
                    if (alert.ShowAmount)
                    {
                        para.Inlines.Add(new Bold(new Run(" " + _portfolio.Account(a).Amount.ToString("#,0.00"))));
                    }

                    flowDocument.Blocks.Add(para);
                }

                foreach (PortfolioDossier.ClassReference reference in _portfolio.Account(a).References)
                {
                    para = FreshPara(12, @fixed: true);
                    para.Inlines.Add(new Run(reference.Caption + ": "));
                    para.Inlines.Add(new Bold(new Run(reference.TextWithReturns)));
                    flowDocument.Blocks.Add(para);
                }

                container = FreshLineContainer(1);
                flowDocument.Blocks.Add(new Paragraph(container));
            }
        }

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("SERVICES, UTILITIES, POLICIES ETC."));
        flowDocument.Blocks.Add(para);

        container = FreshLineContainer(2);
        flowDocument.Blocks.Add(new Paragraph(container));

        serial = 0;

        for (int a = 0; a < _portfolio.ServiceCount; a++)
        {
            if (_portfolio.Service(a).IncludeInDocument)
            {
                serial++;
                para = FreshPara(14);
                para.Inlines.Add(new Run($"S{serial}:"));
                flowDocument.Blocks.Add(para);

                para = FreshPara(14);
                para.Inlines.Add(new Run(_portfolio.Service(a).GroupName + ": "));
                para.Inlines.Add(new Bold(new Run(_portfolio.Service(a).TitleSpecifics)));
                flowDocument.Blocks.Add(para);

                para = FreshPara(12);
                para.Inlines.Add(new Run("Provider: "));
                para.Inlines.Add(new Bold(new Run(_portfolio.Service(a).ProviderOrganisation)));
                flowDocument.Blocks.Add(para);

                if ((_portfolio.Service(a).LastDate.Year > 2000) && (_portfolio.Service(a).Amount > 0))
                {
                    para = FreshPara(12);
                    para.Inlines.Add(new Run("Last payment: "));
                    string currency = PortfolioCore.PoundSymbol.ToString();
                    if (_portfolio.Service(a).CurrencyEuro)
                    {
                        currency = PortfolioCore.EuroSymbol.ToString();
                    }

                    para.Inlines.Add(new Bold(new Run(currency)));
                    para.Inlines.Add(new Bold(new Run(_portfolio.Service(a).Amount.ToString("#,0.00"))));
                    para.Inlines.Add(new Run(" on " + _portfolio.Service(a).LastDate.ToLongDateString()));
                    flowDocument.Blocks.Add(para);
                }

                para = FreshPara(12);
                para.Inlines.Add(new Run("Direct debit: "));
                para.Inlines.Add(_portfolio.Service(a).Option ? new Bold(new Run("Yes")) : new Bold(new Run("No")));
                flowDocument.Blocks.Add(para);

                foreach (PortfolioDossier.ClassAlert alert in _portfolio.Service(a).Alerts)
                {
                    para = FreshPara(12);
                    para.Inlines.Add(
                        new Run(alert.AlertDate.ToShortDateString() + ": "));
                    para.Inlines.Add(new Bold(new Run(alert.Caption)));
                    if (alert.ShowAmount)
                    {
                        para.Inlines.Add(
                            new Bold(new Run(" " + _portfolio.Service(a).Amount.ToString("#,0.00"))));
                    }

                    flowDocument.Blocks.Add(para);
                }

                foreach (var reference in _portfolio.Service(a).References)
                {
                    para = FreshPara(12, @fixed: true);
                    para.Inlines.Add(new Run(reference.Caption + ": "));
                    para.Inlines.Add(new Bold(new Run(reference.TextWithReturns)));
                    flowDocument.Blocks.Add(para);
                }

                container = FreshLineContainer(1);
                flowDocument.Blocks.Add(new Paragraph(container));

            }
        }

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("GIFTS I HAVE MADE IN THE LAST 7 YEARS"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run(
            "This is a list of gifts I have made which may count as part of my estate for inheritance tax purposes. Donations made to charities and political parties are excluded. I have calculated what I deem to be the amount of my gifts made within the last seven years and which will count towards the value of my estate, after deducting the annual exemption in each tax year. The amounts and dates may be verified by reference to my bank statements saved under 'Jbh.Original\\Jbh.Business\\Finance', particularly those for NatWest and First Direct bank accounts."));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(new Run("A summary follows."));
        flowDocument.Blocks.Add(para);

        container = FreshLineContainer(2);
        flowDocument.Blocks.Add(new Paragraph(container));

        GiftList gifts = new GiftList();

        foreach (GiftList.GiftGiven g in gifts.Gifts)
        {
            if (g.LessThanSevenYearsOld)
            {
                para = FreshPara(14);
                para.Inlines.Add(new Run(g.GiftDate.ToString("dd MMM yyyy") + ": "));
                para.Inlines.Add(new Bold(new Run(" £" + g.Amount.ToString("#,0.00"))));
                para.Inlines.Add(new Run($" ({g.AgeString} ago) "));
                para.Inlines.Add(new Bold(new Run(g.Detail)));
                flowDocument.Blocks.Add(para);
            }
        }

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("GIFTS MADE IN EACH TAX YEAR"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(12);
        para.Inlines.Add(
            new Run($"The net amount is the gross amount less the annual exemption of £{GiftList.AnnualExemption}."));
        flowDocument.Blocks.Add(para);

        for (int yr = DateTime.Today.Year - 8; yr <= DateTime.Today.Year; yr++)
        {
            para = FreshPara(14);
            para.Inlines.Add(new Run($"{yr}-{yr + 1}: "));
            para.Inlines.Add(new Run(" Gross £" + gifts.TaxYearTotal(yr, out var _).ToString("#,0.00")));
            para.Inlines.Add(
                new Bold(new Run(" Net £" + gifts.TaxYearTotalLessAnnualExemption(yr).ToString("#,0.00"))));
            flowDocument.Blocks.Add(para);
        }

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("SUMMARY"));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14);
        para.Inlines.Add(new Run("All gifts in past seven years: "));
        para.Inlines.Add(new Bold(new Run($"£{gifts.SevenYearTimeTotal(out var _):#,0.00}")));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14);
        para.Inlines.Add(new Run("Potentially chargeable gifts: "));
        para.Inlines.Add(new Bold(new Run($"£{gifts.MyCalculationOfTotalPotentiallyChargeableGifts():#,0.00}")));
        flowDocument.Blocks.Add(para);

        para = FreshPara(14, bolded: true);
        para.Inlines.Add(new Run("END OF DOCUMENT"));
        flowDocument.Blocks.Add(para);

        return flowDocument;
    }

    private void ButtonAccounts_Click(object sender, RoutedEventArgs e)
    {
        PortfolioListWindow w
            = new PortfolioListWindow(PortfolioDossier.DossierTypeConstants.AccountDossier, _portfolio)
            {
                Owner = this
            };
        w.ShowDialog();
        ShowCounts();
    }

    private void ButtonServices_Click(object sender, RoutedEventArgs e)
    {
        PortfolioListWindow w
            = new PortfolioListWindow(PortfolioDossier.DossierTypeConstants.ServiceDossier, _portfolio)
            {
                Owner = this
            };
        w.ShowDialog();
        ShowCounts();
    }

    private void ButtonDueDates_Click(object sender, RoutedEventArgs e)
    {
        PortfolioAlertsWindow w = new PortfolioAlertsWindow(_portfolio.DueDates())
        {
            Owner = this
        };
        w.ShowDialog();
    }

    private void GiftsButton_Click(object sender, RoutedEventArgs e)
    {
        GiftsWindow win = new GiftsWindow() {Owner = this};
        win.ShowDialog();
    }

    private void ButtonCards_Click(object sender, RoutedEventArgs e)
    {
        CardsWindow cw = new CardsWindow() {Owner = this};
        cw.ShowDialog();
    }

    private void ButtonDocument_Click(object sender, RoutedEventArgs e)
    {
        // Save to xps document but NB Microsoft is retiring the xps format
        string path = GetLocationForExportedDocument();
        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
        {
            AddExtension = true, CheckFileExists = false, CheckPathExists = true, DefaultExt = ".xps"
            , FileName = "JBH_Accounts_Policies_Services_" + DateTime.Today.ToString("yyyy-MM-dd") + ".xps"
            , Filter = "XPS Document (.xps)|*.xps|All files (*.*)|*.*", OverwritePrompt = true
            , Title = "Save details of my accounts and services to a document", ValidateNames = true
        };
        if (!string.IsNullOrWhiteSpace(path))
        {
            dlg.InitialDirectory = path;
        }

        bool? result = dlg.ShowDialog();
        if (!result.HasValue)
        {
            return;
        }

        if (!result.Value)
        {
            return;
        }

        FlowDocument
            testdoc = CreateFlowDocument(96 * 8.27
                , 96 * 11.7); // A4 approx (inches x 96 dpi) THIS IS WHERE THE CONTENT IS SPECIFIED
        // Save document
        string filename = dlg.FileName;
        if (System.IO.File.Exists(filename))
        {
            System.IO.File.Delete(filename);
        } // else will crash when trying to create a doc with the same name

        XpsDocument doc = new XpsDocument(dlg.FileName, System.IO.FileAccess.ReadWrite);
        XpsDocumentWriter docWriter = XpsDocument.CreateXpsDocumentWriter(doc);
        IDocumentPaginatorSource paginer = testdoc;
        docWriter.Write(paginer.DocumentPaginator);
        doc.Close();
        _portfolio.LastDocumentExport = DateTime.Now;
        ShowCounts();
    }

    private void PrintButton_Click(object sender, RoutedEventArgs e)
    {
        // print document - e.g. in order to use 'Print to PDF' or 'Print to file' printer driver

        FlowDocument
            testdoc = CreateFlowDocument(96 * 8.27
                , 96 * 11.7); // A4 approx (inches x 96 dpi) THIS IS WHERE THE CONTENT IS SPECIFIED

        //Now print using PrintDialog
        var pd = new System.Windows.Controls.PrintDialog();

        bool? back = pd.ShowDialog();
        if (back.HasValue && back.Value)
        {
            testdoc.PageHeight = pd.PrintableAreaHeight;
            testdoc.PageWidth = pd.PrintableAreaWidth;
            IDocumentPaginatorSource idocument = testdoc;

            pd.PrintDocument(idocument.DocumentPaginator, "Printing FlowDocument");
        }

        _portfolio.LastDocumentExport = DateTime.Now;
        ShowCounts();
    }
}