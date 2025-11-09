using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml.Linq;
using CS106.Model;

namespace CS106.Model
{
    public class SQL_Database
    {
        public static SQLiteConnection? sql_database { get; set; }

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
            public SQL_EmployeeDataStruct(long employee_id, string? username, string? name, string? job_title, double pay_rate, string hire_date, long total_leave,long leave_used)
            {
                this.employee_id = employee_id;
                this.name = name;
                this.username = username;
                this.job_title = job_title;
                this.pay_rate = pay_rate;
                this.hire_date = hire_date;
                this.total_leave = total_leave;
                this.leave_used = leave_used;
            }
        }


        public class SQL_MessageDataStruct
        {
            public long employee_id { get; set; }
            public string? reply_message { get; set; }
            public string? send_message { get; set; }
            public DateTime recieve_data { get; set; }
            public SQL_MessageDataStruct(long employee_id, string? reply_message, string? send_message, DateTime recieve_data)
            {
                this.employee_id = employee_id;
                this.reply_message = reply_message;
                this.send_message = send_message;
                this.recieve_data = recieve_data;
            }
        }


        public class SQL_PreformanceReviewDataStruct
        {
            public long employee_id { get; set; }
            public DateTime review_data { get; set; }
            public string? feedback { get; set; }
            public long review_score { get; set; }

            public SQL_PreformanceReviewDataStruct(long employee_id, DateTime review_data, string? feedback, long review_score)
            {
                this.employee_id = employee_id;
                this.review_data = review_data;
                this.feedback = feedback;
                this.review_score = review_score;
            }
        }

        public class SQL_RequestDataStruct
        {
            public long employee_id { get; set; }
            public string? request_type { get; set; }
            public string? leave_status { get; set; }
            public long total_leave { get; set; }
            public long leave_used { get; set; }
            public string leave_start_date { get; set; }
            public string leave_end_date { get; set; }

            public SQL_RequestDataStruct(long employee_id, string? request_type, string? leave_status, long total_leave, long leave_used, string leave_start_date, string leave_end_date)
            {
                this.employee_id = employee_id;
                this.request_type = request_type;
                this.leave_status = leave_status;
                this.total_leave = total_leave;
                this.leave_used = leave_used;
                this.leave_start_date = leave_start_date;
                this.leave_end_date = leave_end_date;
            }
        }


        public class SQL_RosterDataStruct
        {
            public long employee_id { get; set; }
            public DateTime shift_date { get; set; }
            public float shift_start_time { get; set; }
            public float shift_finish_time { get; set; }
            public SQL_RosterDataStruct(long employee_id, DateTime shift_date, float shift_start_time, float shift_finish_time)
            {
                this.employee_id = employee_id;
                this.shift_date = shift_date;
                this.shift_start_time = shift_start_time;
                this.shift_finish_time = shift_finish_time;
            }
        }


        public class SQL_TrainingReportDataStruct
        {
            public long employee_id { get; set; }
            public string? training_course { get; set; }
            public DateTime data_aquired { get; set; }
            public string? status { get; set; }
            public long leave_used { get; set; }
            public DateTime date_expired { get; set; }

            public SQL_TrainingReportDataStruct(long employee_id, string? training_course, DateTime data_aquired, string? status, long leave_used, DateTime date_expired)
            {
                this.employee_id = employee_id;
                this.training_course = training_course;
                this.data_aquired = data_aquired;
                this.status = status;
                this.leave_used = leave_used;
                this.date_expired = date_expired;
            }
        }

        public class SQL_UserDataStruct
        {
            public long employee_id { get; set; }
            public string? email { get; set; }
            public string? username { get; set; }
            public string? password { get; set; }

            public SQL_UserDataStruct(long employee_id, string? email, string? username, string? password)
            {
                this.employee_id = employee_id;
                this.email = email;
                this.username = username;
                this.password = password;
            }
        }


