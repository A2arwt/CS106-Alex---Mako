using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS106.Model;
using static CS106.Model.SQL_Database;

namespace CS106.Model
{
    public class EmployeeManagementSystem
    {
        static Database? database { get; set; }
        public static Database.SQL_EmployeeDataStruct? current_user { get; set; }
        public static bool is_admin = false;

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

        public static List<SQL_RequestDataStruct> GetRequest()
        {

            return database.SQL_SelectAllRequest();
        }


        public static List<SQL_RequestDataStruct> GetRequest(long ID)
        {

            var requestlist = database.SQL_SelectAllRequest();
            List<SQL_RequestDataStruct> request = (List<SQL_RequestDataStruct>)(from item in requestlist
                                                                                                      where item.employee_id == ID
                                                                                                      select item);
            return request;
        }

        public static List<SQL_RosterDataStruct> GetRoster()
        {
            return database.SQL_SelectAllRoster();
        }

        public static List<SQL_RosterDataStruct> GetRoster(long ID)
        {
            var requestlist = database.SQL_SelectAllRoster();
            List<SQL_RosterDataStruct> request = (List<SQL_RosterDataStruct>)(from item in requestlist
                                                                                                    where item.employee_id == ID
                                                                                                    select item);
            return request;
        }

        public static void UpdateRequest(long ID, long request_number, string type, string status, long total, long used, string start, string end)
        {
            SQL_RequestDataStruct request = new SQL_RequestDataStruct();
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

        public static void AddRoster(long employee_id, string shift_date, double shift_start_time, double shift_finish_time)
        {
            SQL_RosterDataStruct data = new SQL_RosterDataStruct();
            data.employee_id = employee_id;
            data.shift_date = shift_date;
            data.shift_start_time = shift_start_time;
            data.shift_finish_time = shift_finish_time;
            database.SQL_InsertRostertData(data);
        }
        public static void UpdateRoster(long roster_id, string shift_date, double shift_start_time, double shift_finish_time)
        {
            SQL_RosterDataStruct data = new SQL_RosterDataStruct();
            data.roster_id = roster_id;
            data.shift_date = shift_date;
            data.shift_start_time = shift_start_time;
            data.shift_finish_time = shift_finish_time;
            database.SQL_UpdateRoster(data);
        }


        public static void DeleteRoster(long roster_id)
        {
            database.SQL_DeleteRoster(roster_id);
        }


        public static List<SQL_EmployeeDataStruct> GetEmployee()
        {
            return database.SQL_SelectAllEmployees();
        }
        public static List<SQL_EmployeeDataStruct>? GetEmployee(long ID)
        {
            var requestlist = database.SQL_SelectAllEmployees();
            var request = (from item in requestlist
                           where item.employee_id == ID
                           select item);
            return requestlist;
        }

        public static SQL_UserDataStruct GetUser(long ID)
        {
            return database.GetUser(ID);
        }
        public static List<SQL_UserDataStruct> GetUsers()
        {
            return database.GetUser();
        }
        public static void UpdateUsers(SQL_UserDataStruct data)
        {
             database.up;
        }

    }

}
