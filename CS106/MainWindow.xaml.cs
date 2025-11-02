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

namespace CS106;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
#warning delete i
    int i = 1;


    class EmployeeDataStruct
    {
        public long employee_id { get; set; }
        public string? name { get; set; }
        public string? username { get; set; }
        public string? job_tittle { get; set; }
        public long pay_rate { get; set; }
        public DateTime hire_date { get; set; }
    }


    class MessageDataStruct
    {
        public long employee_id { get; set; }
        public string? reply_message { get; set; }
        public string? send_message { get; set; }
        public DateTime recieve_data { get; set; }
    }


    class PreformanceReviewDataStruct
    {
        public long employee_id { get; set; }
        public DateTime review_data { get; set; }
        public string? feedback { get; set; }
        public long review_score { get; set; }
    }
    class RequestDataStruct
    {
        public long employee_id { get; set; }
        public string? request_type { get; set; }
        public string? leave_status { get; set; }
        public long total_leave { get; set; }
        public long leave_used { get; set; }
        public DateTime leave_start_date { get; set; }
        public DateTime leave_end_date { get; set; }
    }


    class TrainingReportDataStruct
    {
        long employee_id { get; set; }
        string? training_course { get; set; }
        DateTime data_aquired { get; set; }
        string? status { get; set; }
        long leave_used { get; set; }
        DateTime date_expired { get; set; }
    }


    class UserDataStruct
    {
        long employee_id { get; set; }
        string? email { get; set; }
        string? username { get; set; }
        string? password { get; set; }
    }



    SQLiteConnection database;
    public MainWindow()
    {
        InitializeComponent();
        database = new SQLiteConnection("Data Source=./database/CS106.db");
        database.Open();


    }
    long SignIn(string username, string password)
    {
        /*
         * This function returns the employee ID of the user or -1 if the user does not exist
         */
        var command = new SQLiteCommand("select employee_id from user where  password is @password and username is @username", database);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);

        var result = command.ExecuteReader();
        if (result.Read())
        {
            return (long)result[0];
        }


        //p.Visibility = System.Windows.Visibility.Hidden;
        return -1;
    }

    void CreateUser(string name,string username,string job,float payrate)
    {
        /*  
         *  this function create both a user and employee table
         */
        var command = new SQLiteCommand("insert into employee(name,job_tittle,pay_rate,hire_date,username)" +
                                                    " VALUES(@name,@job,@payrate,@data,@username)", database);
        command.Parameters.AddWithValue("@data", DateTime.Now.ToString("dd - MM - yyyy hh: mm:ss.fff"));
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@job", job);
        command.Parameters.AddWithValue("@payrate", payrate);

        command.ExecuteNonQuery();


        command = new SQLiteCommand("select employee_id from employee where username is @username", database);
        command.Parameters.AddWithValue("@username", username);
        var result = command.ExecuteReader();
        if (result.Read())
        {
            var employee_id = (long)result[0];
            command = new SQLiteCommand("insert into user(employee_id,username)" +
                                                    " VALUES(@employee_id,@username)", database);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@employee_id", employee_id);
            command.ExecuteNonQuery();


        }
    }

    void DeleteEmployee(string employee_id)
    {
        var command = new SQLiteCommand("delete from employee where employee_id is @employee_id", database);
        command.Parameters.AddWithValue("@employee_id", employee_id);
        command.ExecuteNonQuery();
    }

    List<EmployeeDataStruct>? SelectAllEmployees()
    {
        var command = new SQLiteCommand("select * from messages", database);
        var result = command.ExecuteReader();
        List<EmployeeDataStruct> data = new List<EmployeeDataStruct>();
        while (result.Read())
        {
            EmployeeDataStruct i = new EmployeeDataStruct();
            i.employee_id = (long)result[0];
            i.name = (string)result[1];
            i.username = (string)result[2];
            i.job_tittle = (string)result[3];
            i.pay_rate = (long)result[5];
            i.hire_date = (DateTime)result[6];
            data.Add(i);
        }
        return data;

    }


    List<MessageDataStruct> SelectAllMessages()
    {
        var command = new SQLiteCommand("select * from performance_review", database);
        var result = command.ExecuteReader();
        List<MessageDataStruct> data = new List<MessageDataStruct>();
        while (result.Read())
        {
            MessageDataStruct i = new MessageDataStruct();
            i.employee_id = (long)result[0];
            i.reply_message = (string)result[1];
            i.send_message = (string)result[2];
            i.recieve_data = (DateTime)result[3];
            data.Add(i);
        }
        return data;
    }
    List<RequestDataStruct> SelectAll()
    {
        var command = new SQLiteCommand("select * from request", database);
        var result = command.ExecuteReader();
        List<RequestDataStruct> data = new List<RequestDataStruct>(); 
        while (result.Read())
        {
            RequestDataStruct i = new RequestDataStruct();
            i.employee_id = (long)result[0];
            i.request_type = (string)result[1];
            i.leave_status = (string)result[2];
            i.total_leave = (long)result[3];
            data.Add(i);
        }
        return data;
    }
    PreformanceReviewDataStruct? SelectAllPreformanceReviews()
    {
        var command = new SQLiteCommand("select * from roster", database);
        var result = command.ExecuteReader();
        List<PreformanceReviewDataStruct> data;
        if (result.Read())
        {
            PreformanceReviewDataStruct i = new PreformanceReviewDataStruct();
            i.employee_id = (long)result[0];
            i.review_data = (DateTime)result[1];
            i.feedback = (string)result[2];
            i.review_score = (long)result[3];
            //
            return i;
        }
        return null;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        p.Text = SignIn(username.Text,password.Password).ToString();
        CreateUser("new","ddd" + ++i, "other", 3);
    }

    
}