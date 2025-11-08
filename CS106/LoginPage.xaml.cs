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
using CS106.Model;

namespace CS106
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Database.user = MainWindow.sql_database.login(Username.Text.ToLower().Trim(), Password.Password.ToLower().Trim());
            if (Database.user != null)
               {
                if (Database.user.username.ToLower().Trim() == "admin")
                {
                    NavigationService.Navigate(new Menu());

                }
                else
                    NavigationService.Navigate(new UserMune()); 
                }
        }
    }
}
