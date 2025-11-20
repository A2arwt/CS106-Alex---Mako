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

            MainWindow.ManagementSystem.login(Username.Text, Password.Password);
            if (EmployeeManagementSystem.current_user != null)
            {
                if (EmployeeManagementSystem.current_user.username == "admin" || EmployeeManagementSystem.current_user.job_title == "admin")
                {
                    EmployeeManagementSystem.is_admin = true;
                    NavigationService.Navigate(new SplashPage());

                }
                else { 
                    EmployeeManagementSystem.is_admin = false;
                NavigationService.Navigate(new SplashPage());
                }
                
            }
        }
    }
}
