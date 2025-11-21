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
using CS106.Model;
using System.Windows.Shapes;

namespace CS106
{
    /// <summary>
    /// Interaction logic for PersonalDetails.xaml
    /// </summary>
    public partial class PersonalDetails : Page
    {

        public PersonalDetails()
        {
            InitializeComponent();

            var leavelist = EmployeeManagementSystem.GetRequest();

            foreach(var item in leavelist)
            {
                if (item.employee_id == EmployeeManagementSystem.current_user.employee_id)
                {
                    TextBlock text = new TextBlock();
                    text.Text = item.request_type + " From " + item.leave_start_date + " to " + item.leave_end_date + " " +item.leave_status;
                    Leave.Items.Add(text);
                }
            }

            /////////////////-------------------------------------------------------------------salary_details

            var employee_roster = EmployeeManagementSystem.GetRoster();
            TextBlock pay;
            double earning = 0;
            int count = 0;

            foreach(var item in employee_roster)
            {
                if(item.employee_id == EmployeeManagementSystem.current_user.employee_id)
                {
                    double time = item.shift_finish_time - item.shift_start_time;
                    earning += EmployeeManagementSystem.current_user.pay_rate * time;
                    count++;
                }
                if(count > 6)
                {
                    pay = new TextBlock();
                    pay.Text = "Weekly earnings: $" + earning;
                    salary_details.Items.Add(pay);
                    earning = 0;
                }

            }
            pay = new TextBlock();
            pay.Text = "Weekly earnings: $" + earning;
            salary_details.Items.Add(pay);
            earning = 0;
            /////////////----------------------------------------------------------------------------


            var employees_review = EmployeeManagementSystem.GetPreformanceReview();
           

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

                if(i.employee_id == EmployeeManagementSystem.current_user.employee_id)
                Preforemance_review.Items.Add(stack);

            }

            //// training report
            ///
             var employees_review_ = EmployeeManagementSystem.GetTrainingReport();


            foreach (var i in employees_review_)
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


                training_report.Items.Add(stack);

            }
        }

        private void Message(object sender, RoutedEventArgs e)
        {
            if(EmployeeManagementSystem.is_admin)
            NavigationService.Navigate(new Messages());
            else
            NavigationService.Navigate(new MassageEmployee());

        }
    }
}
