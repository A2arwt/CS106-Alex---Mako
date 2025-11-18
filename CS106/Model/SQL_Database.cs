using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using CS106.Model;


namespace CS106.Model
{
    public class SQL_Database
    {

        public class SQL_EmployeeDataStruct
        {
            public long employee_id { get; set; }
            public string? name { get; set; }
            public string? username { get; set; }
            public string? job_title { get; set; }
            public double pay_rate { get; set; }
            public string hire_date { get; set; }
            public long total_leave { get; set; }
            public long leave_used { get; set; }
        }


        public class SQL_MessageDataStruct
        {
            public long employee_id { get; set; }
            public string? reply_message { get; set; }
            public string? send_message { get; set; }
            public string recieve_data { get; set; }

        }


        public class SQL_PreformanceReviewDataStruct
        {
            public long employee_id { get; set; }
            public string review_data { get; set; }
            public string? feedback { get; set; }
            public long review_score { get; set; }


        }

        public class SQL_RequestDataStruct
        {
            public long employee_id { get; set; }
            public long request_number { get; set; }
            public string? request_type { get; set; }
            public string? leave_status { get; set; }
            public long total_leave { get; set; }
            public long leave_used { get; set; }
            public string leave_start_date { get; set; }
            public string leave_end_date { get; set; }


        }


        public class SQL_RosterDataStruct
        {
            public long employee_id { get; set; }
            public long roster_id { get; set; }
            public string? shift_date { get; set; }
            public double shift_start_time { get; set; }
            public double shift_finish_time { get; set; }

        }


        public class SQL_TrainingReportDataStruct
        {
            public long employee_id { get; set; }
            public string? training_course { get; set; }
            public string data_aquired { get; set; }
            public string? status { get; set; }
            public long leave_used { get; set; }
            public string date_expired { get; set; }


        }

        public class SQL_UserDataStruct
        {
            public long employee_id { get; set; }
            public string? email { get; set; }
            public string? username { get; set; }
            public string? password { get; set; }

        }


