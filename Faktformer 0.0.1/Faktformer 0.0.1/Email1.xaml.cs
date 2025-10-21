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
    /// Logika interakcji dla klasy Email1.xaml
    /// </summary>
    public partial class Email1 : Window
    {
        public Email1()
        {
            InitializeComponent();
        }

        //zmienne wyjściowe
        public bool succes { get; set; } = false;


        public string SMTP {  get; set; }
        public string userEmail { get; set; }
        public string passwordEmail { get; set; }
        public string emailFrom { get; set; }
        public string raport { get; set; }


        public int selectedIndexOfPort { get; set; }
        public int portNumber { get; set; }


        //przycisk zapisz
        //wczytuje dane z okna do danych wyjśćiowych i zamyka okno
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //sprawdza czy port jest liczbą
            if (int.TryParse(TextBoxPortEmail1.Text, out int tempPortNmber))
            {
                succes = true;
                portNumber = tempPortNmber;
                selectedIndexOfPort = ComboBoxPortEamil1.SelectedIndex;
                SMTP = TextBoxSMTPEmail1.Text;
                userEmail = TextBoxUserEmail1.Text;
                passwordEmail = PasswordBoxPasswordEmail1.Password;
                emailFrom = TextBoxOdEmail1.Text;
                raport = TextBoxRaportEmail1.Text;
                Close();
            }
            //w przypadku gdy nie jest, pokaż messagebox błąd
            else
            {
                MessageBox.Show("Podano niepoprawny port", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                
        }

        //guzik anuluj
        //zamyka okno
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            succes = false;
            Close();
        }

        //prycisk wyśij mail testowy
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //tu wprowadź kod wysyłający maila
        }
    }
}
