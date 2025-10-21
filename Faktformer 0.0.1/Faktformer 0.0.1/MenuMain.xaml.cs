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


namespace Faktformer_0._0._1
{
    /// <summary>
    /// Logika interakcji dla klasy MenuMain.xaml
    /// </summary>
    public partial class MenuMain : UserControl
    {
        
        public MenuMain()
        {
            InitializeComponent();
        }
       
        //Opcja menu kliknięta która ma otworzyć okno o id 0
        private void MenuItemHarmonogram_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).OpenWindow("Harmonogram");
            
        }

        //Opcja menu kliknięta która ma otworzyć okno o id 2
        private void MenuItemBazaDanych_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).OpenWindow("BazaDanych");

        }

        //Opcja menu kliknięta która ma otworzyć okno o id 3
        private void MenuItemEmail_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).OpenWindow("Email");
        }

        //Opcja menu kliknięta która ma otworzyć okno o id 4
        private void MenuItemParametryPracy_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).OpenWindow("ParametryPracy");
        }

        private void MenuItemWyloguj_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).OpenWindow("ProfilSelector");
        }
    }
}
