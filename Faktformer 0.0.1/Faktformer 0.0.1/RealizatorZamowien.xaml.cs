using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy RealizatorZamowien.xaml
    /// </summary>
    public partial class RealizatorZamowien : Window
    {
        //konstruktor pobiera dane do wyświetlenia przez ComboBoxy
        public RealizatorZamowien(List<string> magazyny, List<string> rodzaje, List<string> waluty, List<string> platnosci, List<string> terminy, List<string> statusy, List<string> kategoria, List<string> grupaKontrahentow, List<string> flagiWlasne, List<string> cechaKontrahentow)
        {
            InitializeComponent();
            ItemCreator.SetComboBoxItems(magazyny, ref ComboBoxMagazyn);
            ItemCreator.SetComboBoxItems(rodzaje, ref ComboBoxRodzajFaktura);
            ItemCreator.SetComboBoxItems(waluty, ref ComboBoxWaluta);
            ItemCreator.SetComboBoxItems(platnosci, ref ComboBoxPlatnosc);
            ItemCreator.SetComboBoxItems(terminy, ref ComboBoxTermin);
            ItemCreator.SetComboBoxItems(statusy, ref ComboBoxStatus);
            ItemCreator.SetComboBoxItems(kategoria, ref ComboBoxKategoria);
            ItemCreator.SetComboBoxItems(grupaKontrahentow, ref ComboBoxGrupaKontrahent);
            ItemCreator.SetComboBoxItems(flagiWlasne, ref ComboBoxFlagaWlasna);
            ItemCreator.SetComboBoxItems(cechaKontrahentow, ref ComboBoxCechaKontrahent);
        }
        //zmienne wyjściowe

        public string name { get; set; }
        public string description { get; set; }

        public bool zadanieAktywne { get; set; }
        public bool przypadek { get; set; }
        public bool zapiszWydruki { get; set; }
        public bool dateToChecked { get; set; }
        public bool wyrownajDoDaty { get; set; }

        public int selectedIndexOfMagazyn { get; set; }
        public int selectedIndexOfRodzaj { get; set; }

        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

        public int numberOfTime { get; set; }
        public int timeFormat { get; set; }


        public string filePath { get; set; }
        private string tempFilePath { get; set; }

        public int selectedIndexOfRodzajFaktury { get; set; }
        public int selectedIndexOfWaluta { get; set; }
        public int selectedIndexOfPlatnosc { get; set; }
        public int selectedIndexOfTermin { get; set; }
        public int selectedIndexOfStatus { get; set; }
        public int selectedIndexOfKategoria { get; set; }

        public int selectedIndexOfGrupaKontrhent { get; set; }
        public int selectedIndexOfCechaKontrahent {  get; set; }
        public int selectedIndexOfFlaga {  get; set; }
        public string descriptonContains {  get; set; }

        public bool succes { get; set; } = false;

        //zamyka okno
        private void ButtonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            succes = false;
            Close();
        }

        //przekazuje dane do okna głównego
        private void ButtonZapisz_Click(object sender, RoutedEventArgs e)
        {
            //sprawdza czy wybrana ścieżka istnieje
            succes = true;
            if (CheckBoxZapiszWydruki.IsChecked == true)
            {
                if (!File.Exists(tempFilePath))
                {
                    succes = false;
                }
                else
                {
                    filePath = tempFilePath;
                }
            }
            if (succes)
            {
                name = TextBoxNazwa.Text;
                description = TextBoxOpis.Text;
                dateFrom = DatePickerOd.DisplayDate;
                if (CheckBoxDate.IsChecked == true)
                {
                    dateTo = DatePickerDo.DisplayDate;
                }
                numberOfTime = int.Parse(NumBoxOkres.Text);
                timeFormat = ComboBoxOkres.SelectedIndex;
                dateToChecked = BoolNullToBool.Convert(CheckBoxDate.IsChecked);
                zadanieAktywne = BoolNullToBool.Convert(CheckBoxZadanieAktywne.IsChecked);
                zapiszWydruki = BoolNullToBool.Convert(CheckBoxZapiszWydruki.IsChecked);
                wyrownajDoDaty = BoolNullToBool.Convert(CheckBoxDateMonth.IsChecked);
                selectedIndexOfMagazyn = ComboBoxMagazyn.SelectedIndex;
                selectedIndexOfRodzajFaktury = ComboBoxRodzajFaktura.SelectedIndex;
                selectedIndexOfWaluta = ComboBoxWaluta.SelectedIndex;
                selectedIndexOfPlatnosc = ComboBoxPlatnosc.SelectedIndex;
                selectedIndexOfTermin = ComboBoxTermin.SelectedIndex;
                selectedIndexOfStatus = ComboBoxStatus.SelectedIndex;
                selectedIndexOfKategoria = ComboBoxKategoria.SelectedIndex;
                selectedIndexOfGrupaKontrhent = ComboBoxGrupaKontrahent.SelectedIndex;
                selectedIndexOfCechaKontrahent = ComboBoxCechaKontrahent.SelectedIndex;
                selectedIndexOfFlaga = ComboBoxFlagaWlasna.SelectedIndex;
                descriptonContains = TextBoxOpisZawiera.Text;
                Close();
            }
            else
            {
                MessageBox.Show("Podana ścieżka nie istnieje", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //włącza/wyłącza DataPickera zależnie od CheckBoxa
        private void CheckBoxDate_Click(object sender, RoutedEventArgs e)
        {
            DatePickerDo.IsEnabled = BoolNullToBool.Convert(CheckBoxDate.IsChecked);
        }

        //włącza/wyłacza FileDialog Button i TextBox zależnie od CheckBoxa
        private void CheckBoxZapiszWydruki_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxZapiszWydruki.IsChecked == true)
            {
                TextBoxFile.IsEnabled = true;
                ButtonFile.IsEnabled = true;
            }
            else
            {
                TextBoxFile.IsEnabled = false;
                ButtonFile.IsEnabled = false;
            }
        }

        //kliknięcie guzika otwiera eksplorator plików i wpisuje ścieżke do textboxa
        private void ButtonSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? succes = fileDialog.ShowDialog();
            if (succes == true)
            {
                tempFilePath = fileDialog.FileName;
                TextBoxFile.Text = tempFilePath;
            }

        }
        //updatowanie zmeinnej zgodnie z textboxem
        private void TextBoxFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            tempFilePath = TextBoxFile.Text;
        }
    }
}