        public SQL_EmployeeDataStruct? SQL_GetEmployee(string username, string password)
        {
            /*
             * This function returns the employee Struct of the user or null if the user does not exist
             */
            var command = new SQLiteCommand("select employee_id from user where  password is @password and username is @username", sql_database);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            var result = command.ExecuteReader();
            if (result.Read())
            {
                command = new SQLiteCommand("select * from employee where  employee_id is @employee_id", sql_database);
                command.Parameters.AddWithValue("@employee_id", result[0]);
                result = command.ExecuteReader();
                if (result.Read())
                {
                    var employee_id = (long)result["employee_id"];
                    var usernameE = (string)result["username"];
                    var name = (string)result["name"];
                    var job_title = (string)result["job_title"];
                    var pay_rate = (double)result["pay_rate"];
                    var total_leave = (long)result["total_leave"];
                    var leave_used = (long)result["leave_used"];
                    string hire_date = DateTime.Now.ToString();
                    if(result[5] != DBNull.Value)
                        hire_date = (string)result["hire_date"];

                    SQL_EmployeeDataStruct employee = new SQL_EmployeeDataStruct(employee_id, usernameE, name, job_title, pay_rate, hire_date, total_leave, leave_used);
                
                    return employee;
                }
            }
     
                return null;


                //p.Visibility = System.Windows.Visibility.Hidden;

        }

        public void SQL_CreateEmployee(SQL_EmployeeDataStruct data)
        {
            /*  
             *  this function create both a user and employee table
             */
            var command = new SQLiteCommand("insert into employee(name,job_title,pay_rate,hire_date,username)" +
                                                        " VALUES(@name,@job,@payrate,@data,@username)", sql_database);
            command.Parameters.AddWithValue("@data", DateTime.Now.ToString("dd - MM - yyyy hh: mm:ss.fff"));
            command.Parameters.AddWithValue("@name", data.name);
            command.Parameters.AddWithValue("@username", data.username);
            command.Parameters.AddWithValue("@job", data.job_title);
            command.Parameters.AddWithValue("@payrate", data.pay_rate);

            command.ExecuteNonQuery();


            command = new SQLiteCommand("select employee_id from employee where username is @username", sql_database);
            command.Parameters.AddWithValue("@username", data.username);
            var result = command.ExecuteReader();
            if (result.Read())
            {
                var employee_id = (long)result[0];
                command = new SQLiteCommand("insert into user(employee_id,username)" +
                                                        " VALUES(@employee_id,@username)", sql_database);
                command.Parameters.AddWithValue("@username", data.username);
                command.Parameters.AddWithValue("@employee_id", employee_id);
                command.ExecuteNonQuery();


            }
        }

