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
using Xceed.Wpf.Toolkit;


namespace Faktformer_0._0._1
{
    /// <summary>
    /// Logika interakcji dla klasy Harmonogram1.xaml
    /// </summary>
    public partial class Harmonogram1 : Window
    {
        public Harmonogram1()
        {
            InitializeComponent();
        }
        //zmienne wyjściowe
        public bool succes { get; set; }


        public string user {  get; set; }
        public string password { get; set; }


        public DateTime date {  get; set; }


        public int liczbaDni { get; set; }
        public int liczbaCzasu { get; set; }


        public bool[] selectedDaysOfWeek { get; set; } = new bool[7];
        public bool coOkresCzasuCzyTydzien {  get; set; }
        public bool czyPowiadoienia {  get; set; }
        public bool uruchomZadanieJakoUzytkownik {  get; set; }


        //guzik anuluj
        //zamyka okno
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            succes = false;
            Close();
        }

        //guzik zapisz
        //wczytuje dane z okna do danych wyjśćiowych i zamyka okno
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            succes = true;
            date = PickedDate.DisplayDate;
            liczbaDni = int.Parse(NumBoxLiczbaDni.Text);
            liczbaCzasu = int.Parse(NumBoxLiczbaCzasu.Text);
            user = TextBoxUser.Text;
            password = PasswordBoxPassword.Password;
            coOkresCzasuCzyTydzien = (bool)RadioOkresCzasu.IsChecked;
            czyPowiadoienia = (bool)CheckBoxPowiadomienia.IsChecked;
            uruchomZadanieJakoUzytkownik = (bool)CheckBoxUruchomZadanieJakoUzytkownik.IsChecked;
            selectedDaysOfWeek[0] = (bool)CheckBoxMonday.IsChecked;
            selectedDaysOfWeek[1] = (bool)CheckBoxTuesday.IsChecked;
            selectedDaysOfWeek[2] = (bool)CheckBoxWednesday.IsChecked;
            selectedDaysOfWeek[3] = (bool)CheckBoxThursday.IsChecked;
            selectedDaysOfWeek[4] = (bool)CheckBoxFriday.IsChecked;
            selectedDaysOfWeek[5] = (bool)CheckBoxSaturday.IsChecked;
            selectedDaysOfWeek[6] = (bool)CheckBoxSunday.IsChecked;
            Close();
        }

        //Ustawienie dany w polu dany na aktualną datę po załadowaniu okna
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PickedDate.DisplayDate = DateTime.Now;
            PickedDate.Text = DateTime.Now.ToString();
        }

        //Jeśli radio zaznaczone odznacz pozostałę (1)
        private void radio1_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool)RadioOkresCzasu.IsChecked) { RadioCoTydzien.IsChecked = false; }
        }

        //Jeśli radio zaznaczone odznacz pozostałę (2)
        private void radio2_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool)RadioCoTydzien.IsChecked) { RadioOkresCzasu.IsChecked = false; }
        }
    }
}
