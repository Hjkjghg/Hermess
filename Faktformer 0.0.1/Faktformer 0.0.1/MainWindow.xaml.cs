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
                //wbij dane
            }
            else
            {
                OpenWindow("ProfilSelector");
            }
        }

        //przechowuje króra karegoria jest wybrana
        private string selectdTable;

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
                switch (selectdTable)
                {
                    case "Zadania":
                        CreateMainTable(GetFromDB1());
                        break;
                    case "Historia":
                        CreateMainTable(GetFromDB2());
                        break;
                    case "Log":
                        CreateMainTable(GetFromDB3());
                        break;
                }
                MainCheckBoxUncheck();
            }
        }

        //wprowadza wizualną zawartość tabeli po jej załadowaniu
        private void TableMain_Loaded(object sender, RoutedEventArgs e)
        {
            selectdTable = "Zadania";
            selectFromCorrectDBToTable();
        }

        //jeśli checkbox główny został zaznaczony bądź odznaczony, adekwatnie zareagój resztą checkboxów
        private void CheckboxCheckedAll(object sender, RoutedEventArgs e)
        {
            //sprawdza stan zaznaczenia głównego checkboxa i ustawia ten stan zaznaczenia dla wszystki checkboxów
            for(int i = 0; i < totalTableLength; i++)
            {
                string checkBoxName = $"tableCheckBox{i}";
                CheckBox checkBoxByName = (CheckBox)TabPanel.FindName(checkBoxName);
                checkBoxByName.IsChecked = MainCheckBox.IsChecked;
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
                CheckBox checkBoxTest = (CheckBox)TabPanel.FindName($"tableCheckBox{i}");
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
        private void CreateMainTable(Tables dataFromDB)
        {
            TabPanel.Children.Clear();
            totalTableLength = dataFromDB.ReturnLenght(0);

            for (int j = 0; j < dataFromDB.ReturnLenght(0); j++)
            {
                List<string> temp1 = ListCaster.CastString(dataFromDB.ReturnRow(j));
                CreateNewGridAsTableItem(j, temp1);
            }
            DisplayTableNumS();
            //P.S. To jest głupie ale włówczas nie mogłem wymyśleć lepszego rozwiązania
        }

        //wyświetla numer rekordów i numer zaznaczonych rekordów
        //przyjmuje int, który jest liczbą zaznaczonych rekordów
        //jeśli liczb zaznaczonych rekordów to zero, bądź nie jest podana, to wyświetla */ilość_rekordów
        private void DisplayTableNumS(int recordnum = 0)
        {
            TableNumerer.Text = (recordnum != 0) ? recordnum.ToString() + "/" + totalTableLength.ToString() : TableNumerer.Text = "*/" + totalTableLength.ToString();
        }

        //odzaznacza głowny checkbox
        //jest urzywana w przypadku odświerzenia tabeli, bądź gdy jeden pub więcej podrzędnych checkboxów zostanie odznaczonych
        private void MainCheckBoxUncheck()
        {
            MainCheckBox.IsChecked = false;            
        }

        //tworzy nową siatkę, wypełnia ją danymi i wrzuca do stack panelu
        //przyjmuje int, który jest Lp. i id kolumny, oraz string[] który przyjmuje dane do wprowadzenia
        private void CreateNewGridAsTableItem(int elementId, List<string> dataFromDB)
        {
            string NameNumber = elementId.ToString();

            //tworzenie bordera tabeli
            Border myNewBorder = CreateNewBorderWithTemplate($"tableBorder{NameNumber}", ExampleBorder.Height, ExampleBorder.Width, ExampleBorder.VerticalAlignment, ExampleBorder.HorizontalAlignment, ExampleBorder.BorderThickness, ExampleBorder.BorderBrush);

            //tworzenie siatki
            Grid myNewGrid = CreateNewGridWithTemplate($"tableElement{NameNumber}", ExampleTable.Height, ExampleTable.Width, ExampleTable.VerticalAlignment, ExampleTable.HorizontalAlignment, ExampleTable.ShowGridLines);

            //utworzenie kolumn siatki oraz zdefiniowanie ich długośći i ilości
            for (int i = 0; i < ExampleTable.ColumnDefinitions.Count; i++)
            {
                myNewGrid.ColumnDefinitions.Add(CreateNewColumnDefinitionOfWidth(ExampleTable.ColumnDefinitions[i].Width));
            }

            //utworzenie rędu siatki
            myNewGrid.RowDefinitions.Add(CreateNewRowDefinitionOfHeigth(ExampleTable.RowDefinitions[0].Height));

            //tworzenie checkboxa i ustawienie jego parametrów
            CheckBox myNewCheckBox = CreateNewCheckBoxWithTemplate($"tableCheckBox{NameNumber}", MainCheckBox.Height, MainCheckBox.Width, MainCheckBox.VerticalAlignment, MainCheckBox.HorizontalAlignment, MainCheckBox.Margin, new RoutedEventHandler(CheckboxCheckedUncheckedE));
            
            Grid.SetColumn(myNewCheckBox, 0);

            //tworzenie i rsetowanie nazwy checkboxa dla zautomatyzowanwgo kodu
            if ((CheckBox)TabPanel.FindName($"tableCheckBox{elementId}") != null) {
                UnregisterName($"tableCheckBox{elementId}");
            }
            RegisterName(myNewCheckBox.Name, myNewCheckBox);

            //alokacja checkboxa do siatki
            myNewGrid.Children.Add(myNewCheckBox);

            //tworzenie textblocków do wyświetlania danych i ustawienie ich parametrów
            for (int i = 1; i <= dataFromDB.Count; i++) {
                TextBlock myNewTextBlock = CreateNewTextBlockWithTemplateNC(dataFromDB[i - 1], ExampleTextBlock.Margin, ExampleTextBlock.Height, ExampleTextBlock.VerticalAlignment, ExampleTextBlock.Foreground);
                Grid.SetColumn(myNewTextBlock, i);

                //oraz alokacja ich do siatki
                myNewGrid.Children.Add(myNewTextBlock);
            }

            //alokacja siatki do bordera
            myNewBorder.Child = myNewGrid;

            //wstwienie bordera z siatką do stackpanela
            TabPanel.Children.Add(myNewBorder);
        }

        //tworzy i zwraca siatkę według podanych parametrów
        private Grid CreateNewGridWithTemplate(string name, double height, double width, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, bool showGridLines)
        {
            Grid newGrid = new Grid();
            newGrid.Height = height;
            newGrid.Width = width;
            newGrid.HorizontalAlignment = horizontalAlignment;
            newGrid.VerticalAlignment = verticalAlignment;
            newGrid.ShowGridLines = showGridLines;
            newGrid.Name = name;
            return newGrid;
        }

        //tworzy i zwraca kolumne według podanych parametrów
        private ColumnDefinition CreateNewColumnDefinitionOfWidth(GridLength width)
        {
            ColumnDefinition newColumnDefinition = new ColumnDefinition();
            newColumnDefinition.Width = width;
            return newColumnDefinition;
        }

        //tworzy i zwraca rząd według podanych parametrów
        private RowDefinition CreateNewRowDefinitionOfHeigth(GridLength heigth)
        {
            RowDefinition newRowDefinition = new RowDefinition();
            newRowDefinition.Height = heigth;
            return newRowDefinition;
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

        //tworzy i zwraca checkboxa według podanych parametrów
        private CheckBox CreateNewCheckBoxWithTemplate(string name, double height, double width, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment, Thickness margin, RoutedEventHandler routedEventHandler)
        {
            CheckBox newCheckBox = new CheckBox();
            newCheckBox.Name = name;
            newCheckBox.Height = height;
            newCheckBox.Width = width;
            newCheckBox.Margin = margin;
            newCheckBox.VerticalAlignment = verticalAlignment;
            newCheckBox.HorizontalAlignment = horizontalAlignment;
            newCheckBox.Click += routedEventHandler;
            return newCheckBox;
        }

        //tworz i zwraca checkboxa według podanych parametrów
        private TextBlock CreateNewTextBlockWithTemplateNC(string text, Thickness margin, double height, VerticalAlignment verticalAlignment, Brush foreground)
        {
            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Margin = margin;
            newTextBlock.Height = height;
            newTextBlock.VerticalAlignment = verticalAlignment;
            newTextBlock.Text = text;
            newTextBlock.Foreground = foreground;
            return newTextBlock;
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
            selectdTable = "Zadania";
            selectFromCorrectDBToTable();
        }
        //przejście do tab2
        private void ButtonTabHistoria_Click(object sender, RoutedEventArgs e)
        {
            selectdTable = "Historia";
            selectFromCorrectDBToTable();
        }
        //przejście do tab3
        private void ButtonTabLog_Click(object sender, RoutedEventArgs e)
        {
            selectdTable = "Log";
            selectFromCorrectDBToTable();
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