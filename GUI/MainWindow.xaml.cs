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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TPCSharp;

namespace GUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddPanel.Visibility != Visibility.Visible)
            {
                HideAllPanel();
                AddPanel.Visibility = Visibility.Visible;
            }
            //AddSalarieDialog addSalarieDialog = new AddSalarieDialog();
            //addSalarieDialog.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchMatriculePanel.Visibility != Visibility.Visible)
            {
                HideAllPanel();
                SearchMatriculePanel.Visibility = Visibility.Visible;
            }
        }

        private void HideAllPanel()
        {
            AddPanel.Visibility = Visibility.Collapsed;
            SearchMatriculePanel.Visibility = Visibility.Collapsed;
        }

        private void SearchMatriculeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Int32 mat = Convert.ToInt32(MatriculeTextBox.Text);

                Salarie s = TPCSharp.Application.GetSalarieByMatricule<Salarie>(mat);
                if (s != null)
                {
                    MessageBox.Show("Salarié Trouvé", "Salarié Trouvé", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Aucun salarié correspond à ce matricule !", "Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                    MatriculeTextBox.Text = "";
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Le matricule n'est pas un bon format !", "Format Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                MatriculeTextBox.Text = "";
            }
            catch (OverflowException)
            {
                MessageBox.Show("Le matricule est trop long !", "OverFlow Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                MatriculeTextBox.Text = "";
            }
            
            
        }
    }  
}
