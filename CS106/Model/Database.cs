using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CS106.Model
{
    public class Database
    {
        public SQLiteConnection? sql_database { get; set; }

        public class SQL_EmployeeDataStruct
        {
            public long employee_id { get; set; }
            public string? name { get; set; }
            public string? username { get; set; }
            public string? job_title { get; set; }
            public long pay_rate { get; set; }
            public DateTime hire_date { get; set; }
        }


        public class SQL_MessageDataStruct
        {
            public long employee_id { get; set; }
            public string? reply_message { get; set; }
            public string? send_message { get; set; }
            public DateTime recieve_data { get; set; }
        }


        public class SQL_PreformanceReviewDataStruct
        {
            public long employee_id { get; set; }
            public DateTime review_data { get; set; }
            public string? feedback { get; set; }
            public long review_score { get; set; }
        }

        public class SQL_RequestDataStruct
        {
            public long employee_id { get; set; }
            public string? request_type { get; set; }
            public string? leave_status { get; set; }
            public long total_leave { get; set; }
            public long leave_used { get; set; }
            public DateTime leave_start_date { get; set; }
            public DateTime leave_end_date { get; set; }
        }


        public class SQL_RosterDataStruct
        {
            public long employee_id { get; set; }
            public DateTime shift_date { get; set; }
            public float shift_start_time { get; set; }
            public float shift_finish_time { get; set; }
        }


        public class SQL_TrainingReportDataStruct
        {
            public long employee_id { get; set; }
            public string? training_course { get; set; }
            public DateTime data_aquired { get; set; }
            public string? status { get; set; }
            public long leave_used { get; set; }
            public DateTime date_expired { get; set; }
        }

        public class SQL_UserDataStruct
        {
            public long employee_id { get; set; }
            public string? email { get; set; }
            public string? username { get; set; }
            public string? password { get; set; }
        }


        public long SQL_GetUserEmployeeID(string username, string password)
        {
            /*
             * This function returns the employee ID of the user or -1 if the user does not exist
             */
            var command = new SQLiteCommand("select employee_id from user where  password is @password and username is @username", sql_database);
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

        public void SQL_CreateEmployee(string name, string username, string job, float payrate)
        {
            /*  
             *  this function create both a user and employee table
             */
            var command = new SQLiteCommand("insert into employee(name,job_title,pay_rate,hire_date,username)" +
                                                        " VALUES(@name,@job,@payrate,@data,@username)", sql_database);
            command.Parameters.AddWithValue("@data", DateTime.Now.ToString("dd - MM - yyyy hh: mm:ss.fff"));
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@job", job);
            command.Parameters.AddWithValue("@payrate", payrate);

            command.ExecuteNonQuery();


            command = new SQLiteCommand("select employee_id from employee where username is @username", sql_database);
            command.Parameters.AddWithValue("@username", username);
            var result = command.ExecuteReader();
            if (result.Read())
            {
                var employee_id = (long)result[0];
                command = new SQLiteCommand("insert into user(employee_id,username)" +
                                                        " VALUES(@employee_id,@username)", sql_database);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@employee_id", employee_id);
                command.ExecuteNonQuery();


            }
        }

        public void SQL_DeleteEmployee(string employee_id)
        {
            var command = new SQLiteCommand("delete from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", employee_id);
            command.ExecuteNonQuery();
        }

        public  List<SQL_EmployeeDataStruct>? SQL_SelectAllEmployees()
        {
            var command = new SQLiteCommand("select * from Employee", sql_database);
            var result = command.ExecuteReader();
            List<SQL_EmployeeDataStruct> data = new List<SQL_EmployeeDataStruct>();
            while (result.Read())
            {
                SQL_EmployeeDataStruct i = new SQL_EmployeeDataStruct();
                i.employee_id = (long)result[0];
                i.name = (string)result[1];
                i.username = (string)result[2];
                i.job_title = (string)result[3];
                i.pay_rate = (long)result[5];
                i.hire_date = (DateTime)result[6];
                data.Add(i);
            }
            return data;

        }


        public List<SQL_MessageDataStruct> SQL_SelectAllMessages()
        {
            var command = new SQLiteCommand("select * from Messages ", sql_database);
            var result = command.ExecuteReader();
            List<SQL_MessageDataStruct> data = new List<SQL_MessageDataStruct>();
            while (result.Read())
            {
                SQL_MessageDataStruct i = new SQL_MessageDataStruct();
                i.employee_id = (long)result[0];
                i.reply_message = (string)result[1];
                i.send_message = (string)result[2];
                i.recieve_data = (DateTime)result[3];
                data.Add(i);
            }//performance_review
            return data;
        }
        public List<SQL_PreformanceReviewDataStruct> SQL_SelectAllPreformanceReviews()
        {
            var command = new SQLiteCommand("select * from roster", sql_database);
            var result = command.ExecuteReader();
            List<SQL_PreformanceReviewDataStruct> data = new List<SQL_PreformanceReviewDataStruct>();
            while (result.Read())
            {
                SQL_PreformanceReviewDataStruct i = new SQL_PreformanceReviewDataStruct();
                i.employee_id = (long)result[0];
                i.review_data = (DateTime)result[1];
                i.feedback = (string)result[2];
                i.review_score = (long)result[3];
                //
                data.Add(i);
            }
            return data;
        }
        public List<SQL_RequestDataStruct> SQL_SelectAllRequest()
        {
            var command = new SQLiteCommand("select * from request", sql_database);
            var result = command.ExecuteReader();
            List<SQL_RequestDataStruct> data = new List<SQL_RequestDataStruct>();
            while (result.Read())
            {
                SQL_RequestDataStruct i = new SQL_RequestDataStruct();
                i.employee_id = (long)result[0];
                i.request_type = (string)result[1];
                i.leave_status = (string)result[2];
                i.total_leave = (long)result[3];
                data.Add(i);
            }
            return data;
        }


        public List<SQL_RosterDataStruct> SQL_SelectAllRoster()
        {
            var command = new SQLiteCommand("select * from request", sql_database);
            var result = command.ExecuteReader();
            List<SQL_RosterDataStruct> data = new List<SQL_RosterDataStruct>();
            while (result.Read())
            {
                SQL_RosterDataStruct i = new SQL_RosterDataStruct();
                i.employee_id = (long)result[0];
                i.shift_date = (DateTime)result[1];
                i.shift_start_time = (long)result[2];
                i.shift_finish_time = (long)result[3];
                data.Add(i);
            }
            return data;
        }


        public List<SQL_TrainingReportDataStruct> SQL_SelectAllTrainingReport()
        {
            var command = new SQLiteCommand("select * from request", sql_database);
            var result = command.ExecuteReader();
            List<SQL_TrainingReportDataStruct> data = new List<SQL_TrainingReportDataStruct>();
            while (result.Read())
            {
                SQL_TrainingReportDataStruct i = new SQL_TrainingReportDataStruct();
                i.employee_id = (long)result[0];
                i.training_course = (string)result[1];
                i.data_aquired = (DateTime)result[2];
                i.status = (string)result[3];
                i.date_expired = (DateTime)result[4];
                data.Add(i);
            }
            return data;
        }


        public List<SQL_UserDataStruct> SQL_SelectAllUser()
        {
            var command = new SQLiteCommand("select * from request", sql_database);
            var result = command.ExecuteReader();
            List<SQL_UserDataStruct> data = new List<SQL_UserDataStruct>();
            while (result.Read())
            {
                SQL_UserDataStruct i = new SQL_UserDataStruct();
                i.employee_id = (long)result[0];
                i.email = (string)result[1];
                i.username = (string)result[2];
                i.password = (string)result[3];
                data.Add(i);
            }
            return data;
        }


        public void SQL_UpdateEmployees(SQL_EmployeeDataStruct data)
        {
            var command = new SQLiteCommand("update Employee set name = @name, username = @username, job_title = @job_title, pay_rate = @pay_rate,hire_date = @hire_date" +
                " where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            command.Parameters.AddWithValue("@name", data.name);
            command.Parameters.AddWithValue("@username", data.username);
            command.Parameters.AddWithValue("@job_title", data.job_title);
            command.Parameters.AddWithValue("@pay_rate", data.pay_rate);
            command.Parameters.AddWithValue("@hire_date", data.hire_date);
            command.ExecuteNonQuery();
        }


        public void SQL_UpdateMessages(SQL_MessageDataStruct data)
        {

            var command = new SQLiteCommand("update Employee set reply_message = @reply_message, send_message = @send_message, recieve_data = @recieve_data" +
                "where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            command.Parameters.AddWithValue("@reply_message", data.reply_message);
            command.Parameters.AddWithValue("@send_message", data.send_message);
            command.Parameters.AddWithValue("@recieve_data", data.recieve_data);
            command.ExecuteNonQuery();
        }
        public void SQL_UpdatePreformanceReviews(SQL_PreformanceReviewDataStruct data)
        {
            var command = new SQLiteCommand("update Employee set review_data = @review_data, feedback = @feedback, review_score = @review_score" +
                "where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            command.Parameters.AddWithValue("@review_data", data.review_data);
            command.Parameters.AddWithValue("@feedback", data.feedback);
            command.Parameters.AddWithValue("@review_score", data.review_score);
            command.ExecuteNonQuery();
        }
        public void SQL_UpdateRequest(SQL_RequestDataStruct data)
        {
            var command = new SQLiteCommand("update Employee set request_type = @request_type, leave_status = @leave_status, total_leave = @total_leave" +
                "leave_used = @leave_used, leave_start_date = @leave_start_date,leave_end_date = @leave_end_date" +
                "where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            command.Parameters.AddWithValue("@request_type", data.request_type);
            command.Parameters.AddWithValue("@leave_status", data.leave_status);
            command.Parameters.AddWithValue("@total_leave", data.total_leave);
            command.Parameters.AddWithValue("@leave_used", data.leave_used);
            command.Parameters.AddWithValue("@leave_start_date", data.leave_start_date);
            command.Parameters.AddWithValue("@leave_end_date", data.leave_end_date);
            command.ExecuteNonQuery();
        }

        public void SQL_UpdateRoster(SQL_RosterDataStruct data)
        {
            var command = new SQLiteCommand("update Employee set shift_date = @shift_date, shift_start_time = @shift_start_time, shift_finish_time = @shift_finish_time" +
                "where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            command.Parameters.AddWithValue("@shift_date", data.shift_date);
            command.Parameters.AddWithValue("@shift_start_time", data.shift_start_time);
            command.Parameters.AddWithValue("@shift_finish_time", data.shift_finish_time);
            command.ExecuteNonQuery();
        }


        public void SQL_UpdateTrainingReport(SQL_TrainingReportDataStruct data)
        {
            var command = new SQLiteCommand("update Employee set training_course = @training_course, data_aquired = @data_aquired, status = @status," +
                "date_expired = @date_expired where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            command.Parameters.AddWithValue("@training_course", data.training_course);
            command.Parameters.AddWithValue("@data_aquired", data.data_aquired);
            command.Parameters.AddWithValue("@status", data.status);
            command.Parameters.AddWithValue("@date_expired", data.date_expired);
            command.ExecuteNonQuery();
        }


        public void SQL_SelectAllUser(SQL_UserDataStruct data)
        {
#warning add error checking here
            if (SQL_GetUserEmployeeID(data.username, data.password) != -1)
                return;
            var command = new SQLiteCommand("update Employee set email = @email, username = @username, password = @password," +
                "where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            command.Parameters.AddWithValue("@email", data.email);
            command.Parameters.AddWithValue("@username", data.username);
            command.Parameters.AddWithValue("@password", data.password);
            command.ExecuteNonQuery();
        }




    }
}
