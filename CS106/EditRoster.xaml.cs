using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditRoster.xaml
    /// </summary>
    public partial class EditRoster : Page
    {
        public EditRoster()
        {
            workers = new ObservableCollection<string>();
            InitializeComponent();
            DataContext = this;

            var employee_list = EmployeeManagementSystem.GetEmployee();

            foreach (var i in employee_list)
            {
                TextBlock text = new TextBlock();
                if(Name != null)
                text.Text = i.employee_id + ": " + i.name.ToString();
                workers.Add(text.Text);
                
            }
        }
        private ObservableCollection<string> workers;
        public ObservableCollection<string> Workers
        {
            get { return workers; }
            set { Workers = value; }
        }


        private void LeaveRequestManagement(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminLeaveRequest());

        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            var roster = workers_list.SelectedItems;
            foreach(var i in roster)
            {
                workers.Add(i.ToString());
            }
        }

        private void AddRoster(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRoster());

        }
    }
}
