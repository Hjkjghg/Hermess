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
    /// Logika interakcji dla klasy ProfilSelector1.xaml
    /// </summary>
    public partial class ProfilSelector1 : Window
    {
        public ProfilSelector1()
        {
            InitializeComponent();
        }

        private List<string> profile = new List<string>();
        private string podmiot = "";
        private List<string> user = new List<string>();
        private bool progresingAppOnclosing = false;

        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            int b =((MainWindow)System.Windows.Application.Current.MainWindow).TryLogin();
            switch (b) 
            {
                case 0:
                    progresingAppOnclosing = true;
                    Close();
                    break;
                case 1:
                    MessageBox.Show("Niepoprawne dane");
                    break;
            }
        }

        private void ProfilSelector1_Closed(object sender, EventArgs e)
        {
            if (!progresingAppOnclosing)
            {
                System.Windows.Application.Current.Shutdown();
            }
            
        }

        private void ProfilSelector_Loaded(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).PobierzDaneZ(out profile, out podmiot, out user);
            ItemCreator.SetComboBoxItems(profile, ref Profil);
            Profil.SelectedIndex = 0;
            Podmiot.Text = podmiot;
            ItemCreator.SetComboBoxItems(user, ref User);
            User.SelectedIndex = 0;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
