using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;


namespace Faktformer_0._0._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //po załadowaniu aplikacji sprawdza czy wcześniej zapisane dane profilu podczas ostatniej operacji aplikacji są prawdzie
            //jeśli tak nie jest to otwiera okno logowania
            if (CzyZalogowanoJuz())
            {
                logedIn = true;
                LoadAllTables();
                SwichTabs();
                //wbij dane
            }
            else
            {
                OpenWindow("ProfilSelector");
            }
        }

        //przechowuje króra karegoria jest wybrana
        private string selectedTable = "Zadania";
        private int selectedTableIndex;

        //zenętrzna zmienna przechoująca ilość rekordów w aktualnie wyświetlanej tabeli, dla urzytku w innych funkcjach
        private int totalTableLength;

        //narazie w ten sposób, by działało
        //!!! przy uzupełnieniu funkcjonalności profili zmień na false, jak i dostosuj odpowiednią funkcje poniżej
        private bool logedIn = true;

        //guzik odświeżający
        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            selectFromCorrectDBToTable();            
        }

        //wybiera konkretne zapytanie do pobrania danych;
        private void selectFromCorrectDBToTable()
        {
            if (logedIn)
            {
                switch (selectedTable)
                {
                    case "Zadania":
                        CreateMainTable(GetFromDB1(), 1);
                        break;
                    case "Historia":
                        CreateMainTable(GetFromDB2(), 2);
                        break;
                    case "Log":
                        CreateMainTable(GetFromDB3(), 3);
                        break;
                }
                MainCheckBoxUncheck();
            }
        }
        private void LoadAllTables()
        {
            if (logedIn)
            {
                CreateMainTable(GetFromDB1(), 1);
                CreateMainTable(GetFromDB2(), 2);
                CreateMainTable(GetFromDB3(), 3);
                selectedTableIndex = 1;
            }
        }

        private void SwichTabs(string tab = "Zadania")
        {
            if (logedIn)
            {
                switch (selectedTable)
                {
                    case "Zadania":
                        GridSettings.EnableDisabelGrid(ref Zadania, true);
                        GridSettings.EnableDisabelGrid(ref Historia, false);
                        GridSettings.EnableDisabelGrid(ref Log, false);
                        selectedTableIndex = 1;
                        selectedTable = "Zadania";
                        break;
                    case "Historia":
                        GridSettings.EnableDisabelGrid(ref Zadania, false);
                        GridSettings.EnableDisabelGrid(ref Historia, true);
                        GridSettings.EnableDisabelGrid(ref Log, false);
                        selectedTableIndex = 2;
                        selectedTable = "Historia";
                        break;
                    case "Log":
                        GridSettings.EnableDisabelGrid(ref Zadania, false);
                        GridSettings.EnableDisabelGrid(ref Historia, false);
                        GridSettings.EnableDisabelGrid(ref Log, true);
                        selectedTableIndex = 3;
                        selectedTable = "Log";
                        break;
                }
            }
        }


        //wprowadza wizualną zawartość tabeli po jej załadowaniu
        private void TableMain_Loaded(object sender, RoutedEventArgs e)
        {
            //selectFromCorrectDBToTable();
        }

        //jeśli checkbox główny został zaznaczony bądź odznaczony, adekwatnie zareagój resztą checkboxów
        private void CheckboxCheckedAll(object sender, RoutedEventArgs e)
        {
            //sprawdza stan zaznaczenia głównego checkboxa i ustawia ten stan zaznaczenia dla wszystki checkboxów
            for(int i = 0; i < totalTableLength; i++)
            {
                string checkBoxName = $"table{selectedTableIndex}CheckBox{i}";
                CheckBox checkBoxByName = (CheckBox)this.FindName(checkBoxName);
                checkBoxByName.IsChecked = ((CheckBox)this.FindName($"MainCheckBox{selectedTableIndex.ToString()}")).IsChecked;
            }
            WhatChecked();
        }

        //wprowdza wizualne zmiany po kliknięciu checboxa
        private void CheckboxCheckedUncheckedE(object sender, RoutedEventArgs e)
        {
            MainCheckBoxUncheck();
            WhatChecked();
        }

        //sprawdza które checkboxy w tabeli zostały zaznaczone, updatuje ilość znaczonych rekordów na numeratorze
        //zwraca array tupu bool[], przechowujący które rekordy zostały zaznaczone
        //UWAGA! jeśli tabla nie istnieje, bądź nie ma elemantów, zawsze zwróci array wypełniony false
        private List<bool> WhatChecked() 
        {
            List<bool> selectedCheckBoxes = new List<bool>();
            int toDisplay = 0;
            for (int i = 0; i < totalTableLength; i++)
            {
                CheckBox checkBoxTest = (CheckBox)(this.FindName($"table{selectedTableIndex.ToString()}CheckBox{i}"));
                if(checkBoxTest.IsChecked == true)
                {
                    selectedCheckBoxes.Add(true);
                    toDisplay++;
                }
                else
                {
                    selectedCheckBoxes.Add(false);
                }                
            }
            DisplayTableNumS(toDisplay);
            return selectedCheckBoxes;
        }
        
        //separuje 2-wymiarowy array na wiele 1-wymiarowych i następnie wywołuje funkcje tworzącą rząd dla każdego z nich
        //przyjmuje dane typu string[,]
        private void CreateMainTable(Tables dataFromDB, int selectedTableInt)
        {
            ((StackPanel)this.FindName($"TabPanel{selectedTableInt.ToString()}")).Children.Clear();
            totalTableLength = dataFromDB.ReturnLenght(0);
            selectedTableIndex = selectedTableInt;

            for (int j = 0; j < dataFromDB.ReturnLenght(0); j++)
            {
                List<string> temp1 = ListCaster.CastString(dataFromDB.ReturnRow(j));
                CreateNewGridAsTableItem(j, temp1, selectedTableInt);
            }
            DisplayTableNumS();
            //P.S. To jest głupie ale włówczas nie mogłem wymyśleć lepszego rozwiązania
        }

        //wyświetla numer rekordów i numer zaznaczonych rekordów
        //przyjmuje int, który jest liczbą zaznaczonych rekordów
        //jeśli liczb zaznaczonych rekordów to zero, bądź nie jest podana, to wyświetla */ilość_rekordów
        private void DisplayTableNumS(int recordnum = 0)
        {
            ((TextBlock)this.FindName($"TableNumerer{selectedTableIndex.ToString()}")).Text = (recordnum != 0) ? recordnum.ToString() + "/" + totalTableLength.ToString() : "*/" + totalTableLength.ToString();
        }

        //odzaznacza głowny checkbox
        //jest urzywana w przypadku odświerzenia tabeli, bądź gdy jeden pub więcej podrzędnych checkboxów zostanie odznaczonych
        private void MainCheckBoxUncheck()
        {

            ((CheckBox)this.FindName($"MainCheckBox{selectedTableIndex.ToString()}")).IsChecked = false;            
        }

        //tworzy nową siatkę, wypełnia ją danymi i wrzuca do stack panelu
        //przyjmuje int, który jest Lp. i id kolumny, oraz string[] który przyjmuje dane do wprowadzenia
        private void CreateNewGridAsTableItem(int elementId, List<string> dataFromDB, int selectedTable)
        {
            string NameNumber = elementId.ToString();
            string sTNumber = selectedTable.ToString();
            Grid exampleTable = (Grid)this.FindName($"ExampleTable{sTNumber}");
            Border exampleBorder = (Border)this.FindName($"ExampleBorder{sTNumber}");
            CheckBox mainCheckBox = (CheckBox)this.FindName($"MainCheckBox{sTNumber}");
            TextBlock exampleTextBlock = (TextBlock)this.FindName($"ExampleTextBlock{sTNumber}");
            StackPanel tabPanel = (StackPanel)this.FindName($"TabPanel{sTNumber}");

            GridCreator gridCreator = new GridCreator(this);
            gridCreator.SetGridHeight(exampleTable.Height);
            gridCreator.SetGridWidth(exampleTable.Width);
            gridCreator.ToggleGridsGridLines();
            gridCreator.AddGridRow();
            for(int i = 0; i < exampleTable.ColumnDefinitions.Count; i++)
            {
                gridCreator.AddGridColumn(exampleTable.ColumnDefinitions[i].Width);
            }
            gridCreator.AddCheckBoxToGrid($"table{sTNumber}CheckBox{NameNumber}", "", mainCheckBox.HorizontalAlignment, mainCheckBox.VerticalAlignment, 0, 0, mainCheckBox.Margin);
            gridCreator.AddEventListenerToElementByName(new RoutedEventHandler(CheckboxCheckedUncheckedE), $"table{selectedTable}CheckBox{NameNumber}", GridCreator.eventType.Click);
            for (int i = 0; i < exampleTable.ColumnDefinitions.Count - 1; i++)
            {
                gridCreator.AddTextBlockToGrid($"table{sTNumber}Textbox{NameNumber}U{i.ToString()}", dataFromDB[i], exampleTextBlock.HorizontalAlignment, exampleTextBlock.VerticalAlignment, 0, i + 1, exampleTextBlock.Margin);
            }

            //tworzenie bordera tabeli
            Border myNewBorder = CreateNewBorderWithTemplate($"table{sTNumber}Border{NameNumber}", exampleBorder.Height, exampleBorder.Width, exampleBorder.VerticalAlignment, exampleBorder.HorizontalAlignment, exampleBorder.BorderThickness, exampleBorder.BorderBrush);

            //alokacja siatki do bordera
            gridCreator.AppendGridTo(ref myNewBorder);

            //wstwienie bordera z siatką do stackpanela
            tabPanel.Children.Add(myNewBorder);
        } 

        //tworzy i zwraca obramowanie według podanych parametrów
        private Border CreateNewBorderWithTemplate(string name, double height, double width, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Thickness borderThicknes, Brush borderBrush)
        {
            Border newBorder = new Border();
            newBorder.BorderThickness = borderThicknes;
            newBorder.BorderBrush = borderBrush;
            newBorder.Name = name;
            newBorder.Height = height;
            newBorder.Width = width;
            newBorder.HorizontalAlignment = horizontalAlignment;
            newBorder.VerticalAlignment = verticalAlignment;
            return newBorder;
        }


        //*** autentykacja podanych SQL jak i pobranie danych ma nastąpić tutaj
        //tu wsadź funkcje pobierającą dane z tabeli
        private Tables GetFromDB1()
        {
            string[,] fromDB = { {"a", "b", "c", "d", "e", "f" }, { "a", "b", "c", "d", "e", "f" } };
            Tables dataDB = new Tables();
            dataDB.SetContentTo(fromDB);
            return dataDB;
        }
        //tu wsadź funkcje pobierającą dane z tabeli
        private Tables GetFromDB2()
        {
            string[,] fromDB = { { "g", "h", "i", "j", "k", "l" }, { "m", "n", "o", "p", "s", "t" } };
            Tables dataDB = new Tables();
            dataDB.SetContentTo(fromDB);
            return dataDB;
        }
        //tu wsadź funkcje pobierającą dane z tabeli
        private Tables GetFromDB3()
        {
            string[,] fromDB = { { "1", "2", "3", "4", "5", "6" }, { "7", "8", "9", "10", "11", "12" } };
            Tables dataDB = new Tables();
            dataDB.SetContentTo(fromDB);
            return dataDB;
        }
        
        
        //prejście do tab1
        private void ButtonTabZadania_Click(object sender, RoutedEventArgs e)
        {
            selectedTable = "Zadania";
            SwichTabs("Zadania");
        }
        //przejście do tab2
        private void ButtonTabHistoria_Click(object sender, RoutedEventArgs e)
        {
            selectedTable = "Historia";
            SwichTabs("Historia");
        }
        //przejście do tab3
        private void ButtonTabLog_Click(object sender, RoutedEventArgs e)
        {
            selectedTable = "Log";
            SwichTabs("Log");
        }
        //Uruchomienie okna dialogowego tworzenia nowego zadania
        private void ButtonNoweZadanie_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow("ZwykleZadanie");
        }


        //Uruchamienie okien dialogowych zależnie od im przypisanej im nazwy
        public void OpenWindow(string windowName)
        {
            switch(windowName)
            {
                case "Harmonogram":
                    Harmonogram1 harmonogram1 = new Harmonogram1();
                    harmonogram1.ShowDialog();
                    if (harmonogram1.succes)
                    {
                        //przyjęcie danych
                    }
                    
                    break;
                case "ZwykleZadanie":
                    ZwykleZadanie1 zwykleZadanie1 = new ZwykleZadanie1();
                    zwykleZadanie1.ShowDialog();
                    if (zwykleZadanie1.succes)
                    {
                        //przyjęcie danych
                    }
                    break;
                case "BazaDanych":
                    BazaDanych1 bazaDanych1 = new BazaDanych1();
                    bazaDanych1.ShowDialog();
                    if (bazaDanych1.succes)
                    {
                        //przyjęcie danych
                    }
                    break;
                case "Email":
                    Email1 email1 = new Email1();
                    email1.ShowDialog();
                    if (email1.succes)
                    {
                        //przyjęcie danych
                    }
                    break;
                case "ParametryPracy":
                    ParametryPracy1 parametryPracy1 = new ParametryPracy1();
                    parametryPracy1.ShowDialog();
                    if (parametryPracy1.succes)
                    {
                        //przyjęcie danych
                    }
                    break;
                case "PowielaczDokumentow":
                    PowielaczDokumentow powielaczDokumentow = new PowielaczDokumentow();
                    powielaczDokumentow.ShowDialog();
                    if (powielaczDokumentow.succes)
                    {
                        //przyjęcie danych
                    }
                    break;
                case "ReailzatorZamowien":
                    RealizatorZamowien realizatorZamowien = new RealizatorZamowien();
                    realizatorZamowien.ShowDialog();
                    if (realizatorZamowien.succes)
                    {
                        //przyjęcie danych
                    }
                    break;
                case "ProfilSelector":
                    mWindow.Visibility = Visibility.Hidden;
                    mWindow.ShowInTaskbar = false;
                    ProfilSelector1 profilSelector1 = new ProfilSelector1();
                    profilSelector1.ShowDialog();
                    break;

            }
        }


        //niegotowe, ale nie mam pojęcia jak ma się integrować z subiektem GT
        //funkcja wywoływana z okna logowania, próbuje się połączyć z subiektem GT(lub naszym serwerem)
        public int TryLogin()
        {
            //dodaj argumenty z okna logowania            
                
            if (ZalogujTeraz() == 0)
            {
                logedIn = true;
                //wbij dane
                LoadAllTables();
                SwichTabs();
                mWindow.Visibility = Visibility.Visible;
                mWindow.ShowInTaskbar = true;
                return 0;
            }
            else
            {
                return 1;
            }                            
        }

        public void LogOut()
        {
            logedIn = false;
            //wyczyść dane
            OpenWindow("ProfilSelector");
        }

        //sprawdza czy profil jest już zalogowany(nie wylogowano po zamknięciu programu)
        //dalej uwierzytelnij dane
        //niegotowe, ale nie mam pojęcia jak ma się integrować z subiektem GT
        private bool CzyZalogowanoJuz()
        {
            //pryjmuje zapisane argumenty
            return false;
        }
        //tu wsadź sprawdzenie zgodności wprowadzonych danych profilu
        private int ZalogujTeraz()
        {
            //przyjmuje argumenty od urzytkownika
            return 0;
        }
        //pobiara dane do wyświetlenia w formuarzu logowania
        public bool PobierzDaneZ(out List <string> profile, out string podmiot, out List <string> uzytkownicy)
        {
            profile = new List<string>();
            uzytkownicy = new List<string>();
            profile.Add("Domyślny");
            uzytkownicy.Add("");
            podmiot = "Podmiot";
            return true;
        }

        private void ButtonPowielDokumenty_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow("PowielaczDokumentow");
        }

        private void ButtonRealizojZamowienie_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow("ReailzatorZamowien");
        }
    }
}