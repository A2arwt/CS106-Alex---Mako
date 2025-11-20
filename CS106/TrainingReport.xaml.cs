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
    /// Interaction logic for TrainingReport.xaml
    /// </summary>
    public partial class TrainingReport : Page
    {
        List<CS106.Model.Database.SQL_EmployeeDataStruct> employees;

        public TrainingReport()
        {
            InitializeComponent();
            employees = EmployeeManagementSystem.GetEmployee();
            var employees_review = EmployeeManagementSystem.GetTrainingReport();
            foreach (var i in employees)
            {
                employee_list.Items.Add(i.employee_id + ": " + i.name);
            }

            foreach (var i in employees_review)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Vertical;

                TextBlock ID = new TextBlock();
                ID.Text = i.training_id.ToString() + ": ";
                foreach (var item in EmployeeManagementSystem.GetEmployee(i.employee_id))
                {
                    if (item.employee_id == i.employee_id)
                    {
                        ID.Text += item.name;
                        break;
                    }
                }
                stack.Children.Add(ID);

                //TextBlock name = new TextBlock();
                //name.Text = EmployeeManagementSystem.GetEmployee(i.employee_id).First().name;
                //stack.Children.Add(name);


                TextBlock training_course = new TextBlock();
                training_course.Text = "Name: " + i.training_course;
                stack.Children.Add(training_course);


                TextBlock data_aquired = new TextBlock();
                data_aquired.Text = "Date Aquired: " + i.data_aquired;
                stack.Children.Add(data_aquired);


                TextBlock status = new TextBlock();
                status.Text = "Status: " + i.status;
                stack.Children.Add(status);

                TextBlock date_expired = new TextBlock();
                date_expired.Text = "date_expired: " + i.date_expired;
                stack.Children.Add(date_expired);


                review_list.Items.Add(stack);

            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            var data = new SQL_Database.SQL_TrainingReportDataStruct();
            if (employee_list.SelectedItem == null)
            {
                MessageBox.Show("select a user");
                return;
            }
            string id = employee_list.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("select a user");
                return;
            }
            data.employee_id = long.Parse(id.Substring(0, id.IndexOf(":")));


            data.data_aquired = date_aquired.SelectedDate.ToString();
            if (string.IsNullOrWhiteSpace(data.data_aquired))
            {
                MessageBox.Show("Enter acquired date");
                return;
            }

            data.date_expired = date_expired.SelectedDate.ToString();
            if (string.IsNullOrWhiteSpace(data.date_expired))
            {
                data.date_expired = "N/A";
            }

            data.status = status.Text;
            if (string.IsNullOrWhiteSpace(data.status))
            {
                MessageBox.Show("Enter status");
                return;
            }

            data.training_course = training_course.Text;
            if (string.IsNullOrWhiteSpace(data.training_course))
            {
                MessageBox.Show("Enter Training Course");
                return;
            }
            
            
            EmployeeManagementSystem.CreateTrainingReport(data);
            NavigationService.Navigate(new TrainingReport());
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            var data = new SQL_Database.SQL_TrainingReportDataStruct();
            var old_data = EmployeeManagementSystem.GetTrainingReport();



            if (review_list.SelectedItem == null)
            {
                MessageBox.Show("select a review");
                return;
            }


            string id = ((TextBlock)(((StackPanel)(review_list.SelectedItem)).Children[0])).Text;



            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("select a review");
                return;
            }
            if (long.TryParse(id.Substring(0, id.IndexOf(":")), out long answer))
            {
                data.training_id = answer;
            }
            else
            {
                MessageBox.Show("Select training report");
                return;
            }
            SQL_Database.SQL_TrainingReportDataStruct userdata = null;

            foreach (var i in old_data)
            {
                if (i.training_id == data.training_id)
                {
                    userdata = i;
                    data.employee_id = i.employee_id;
                }
            }
            if (userdata == null)
            {
                MessageBox.Show("Select a training report");
                return;
            }



            data.employee_id = userdata.employee_id;
            if (date_aquired.SelectedDate == null)
            {
                data.data_aquired = userdata.data_aquired;

            }
            else
                data.data_aquired = date_aquired.SelectedDate.ToString();

            data.employee_id = userdata.employee_id;
            if (date_expired.SelectedDate == null)
            {
                data.date_expired = userdata.date_expired;

            }
            else
                data.date_expired = date_expired.SelectedDate.ToString();




            if (string.IsNullOrWhiteSpace(training_course.Text))
            {
                data.training_course = userdata.training_course;
            }
            else
                data.training_course = training_course.Text;


            if (string.IsNullOrWhiteSpace(status.Text))
            {
                data.status = userdata.status;
            }
            else
                data.status = status.Text;


            

            EmployeeManagementSystem.UpdateTrainingReport(data);
            NavigationService.Navigate(new TrainingReport());
        }

    }
}
