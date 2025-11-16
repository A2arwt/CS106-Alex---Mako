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
    /// Interaction logic for AddRoster.xaml
    /// </summary>
    public partial class AddRoster : Page
    {
        public AddRoster()
        {
            InitializeComponent();
        }

        private void LeaveRequestManagement(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminLeaveRequest());

        }



        private void EditRoster(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditRoster());

        }
    }
}
