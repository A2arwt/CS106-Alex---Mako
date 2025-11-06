using CS106.Model;
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

namespace CS106
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        Database sql_database;
        public Login()
        {
            InitializeComponent();
             sql_database = new Database();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var i = sql_database.login(Username.Text, Password.Password);
            if(i != null)
            Nav.Navigate(new Menu());
        }

        private void nav_Navigated(object sender, NavigationEventArgs e)
        {
        }
    }
}
