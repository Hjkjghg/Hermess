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
    /// Logika interakcji dla klasy ZwykleZadanie1.xaml
    /// </summary>
    public partial class ZwykleZadanie1 : Window
    {
        public ZwykleZadanie1()
        {
            InitializeComponent();
        }

        //zmienne wyjściowe
        private string filePath1 = "";
        private string filePath2 = "";

        public string filePathData1 { get; set; }
        public string filePathData2 { get; set; }


        public bool succes { get; set; } = false;


        public bool zadanieAktywne { get; set; }
        public bool powiel {  get; set; }
        public bool aktualizuj { get; set; }
        public bool przypadek {  get; set; }
        public bool zapiszWydruki { get; set; }
        public bool dateToChecked { get; set; }


        public string name { get; set; } = "";
        public string description { get; set; } = "";


        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

        public int numberOfTime { get; set; }
        public int timeFormat { get; set; }


        public int selectedIndexOfMagazyn {  get; set; }
        public int selectedIndexOfRodzajFaktury { get; set; }
        public int selectedIndexOfWaluta {  get; set; }
        public int selectedIndexOfPlatnosc {  get; set; }
        public int selectedIndexOfTermin {  get; set; }
        public int selectedIndexOfStatus { get; set; }
        public int selectedIndexOfKategoria { get; set; }


        public Tables KontData { get; set; }

        //obiekty tablicowe zewnętrzne do pracy w innych funkcjach
        private Tables TableSelectedDataKon = new Tables();
        private Tables TableTempDataKon = new Tables();

        //kliknięcie guzika otwiera eksplorator plików i wpisuje ścieżke do textboxa (1)
        private void ButtonSelectFile1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? succes = fileDialog.ShowDialog();
            if (succes == true)
            {
                filePath1 = fileDialog.FileName;
                TextBoxFile1.Text = filePath1;
            }
            
        }
        //updatowanie zmeinnej zgodnie z textboxem (1)
        private void TextBoxFile1_TextChanged(object sender, TextChangedEventArgs e)
        {
            filePath1 = TextBoxFile1.Text;
        }

        //kliknięcie guzika otwiera eksplorator plików i wpisuje ścieżke do textboxa (2)
        private void ButtonSelectFile2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? succes = fileDialog.ShowDialog();
            if (succes == true)
            {
                filePath2 = fileDialog.FileName;
                TextBoxFile2.Text = filePath2;
            }

        }

        //updatowanie zmeinnej zgodnie z textboxem (2)
        private void TextBoxFile2_TextChanged(object sender, TextChangedEventArgs e)
        {
            filePath2 = TextBoxFile2.Text;
        }

        //guzik zapisz
        //wczytuje dane z okna do danych wyjśćiowych i zamyka okno
        private void ButtonZapisz_Click(object sender, RoutedEventArgs e)
        {
            succes = true;
            //sprawdza czy wybrane pliki istnieją
            if (CheckBoxPowiel.IsChecked == true)
            {
                if (!File.Exists(filePath1)) 
                {
                    succes = false;
                }
                else
                {
                    filePathData1 = filePath1;
                }
            }
            if (CheckBoxZapiszWydruki.IsChecked == true)
            {
                if (!File.Exists(filePath2))
                {
                    succes = false;
                }
                else
                {
                    filePathData2 = filePath2;
                }
            }
            //ustawienie zamiennych wjściowych
            if (succes)
            {
                zadanieAktywne = BoolNullToBool.Convert(CheckBoxZadnieAktywne.IsChecked);
                powiel = BoolNullToBool.Convert(CheckBoxPowiel.IsChecked);
                aktualizuj = BoolNullToBool.Convert(CheckBoxAktualizuj.IsChecked);
                przypadek = BoolNullToBool.Convert(CheckBoxPrzypadek.IsChecked);
                zapiszWydruki = BoolNullToBool.Convert(CheckBoxZapiszWydruki.IsChecked);
                dateToChecked = BoolNullToBool.Convert(CheckBoxDateMonth.IsChecked);
                name = TextBoxNazwa.Text;
                dateFrom = DatePickerOd.DisplayDate;
                description = TextBoxOpis1.Text;
                if (CheckBoxDate.IsChecked == true)
                {
                    dateTo = DatePickerDo.DisplayDate;
                }
                numberOfTime = int.Parse(NumBoxOkres.Text);
                timeFormat = ComboBoxOkres.SelectedIndex;
                selectedIndexOfMagazyn = ComboBoxMagazyn.SelectedIndex;
                selectedIndexOfRodzajFaktury = ComboBoxRodzajFaktura.SelectedIndex;
                selectedIndexOfWaluta = ComboBoxWaluta.SelectedIndex;
                selectedIndexOfPlatnosc = ComboBoxPlatnosc.SelectedIndex;
                selectedIndexOfTermin = ComboBoxTermin.SelectedIndex;
                selectedIndexOfStatus = ComboBoxStatus.SelectedIndex;
                selectedIndexOfKategoria = ComboBoxKategoria.SelectedIndex;
                //KontData = new string[SelectedDataKON.Count, SelectedDataKON[0].Count];

                //konwertuje listę na arraya wyjściowego
                KontData.SetContentTo(TableSelectedDataKon.ReturnList());
                Close();
            }
            //w przypadku gdy jeden z plików nie iintnieje, pokaż messagebox błąd
            else
            {
                MessageBox.Show("Jeden lub więcej z podanych ścierzek nie istnieje", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //guzik anuluj
        //zamyka okno
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

        //włącza i wyłącza interakcje z comboboxem zależnie od checkboxa
        private void CheckBoxAktualizuj_Click(object sender, RoutedEventArgs e)
        {                       
            LabelRodzajCeny.IsEnabled = BoolNullToBool.Convert(CheckBoxAktualizuj.IsChecked);
            ComboBoxRodzajCeny.IsEnabled = BoolNullToBool.Convert(CheckBoxAktualizuj.IsChecked);           
        }

        //włącza i wyłącza interakcje z filepickerem zależnie od checkboxa (1)
        private void CheckBoxPowiel_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFile1.IsEnabled = BoolNullToBool.Convert(CheckBoxPowiel.IsChecked);
            ButtonFile1.IsEnabled = BoolNullToBool.Convert(CheckBoxPowiel.IsChecked);
        }

        //włącza i wyłącza interakcje z filepickerem zależnie od checkboxa (2)
        private void CheckBoxZapiszWydruki_Click(object sender, RoutedEventArgs e)
        {
            TextBoxFile2.IsEnabled = BoolNullToBool.Convert(CheckBoxZapiszWydruki.IsChecked);
            ButtonFile2.IsEnabled = BoolNullToBool.Convert(CheckBoxZapiszWydruki.IsChecked);
        }

        //przełącza kategorie na parametry
        private void ButtonParametry_Click(object sender, RoutedEventArgs e)
        {
            FrontujSiatkeOdKategorii("Parametry");
        }

        //przełącza kategorie na Kontrahenci
        private void ButtonKontrahenci_Click(object sender, RoutedEventArgs e)
        {
            FrontujSiatkeOdKategorii("Kontrahenci");
        }

        //przełącza kategorie na PozycjeDokumentu
        private void ButtonPozycjeDokumentu_Click(object sender, RoutedEventArgs e)
        {
            FrontujSiatkeOdKategorii("PozycjeDokumentu");
        }

        //Frontuje siatke zależnie od nazwy
        private void FrontujSiatkeOdKategorii(string gridName)
        {
            switch (gridName)
            {
                case "Parametry":
                    GridSettings.EnableDisabelGrid(ref Parametry, true);
                    GridSettings.EnableDisabelGrid(ref Kontrahenci, false);
                    GridSettings.EnableDisabelGrid(ref PozycjeDokumentu, false);
                    break;
                case "Kontrahenci":
                    GridSettings.EnableDisabelGrid(ref Kontrahenci, true);
                    GridSettings.EnableDisabelGrid(ref Parametry, false);
                    GridSettings.EnableDisabelGrid(ref PozycjeDokumentu, false);
                    CreateTableKON(GetFromDB1());
                    break;
                case "PozycjeDokumentu":
                    GridSettings.EnableDisabelGrid(ref PozycjeDokumentu, true);
                    GridSettings.EnableDisabelGrid(ref Parametry, false);
                    GridSettings.EnableDisabelGrid(ref Kontrahenci, false);
                    break;
            }
        }

        //Tworzy tabele kontrahentów
        private void CreateTableKON(Tables dataFromDB)
        {
            TableTempDataKon.SetContentTo(dataFromDB.table);
            ListViewKontrahenciZZ1.Items.Clear();
            for (int i = 0; i < dataFromDB.ReturnLenght(0); i++)
            {
                List<string> temp1 = ListCaster.CastString(dataFromDB.ReturnRow(i));
                CreateNewGridAsTableItemKON(i, temp1);

            }
            //P.S. To jest to samo rozwiązanie co w głównym oknie
        }

        //tworzy nową siatkę, wypełnia ją danymi i dodaje ją jako element viewlisty
        //przyjmuje int, który jest Lp. i id kolumny, oraz string[] który przyjmuje dane do wprowadzenia
        private void CreateNewGridAsTableItemKON(int elementId,List<string> dataFromDB)
        {
            ListViewItem myNewItem = CreateNewListViewItem($"ListItem{elementId.ToString()}", HorizontalAlignment.Stretch);
            GridCreator gridCreator = new GridCreator(this);
            gridCreator.ToggleGridsGridLines();
            gridCreator.SetGridWidth(KONGridExample.Width);
            gridCreator.AddGridRow();
            for (int i = 0; i < KONGridExample.ColumnDefinitions.Count; i++)
            {
                gridCreator.AddGridColumn(KONGridExample.ColumnDefinitions[i].Width);
                gridCreator.AddTextBlockToGrid($"ListItem{elementId.ToString()}TextBlock{i.ToString()}", dataFromDB[i], ExampleTextBlockKON.HorizontalAlignment, ExampleTextBlockKON.VerticalAlignment, 0, i, ExampleTextBlockKON.Margin);
            }
            myNewItem.Content = gridCreator.GetGrid();
            ListViewKontrahenciZZ1.Items.Add(myNewItem);
        }

        //tworzy i zwraca element listy według parametrów
        private ListViewItem CreateNewListViewItem(string name, HorizontalAlignment horizontalAlignment)
        {
            ListViewItem newItem = new ListViewItem();
            newItem.HorizontalAlignment = horizontalAlignment;
            newItem.Name = name;
            return newItem;
        }

        //tworzy i zwraca siatkę według parametrów
        private Grid CreateNewGrid(bool showGridLines, HorizontalAlignment horizontalAlignment, double width)
        {
            Grid newGrid = new Grid();
            newGrid.ShowGridLines = showGridLines;
            newGrid.HorizontalAlignment = horizontalAlignment;
            newGrid.Width = width;
            return newGrid;
        }

        //tworzy i zwraca kolumne wedłud parametrów
        private ColumnDefinition CreateNewColumnDefinition(GridLength width)
        {
            ColumnDefinition newColumnDefinition = new ColumnDefinition();
            newColumnDefinition.Width = width;
            return newColumnDefinition;
        }

        //tworzy i zwraca labela według parametrów
        private Label CreateNewLabel(string content)
        {
            Label newLabel = new Label();
            newLabel.Content = content;
            return newLabel;
        }

        //tu wsadź funkcje pobierającą dane z tabeli
        private Tables GetFromDB1()
        {
            Tables tableFromDB = new Tables();
            string[,] table = { { "a", "b", "c" }, { "d", "e", "f" } };
            tableFromDB.SetContentTo(table);
            return tableFromDB;
        }

        //Dodaje kontrahenta do listy kńcowej
        private void ButtonKontrehenciZZ1(object sender, RoutedEventArgs e)
        {

            if (ListViewKontrahenciZZ1.SelectedItem != null)
            {
                if (ListViewKontrahenciZZ1.SelectedIndex.GetType() == typeof(int))
                {
                    TableSelectedDataKon.AddRow(TableTempDataKon.ReturnRow(ListViewKontrahenciZZ1.SelectedIndex));
                }
            }

        }
    }
}
