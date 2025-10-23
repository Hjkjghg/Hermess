using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Logika interakcji dla klasy PowielaczDokumentow.xaml
    /// </summary>
    public partial class PowielaczDokumentow : Window
    {
        public PowielaczDokumentow(List<string> magazyny, List<string> rodzaje)
        {
            InitializeComponent();
            ItemCreator.SetComboBoxItems(magazyny, ref ComboBoxMagazyn);
            ItemCreator.SetComboBoxItems(rodzaje, ref ComboBoxRodzaj);
        }

        public bool succes { get; set; } = false;
        
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

        private void ButtonZapisz_Click(object sender, RoutedEventArgs e)
        {
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
                selectedIndexOfMagazyn = ComboBoxMagazyn.SelectedIndex;
                selectedIndexOfRodzaj = ComboBoxRodzaj.SelectedIndex;
                dateToChecked = BoolNullToBool.Convert(CheckBoxDate.IsChecked);
                zadanieAktywne = BoolNullToBool.Convert(CheckBoxZadanieAktywne.IsChecked);
                przypadek = BoolNullToBool.Convert(CheckBoxPrzypadek.IsChecked);
                zapiszWydruki = BoolNullToBool.Convert(CheckBoxZapiszWydruki.IsChecked);
                wyrownajDoDaty = BoolNullToBool.Convert(CheckBoxDateMonth.IsChecked);
                Close();
            }
        }

        private void ButtonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            succes = false;
            Close();
        }

        //włącza i wyłącza interakcje z datepickerem zależnie od checkboxa
        private void CheckBoxDate_Click(object sender, RoutedEventArgs e)
        {
            DatePickerDo.IsEnabled = BoolNullToBool.Convert(CheckBoxDate.IsChecked);
        }

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

        //kliknięcie guzika otwiera eksplorator plików i wpisuje ścieżke do textboxa (1)
        private void ButtonSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? succes = fileDialog.ShowDialog();
            if (succes == true)
            {
                filePath = fileDialog.FileName;
                TextBoxFile.Text = filePath;
            }

        }
        //updatowanie zmeinnej zgodnie z textboxem (1)
        private void TextBoxFile_TextChanged(object sender, TextChangedEventArgs e)
        {
            filePath = TextBoxFile.Text;
        }

        private void ButtonParametry_Click(object sender, RoutedEventArgs e)
        {
            FrontujSiatkeOdKategorii("Parametry");
        }

        private void ButtonDokumenty_Click(object sender, RoutedEventArgs e)
        {
            FrontujSiatkeOdKategorii("Dokumenty");
        }

        private void FrontujSiatkeOdKategorii(string gridName)
        {
            switch (gridName)
            {
                case "Parametry":
                    EnableDisabelGrid(ref Parametry, true);
                    EnableDisabelGrid(ref Dokumenty, false);
                    break;
                case "Dokumenty":
                    EnableDisabelGrid(ref Dokumenty, true);
                    EnableDisabelGrid(ref Parametry, false);
                    break;
            }
        }

        private void EnableDisabelGrid(ref Grid siatka, bool enabled)
        {
            siatka.Visibility = enabled ? Visibility.Visible : Visibility.Hidden;
            siatka.IsEnabled = enabled;
            Panel.SetZIndex(siatka, enabled ? 1 : -1);
        }
    }
}
