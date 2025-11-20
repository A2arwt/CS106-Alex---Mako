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
            var employees_review = EmployeeManagementSystem.GetPreformanceReview();
            foreach (var i in employees)
            {
                employee_list.Items.Add(i.employee_id + ": " + i.name);
            }

            foreach (var i in employees_review)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Vertical;

                TextBlock ID = new TextBlock();
                ID.Text = i.review_id.ToString() + ": ";
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


                TextBlock date = new TextBlock();
                date.Text = "Review Date: " + i.review_data;
                stack.Children.Add(date);


                TextBlock feedback = new TextBlock();
                feedback.Text = "Feedback: " + i.feedback;
                stack.Children.Add(feedback);


                TextBlock score = new TextBlock();
                score.Text = "Review score: " + i.review_score.ToString();
                stack.Children.Add(score);


                review_list.Items.Add(stack);

            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            var data = new SQL_Database.SQL_PreformanceReviewDataStruct();
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
            data.review_data = review_date.SelectedDate.ToString();
            if (string.IsNullOrWhiteSpace(data.review_data))
            {
                MessageBox.Show("Enter review date");
                return;
            }
            data.feedback = feedback.Text;
            if (string.IsNullOrWhiteSpace(data.feedback))
            {
                MessageBox.Show("Enter feedback");
                return;
            }
            if (string.IsNullOrWhiteSpace(review_score.Text))
            {
                MessageBox.Show("Enter review score");
                return;
            }

            if (string.IsNullOrWhiteSpace(data.review_score.ToString()))
            {
                MessageBox.Show("Enter review score");
                return;
            }
            if (!long.TryParse(review_score.Text.ToString(), out long answer))
            {
                MessageBox.Show("Enter review score");
                return;
            }
            data.review_score = long.Parse(review_score.Text);
            EmployeeManagementSystem.CreatePreformanceReview(data);
            NavigationService.Navigate(new preformance_review());
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            var data = new SQL_Database.SQL_PreformanceReviewDataStruct();
            var old_data = EmployeeManagementSystem.GetPreformanceReview();



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
                data.review_id = answer;
            }
            else
            {
                MessageBox.Show("Select a review");
                return;
            }
            SQL_Database.SQL_PreformanceReviewDataStruct userdata = null;

            foreach (var i in old_data)
            {
                if (i.review_id == data.review_id)
                {
                    userdata = i;
                    data.employee_id = i.employee_id;
                }
            }
            if (userdata == null)
            {
                MessageBox.Show("Select a User");
                return;
            }



            data.employee_id = userdata.employee_id;
            if (review_date.SelectedDate == null)
            {
                data.review_data = userdata.review_data;

            }
            else
                data.review_data = review_date.SelectedDate.ToString();

            if (string.IsNullOrWhiteSpace(feedback.Text))
            {
                data.feedback = userdata.feedback;
            }
            else
                data.feedback = feedback.Text;


            if (string.IsNullOrWhiteSpace(review_score.Text) && !long.TryParse(review_score.Text.ToString(), out long _answer))
            {
                data.review_score = userdata.review_score;
            }
            else
                data.review_score = long.Parse(review_score.Text);

            EmployeeManagementSystem.UpdatePreformanceReview(data);
            NavigationService.Navigate(new preformance_review());
        }

    }
}
