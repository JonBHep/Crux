using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Crux
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
    {
        internal MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string path = Jbh.AppManager.DataPath;
            path = System.IO.Path.Combine(path, "JohnWells1946.jpg");
            Uri uri = new Uri(path, UriKind.Absolute);
            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.UriSource = uri;
            bmi.EndInit();
            ImageBackground.Source = bmi;
            ImageBackground.Stretch = Stretch.None;
            // TODO Research embedding images
        }
                
        private void ButtonPasswords_Click(object sender, RoutedEventArgs e)
        {
            MotsStartWindow w = new MotsStartWindow()
            {
                Owner = this
            };
            w.ShowDialog();
        }

        private void ButtonAccounts_Click(object sender, RoutedEventArgs e)
        {
            PortfolioStartWindow w = new PortfolioStartWindow()
            {
                Owner = this
            };
            w.ShowDialog();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AboutBox w=new AboutBox {Owner = this};
             w.ShowDialog();
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock tb){tb.FontWeight = FontWeights.Bold;}
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBlock tb){tb.FontWeight = FontWeights.Normal;}
        }
    }
}