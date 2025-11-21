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
    /// Interaction logic for MassageEmployee.xaml
    /// </summary>
    public partial class MassageEmployee : Page
    {
        List<CS106.Model.Database.SQL_MessageDataStruct> message_list;

        public static long ID = -1;
        public MassageEmployee()
        {
            InitializeComponent();


            message_list = EmployeeManagementSystem.GetMessages();

            // Get unique employee IDs who have messages
            var uniqueEmployeeIds = message_list
                .Select(m => m.employee_id)
                .Distinct()
                .ToList();

            // Also include employees involved via message_pointer
            var pointerEmployeeIds = message_list
                .Where(m => m.message_pointer > 0)
                .Select(m => m.message_pointer)
                .Distinct()
                .ToList();

            uniqueEmployeeIds.AddRange(pointerEmployeeIds);
            uniqueEmployeeIds = uniqueEmployeeIds.Distinct().ToList();

            // Remove current user from the list if they're in it
            uniqueEmployeeIds.Remove(EmployeeManagementSystem.current_user.employee_id);

            foreach (var employeeId in uniqueEmployeeIds)
            {
                var employee = EmployeeManagementSystem.GetEmployee(employeeId).FirstOrDefault();
                if (employee == null) continue;

                // Get all messages for this conversation (both directions)
                var conversationMessages = message_list
                    .Where(m => m.employee_id == employeeId || m.message_pointer == employeeId)
                    .OrderByDescending(m => DateTime.Parse(m.recieve_data))
                    .ToList();

                if (conversationMessages.Count == 0) continue;

                var lastMessage = conversationMessages.First();

                // Create conversation button
                Border conversationBorder = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(245, 245, 245)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(5),
                    CornerRadius = new CornerRadius(8),
                    Cursor = Cursors.Hand
                };

                StackPanel conversationStack = new StackPanel
                {
                    Margin = new Thickness(10)
                };

                // Employee name
                TextBlock nameBlock = new TextBlock
                {
                    Text = $"{employee.name} (ID: {employee.employee_id})",
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51))
                };

                // Last message preview
                string messagePreview = lastMessage.send_message;
                if (messagePreview.Length > 50)
                    messagePreview = messagePreview.Substring(0, 50) + "...";

                TextBlock messageBlock = new TextBlock
                {
                    Text = messagePreview,
                    FontSize = 12,
                    Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102)),
                    Margin = new Thickness(0, 5, 0, 0),
                    TextWrapping = TextWrapping.Wrap
                };

                // Date
                TextBlock dateBlock = new TextBlock
                {
                    Text = DateTime.Parse(lastMessage.recieve_data).ToString("dd/MM/yyyy HH:mm"),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(153, 153, 153)),
                    Margin = new Thickness(0, 3, 0, 0)
                };

                conversationStack.Children.Add(nameBlock);
                conversationStack.Children.Add(messageBlock);
                conversationStack.Children.Add(dateBlock);

                conversationBorder.Child = conversationStack;
                conversationBorder.Tag = employeeId;
                conversationBorder.MouseDown += ConversationBorder_MouseDown;

                // Hover effect
                conversationBorder.MouseEnter += (s, e) =>
                {
                    conversationBorder.Background = new SolidColorBrush(Color.FromRgb(235, 235, 235));
                };
                conversationBorder.MouseLeave += (s, e) =>
                {
                    conversationBorder.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                };

                message_panel.Children.Add(conversationBorder);
            }

            // Employee list for starting new conversations
            var allEmployees = EmployeeManagementSystem.GetEmployee();

            foreach (var employee in allEmployees)
            {
                // Skip current user
                if (employee.employee_id == EmployeeManagementSystem.current_user.employee_id)
                    continue;

                Border employeeBorder = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(240, 248, 255)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(173, 216, 230)),
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(5),
                    CornerRadius = new CornerRadius(6),
                    Cursor = Cursors.Hand
                };

                TextBlock employeeBlock = new TextBlock
                {
                    Text = $"{employee.name} (ID: {employee.employee_id})",
                    Margin = new Thickness(10, 8, 10, 8),
                    FontSize = 13,
                    Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51))
                };

                employeeBorder.Child = employeeBlock;
                employeeBorder.Tag = employee.employee_id;
                employeeBorder.MouseDown += ConversationBorder_MouseDown;

                // Hover effect
                employeeBorder.MouseEnter += (s, e) =>
                {
                    employeeBorder.Background = new SolidColorBrush(Color.FromRgb(225, 240, 255));
                };
                employeeBorder.MouseLeave += (s, e) =>
                {
                    employeeBorder.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
                };

                employee_panel.Children.Add(employeeBorder);
            }
        }

        private void ConversationBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            if (border != null && border.Tag != null)
            {
                ID = (long)border.Tag;
                NavigationService.Navigate(new OpenMassageEmployee());
            }
        }
    }
}
