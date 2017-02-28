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
using TPCSharp;
using EmailAdresse;

namespace GUI
{
    /// <summary>
    /// Logique d'interaction pour AddSalarieDialog.xaml
    /// </summary>
    public partial class AddSalarieDialog : Window
    {
        public AddSalarieDialog()
        {
            InitializeComponent();
        }


        private void checkInput()
        {

            //Check the type, type = -1 if no type selected or wring
            int typeComboBoxIndex = typeComboBox.SelectedIndex;
            int type;
            switch (typeComboBoxIndex)
            {
                case 0:
                    type = (Int32)Salarie.Salaries.Commercial;
                    break;
                case 1:
                    type = (Int32)Salarie.Salaries.Technicien;
                    break;
                default:
                    type = -1;
                    break;
            }

            //Get The Name from the nameTextBox
            String name = nameTextBox.Text;

            //Get the email from the emailTextBox
            String email = emailTextBox.Text;

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (Program.checkEmail(emailTextBox.Text))
            {
                MessageBox.Show("Email Ok", "All Good", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Email not good !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

