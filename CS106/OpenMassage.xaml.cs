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
            InitializeComponent();
            LoadMessages();
        }

        private void LoadMessages()
        {
            message_panel.Children.Clear();

            List<CS106.Model.Database.SQL_MessageDataStruct> message_list = EmployeeManagementSystem.GetMessages();
            long currentUserId = EmployeeManagementSystem.current_user.employee_id;
            long otherUserId = Messages.ID;

            // Get all messages in this conversation (both directions)
            // Messages where current user sent to other user
            var sentMessages = message_list
                .Where(m => m.employee_id == currentUserId && m.message_pointer == otherUserId)
                .ToList();

            // Messages where other user sent to current user
            var receivedMessages = message_list
                .Where(m => m.employee_id == otherUserId && m.message_pointer == currentUserId)
                .ToList();

            // Also get messages where employee_id matches and message_pointer is 0 or -1 (older format)
            var legacyMessages = message_list
                .Where(m => (m.employee_id == currentUserId || m.employee_id == otherUserId) &&
                           (m.message_pointer == 0 || m.message_pointer == -1))
                .ToList();

            // Combine all messages
            var allMessages = sentMessages
                .Concat(receivedMessages)
                .Concat(legacyMessages)
                .OrderBy(m => DateTime.Parse(m.recieve_data))
                .ToList();

            // Display messages
            foreach (var msg in allMessages)
            {
                var sender = EmployeeManagementSystem.GetEmployee(msg.employee_id).FirstOrDefault();
                if (sender == null) continue;

                bool isCurrentUser = msg.employee_id == currentUserId;

                Border messageBorder = new Border
                {
                    Background = isCurrentUser
                        ? new SolidColorBrush(Color.FromRgb(220, 240, 255))
                        : new SolidColorBrush(Color.FromRgb(245, 245, 245)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(10, 5, 10, 5),
                    Padding = new Thickness(12),
                    HorizontalAlignment = isCurrentUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                    MaxWidth = 500
                };

                StackPanel messageStack = new StackPanel();

                // Sender name
                TextBlock senderBlock = new TextBlock
                {
                    Text = $"{sender.name} (ID: {sender.employee_id})",
                    FontWeight = FontWeights.Bold,
                    FontSize = 12,
                    Foreground = isCurrentUser
                        ? new SolidColorBrush(Color.FromRgb(0, 102, 204))
                        : new SolidColorBrush(Color.FromRgb(51, 51, 51)),
                    Margin = new Thickness(0, 0, 0, 5)
                };

                // Message content
                TextBlock messageBlock = new TextBlock
                {
                    Text = msg.send_message,
                    FontSize = 13,
                    Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51)),
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 0, 0, 5)
                };

                // Timestamp
                TextBlock timeBlock = new TextBlock
                {
                    Text = DateTime.Parse(msg.recieve_data).ToString("dd/MM/yyyy HH:mm:ss"),
                    FontSize = 10,
                    Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128)),
                    HorizontalAlignment = HorizontalAlignment.Right
                };

                messageStack.Children.Add(senderBlock);
                messageStack.Children.Add(messageBlock);
                messageStack.Children.Add(timeBlock);

                messageBorder.Child = messageStack;
                message_panel.Children.Add(messageBorder);
            }

            // Auto-scroll to bottom
            if (message_panel.Children.Count > 0)
            {
                var lastMessage = message_panel.Children[message_panel.Children.Count - 1] as FrameworkElement;
                lastMessage?.BringIntoView();
            }
        }

        private void Send(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
                return;

            long currentUserId = EmployeeManagementSystem.current_user.employee_id;
            long otherUserId = Messages.ID;

            // Send message with proper pointer to indicate recipient
            EmployeeManagementSystem.SendMessage(currentUserId, message.Text, otherUserId);

            // Clear input
            message.Text = "";

            // Reload messages to show the new one
            LoadMessages();
        }
    }
}