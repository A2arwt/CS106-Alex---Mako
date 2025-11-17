using CS106.Model;
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
            var employee_list = EmployeeManagementSystem.GetEmployee();

            foreach (var i in employee_list)
            {
                workers_list.Items.Add(i.employee_id + ": " + i.name);

            }
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            var roster = workers_list.SelectedItems;
            
            if(TimeSpan.TryParse(start_time.Text, out TimeSpan s_time) && TimeSpan.TryParse(end_time.Text, out TimeSpan e_time))
            {
                foreach (var i in roster)
                {
                    if (i != null)
                    {
                        #warning chech the message and make sure they give the right info

                        if (date.SelectedDate != null)
                        {
                            if(date.SelectedDate < DateTime.Now)
                            {
                                MessageBox.Show("Please pick a valid date");

                            }
                            else
                            {

                                string employee_id = new string(i.ToString().Where(char.IsDigit).ToArray());
                                string _time = new string(start_time.ToString().Where(char.IsDigit).ToArray());
                                string __time = new string(end_time.ToString().Where(char.IsDigit).ToArray());
                                double i_time = double.Parse(_time);
                                double i__time = double.Parse(__time);

                                if (i_time > 17.30 && i__time > 17.30)
                                {
                                    i_time /= 100;
                                    i__time /= 100;
                                }
                                    if(i_time >= 9 && i_time <= 17.30 && i__time >= 9 && i__time <= 17.30)
                                    {
                                        if (i_time < i__time)
                                            EmployeeManagementSystem.AddRoster(long.Parse(employee_id), date.SelectedDate.Value.ToString(), i_time, i__time);
                                        else
                                            MessageBox.Show("Please pick a valid time. start time is greater then end time working time is 9:00 to 17:30");
                                    }
                                    else
                                        MessageBox.Show("Please pick a valid time. working time is 9:00 to 17:30");
                                
                             
                            }
                        }
                        else
                            MessageBox.Show("Please pick a valid date");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid time (e.g., 14:30)");
            }
        }
    }
}
