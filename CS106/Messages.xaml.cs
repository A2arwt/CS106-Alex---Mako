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
    /// Interaction logic for Messages.xaml
    /// </summary>
    public partial class Messages : Page
    {
        List<CS106.Model.Database.SQL_MessageDataStruct> message_list;

        public static long ID = -1;

        public Messages()
        {
            InitializeComponent();

            message_list = EmployeeManagementSystem.GetMessages();
            foreach (var i in message_list)
            {
                var name = EmployeeManagementSystem.GetEmployee(i.employee_id).First();
                var employee_list = EmployeeManagementSystem.GetEmployee(i.employee_id).First();
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;

                

                var request = (from item in message_list
                               where item.employee_id == i.employee_id
                               select item);
                var sorted = request
                    .OrderByDescending(e => DateTime.Parse(e.recieve_data))
                    .ToList();

                Button chat = new Button();
                chat.Content = i.employee_id + ": " + name.name + "\n" + sorted.First().send_message;
                stack.Children.Add(chat);

                chat.Click += Chat;
                chat.Tag = i.employee_id;
                message_panel.Children.Add(stack);


            }
            var employees = EmployeeManagementSystem.GetEmployee();

            foreach (var i in employees)
            {
                var employee_list = EmployeeManagementSystem.GetEmployee(i.employee_id).First();
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;


                Button chat = new Button();
                chat.Content = i.employee_id + ": " + i.name;
                stack.Children.Add(chat);

                chat.Click += Chat;
                chat.Tag = i.employee_id;
                employee_panel.Children.Add(stack);


            }

        }

        private void Chat(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ID = (long)btn.Tag;
            NavigationService.Navigate(new OpenMassage());


        }
    }
}
//message_list = EmployeeManagementSystem.GetMessages();
//foreach (var i in message_list)
//{
//    var name = EmployeeManagementSystem.GetEmployee(i.employee_id).First();
//    var employee_list = EmployeeManagementSystem.GetEmployee(i.employee_id).First();

//    var request = (from item in message_list
//                   where item.employee_id == i.employee_id
//                   select item);
//    var sorted = request
//        .OrderByDescending(e => DateTime.Parse(e.recieve_data))
//        .ToList();


//    foreach (var item in sorted)
//    {
//        message_panel.Items.Add(i.employee_id + ": " + name.name + "\n" + sorted.First().send_message);
//        break;

//    }

//    //new_workers_list.Items.Add(i.employee_id + ": " + i.name);
//}