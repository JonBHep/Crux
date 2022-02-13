using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Crux;

public partial class PortfolioAlertsWindow
{
    
    public PortfolioAlertsWindow(List<PortfolioCore.ClassDueDate> alerts)
        {
            InitializeComponent();
            _dueDates = alerts;
        }

        private readonly List<PortfolioCore.ClassDueDate> _dueDates;
        private int _yearMin = DateTime.Today.Year;
        private int _yearMax = DateTime.Today.Year;
        private System.Collections.ObjectModel.ObservableCollection<AlertItem> _alertItems;

        private class AlertItem
        {
            public string DueDate { get; set; }
            public string Day { get; set; }
            public string Type { get; set; }
            public string Item { get; set; }
            public string Alert { get; set; }
            public string Amount { get; set; }
            public string Away { get; set; }
            public bool Overdue { get; set; }
            public string Grouper { get; set; }
        }
            
        private void BuildList()
        {
            _alertItems = new System.Collections.ObjectModel.ObservableCollection<AlertItem>();
            DateTime tomorrow = DateTime.Today.AddDays(1);
            foreach (PortfolioCore.ClassDueDate alrt in _dueDates)
            {
                AlertItem ai = new AlertItem
                {
                    Grouper = alrt.Due.Year.ToString(),
                    DueDate = alrt.Due.ToString("d MMMM"),
                    Day = alrt.Due.ToString("ddd"),
                    Type = alrt.Category
                };
                if (ai.Type == "TODAY") { ai.Grouper = "TODAY"; }
                ai.Item = alrt.PortfolioDescription;
                ai.Alert = alrt.AlertDescription;
                ai.Amount = alrt.Amount;
                _alertItems.Add(ai);
                int dys = alrt.DaysAway;
                ai.Away = Math.Abs(dys).ToString() + " days";
                if (dys > 0) { ai.Overdue = true; ai.Grouper = "Overdue"; }
            }
        }
     
        private void GetMinAndMaxYears()
        {
            int y = 0;
            foreach (PortfolioCore.ClassDueDate dd in _dueDates)
            {
                y = dd.Due.Year;
                if (y < _yearMin) { _yearMin = y; }
                if (y > _yearMax) { _yearMax = y; }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            GetMinAndMaxYears();
            BuildList();
            listviewAlerts.ItemsSource = _alertItems;
            // set up listview grouping (see also XAML)
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listviewAlerts.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Grouper");
            view.GroupDescriptions.Add(groupDescription);
        }

}