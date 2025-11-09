using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Xml.Linq;
using CS106.Model;

namespace CS106;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{
        public static EmployeeManagementSystem ManagementSystem = new EmployeeManagementSystem();

    public MainWindow()
    {
        InitializeComponent();
        main_frame.Navigate(new LoginPage());
    }

}