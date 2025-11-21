using CS106.Model;
using CS106.view.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
            welcame_text.Text = "Welcome " + EmployeeManagementSystem.current_user.name + "!\n";
            welcome_text.Text = "Employee ID: " + EmployeeManagementSystem.current_user.employee_id;
            events.Text = "\nChristmas day - 25/12/2025 \n\n Boxing day - 26/12/2025 \n\n New Years day - 1/01/2026\n\n";


            var request = EmployeeManagementSystem.GetRoster();
            for (int i = 0; i < request.Count; i++)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                


                TextBlock request_number = new TextBlock();
                request_number.Text = "Date: " + request[i].shift_date.ToString() ;
                stack.Children.Add(request_number);



                TextBlock type = new TextBlock();
                type.Text = " Start Time: "+ request[i].shift_start_time.ToString() +" End Time: ";
                stack.Children.Add(type);

                TextBlock status = new TextBlock();
                status.Text = " "+ request[i].shift_finish_time.ToString();
                stack.Children.Add(status);

                shift_stack.Items.Add(stack);
            }

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
