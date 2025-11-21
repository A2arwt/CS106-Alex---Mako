using CS106.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CS106.view.UserControls
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
            if (!EmployeeManagementSystem.is_admin)
            {
                foreach (UIElement child in pagelist.Children)
                {
                    if (Grid.GetRow(child) == 5)
                        child.Visibility = Visibility.Collapsed;
                }
            }

        }

        private void LeaveRequest(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new AdminLeaveRequest());
        }

        private void SalaryDetails(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new SalaryDetails());
        }

        private void PersonalDetails(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new PersonalDetails());
        }

        private void ComanyEvent(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new CompanyInfo());
        }

        private void Resignation(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Resignation());
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new CS106.LoginPage());
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new ExitEmployee());
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new CS106.Menu());
        }

        private void Message(object sender, RoutedEventArgs e)
        {
            
                var nav = NavigationService.GetNavigationService(this);
                nav.Navigate(new CS106.MassageEmployee());
            
        }
    }
}
