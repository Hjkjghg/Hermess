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
using System.Windows.Shapes;

namespace Faktformer_0._0._1
{
    /// <summary>
    /// Logika interakcji dla klasy BazaDanych1.xaml
    /// </summary>
    public partial class BazaDanych1 : Window
    {
        //konstruktor pobiera dane do wyświetlenia przez ComboBoxy
        public BazaDanych1(List<string> podmiots, List<string> users)
        {
            InitializeComponent();
            ItemCreator.SetComboBoxItems(podmiots, ref ComboBoxPodmiot);
            ItemCreator.SetComboBoxItems(users, ref ComboBoxUser);
        }

        //zmienne wyjściowe
        public bool succes { get; set; } = false;


        public string serwer {  get; set; }
        public string user { get; set; }
        public string userPassword { get; set; }


        public int indexOfPodmiot { get; set; }
        public int indexOfSubiekt { get; set; }
        public string subiektPassword { get; set; }


        //guzik anuluj
        //zamyka okno
        private void ButtonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            succes = false;
            Close();
        }

        //przycisk zapisz
        //wczytuje dane z okna do danych wyjśćiowych i zamyka okno
        private void ButtonZapisz_Click(object sender, RoutedEventArgs e)
        {
            succes = true;
            serwer = TextBoxSerwer.Text;
            user = TextBoxUser.Text;
            userPassword = PasswordBoxBaza.Password;
            indexOfPodmiot = ComboBoxPodmiot.SelectedIndex;
            indexOfSubiekt = ComboBoxUser.SelectedIndex;
            subiektPassword = PasswordBoxSubiekt.Password;
            Close();
        }

        //sprawdza połączenie z bazą danych
        private void ButtonSprawdzPolanczenie_Click(object sender, RoutedEventArgs e)
        {
            //wsaw kod sprawdzający połączenie
        }
    }
}
