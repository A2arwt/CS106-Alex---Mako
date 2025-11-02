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

namespace CS106;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

    }
    long SignIn(string username, string password)
    {
        var database = new SQLiteConnection("Data Source=./database/CS106.db");
        database.Open();
        var command = new SQLiteCommand("select employee_id from user where  password is \"" + password + "\" and username is \"" + username + "\"", database);
        var result = command.ExecuteReader();
        if (result.Read())
        {
            return (long)result[0];
        }


        //p.Visibility = System.Windows.Visibility.Hidden;
        database.Close();
        return -1;
    }

    void CreateUser(string name,string username,string job,float payrate)
    {
        var database = new SQLiteConnection("Data Source=./database/CS106.db");
        database.Open();
        var command = new SQLiteCommand("insert into employee(name,job_tittle,pay_rate,hire_date,username)" +
                                                    " VALUES(@name,@job,@payrate,@data,@username)", database);
        command.Parameters.AddWithValue("@data", DateTime.Now.ToString("dd - MM - yyyy hh: mm:ss.fff"));
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@job", job);
        command.Parameters.AddWithValue("@payrate", payrate);
        //command.Parameters.AddWithValue("@data", DateTime.Now);
        var result = command.ExecuteNonQuery();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
            //p.Text = string.Format("{0}", i[0]);
        p.Text = SignIn(username.Text,password.Password).ToString();
        CreateUser("new","ddd", "other", 3);
    }

    
}