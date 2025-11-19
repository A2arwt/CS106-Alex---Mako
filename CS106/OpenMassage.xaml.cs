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
using CS106.Model;

namespace CS106
{
    /// <summary>
    /// Interaction logic for OpenMassage.xaml
    /// </summary>
    public partial class OpenMassage : Page
    {
        public OpenMassage()
        {
            List<CS106.Model.Database.SQL_MessageDataStruct> message_list;
            InitializeComponent();
            message_list = EmployeeManagementSystem.GetMessages();

 
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;



                var request = (from item in message_list
                               where item.employee_id == Messages.ID 
                               select item);

            var request_cu = (from item in message_list
                           where item.employee_id == EmployeeManagementSystem.current_user.employee_id
                           select item);
            foreach(var i in request_cu)
            {
                request.Append(i);
            }
            var sorted = request
                    .OrderByDescending(e => DateTime.Parse(e.recieve_data))
                    .ToList();


            foreach(var i in sorted)
            {
                Button chat = new Button();
                chat.Content = i.employee_id + ": " + 
                    EmployeeManagementSystem.GetEmployee(i.employee_id).First().name+ "\n" + i.send_message;
                stack.Children.Add(chat);
                message_panel.Children.Add(chat);

            }





            
        }

        private void Send(object sender, RoutedEventArgs e)
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Button chat = new Button();
            chat.Content = EmployeeManagementSystem.current_user.employee_id + EmployeeManagementSystem.current_user.name +": " + message.Text;
            stack.Children.Add(chat);
            message_panel.Children.Add(chat);
            EmployeeManagementSystem.SendMessage();
        }
    }
}
