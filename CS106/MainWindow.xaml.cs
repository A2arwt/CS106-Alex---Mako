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
#warning delete i
    int i = 1;

    Database sql_database = new Database();
    public MainWindow()
    {
        InitializeComponent();
        sql_database.sql_database = new SQLiteConnection("Data Source=./database/CS106.db");
        sql_database.sql_database.Open();


    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        p.Text = sql_database.SQL_GetUserEmployeeID(username.Text,password.Password).ToString();
        sql_database.SQL_CreateEmployee("new","ddd" + ++i, "other", 3);
    }

    
}