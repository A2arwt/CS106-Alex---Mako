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

namespace CS106;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();



        var cc = new SQLiteConnection("Data Source=./database/CS106.db");
        cc.Open();
        var com = new SQLiteCommand("select * from user", cc);
        var i = com.ExecuteReader();
        cc.Close();
        p.Text = i.ToString();
    }
}