        public void SQL_GetEmployee(string username, string password)
        {
            /*
             * This function returns the employee Struct of the user or null if the user does not exist
             */

            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                long ID = -1;
                using (var command = new SQLiteCommand("select employee_id from user where  password = @password and username = @username", sql_database))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            ID = (long)result[0];
                        }
                    }
                }

                using (var command = new SQLiteCommand("select * from employee where  employee_id = @employee_id", sql_database))
                {
                    if (ID == -1)
                        return;
                    command.Parameters.AddWithValue("@employee_id", ID);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            EmployeeManagementSystem.current_user = new SQL_EmployeeDataStruct();
                            EmployeeManagementSystem.current_user.employee_id = (long)result["employee_id"];
                            EmployeeManagementSystem.current_user.username = (string)result["username"];
                            EmployeeManagementSystem.current_user.name = (string)result["name"];
                            EmployeeManagementSystem.current_user.job_title = (string)result["job_title"];
                            EmployeeManagementSystem.current_user.pay_rate = (double)result["pay_rate"];
                            EmployeeManagementSystem.current_user.total_leave = (long)result["total_leave"];
                            EmployeeManagementSystem.current_user.leave_used = (long)result["leave_used"];
                            EmployeeManagementSystem.current_user.hire_date = DateTime.Now.ToString();
                            if (result[5] != DBNull.Value)
                                EmployeeManagementSystem.current_user.hire_date = (string)result["hire_date"];
                        }
                    }

                }

            }

        }

        public void SQL_CreateEmployee(SQL_EmployeeDataStruct data)
        {
            /*  
             *  this function create both a user and employee table
             */

            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();

                using (var command = new SQLiteCommand("insert into employee(name,job_title,pay_rate,hire_date,username)" +
                                                            " VALUES(@name,@job,@payrate,@data,@username)", sql_database))
                {
                    command.Parameters.AddWithValue("@data", DateTime.Now.ToString("dd - MM - yyyy hh: mm:ss.fff"));
                    command.Parameters.AddWithValue("@name", data.name);
                    command.Parameters.AddWithValue("@username", data.username);
                    command.Parameters.AddWithValue("@job", data.job_title);
                    command.Parameters.AddWithValue("@payrate", data.pay_rate);

                    command.ExecuteNonQuery();

                }
                long employee_id = -1;
                using (var command = new SQLiteCommand("select employee_id from employee where username = @username", sql_database))
                {
                    command.Parameters.AddWithValue("@username", data.username);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            employee_id = (long)result["employee_id"];
                        }
                    }

                }

                using (var command = new SQLiteCommand("insert into user(employee_id,username) VALUES(@employee_id,@username)", sql_database))
                {
                    command.Parameters.AddWithValue("@username", data.username);
                    command.Parameters.AddWithValue("@employee_id", employee_id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SQL_DeleteEmployee(long employee_id)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("delete  from employee where employee_id = @employee_id", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", employee_id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SQL_DeleteRoster(long roster_id)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("delete  from roster where roster_id = @roster_id", sql_database))
                {
                    command.Parameters.AddWithValue("@roster_id", roster_id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<SQL_EmployeeDataStruct>? SQL_SelectAllEmployees()
        {
            List<SQL_EmployeeDataStruct> data = new List<SQL_EmployeeDataStruct>();

            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();

                using (var command = new SQLiteCommand("select * from Employee", sql_database))
                {
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            SQL_EmployeeDataStruct i = new SQL_EmployeeDataStruct();
                            i.employee_id = (long)result["employee_id"];
                            i.username = (string)result["username"];
                            i.name = (string)result["name"];
                            i.job_title = (string)result["job_title"];
                            i.pay_rate = (double)result["pay_rate"];
                            i.total_leave = (long)result["total_leave"];
                            i.leave_used = (long)result["leave_used"];
                            i.hire_date = (string)result["hire_date"];
                            data.Add(i);
                        }
                    }
                }
            }
            return data;
        }


        public List<SQL_MessageDataStruct> SQL_SelectAllMessages()
        {
            List<SQL_MessageDataStruct> data = new List<SQL_MessageDataStruct>();

            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("select * from Messages ", sql_database))
                {
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            SQL_MessageDataStruct i = new SQL_MessageDataStruct();
                            i.employee_id = (long)result["employee_id"];
                            i.recieve_data = (string)result["recieve_data"];
                            i.reply_message = (string)result["reply_message"];
                            i.send_message = (string)result["send_message"];
                            data.Add(i);
                        }
                    }

                }
            }
            return data;
        }
        public List<SQL_PreformanceReviewDataStruct> SQL_SelectAllPreformanceReviews()
        {
            List<SQL_PreformanceReviewDataStruct> data = new List<SQL_PreformanceReviewDataStruct>();
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("select * from roster", sql_database))
                {
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            SQL_PreformanceReviewDataStruct i = new SQL_PreformanceReviewDataStruct();
                            i.employee_id = (long)result["employee_id"];
                            i.review_data = (string)result["review_data"];
                            i.feedback = (string)result["feedback"];
                            i.review_score = (long)result["review_score"];
                            data.Add(i);
                        }
                    }

                }
            }
            return data;
        }
        public List<SQL_RequestDataStruct> SQL_SelectAllRequest()
        {
            List<SQL_RequestDataStruct> data = new List<SQL_RequestDataStruct>();
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("select * from request", sql_database))
                {
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            SQL_RequestDataStruct i = new SQL_RequestDataStruct();
                            i.employee_id = (long)result["employee_id"];
                            i.request_number = (long)result["request_number"];
                            i.request_type = (string)result["request_type"];
                            i.leave_status = (string)result["leave_status"];
                            i.total_leave = (long)result["total_leave"];
                            i.leave_used = (long)result["leave_used"];
                            i.leave_start_date = (string)result["leave_start_date"];
                            i.leave_end_date = (string)result["leave_end_date"];
                            data.Add(i);
                        }
                    }

                }
            }
            return data;
        }


        public List<SQL_RosterDataStruct> SQL_SelectAllRoster()
        {
            List<SQL_RosterDataStruct> data = new List<SQL_RosterDataStruct>();

            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("select * from roster", sql_database))
                {
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            SQL_RosterDataStruct i = new SQL_RosterDataStruct();
                            i.employee_id = (long)result["employee_id"];
                            i.roster_id = (long)result["roster_id"];
                            i.shift_date = (string)result["shift_data"];
                            i.shift_start_time = (double)result["shift_start_time"];
                            i.shift_finish_time = (double)result["shift_finish_time"];
                            data.Add(i);
                        }
                    }

                }

            }
            return data;
        }


        public List<SQL_TrainingReportDataStruct> SQL_SelectAllTrainingReport()
        {
            List<SQL_TrainingReportDataStruct> data = new List<SQL_TrainingReportDataStruct>();
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("select * from request", sql_database))
                {
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            SQL_TrainingReportDataStruct i = new SQL_TrainingReportDataStruct();
                            i.employee_id = (long)result["employee_id"];
                            i.training_course = (string)result["training_course"];
                            i.data_aquired = (string)result["data_aquired"];
                            i.status = (string)result["status"];
                            i.date_expired = (string)result["date_expired"];
                            data.Add(i);
                        }
                    }

                }
            }
            return data;
        }


        public List<SQL_UserDataStruct> SQL_SelectAllUser()
        {
            List<SQL_UserDataStruct> data = new List<SQL_UserDataStruct>();
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("select * from user", sql_database))
                {
                    using (var result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            SQL_UserDataStruct i = new SQL_UserDataStruct();
                            i.employee_id = (long)result["employee_id"];
                            i.email = (string)result["email"];
                            i.username = (string)result["username"];
                            i.password = (string)result["password"];
                            data.Add(i);
                        }
                    }

                }

            }
            return data;
        }


        public void SQL_UpdateEmployees(SQL_EmployeeDataStruct data)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                bool Is_user_real = false;
                using (var command = new SQLiteCommand("select employee_id from employee where employee_id = @employee_id", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            Is_user_real = true;
                        }
                    }

                }

                if (Is_user_real)
                    using (var command = new SQLiteCommand("select employee_id from employee where employee_id = @employee_id", sql_database))
                    {
                        command.CommandText = "update Employee set name = @name, username = @username, job_title = @job_title, pay_rate = @pay_rate,hire_date = @hire_date" +
                            " where employee_id = @employee_id";
                        command.Parameters.AddWithValue("@employee_id", data.employee_id);
                        command.Parameters.AddWithValue("@name", data.name);
                        command.Parameters.AddWithValue("@username", data.username);
                        command.Parameters.AddWithValue("@job_title", data.job_title);
                        command.Parameters.AddWithValue("@pay_rate", data.pay_rate);
                        command.Parameters.AddWithValue("@hire_date", data.hire_date);
                        command.ExecuteNonQuery();
                    }
            }
        }


        public void SQL_UpdateMessages(SQL_MessageDataStruct data)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                bool Is_user_real = false;
                using (var command = new SQLiteCommand("select employee_id from employee where employee_id = @employee_id", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            Is_user_real = true;

                        }
                    }

                }
                if (Is_user_real)
                    using (var command = new SQLiteCommand("update Messages set reply_message = @reply_message, send_message = @send_message, recieve_data = @recieve_data" +
                            "where employee_id = @employee_id", sql_database))
                    {
                        command.Parameters.AddWithValue("@employee_id", data.employee_id);
                        command.Parameters.AddWithValue("@reply_message", data.reply_message);
                        command.Parameters.AddWithValue("@send_message", data.send_message);
                        command.Parameters.AddWithValue("@recieve_data", data.recieve_data);
                        command.ExecuteNonQuery();
                    }


            }
        }
        public void SQL_UpdatePreformanceReviews(SQL_PreformanceReviewDataStruct data)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                bool Is_user_real = false;
                using (var command = new SQLiteCommand("select employee_id from employee where employee_id = @employee_id", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            Is_user_real = true;
                        }
                    }
                }
                if (Is_user_real)
                    using (var command = new SQLiteCommand("update preformance_review set review_data = @review_data, feedback = @feedback, review_score = @review_score\" +\r\n                       \"where employee_id = @employee_id", sql_database))
                    {
                        command.Parameters.AddWithValue("@employee_id", data.employee_id);
                        command.Parameters.AddWithValue("@review_data", data.review_data);
                        command.Parameters.AddWithValue("@feedback", data.feedback);
                        command.Parameters.AddWithValue("@review_score", data.review_score);
                        command.ExecuteNonQuery();
                    }
            }
        }
        public void SQL_UpdateRequest(SQL_RequestDataStruct data)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                bool Is_user_real = false;
                using (var command = new SQLiteCommand("select employee_id from employee where employee_id = @employee_id", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            Is_user_real = true;
                        }
                    }

                }

                if (Is_user_real)
                    using (var command = new SQLiteCommand("update request " +
                        "set request_type = @request_type, leave_status = @leave_status, total_leave = @total_leave," +
                        "leave_used = @leave_used, leave_start_date = @leave_start_date,leave_end_date = @leave_end_date " +
                        "where employee_id = @employee_id and request_number = @request_number", sql_database))
                    {
                        command.Parameters.AddWithValue("@employee_id", data.employee_id);
                        command.Parameters.AddWithValue("@request_number", data.request_number);
                        command.Parameters.AddWithValue("@request_type", data.request_type);
                        command.Parameters.AddWithValue("@leave_status", data.leave_status);
                        command.Parameters.AddWithValue("@total_leave", data.total_leave);
                        command.Parameters.AddWithValue("@leave_used", data.leave_used);
                        command.Parameters.AddWithValue("@leave_start_date", data.leave_start_date);
                        command.Parameters.AddWithValue("@leave_end_date", data.leave_end_date);
                        command.ExecuteNonQuery();
                    }
            }
        }

        public void SQL_UpdateRoster(SQL_RosterDataStruct data)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                bool Is_user_real = false;
                using (var command = new SQLiteCommand("select employee_id from roster where roster_id = @roster_id", sql_database))
                {
                    command.Parameters.AddWithValue("@roster_id", data.roster_id);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            Is_user_real = true;
                        }
                    }
                }

                if (Is_user_real)
                    using (var command = new SQLiteCommand("update roster set shift_data = @shift_date, shift_start_time = @shift_start_time, shift_finish_time = @shift_finish_time where roster_id = @roster_id", sql_database))
                    {
                        command.Parameters.AddWithValue("@roster_id", data.roster_id);
                        command.Parameters.AddWithValue("@shift_date", data.shift_date);
                        command.Parameters.AddWithValue("@shift_start_time", data.shift_start_time);
                        command.Parameters.AddWithValue("@shift_finish_time", data.shift_finish_time);
                        command.ExecuteNonQuery();
                    }
            }
        }


        public void SQL_UpdateTrainingReport(SQL_TrainingReportDataStruct data)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                bool Is_user_real = true;
                using (var command = new SQLiteCommand("select employee_id from employee where employee_id = @employee_id", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {

                        }
                    }

                }
                if (Is_user_real)
                    using (var command = new SQLiteCommand("update training_report set training_course = @training_course, data_aquired = @data_aquired, status = @status,\" +\r\n                                \"date_expired = @date_expired where employee_id = @employee_id", sql_database))
                    {
                        command.Parameters.AddWithValue("@employee_id", data.employee_id);
                        command.Parameters.AddWithValue("@training_course", data.training_course);
                        command.Parameters.AddWithValue("@data_aquired", data.data_aquired);
                        command.Parameters.AddWithValue("@status", data.status);
                        command.Parameters.AddWithValue("@date_expired", data.date_expired);
                        command.ExecuteNonQuery();
                    }


            }
        }


        public void SQL_UpdateUser(SQL_UserDataStruct data)
        {
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                bool Has_username_changed = true;
                bool id_exist = false;
                using (var command = new SQLiteCommand("select  username from employee where employee_id = @employee_id", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            //check if the employee ID has been change. if so returns -1
                            //checks if the username is take. if so doesnt change the username and return 1 or else return 0
                            if ((string)result["username"] == data.username)
                            {
                                Has_username_changed = false;
                            }
                            id_exist = true;

                        }
                    }
                    
                }


                if(id_exist)
                {
                    if (Has_username_changed == false)
                    {
                        using (var command = new SQLiteCommand("update user set email = @email, username = @username, password = @password where employee_id = @employee_id", sql_database))
                        {
                            command.Parameters.AddWithValue("@email", data.email);
                            command.Parameters.AddWithValue("@employee_id", data.employee_id);
                            command.Parameters.AddWithValue("@password", data.password);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (var command = new SQLiteCommand("select * from user where username = @username", sql_database))
                        {
                            bool does_username_exist = false;
                            command.Parameters.AddWithValue("@username", data.username);
                            using (var result = command.ExecuteReader())
                            {
                                if (result.Read())
                                {
                                    does_username_exist = true;
                                }
                            }
                            if(does_username_exist == false)
                            {

                            }
                        }
                        using (var command = new SQLiteCommand("select username from employee where username = @username", sql_database))
                        {
                            command.CommandText = "update user set email = @email, username = @username, password = @password where employee_id = @employee_id";
                            command.Parameters.AddWithValue("@email", data.email);
                            command.Parameters.AddWithValue("@username", data.username);
                            command.Parameters.AddWithValue("@employee_id", data.employee_id);
                            command.Parameters.AddWithValue("@password", data.password);
                            command.ExecuteNonQuery();
                            //change the username in employee id so they keep pointing to each other
                            command.CommandText = "update employee set  username = @username where employee_id = @employee_id";
                            command.Parameters.AddWithValue("@username", data.username);
                            command.Parameters.AddWithValue("@employee_id", data.employee_id);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                
            }
        }


        public void SQL_InsertMessageData(SQL_MessageDataStruct data)
        {
            /*  
             *  
             */
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("insert into messages(employee_id,reply_message,send_message,recieve_data)" +
                                                            " VALUES(@employee_id,@reply_message,@send_message,@recieve_data)", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    command.Parameters.AddWithValue("@reply_message", data.reply_message);
                    command.Parameters.AddWithValue("@send_message", data.send_message);
                    command.Parameters.AddWithValue("@recieve_data", data.recieve_data);
                    command.ExecuteNonQuery();
                }

            }
        }


        public void SQL_InsertPreformanceReviewData(SQL_PreformanceReviewDataStruct data)
        {
            /*  
             *  
             */
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();

                using (var command = new SQLiteCommand("insert into preformance_review(employee_id,review_data,feedback,review_score)" +
                                                            " VALUES(@employee_id,@review_data,@feedback,@review_score)", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    command.Parameters.AddWithValue("@review_data", data.review_data);
                    command.Parameters.AddWithValue("@feedback", data.feedback);
                    command.Parameters.AddWithValue("@review_score", data.review_score);

                    command.ExecuteNonQuery();
                }

            }
        }

        public void SQL_InsertRequestData(long employee_id, string request_type, string leave_start_date, string leave_end_date)
        {
            /*  
             *  i think the code below is deuplicated, but i don't care because it works.... i think
             */

            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("insert into request(employee_id,request_type,leave_status,total_leave," +
                                                                      "leave_used,leave_start_date,leave_end_date)" +
                                                                      " VALUES(@employee_id,@request_type,@leave_status,@total_leave," +
                                                                      "@leave_used,@leave_start_date,@leave_end_date)", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", employee_id);
                    command.Parameters.AddWithValue("@request_type", request_type);
                    command.Parameters.AddWithValue("@leave_status", "pending");
                    command.Parameters.AddWithValue("@total_leave", EmployeeManagementSystem.current_user.total_leave);
                    command.Parameters.AddWithValue("@leave_used", EmployeeManagementSystem.current_user.leave_used);
                    command.Parameters.AddWithValue("@leave_start_date", leave_start_date);
                    command.Parameters.AddWithValue("@leave_end_date", leave_end_date);
                    command.ExecuteNonQuery();
                }

            }
        }

        public void SQL_InsertRostertData(SQL_RosterDataStruct data)
        {
            /*  
             *  
             */

            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("insert into roster(employee_id,shift_data,shift_start_time,shift_finish_time)" +
                                                                " VALUES(@employee_id,@shift_date,@shift_start_time,@shift_finish_time)", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    command.Parameters.AddWithValue("@shift_date", data.shift_date);
                    command.Parameters.AddWithValue("@shift_start_time", data.shift_start_time);
                    command.Parameters.AddWithValue("@shift_finish_time", data.shift_finish_time);
                    command.ExecuteNonQuery();
                }

            }
        }



        public void SQL_InsertTrainingReportData(SQL_TrainingReportDataStruct data)
        {
            /*  
             *  
             */
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("insert into training_report(employee_id,training_course,data_aquired,status,date_expired)" +
                                                                " VALUES(@employee_id,@training_course,@data_aquired,@status,@date_expired)", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    command.Parameters.AddWithValue("@training_course", data.training_course);
                    command.Parameters.AddWithValue("@data_aquired", data.data_aquired);
                    command.Parameters.AddWithValue("@status", data.status);
                    command.Parameters.AddWithValue("@date_expired", data.date_expired);
                    command.ExecuteNonQuery();
                }

            }
        }

        public void SQL_InsertUserData(SQL_UserDataStruct data)
        {
            /*  
             *  
             */
            using (SQLiteConnection sql_database = new SQLiteConnection("Data Source=database/CS106.db"))
            {
                sql_database.Open();
                using (var command = new SQLiteCommand("insert into user(employee_id,email,username,password)" +
                                                                " VALUES(@employee_id,@email,@username,@password)", sql_database))
                {
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    command.Parameters.AddWithValue("@email", data.email);
                    command.Parameters.AddWithValue("@username", data.username);
                    command.Parameters.AddWithValue("@password", data.password);
                    command.ExecuteNonQuery();
                }
            }
        }
    }


}
