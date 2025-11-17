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
    /// Interaction logic for ManageLeaveRequest.xaml
    /// </summary>
    public partial class ManageLeaveRequest : Page
    {
        public ManageLeaveRequest()
        {
            InitializeComponent();
            //< TextBlock >
            //        < TextBlock > ikijjjj </ TextBlock >
            //        < ComboBox x: Name = "myCombo" ItemsSource = "{Binding Categories}" DisplayMemberPath = "CategoryName"  SelectedValuePath = "Id" />
            //        < Button x: Name = "myButton" Content = "Show Product" Click = "myButton_Click" />
            //</ TextBlock >
            var request = EmployeeManagementSystem.GetRequest();
            for(int i = 0;i< request.Count; i++)
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                

                TextBlock ID = new TextBlock();
                ID.Text = request[i].employee_id.ToString();
                ID.Background = new SolidColorBrush(Color.FromArgb(0,0xbb,0xe2,0xf2));
                stack.Children.Add(ID);

                TextBlock request_number = new TextBlock();
                request_number.Text = request[i].request_number.ToString();
                request_number.Background = new SolidColorBrush(Color.FromArgb(0,0xbb,0xe2,0xf2));
                stack.Children.Add(request_number);



                TextBlock type = new TextBlock();
                type.Text = request[i].request_type ;
                type.Background = new SolidColorBrush(Color.FromArgb(0,0xbb,0xe2,0xf2));
                stack.Children.Add(type);

                TextBlock status = new TextBlock();
                status.Text = request[i].leave_status ;
                stack.Children.Add(status);

                TextBlock total = new TextBlock();
                total.Text = request[i].total_leave.ToString() ;
                stack.Children.Add(total);

                TextBlock used = new TextBlock();
                used.Text = request[i].leave_used.ToString() ;
                stack.Children.Add(used);

                TextBlock start = new TextBlock();
                start.Text = request[i].leave_start_date ;
                stack.Children.Add(start);

                TextBlock end = new TextBlock();
                end.Text = request[i].leave_end_date ;
                stack.Children.Add(end);

                Button agree = new Button();
                agree.Content = "agree";
                agree.Tag = stack;
                agree.Click += AgreeRequest;
                stack.Children.Add(agree);

                Button reject = new Button();
                reject.Tag = stack;
                reject.Click += RejectRequest;
                reject.Content = "reject";
                stack.Children.Add(reject);

                Stack.Children.Add(stack);
            }

        }

        void AgreeRequest(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            StackPanel stack = (StackPanel)btn.Tag;
            TextBlock rep = (TextBlock)(stack.Children[3]);
            rep.Text = "acepted";

             
            EmployeeManagementSystem.UpdateRequest(
                long.Parse(((TextBlock)stack.Children[0]).Text), 
                long.Parse(((TextBlock)stack.Children[1]).Text), 
                ((TextBlock)stack.Children[2]).Text,
                "acepted",
                long.Parse(((TextBlock)stack.Children[4]).Text),
                long.Parse(((TextBlock)stack.Children[5]).Text),
                ((TextBlock)stack.Children[6]).Text,
                ((TextBlock)stack.Children[7]).Text);

        }
        void RejectRequest(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            StackPanel stack = (StackPanel)btn.Tag;
            TextBlock rep = (TextBlock)(stack.Children[3]);
            rep.Text = "reject";


            EmployeeManagementSystem.UpdateRequest(
                long.Parse(((TextBlock)stack.Children[0]).Text),
                long.Parse(((TextBlock)stack.Children[1]).Text),
                ((TextBlock)stack.Children[2]).Text,
                "reject",
                long.Parse(((TextBlock)stack.Children[4]).Text),
                long.Parse(((TextBlock)stack.Children[5]).Text),
                ((TextBlock)stack.Children[6]).Text,
                ((TextBlock)stack.Children[7]).Text);
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
