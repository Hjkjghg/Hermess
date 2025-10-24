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
    /// Logika interakcji dla klasy ParametryPracy1.xaml
    /// </summary>
    public partial class ParametryPracy1 : Window
    {
        //konstruktor pobiera dane do wyświetlenia przez ComboBoxy
        public ParametryPracy1(List<string> przeliczajWedlug)
        {
            InitializeComponent();
            ItemCreator.SetComboBoxItems(przeliczajWedlug, ref ComboBoxCena);
        }

        //zmienne wyjśćiowe
        public bool succes { get; set; } = false;
        public bool[] boolData { get; set; } = new bool[2];
        public int rodzajCeny { get; set; }
        
        //guzik anuluj
        //zamyka okno
        private void ButtonAnuluj_Click(object sender, RoutedEventArgs e)
        {
            succes = false;
            Close();
        }

        //guzik zapisz
        //wczytuje dane z okna do danych wyjśćiowych i zamyka okno
        private void ButtonZapisz_Click(object sender, RoutedEventArgs e)
        {
            succes = true;
            boolData[0] = BoolNullToBool.Convert(CheckBoxProces.IsChecked);
            boolData[1] = BoolNullToBool.Convert(CheckBoxZalogowanie.IsChecked);
            rodzajCeny = ComboBoxCena.SelectedIndex;
            Close();
        }
    }
}
