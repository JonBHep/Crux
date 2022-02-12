using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crux
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
            imageBackground.Source = bmi;
            imageBackground.Stretch = Stretch.None;
            // TODO Research embedding images
        }
                
        private void ButtonPasswords_Click(object sender, RoutedEventArgs e)
        {
            // MotStartWindow w = new MotStartWindow()
            // {
            //     Owner = this
            // };
            // w.ShowDialog();
        }

        private void ButtonAccounts_Click(object sender, RoutedEventArgs e)
        {
            // PortfolioStartWindow w = new PortfolioStartWindow()
            // {
            //     Owner = this
            // };
            // w.ShowDialog();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // windowAbout w = new windowAbout
            // {
            //     Owner = this
            // };
            // w.ShowDialog();
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.FontWeight = FontWeights.Bold;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.FontWeight = FontWeights.Normal;
        }
    }
}