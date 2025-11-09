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
                ID.Text = request[i].employee_id +"|";
                ID.Background = new SolidColorBrush(Color.FromArgb(0,0xbb,0xe2,0xf2));
                ID.Name = "bbbb";
                stack.Children.Add(ID);



                TextBlock type = new TextBlock();
                type.Text = request[i].request_type + "|";
                type.Background = new SolidColorBrush(Color.FromArgb(0,0xbb,0xe2,0xf2));
                stack.Children.Add(type);

                TextBlock status = new TextBlock();
                status.Text = request[i].leave_status + "|";
                stack.Children.Add(status);

                TextBlock total = new TextBlock();
                total.Text = request[i].total_leave + "|";
                stack.Children.Add(total);

                TextBlock used = new TextBlock();
                used.Text = request[i].leave_used + "|";
                stack.Children.Add(used);

                TextBlock start = new TextBlock();
                start.Text = request[i].leave_start_date + "|";
                stack.Children.Add(start);

                TextBlock end = new TextBlock();
                end.Text = request[i].leave_end_date + "|";
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
            TextBlock rep = (TextBlock)(stack.Children[2]);
            rep.Text = "acepted";

        }
        void RejectRequest(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            StackPanel stack = (StackPanel)btn.Tag;
            TextBlock rep = (TextBlock)(stack.Children[2]);
            rep.Text = "reject";

        }

        
    }
}
