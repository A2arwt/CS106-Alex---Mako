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
    /// 

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
                workers_list.Items.Add(i.employee_id + ": " + i.name);

            }

        }


        private void Select(object sender, RoutedEventArgs e)
        {
            var employee = workers_list.SelectedItem;
            if (employee == null)
            {
                MessageBox.Show("Select employee");
                return;
            }
            string employee_id = employee.ToString().Substring(0, employee.ToString().IndexOf(":"));

            Database.SQL_EmployeeDataStruct? result = null;
            foreach (var i in employee_list)
            {
                if (i.employee_id == long.Parse(employee_id))
                {
                    result = i;
                }
            }
            if (result != null)
            {
                id.Text = "ID: " + result.employee_id.ToString();
                name.Text = "Name: " + result.name;
                username.Text = "Username: " + result.username;
                var user = EmployeeManagementSystem.GetUser(result.employee_id);
                if(user != null)
                {
                    password.Text = "Password: " + user.password;
                }
                else
                {
                    password.Text = "Password: null";
                }
                job_title.Text = "Job title: " + result.job_title;
                pay_rate.Text = "Pay Rate: " + result.pay_rate.ToString();
                hire_date.Text = "Hire date: " + result.hire_date;
                total_leave.Text = "Total leave: " + result.total_leave.ToString();
                leave_used.Text = "Leave used: " + result.leave_used.ToString();
            }
            else
                MessageBox.Show("Select employee");


        }

        private void Change(object sender, RoutedEventArgs e)
        {
            var employee = workers_list.SelectedItem;
            if (employee == null)
            {
                MessageBox.Show("Select employee");
                return;
            }
            long employee_id = long.Parse(employee.ToString().Substring(0, employee.ToString().IndexOf(":")));

            Database.SQL_EmployeeDataStruct? result = null;
            foreach (var i in employee_list)
            {
                if (i.employee_id == employee_id)
                {
                    result = i;
                }
            }
            if(result != null)
            {
                if (name.Text != "")
                    result.name = name.Text;

                if (username.Text != "")
                    result.username = username.Text;

                if (new_job_title.Text != "")
                    result.job_title = new_job_title.Text;


                if (new_pay_rate.Text != "" && new_pay_rate.Text.All(char.IsDigit))
                    result.pay_rate = double.Parse(new_pay_rate.Text);


                    

                if (total_leave.Text != "" && total_leave.Text.All(char.IsDigit))
                    result.total_leave = long.Parse(total_leave.Text) ;
                EmployeeManagementSystem.UpdateRequest
            }
            else
            {
                MessageBox.Show("Select employee");
                return;
            }


                NavigationService.Navigate(new ExitEmployee());

        }

        private void Create(object sender, RoutedEventArgs e)
        {
            Database.SQL_EmployeeDataStruct new_employee = new SQL_Database.SQL_EmployeeDataStruct();
            Database.SQL_UserDataStruct New_user = new SQL_Database.SQL_UserDataStruct();

            NavigationService.Navigate(new ExitEmployee());

        }
    }
}
