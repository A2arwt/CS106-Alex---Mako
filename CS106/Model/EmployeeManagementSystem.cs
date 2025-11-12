using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS106.Model;

namespace CS106.Model
{
    public class EmployeeManagementSystem
    {
        static Database? database { get; set; }
        public static Database.SQL_EmployeeDataStruct? current_user { get; set; }

        public EmployeeManagementSystem()
        {
            if (database == null)
                database = new Database();

        }

        public void login(string? username, string? password)
        {
            database.login(username.ToLower().Trim(), password.ToLower().Trim());

        }
        public static void InsertRequestData(string request_type, string StartDate, string EndDate)
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

        public static void UpdateRequest(long ID, long request_number, string type, string status, long total, long used, string start, string end)
        {
            Database.SQL_RequestDataStruct request = new SQL_Database.SQL_RequestDataStruct();
            request.employee_id = ID;
            request.request_number = request_number;
            request.request_type = type;
            request.leave_status = status;
            request.total_leave = total;
            request.leave_used = used;
            request.leave_start_date = start;
            request.leave_end_date = end;
            if (database != null)
                database.SQL_UpdateRequest(request);
        }
    }

}
