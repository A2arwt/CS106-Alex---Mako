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
    /// Interaction logic for SalaryDetails.xaml
    /// </summary>
    public partial class SalaryDetails : Page
    {
        public SalaryDetails()
        {
            InitializeComponent();
        
        }



        private void LeaveRequest(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminLeaveRequest());
        }

        private void PersonalDetails(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalDetails());
        }

        private void LeaveRequestManagement(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManageLeaveRequest());
        }



        private void CompanyInfo(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompanyInfo());

        }

        private void Resignation(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Resignation());

        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Logout());

        }

        private void ExitEmployee(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new ExitEmployee());

        }
    }
}
