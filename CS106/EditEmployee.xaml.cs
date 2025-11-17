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
    /// Interaction logic for ExitEmployee.xaml
    /// </summary>
    public partial class ExitEmployee : Page
    {
        List<CS106.Model.Database.SQL_EmployeeDataStruct> employee_list;
        public ExitEmployee()
        {
            InitializeComponent();
            employee_list = EmployeeManagementSystem.GetEmployee();
            foreach (var i in employee_list)
            {
                var name = EmployeeManagementSystem.GetEmployee(i.employee_id);
                workers_list.Items.Add(i.employee_id + ": " + name[0].name );

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

        private void Exit(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Logout());

        }

        private void EditRoster(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditRoster());

        }

        private void Select(object sender, RoutedEventArgs e)
        {
            var employee = workers_list.SelectedItem;
            if(employee == null)
            {
                MessageBox.Show("Select employee");
                return;
            }
            string employee_id = employee.ToString().Substring(0, employee.ToString().IndexOf(":"));

            Database.SQL_EmployeeDataStruct? result = null; 
            foreach(var i in employee_list)
            {
                if(i.employee_id == long.Parse(employee_id))
                {
                    result = i;
                }
            }
            if (result != null)
            {
                id.Text = result.employee_id.ToString() + ") " + result.name + " " + result.username;
                job_title.Text = result.job_title;
                pay_rate.Text = result.pay_rate.ToString();
                hire_date.Text = result.hire_date;
                total_leave.Text = result.total_leave.ToString();
                leave_used.Text = result.ToString();
            }
            else
                MessageBox.Show("Select employee");
            

        }
    }
}
