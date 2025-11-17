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

namespace CS106.view.UserControls
{
    /// <summary>
    /// Interaction logic for Mune.xaml
    /// </summary>
    public partial class Mune : UserControl
    {
        public Mune()
        {
            InitializeComponent();
            if (EmployeeManagementSystem.is_admin == false)
            {
                pagelist.Children.RemoveAt(6);
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
            nav.Navigate(new MainWindow());
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            var nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new ExitEmployee());
        }
    }
}
