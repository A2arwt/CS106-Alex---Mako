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
                new_workers_list.Items.Add(i.employee_id + ": " + i.name);
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
                if (user != null)
                {
                    email.Text = "Email: " + user.email;
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
            var employee = new_workers_list.SelectedItem;
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
                    break;
                }
            }
            if (result != null)
            {
                if (!string.IsNullOrWhiteSpace(new_name.Text))
                    result.name = new_name.Text.Trim();




                if (!string.IsNullOrWhiteSpace(new_job_title.Text))
                    result.job_title = new_job_title.Text.Trim();


                if (!string.IsNullOrWhiteSpace(new_pay_rate.Text) && double.TryParse(new_pay_rate.Text, out double answer))
                    result.pay_rate = answer;

                if (new_hire_date.SelectedDate != null)
                    result.hire_date = new_hire_date.SelectedDate.Value.ToString("yyyy-MM-dd");

                if (!string.IsNullOrWhiteSpace(new_total_leave.Text) && long.TryParse(new_total_leave.Text, out long answer_))
                    result.total_leave = answer_;

                if (!string.IsNullOrWhiteSpace(new_leave_used.Text) && long.TryParse(new_leave_used.Text, out long answer__))
                    result.leave_used = answer__;



                var new_user = new SQL_Database.SQL_UserDataStruct();
                var old_user = EmployeeManagementSystem.GetUser(employee_id);

                if (!string.IsNullOrWhiteSpace(new_password.Text))
                    new_user.password = new_password.Text.Trim();
                else
                    new_user.password = old_user.password;


                if (!string.IsNullOrWhiteSpace(new_email.Text))
                    new_user.email = new_email.Text.Trim();
                else
                    new_user.email = old_user.email;

                if (EmployeeManagementSystem.Does_user_exist(new_username.Text.Trim()) == false)
                {
                    new_user.employee_id = employee_id;
                    if (!string.IsNullOrWhiteSpace(new_username.Text))
                        new_user.username = new_username.Text.Trim();
                    else
                        new_user.username = old_user.username;
                    EmployeeManagementSystem.UpdateUsers(new_user);
                    if (!string.IsNullOrWhiteSpace(new_username.Text))
                        result.username = new_username.Text.Trim();
                    else result.username = old_user.username;

                }
                else
                {
                    MessageBox.Show("Username taken, choose another Username");
                    return;
                }
                EmployeeManagementSystem.UpdateEmployee(result);

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

            if (!string.IsNullOrWhiteSpace(new_name.Text))
                new_employee.name = new_name.Text.Trim();



            if (!string.IsNullOrWhiteSpace(new_job_title.Text))
                new_employee.job_title = new_job_title.Text.Trim();


            if (!string.IsNullOrWhiteSpace(new_pay_rate.Text) && double.TryParse(new_pay_rate.Text, out double answer))
                new_employee.pay_rate = answer;

            if (!string.IsNullOrWhiteSpace(new_total_leave.Text) && long.TryParse(new_total_leave.Text, out long answer_))
                new_employee.total_leave = answer_;

            if (string.IsNullOrWhiteSpace(new_leave_used.Text.Trim()) == false && long.TryParse(new_leave_used.Text, out long answer__))
                new_employee.leave_used = answer__;



            Database.SQL_UserDataStruct new_user = new SQL_Database.SQL_UserDataStruct();
            if (!string.IsNullOrWhiteSpace(new_password.Text))
                new_user.password = new_password.Text.Trim();

            if (!string.IsNullOrWhiteSpace(new_email.Text))
                new_user.email = new_email.Text.Trim();

            if (!string.IsNullOrWhiteSpace(new_username.Text))
                new_employee.username = new_username.Text.Trim();
            else
            {
                MessageBox.Show("Username can't be empty");
                return;
            }

            if (EmployeeManagementSystem.GetUser(new_username.Text.Trim()) == null)
            {
                EmployeeManagementSystem.CreateEmployee(new_employee);
                EmployeeManagementSystem.UpdateUsers(new_user);

            }
            else
            {
                MessageBox.Show("Username taken");
                return;
            }




            NavigationService.Navigate(new ExitEmployee());

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var employee = workers_list.SelectedItem;
            if (employee == null)
            {
                MessageBox.Show("Select employee");
                return;
            }
            long employee_id = long.Parse(employee.ToString().Substring(0, employee.ToString().IndexOf(":")));

            EmployeeManagementSystem.DeleteEmployee(employee_id);
            NavigationService.Navigate(new ExitEmployee());

        }
    }
}
