using System;
using System.Collections.Generic;

namespace Crux;

public class PortfolioXml
{
    // Declare the event using EventHandler<T> 
        public event EventHandler<XmlEventArguments> RaiseAlertEvent;
        public event EventHandler<XmlEventArguments> RaisePassMessageEvent;

        // Wrap event invocations inside a protected virtual method to allow derived classes to override the event invocation behavior 
        protected virtual void OnRaiseAlertEvent(XmlEventArguments e)
        // A protected member of a base class is accessible in a derived class only if the access occurs through the derived class type.
        // The implementation of a virtual member can be changed by an overriding member in a derived class.
        {
            // Make a temporary copy of the event to avoid possibility of a race condition if the last subscriber unsubscribes immediately after the null check and before the event is raised.
            EventHandler<XmlEventArguments> handler = RaiseAlertEvent;

            // Event will be null if there are no subscribers 
            if (handler != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
                e.Message += String.Format(" at {0}", DateTime.Now.ToString());

                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        protected virtual void OnRaisePassMessageEvent(XmlEventArguments e)
        {
            EventHandler<XmlEventArguments> handler = RaisePassMessageEvent;
            if (handler != null)
            {
                e.Message += String.Format(" at {0}", DateTime.Now.ToString());
                handler(this, e);
            }
        }

        private int _linesWritten;

        private readonly string _xmlFileSpec;
        private readonly PortfolioCore _info;

        public PortfolioXml(string dataFileSpec, PortfolioCore accountsData) //constructor, providing file path and data
        {
            _xmlFileSpec = dataFileSpec;
            _info = accountsData;
        }

        /// <summary>
        /// Overloaded AddXmlItem methods
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        /// <param name="writer"></param>
        private void AddXmlItem(string tag, string content, System.IO.StreamWriter writer)
        {
            if (string.IsNullOrWhiteSpace(content)) { content = string.Empty; }
            tag = tag.Trim();
            content = AcceptableText(content);
            if (string.IsNullOrWhiteSpace(content)) { return; }
            string s = "<" + tag + ">" + content.Trim() + "</" + tag + ">";
            XmlLineWrite(s, writer);
        }

        /// <summary>
        /// Overloaded AddXmlItem methods
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        /// <param name="writer"></param>
        private void AddXmlItem(string tag, int content, System.IO.StreamWriter writer)
        {
            if (content == 0) { return; }
            string contents = content.ToString();
            string s = "<" + tag.Trim() + ">" + contents + "</" + tag + ">";
            XmlLineWrite(s, writer);
        }

        /// <summary>
        /// Overloaded AddXmlItem methods
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        /// <param name="writer"></param>
        private void AddXmlItem(string tag, bool content, System.IO.StreamWriter writer)
        {
            // Removed prevention of saving 'false' values because e.g. Family.Married defaults to true
            string s = "<" + tag.Trim() + ">" + content.ToString() + "</" + tag + ">";
            XmlLineWrite(s, writer);
        }

        /// <summary>
        /// Overloaded AddXmlItem methods
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        /// <param name="writer"></param>
        private void AddXmlItem(string tag, byte content, System.IO.StreamWriter writer)
        {
            if (content == 0) { return; }
            string contents = content.ToString();
            string s = "<" + tag.Trim() + ">" + contents + "</" + tag + ">";
            XmlLineWrite(s, writer);
        }

        /// <summary>
        /// Overloaded AddXmlItem methods
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        /// <param name="writer"></param>
        private void AddXmlItem(string tag, long content, System.IO.StreamWriter writer)
        {
            if (content == 0) { return; }
            string contents = content.ToString();
            string s = "<" + tag.Trim() + ">" + contents + "</" + tag + ">";
            XmlLineWrite(s, writer);
        }

        private static string AcceptableText(string original)
        {
            string o;
            o = original.Replace('&', '+'); // ampersand messes up xml code
            o = o.Replace('<', '{');
            o = o.Replace('>', '}');
            o = o.Replace('\"', '\''); // double quotes replaced with single
            o = o.Replace((char)0, (char)32);
            o = o.Replace((char)10, (char)32);
            o = o.Replace((char)13, (char)32);
            return o.Trim();
        }

        private enum XmlLineType { Unparseable, Rubric, OpeningTag, ClosingTag, SingleLineEmptyTag, TagPlusContent }

        /// <summary>
        /// September 2018 version
        /// </summary>
        /// <param name="xmlLine">The supplied line of xml code</param>
        /// <param name="tagCaption">The name of the tag referenced in this line</param>
        /// <param name="tagValue">The content of the data in this tag (as a string)</param>
        private static XmlLineType ParseXmlLine(string xmlLine, out string tagCaption, out string tagValue)
        {
            int p1; // first <
            int p2; // first >
            int p3; // start of matching closing tag, if present

            tagCaption = string.Empty;
            tagValue = string.Empty;
            XmlLineType returnType = XmlLineType.Unparseable;

            xmlLine = Solus.PolyAlphabetic.Decoded(xmlLine);
            if (xmlLine.StartsWith("<?xml version"))
            {
                returnType = XmlLineType.Rubric;
            }
            else
            {
                p1 = xmlLine.IndexOf('<');
                p2 = xmlLine.IndexOf('>');
                if ((p1 >= 0) && (p2 > p1))
                {
                    tagCaption = xmlLine.Substring(p1 + 1, p2 - (p1 + 1));
                    p3 = xmlLine.IndexOf("</" + tagCaption + ">", p2 + 1);
                    if (p3 >= 0) // opening and closing tags and content all contained in same line
                    {
                        tagValue = xmlLine.Substring(p2 + 1, p3 - (p2 + 1));
                        returnType = XmlLineType.TagPlusContent;
                    }
                    else
                    {
                        if (xmlLine.Substring(p2 - 1, 2) == "/>") // shorthand <tag/> signifying tag with no content (has no closing tag)
                        {
                            returnType = XmlLineType.SingleLineEmptyTag;
                            tagCaption = tagCaption.Substring(0, tagCaption.Length - 1); // strip off ending slash
                            tagValue = string.Empty;
                        }
                        else
                        {
                            returnType = XmlLineType.OpeningTag;
                            if (tagCaption.StartsWith("/")) { returnType = XmlLineType.ClosingTag; } // closing tag
                        }
                    }
                }

            }
            return returnType;
        }

        private void XmlLineWrite(string linetext, System.IO.StreamWriter sw)
        {
            linetext = linetext.Trim();
            linetext = Solus.PolyAlphabetic.Encoded(linetext);
            sw.WriteLine(linetext);
            _linesWritten++;
        }

        /// <summary>
        /// September 2018 version
        /// </summary>
        /// <param name="VerifiedLines">Number of lines examined in the xml file</param>
        /// <param name="TagResolutionError">Whether any error was detected in the nesting of tags</param>
        /// <param name="TagSequences">List of nested tags found in the xml file</param>
        private void VerifyXmlFile(out int VerifiedLines, out bool TagResolutionError, out List<string> TagSequences)
        {
            // This Sub is not application-specific as MakeXmlFile and ReadXmlFile are: it does not depend on application-specific data characteristics
            // The TagSequences list is returned for diagnositc purposes; no use is made of it in normal use
            int currentLevel = -1;
            List<string> etage = new List<string>();
            string s;

            OnRaisePassMessageEvent(new XmlEventArguments("Verifying data file write"));

            TagSequences = new List<string>();
            VerifiedLines = 0;
            TagResolutionError = false;

            using (System.IO.StreamReader xr = new System.IO.StreamReader(_xmlFileSpec))
            {
                while (!xr.EndOfStream)
                {
                    s = xr.ReadLine();
                    XmlLineType LineType = ParseXmlLine(xmlLine: s, tagCaption: out string tag, tagValue: out string content);
                    VerifiedLines++;

                    switch (LineType)
                    {
                        case XmlLineType.Unparseable:
                            {
                                VerifiedLines--;
                                break;
                            }
                        case XmlLineType.Rubric:
                            {
                                break;
                            }
                        case XmlLineType.OpeningTag:
                            {
                                currentLevel++;
                                if (etage.Count <= currentLevel) { etage.Add(tag); } else { etage[currentLevel] = tag; }
                                // add to TagSequences
                                string OpeningTagPath = string.Empty;
                                for (int z = 0; z <= currentLevel; z++) { OpeningTagPath += "/" + etage[z]; }
                                OpeningTagPath = OpeningTagPath.Substring(1); // strip off the leading / slash
                                if (!TagSequences.Contains(OpeningTagPath)) { TagSequences.Add(OpeningTagPath); }
                                break;
                            }
                        case XmlLineType.ClosingTag:
                            {
                                // Record an error if the closing tag does not match the last opening tag
                                if ("/" + etage[currentLevel] != tag) { TagResolutionError = true; }
                                currentLevel--;
                                break;
                            }
                        case XmlLineType.SingleLineEmptyTag:
                            {
                                // is one-line tag with no content e.g. <Name/>
                                // add to TagSequences
                                string OneLineTagPath = string.Empty;
                                for (int z = 0; z <= currentLevel; z++) { OneLineTagPath += "/" + etage[z]; }
                                OneLineTagPath += "/" + tag;
                                OneLineTagPath = OneLineTagPath.Substring(1); // strip off the leading / slash
                                if (!TagSequences.Contains(OneLineTagPath)) { TagSequences.Add(OneLineTagPath); }
                                break;
                            }
                        case XmlLineType.TagPlusContent:
                            {
                                // opening and closing tags on one line with content
                                // add to TagSequences
                                string OneLineTagPath = string.Empty;
                                for (int z = 0; z <= currentLevel; z++) { OneLineTagPath += "/" + etage[z]; }
                                OneLineTagPath += "/" + tag;
                                OneLineTagPath = OneLineTagPath.Substring(1); // strip off the leading / slash
                                if (!TagSequences.Contains(OneLineTagPath)) { TagSequences.Add(OneLineTagPath); }
                                break;
                            }
                        default:
                            {
                                throw new Exception("Unexpected XmlLineType value in VerifyXmlFile method");
                            }
                    }
                }

                if (currentLevel != -1) { TagResolutionError = true; }
                TagSequences.Sort();
            }
            OnRaisePassMessageEvent(new XmlEventArguments("Xml data saved"));
        }

        /// <summary>
        /// September 2018 version
        /// </summary>
        public void WriteXmlFile()
        {
            // This Sub is not application-specific as MakeXmlFile (private) and ReadXmlFile (public) are: i.e. it does not depend on application-specific data characteristics

            MakeXmlFile();

            VerifyXmlFile(VerifiedLines: out int linesVerified, TagResolutionError: out bool verificationTabError, TagSequences: out List<string> TagStructure);
            if (verificationTabError)
            {
                OnRaiseAlertEvent(new XmlEventArguments("The data file that has just been written could not be verified\nThere were tag resolution errors"));
            }
            else if (linesVerified != _linesWritten)
            {
                OnRaiseAlertEvent(new XmlEventArguments("The data file that has just been written could not be verified\nLines written = " + _linesWritten.ToString() + " lines read = " + linesVerified.ToString()));
            }
        }

        private void MakeXmlFile()
        {
            _linesWritten = 0;

            using (System.IO.StreamWriter xw = new System.IO.StreamWriter(_xmlFileSpec))
            {
                // xml document prolog
                string s = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\" standalone=\"yes\"?>";
                // i.e. <?xml version="1.0" encoding="ISO-8859-1" standalone="yes"?>
                // ISO-8859-1" signifies Western European latin characters
                XmlLineWrite(s, xw);

                XmlLineWrite("<AllData>", xw);
                XmlLineWrite("<Accounts>", xw);
                for (int x = 0; x < _info.AccountCount; x++)
                {
                    XmlLineWrite("<Account>", xw);
                    PortfolioDossier a = _info.Account(x);
                    AddXmlItem("Title", a.Title, xw);
                    AddXmlItem("Obsolete", a.Obsolete.ToString(), xw);
                    AddXmlItem("Balance", a.Amount.ToString(), xw);
                    AddXmlItem("BalanceDate", a.LastDate.ToString(), xw);
                    AddXmlItem("OnlineOperation", a.Option.ToString(), xw);
                    AddXmlItem("CurrencyEuro", a.CurrencyEuro.ToString(), xw);
                    AddXmlItem("Provider", a.ProviderOrganisation, xw);
                    for (int y = 0; y < a.ReferenceCount; y++)
                    {
                        PortfolioDossier.classReference r = a.Reference(y);
                        XmlLineWrite("<Reference-A>", xw);
                        AddXmlItem("ReferenceCaption", r.Caption, xw);
                        AddXmlItem("ReferenceHighlighted", r.Highlighted.ToString(), xw);
                        AddXmlItem("ReferenceValue", r.TextWithoutReturns, xw);
                        XmlLineWrite("</Reference-A>", xw);
                    }
                    for (int y = 0; y < a.AlertCount; y++)
                    {
                        PortfolioDossier.classAlert t = a.Alert(y);
                        XmlLineWrite("<Alert-A>", xw);
                        AddXmlItem("AlertDate", t.AlertDate.ToString(), xw);
                        AddXmlItem("AlertCaption", t.Caption, xw);
                        AddXmlItem("AlertShowAmount", t.ShowAmount.ToString(), xw);
                        XmlLineWrite("</Alert-A>", xw);
                    }
                    XmlLineWrite("</Account>", xw);
                }
                XmlLineWrite("</Accounts>", xw);

                XmlLineWrite("<Services>", xw);
                for (int x = 0; x < _info.ServiceCount; x++)
                {
                    PortfolioDossier a = _info.Service(x);
                    XmlLineWrite("<Service>", xw);
                    AddXmlItem("Title", a.Title, xw);
                    AddXmlItem("Obsolete", a.Obsolete.ToString(), xw);
                    AddXmlItem("Cost", a.Amount.ToString(), xw);
                    AddXmlItem("LastPaymentDate", a.LastDate.ToString(), xw);
                    AddXmlItem("DirectDebit", a.Option.ToString(), xw);
                    AddXmlItem("ElectronicDocumentation", a.IncludeInDocument.ToString(), xw);
                    AddXmlItem("CurrencyEuro", a.CurrencyEuro.ToString(), xw);
                    AddXmlItem("Provider", a.ProviderOrganisation, xw);
                    for (int y = 0; y < a.ReferenceCount; y++)
                    {
                        PortfolioDossier.classReference r = a.Reference(y);
                        XmlLineWrite("<Reference-S>", xw);
                        AddXmlItem("ReferenceCaption", r.Caption, xw);
                        AddXmlItem("ReferenceHighlighted", r.Highlighted.ToString(), xw);
                        AddXmlItem("ReferenceValue", r.TextWithoutReturns, xw);
                        XmlLineWrite("</Reference-S>", xw);
                    }
                    for (int y = 0; y < a.AlertCount; y++)
                    {
                        PortfolioDossier.classAlert t = a.Alert(y);
                        XmlLineWrite("<Alert-S>", xw);
                        AddXmlItem("AlertDate", t.AlertDate.ToString(), xw);
                        AddXmlItem("AlertCaption", t.Caption, xw);
                        AddXmlItem("AlertShowAmount", t.ShowAmount.ToString(), xw);
                        XmlLineWrite("</Alert-S>", xw);
                    }
                    XmlLineWrite("</Service>", xw);
                }
                XmlLineWrite("</Services>", xw);

                XmlLineWrite("<DatedBalances>", xw);
                _info.PurgeDatedBalances(); // flag those which had not changed since the previous recorded balance
                for (int x = 0; x < _info.DatedBalances.Count; x++)
                {
                    if (_info.DatedBalances[x].UnwantedFlag == false)
                    {
                        AddXmlItem("Balance", _info.DatedBalances[x].Specification, xw);
                    }
                }
                AddXmlItem("LastDocExport", _info.LastDocumentExport.ToString(), xw);
                XmlLineWrite("</DatedBalances>", xw);

                XmlLineWrite("</AllData>", xw);
            }

            OnRaisePassMessageEvent(new XmlEventArguments("Data file written"));
        }

        /// <summary>
        /// September 2018 version
        /// </summary>
        public void ReadXmlFile()
        {
            PortfolioDossier accty = new PortfolioDossier(PortfolioDossier.DossierTypeConstants.AccountDossier);
            PortfolioDossier polly = new PortfolioDossier(PortfolioDossier.DossierTypeConstants.ServiceDossier);
            PortfolioDossier.classAlert zAlert = new PortfolioDossier.classAlert();
            PortfolioDossier.classReference zRefer = new PortfolioDossier.classReference();

            List<string> level = new List<string>();
            string s;

            bool valueBool;
            Single valueSingle;
            DateTime valueDateTime;

            List<string> ReportedUnknownTags = new List<string>(); // todo review whether this is correctly implemented

            OnRaisePassMessageEvent(new XmlEventArguments("Starting load"));

            _info.ClearAllData();

            using (System.IO.StreamReader xmlrdr = new System.IO.StreamReader(_xmlFileSpec))
            {
                while (!xmlrdr.EndOfStream)
                {
                    s = xmlrdr.ReadLine();
                    XmlLineType LineType = ParseXmlLine(xmlLine: s, tagCaption: out string tag, tagValue: out string content);
                    switch (LineType)
                    {
                        case XmlLineType.Unparseable:
                        case XmlLineType.Rubric:
                            {
                                break;
                            }
                        case XmlLineType.OpeningTag:
                            {
                                level.Add(tag);
                                switch (tag)
                                {
                                    case "AllData": // level 1
                                    case "Accounts": // level 2
                                    case "Services": // level 2
                                    case "DatedBalances": // level2
                                        { break; }
                                    case "Account": // level 3
                                        { accty = _info.NewAccount; break; }
                                    case "Service": // level 3
                                        { polly = _info.NewService; break; }
                                    case "Reference-A": // level 4
                                        { zRefer = accty.NewReference; break; }
                                    case "Reference-S": // level 4
                                        { zRefer = polly.NewReference; break; }
                                    case "Alert-A": // level 4
                                        { zAlert = accty.NewAlert; break; }
                                    case "Alert-S": // level 4
                                        { zAlert = polly.NewAlert; break; }
                                    default:
                                        {
                                            string msg = $"Unknown XML tag at level {level.Count}\"{tag}\"";
                                            if (!ReportedUnknownTags.Contains(msg))
                                            {
                                                OnRaiseAlertEvent(new XmlEventArguments(msg));
                                                ReportedUnknownTags.Add(msg);
                                            }
                                            break;
                                        }
                                } // end switch
                                break;
                            }
                        case XmlLineType.ClosingTag:
                            {
                                if ("/" + level[level.Count - 1] != tag) //
                                {
                                    OnRaiseAlertEvent(new XmlEventArguments($"Mismatched opening and closing XML tags \"{level[level.Count - 1]}\" and \"{tag}"));
                                }
                                level.RemoveAt(level.Count - 1);
                                break;
                            }
                        case XmlLineType.SingleLineEmptyTag:
                            {
                                break;
                            }
                        case XmlLineType.TagPlusContent:
                            {
                                switch (level[level.Count - 1])
                                {
                                    case "AllData":
                                    case "Accounts":
                                    case "Services":
                                        {
                                            // there are no tag pairs within AllData, Accounts or Services
                                            System.Windows.MessageBox.Show("Unexpected XML tag \"" + tag + "\" within \"" + level[level.Count - 1] + "\"", "Portfolio Xml file reading", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                                            break;
                                        }
                                    case "Account":
                                        {
                                            switch (tag)
                                            {
                                                case "Obsolete":
                                                    { if (bool.TryParse(content, out valueBool)) { accty.Obsolete = valueBool; } break; }
                                                case "Title":
                                                    { accty.Title = content; break; }
                                                case "Balance":
                                                    { if (Single.TryParse(content, out valueSingle)) { accty.Amount = valueSingle; } break; }
                                                case "BalanceDate":
                                                    { if (DateTime.TryParse(content, out valueDateTime)) { accty.LastDate = valueDateTime; } break; }
                                                case "OnlineOperation":
                                                    { if (bool.TryParse(content, out valueBool)) { accty.Option = valueBool; } break; }
                                                case "CurrencyEuro":
                                                    { if (bool.TryParse(content, out valueBool)) { accty.CurrencyEuro = valueBool; } break; }
                                                case "Provider":
                                                    { accty.ProviderOrganisation = content; break; }
                                                default:
                                                    {
                                                        string msg = "Unknown XML tag at level " + level.Count.ToString() + " (\"" + level[level.Count - 1] + "\") \"" + tag + "\"";
                                                        if (!ReportedUnknownTags.Contains(msg))
                                                        {
                                                            ReportedUnknownTags.Add(msg);
                                                            System.Windows.MessageBox.Show(msg, "Portfolio Xml file reading", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                                                        }
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case "Service":
                                        {
                                            switch (tag)
                                            {
                                                case "Obsolete":
                                                    { if (bool.TryParse(content, out valueBool)) { polly.Obsolete = valueBool; } break; }
                                                case "Title":
                                                    { polly.Title = content; break; }
                                                case "Cost":
                                                    { if (Single.TryParse(content, out valueSingle)) { polly.Amount = valueSingle; } break; }
                                                case "LastPaymentDate":
                                                    { if (DateTime.TryParse(content, out valueDateTime)) { polly.LastDate = valueDateTime; } break; }
                                                case "DirectDebit":
                                                    { if (bool.TryParse(content, out valueBool)) { polly.Option = valueBool; } break; }
                                                case "ElectronicDocumentation":
                                                    { if (bool.TryParse(content, out valueBool)) { polly.IncludeInDocument = valueBool; } break; }
                                                case "CurrencyEuro":
                                                    { if (bool.TryParse(content, out valueBool)) { polly.CurrencyEuro = valueBool; } break; }
                                                case "Provider":
                                                    { polly.ProviderOrganisation = content; break; }
                                                default:
                                                    {
                                                        string msg = "Unknown XML tag at level " + level.Count.ToString() + " (\"" + level[level.Count - 1] + "\") \"" + tag + "\"";
                                                        if (!ReportedUnknownTags.Contains(msg))
                                                        {
                                                            System.Windows.MessageBox.Show(msg, "Portfolio Xml file reading", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                                                            ReportedUnknownTags.Add(msg);
                                                        }
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case "Reference-A":
                                    case "Reference-S":
                                        {
                                            switch (tag)
                                            {
                                                case "ReferenceCaption":
                                                    { zRefer.Caption = content; break; }
                                                case "ReferenceHighlighted":
                                                    { if (bool.TryParse(content, out valueBool)) { zRefer.Highlighted = valueBool; }; break; }
                                                case "ReferenceValue":
                                                    { zRefer.TextWithoutReturns = content; break; }
                                                default:
                                                    {
                                                        string msg = "Unknown XML tag at level " + level.Count.ToString() + " (\"" + level[level.Count - 1] + "\") \"" + tag + "\"";
                                                        if (!ReportedUnknownTags.Contains(msg))
                                                        {
                                                            System.Windows.MessageBox.Show(msg, "Portfolio Xml file reading", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                                                            ReportedUnknownTags.Add(msg);
                                                        }
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case "Alert-A":
                                    case "Alert-S":
                                        {
                                            switch (tag)
                                            {
                                                case "AlertDate":
                                                    { if (DateTime.TryParse(content, out valueDateTime)) { zAlert.AlertDate = valueDateTime; } break; }
                                                case "AlertCaption":
                                                    { zAlert.Caption = content; break; }
                                                case "AlertShowAmount":
                                                    { if (bool.TryParse(content, out valueBool)) { zAlert.ShowAmount = valueBool; }; break; }
                                                default:
                                                    {
                                                        string msg = "Unknown XML tag at level " + level.Count.ToString() + " (\"" + level[level.Count - 1] + "\") \"" + tag + "\"";
                                                        if (!ReportedUnknownTags.Contains(msg))
                                                        {
                                                            System.Windows.MessageBox.Show(msg, "Portfolio Xml file reading", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                                                            ReportedUnknownTags.Add(msg);
                                                        }
                                                        break;
                                                    }
                                            }
                                            break;

                                        }
                                    case "DatedBalances":
                                        {
                                            switch (tag)
                                            {
                                                case "Balance":
                                                    { _info.DatedBalances.Add(new PortfolioCore.ClassDatedBalances(content)); break; }
                                                case "LastDocExport":
                                                    { if (DateTime.TryParse(content, out valueDateTime)) { _info.LastDocumentExport = valueDateTime; } break; }
                                                default:
                                                    {
                                                        string msg = "Unknown XML tag at level " + level.Count.ToString() + " (\"" + level[level.Count - 1] + "\") \"" + tag + "\"";
                                                        if (!ReportedUnknownTags.Contains(msg))
                                                        {
                                                            System.Windows.MessageBox.Show(msg, "Portfolio Xml file reading", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                                                            ReportedUnknownTags.Add(msg);
                                                        }
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                    }
                    OnRaisePassMessageEvent(new XmlEventArguments("Finished loading"));
                    if (level.Count != 0) { OnRaiseAlertEvent(new XmlEventArguments("Opening and closing XML tags have not been resolved")); }
                }
            }
        }
}