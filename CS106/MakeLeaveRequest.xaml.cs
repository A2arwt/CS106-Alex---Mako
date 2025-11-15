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
using CS106.Model;

namespace CS106
{
    /// <summary>
    /// Interaction logic for AdminLeaveRequest.xaml
    /// </summary>
    public partial class AdminLeaveRequest : Page
    {
        public AdminLeaveRequest()
        {
            InitializeComponent();

            if (EmployeeManagementSystem.is_admin == false)
            {
                pagelist.Children.RemoveAt(6);
            }
        }

        private void Submit(object sender, RoutedEventArgs e)
        {

            EmployeeManagementSystem.InsertRequestData(Type.Text, StartDate.Text, EndDate.Text);
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

        private void SalaryDetails(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SalaryDetails());

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
