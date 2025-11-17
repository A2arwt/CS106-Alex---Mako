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
using CS106.Model;

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
            welcame_text.Text = "Welcame " + EmployeeManagementSystem.current_user.name + "!\n " +
                "Employee ID:" + EmployeeManagementSystem.current_user.employee_id;



            var request = EmployeeManagementSystem.GetRoster();
            for (int i = 0; i < request.Count; i++)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;


                TextBlock ID = new TextBlock();
                ID.Text = request[i].employee_id.ToString();
                ID.Background = new SolidColorBrush(Color.FromArgb(0, 0xbb, 0xe2, 0xf2));
                stack.Children.Add(ID);

                TextBlock request_number = new TextBlock();
                request_number.Text = request[i].shift_date.ToString();
                request_number.Background = new SolidColorBrush(Color.FromArgb(0, 0xbb, 0xe2, 0xf2));
                stack.Children.Add(request_number);



                TextBlock type = new TextBlock();
                type.Text = request[i].shift_start_time.ToString();
                type.Background = new SolidColorBrush(Color.FromArgb(0, 0xbb, 0xe2, 0xf2));
                stack.Children.Add(type);

                TextBlock status = new TextBlock();
                status.Text = request[i].shift_finish_time.ToString();
                stack.Children.Add(status);

                shift_stack.Children.Add(stack);
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
