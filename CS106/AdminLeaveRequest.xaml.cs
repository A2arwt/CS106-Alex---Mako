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
    /// Interaction logic for AdminLeaveRequest.xaml
    /// </summary>
    public partial class AdminLeaveRequest : Page
    {
        public AdminLeaveRequest()
        {
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {


            MainWindow.sql_database.SQL_InsertRequestData(Database.user.employee_id, null, null, Database.user.leave_used, Database.user.total_leave, StartDate.DataContext.ToString(), EndDate.DataContext.ToString());
        }
    }
}
