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

namespace CS106;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var c = "data=./../database/CS106.db";
        var cc = new SQLiteConnection(c);
        cc.Open();
        var com = new SQLiteCommand("select * from user", cc);
        var i = com.ExecuteReader();
        cc.Close();
        p.Text = i.ToString();
    }
}