        public void SQL_DeleteEmployee(long employee_id)
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
                SQL_EmployeeDataStruct i = new SQL_EmployeeDataStruct((long)result[0], (string)result[1], (string)result[2], (string)result[3], (float)result[4], (string)result[5], (long)result[6], (long)result[7]);
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
                SQL_MessageDataStruct i = new SQL_MessageDataStruct((long)result[0], (string)result[1], (string)result[2], (DateTime)result[3]);
                data.Add(i);
            }
            return data;
        }
        public List<SQL_PreformanceReviewDataStruct> SQL_SelectAllPreformanceReviews()
        {
            var command = new SQLiteCommand("select * from roster", sql_database);
            var result = command.ExecuteReader();
            List<SQL_PreformanceReviewDataStruct> data = new List<SQL_PreformanceReviewDataStruct>();
            while (result.Read())
            {
                SQL_PreformanceReviewDataStruct i = new SQL_PreformanceReviewDataStruct((long)result[0], (DateTime)result[1], (string)result[2], (long)result[3]);
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
                SQL_RequestDataStruct i = new SQL_RequestDataStruct((long)result[0], (string)result[1], (string)result[2], (long)result[3], (long)result[4], (string)result[5], (string)result[6]);
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
                SQL_RosterDataStruct i = new SQL_RosterDataStruct((long)result[0], (DateTime)result[1], (long)result[2], (long)result[3]);
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
                SQL_TrainingReportDataStruct i = new SQL_TrainingReportDataStruct((long)result[0], (string)result[1], (DateTime)result[2], (string)result[3], (long)result[0], (DateTime)result[4]);
                data.Add(i);
                //long employee_id, string? training_course, DateTime data_aquired, string? status, long leave_used, DateTime date_expired
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
                SQL_UserDataStruct i = new SQL_UserDataStruct((long)result[0], (string)result[1], (string)result[2], (string)result[3]);
                data.Add(i);
            }
            return data;
        }


        public int SQL_UpdateEmployees(SQL_EmployeeDataStruct data)
        {
            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();

            if (result.Read())
            {
                command = new SQLiteCommand("update Employee set name = @name, username = @username, job_title = @job_title, pay_rate = @pay_rate,hire_date = @hire_date" +
                " where employee_id is @employee_id", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@name", data.name);
                command.Parameters.AddWithValue("@username", data.username);
                command.Parameters.AddWithValue("@job_title", data.job_title);
                command.Parameters.AddWithValue("@pay_rate", data.pay_rate);
                command.Parameters.AddWithValue("@hire_date", data.hire_date);
                command.ExecuteNonQuery();
                return 0;
            }
            return -1;
        }


        public int SQL_UpdateMessages(SQL_MessageDataStruct data)
        {
            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();

            if (result.Read())
            {
                command = new SQLiteCommand("update Messages set reply_message = @reply_message, send_message = @send_message, recieve_data = @recieve_data" +
                "where employee_id is @employee_id", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@reply_message", data.reply_message);
                command.Parameters.AddWithValue("@send_message", data.send_message);
                command.Parameters.AddWithValue("@recieve_data", data.recieve_data);
                command.ExecuteNonQuery();
                return 0;
            }
            return -1;
        }
        public int SQL_UpdatePreformanceReviews(SQL_PreformanceReviewDataStruct data)
        {
            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();

            if (result.Read())
            {
                 command = new SQLiteCommand("update preformance_review set review_data = @review_data, feedback = @feedback, review_score = @review_score" +
                "where employee_id is @employee_id", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@review_data", data.review_data);
                command.Parameters.AddWithValue("@feedback", data.feedback);
                command.Parameters.AddWithValue("@review_score", data.review_score);
                command.ExecuteNonQuery();
                return 0;
            }
            return -1;
        }
        public int SQL_UpdateRequest(SQL_RequestDataStruct data)
        {
            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();

            if (result.Read())
            {
                command = new SQLiteCommand("update request set request_type = @request_type, leave_status = @leave_status, total_leave = @total_leave" +
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
                return 0;
            }
            return -1;
        }

        public int SQL_UpdateRoster(SQL_RosterDataStruct data)
        {
            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();

            if (result.Read())
            {
                command = new SQLiteCommand("update roster set shift_date = @shift_date, shift_start_time = @shift_start_time, shift_finish_time = @shift_finish_time" +
                "where employee_id is @employee_id", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@shift_date", data.shift_date);
                command.Parameters.AddWithValue("@shift_start_time", data.shift_start_time);
                command.Parameters.AddWithValue("@shift_finish_time", data.shift_finish_time);
                command.ExecuteNonQuery();
            }
            return -1;
                
        }


        public int SQL_UpdateTrainingReport(SQL_TrainingReportDataStruct data)
        {
            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();

            if(result.Read())
            {
                command = new SQLiteCommand("update training_report set training_course = @training_course, data_aquired = @data_aquired, status = @status," +
                    "date_expired = @date_expired where employee_id is @employee_id", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@training_course", data.training_course);
                command.Parameters.AddWithValue("@data_aquired", data.data_aquired);
                command.Parameters.AddWithValue("@status", data.status);
                command.Parameters.AddWithValue("@date_expired", data.date_expired);
                    command.ExecuteNonQuery();
                return 0;
            }
            return -1;
        }


        public int SQL_UpdateUser(SQL_UserDataStruct data)
        {
            //check if the employee ID has been change. if so returns -1
            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();
            if(result.Read())
            {
                //checks if the username is take. if so doesnt change the username and return 1 or else return 0
                command = new SQLiteCommand("select username from employee where username is @username", sql_database);
                command.Parameters.AddWithValue("@username", data.username);
                result = command.ExecuteReader();
                if (result.Read())
                {
                    command = new SQLiteCommand("update user set email = @email, username = @username, password = @password where employee_id is @employee_id", sql_database);
                    command.Parameters.AddWithValue("@email", data.email);
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    command.Parameters.AddWithValue("@password", data.password);
                    command.ExecuteNonQuery();      
                    return 1;
                }else
                {
                    command = new SQLiteCommand("update user set email = @email, username = @username, password = @password where employee_id is @employee_id", sql_database);
                    command.Parameters.AddWithValue("@email", data.email);
                    command.Parameters.AddWithValue("@username", data.username);
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    command.Parameters.AddWithValue("@password", data.password);
                    command.ExecuteNonQuery();
                    //change the username in employee id so they keep pointing to each other
                    command = new SQLiteCommand("update employee set  username = @username where employee_id is @employee_id", sql_database);
                    command.Parameters.AddWithValue("@username", data.username);
                    command.Parameters.AddWithValue("@employee_id", data.employee_id);
                    command.ExecuteNonQuery();
                    return 0;
                }
            }
            return -1;
        }


        public int SQL_InsertMessageData(SQL_MessageDataStruct data)
        {
            /*  
             *  
             */

            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();
            if (result.Read())
            {
                command = new SQLiteCommand("insert into messages(employee_id,reply_message,send_message,recieve_data)" +
                                                        " VALUES(@employee_id,@reply_message,@send_message,@recieve_data)", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@reply_message", data.reply_message);
                command.Parameters.AddWithValue("@send_message", data.send_message);
                command.Parameters.AddWithValue("@recieve_data", data.recieve_data);

                command.ExecuteNonQuery();
            }
            return -1;


            
        }


        public int SQL_InsertPreformanceReviewData(SQL_PreformanceReviewDataStruct data)
        {
            /*  
             *  
             */

            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();
            if (result.Read())
            {
                command = new SQLiteCommand("insert into preformance_review(employee_id,review_data,feedback,review_score)" +
                                                        " VALUES(@employee_id,@review_data,@feedback,@review_score)", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@review_data", data.review_data);
                command.Parameters.AddWithValue("@feedback", data.feedback);
                command.Parameters.AddWithValue("@review_score", data.review_score);

                command.ExecuteNonQuery();
            }
            return -1;



        }

        public void SQL_InsertRequestData(long employee_id,string request_type,string leave_start_date, string leave_end_date)
        {
            /*  
             *  
             */
            if(employee_id == EmployeeManagementSystem.current_user.employee_id)
            {
                var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
                command.Parameters.AddWithValue("@employee_id", employee_id);
                var result = command.ExecuteReader();
                if (result.Read())
                {
                    command = new SQLiteCommand("insert into request(employee_id,request_type,leave_status,total_leave," +
                                                            "leave_used,leave_start_date,leave_end_date)" +
                                                            " VALUES(@employee_id,@request_type,@leave_status,@total_leave," +
                                                            "@leave_used,@leave_start_date,@leave_end_date)", sql_database);
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
            else
            {
                var command = new SQLiteCommand("select employee_id, total_leave, leave_used from employee where employee_id is @employee_id", sql_database);
                command.Parameters.AddWithValue("@employee_id", employee_id);
                var result = command.ExecuteReader();
                if (result.Read())
                {
                    command = new SQLiteCommand("insert into request(employee_id,request_type,leave_status,total_leave," +
                                                            "leave_used,leave_start_date,leave_end_date)" +
                                                            " VALUES(@employee_id,@request_type,@leave_status,@total_leave," +
                                                            "@leave_used,@leave_start_date,@leave_end_date)", sql_database);
                    command.Parameters.AddWithValue("@employee_id", result[0]);
                    command.Parameters.AddWithValue("@request_type", request_type);
                    command.Parameters.AddWithValue("@leave_status", "pending");
                    command.Parameters.AddWithValue("@total_leave", result[1]);
                    command.Parameters.AddWithValue("@leave_used", result[2]);
                    command.Parameters.AddWithValue("@leave_start_date", leave_start_date);
                    command.Parameters.AddWithValue("@leave_end_date", leave_end_date);

                    command.ExecuteNonQuery();
                }
            }




        }

        public int SQL_InsertRostertData(SQL_RosterDataStruct data)
        {
            /*  
             *  
             */

            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();
            if (result.Read())
            {
                command = new SQLiteCommand("insert into roster(employee_id,shift_date,shift_start_time,shift_finish_time)" +
                                                        " VALUES(@employee_id,@shift_date,@shift_start_time,@shift_finish_time)", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@shift_date", data.shift_date);
                command.Parameters.AddWithValue("@shift_start_time", data.shift_start_time);
                command.Parameters.AddWithValue("@shift_finish_time", data.shift_finish_time);

                command.ExecuteNonQuery();
            }
            return -1;



        }

        public List<SQL_RequestDataStruct> GetAllUserRequest()
        {
            return SQL_SelectAllRequest();
        }
        public List<SQL_RequestDataStruct> GetSQL_Requests(long ID)
        {
            var req = SQL_SelectAllRequest();




            return req;
        }

        public int SQL_InsertTrainingReportData(SQL_TrainingReportDataStruct data)
        {
            /*  
             *  
             */

            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();
            if (result.Read())
            {
                command = new SQLiteCommand("insert into roster(employee_id,training_course,data_aquired,status,date_expired)" +
                                                        " VALUES(@employee_id,@training_course,@data_aquired,@status,@date_expired)", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@training_course", data.training_course);
                command.Parameters.AddWithValue("@data_aquired", data.data_aquired);
                command.Parameters.AddWithValue("@status", data.status);
                command.Parameters.AddWithValue("@date_expired", data.date_expired);

                command.ExecuteNonQuery();
            }
            return -1;



        }

        public int SQL_UserData(SQL_UserDataStruct data)
        {
            /*  
             *  
             */

            var command = new SQLiteCommand("select employee_id from employee where employee_id is @employee_id", sql_database);
            command.Parameters.AddWithValue("@employee_id", data.employee_id);
            var result = command.ExecuteReader();
            if (result.Read())
            {
                command = new SQLiteCommand("insert into roster(employee_id,email,username,password)" +
                                                        " VALUES(@employee_id,@email,@username,@password)", sql_database);
                command.Parameters.AddWithValue("@employee_id", data.employee_id);
                command.Parameters.AddWithValue("@email", data.email);
                command.Parameters.AddWithValue("@username", data.username);
                command.Parameters.AddWithValue("@password", data.password);

                command.ExecuteNonQuery();
            }
            return -1;



        }

    }


    public class Database : SQL_Database
    {
        public static SQL_EmployeeDataStruct? user;
        public Database(string path)
        {
            if(path == null)
            sql_database = new SQLiteConnection("Data Source=database/CS106.db");
            else
                sql_database = new SQLiteConnection(path);
            sql_database.Open();
        }
        public Database()
        {
            sql_database = new SQLiteConnection("Data Source=database/CS106.db");
            sql_database.Open();
        }
        ~Database()
        {
            if(sql_database != null)
            sql_database.Close();
        }
        public SQL_EmployeeDataStruct? login(string username,string password)
        {
            return SQL_GetEmployee(username, password);
        }
        



        // public void static AddToDatatbase operator +(
    }
}


public class  EmployeeManagementSystem
{
    static Database database;
    public static Database.SQL_EmployeeDataStruct current_user;

    public EmployeeManagementSystem()
    {
        if (database == null)
            database = new Database();
    }

    public void login(string? username,string? password)
    {
        current_user = database.login(username.ToLower().Trim(), password.ToLower().Trim());

    }
    public static void InsertRequestData(string request_type,string StartDate,string EndDate)
    {
        database.SQL_InsertRequestData(current_user.employee_id, request_type, StartDate, EndDate);

    }

    public static void InsertRequestData(long employee_id, string request_type, string StartDate, string EndDate)
    {
        database.SQL_InsertRequestData(employee_id, request_type, StartDate, EndDate);

    }

    public static List<Database.SQL_RequestDataStruct> GetRequest()
    {
        
        return database.SQL_SelectAllRequest();
    }


    public static List<Database.SQL_RequestDataStruct> GetRequest(long ID)
    {

        var requestlist = database.SQL_SelectAllRequest();
        List<Database.SQL_RequestDataStruct> request = (List<SQL_Database.SQL_RequestDataStruct>)(from item in requestlist
                                                                                                  where item.employee_id == ID
                                                                                                  select item);
        return request;
    }

    public static void UpdateRequest()
    {
        //Database.SQL_RequestDataStruct 
        //database.SQL_UpdateRequest();
    }
}
