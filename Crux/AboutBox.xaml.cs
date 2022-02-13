using System;
using System.Windows;
using System.Windows.Input;

namespace Crux;

public partial class AboutBox : Window
{
    public AboutBox()
    {
        InitializeComponent();
    }
    
    private void PaintCanvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        DialogResult = false;
    }

    private void Window_ContentRendered(object sender, EventArgs e)
    {
        DateTime lastVersion = new DateTime(2022, 2, 13);
        TimeSpan versionAge = DateTime.Today - lastVersion;
        var daze = (int)versionAge.TotalDays;
        var dayString = daze == 1 ? "day" : "days";
        textblockVersion.Text = $"First .NET 6.0 version {lastVersion:dd MMM yyyy} ({daze} {dayString} old)";
        textblockTitle.Text = Jbh.AppManager.AppName;
        textblockDescription.Text = "Essential personal data";
        textblockCopyright.Text ="Jonathan Hepworth 2015 - 2022";
        HistoryTextBlock.Text = "Based on a series of my previous VB and C# applications including 'Essentia', 'Crucial' etc.";
        ImplementationTextBlock.Text = "Adapted February 2022 using Rider IDE in WPF / C# / .NET 6.0";
        
        // TODO Keep this information updated
    }
}