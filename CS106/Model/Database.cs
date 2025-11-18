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

    public class Database : SQL_Database
    {


        public void login(string username, string password)
        {
            SQL_GetEmployee(username, password);
        }

        public List<SQL_RequestDataStruct> GetAllUserRequest()
        {
            return SQL_SelectAllRequest();
        }
        public List<SQL_RequestDataStruct> GetUserRequests(long ID)
        {
            var req = SQL_SelectAllRequest();
            var request = from item in req
                          where item.employee_id == ID
                          select item;
            return (List<SQL_RequestDataStruct>)request;
        }

        public  List<SQL_UserDataStruct> GetUser()
        {
            return SQL_SelectAllUser();
        }
        public  SQL_UserDataStruct GetUser(long ID)
        {
            var request = SQL_SelectAllUser();
            var answer = (from item in request
                         where item.employee_id == ID
                          select item).SingleOrDefault();
            return answer;
        }

        public void UpdateUser(SQL_UserDataStruct Data)
        {
        }
        public void UpdateEmployee(SQL_EmployeeDataStruct Data)
        {
            SQL_UpdateEmployees(Data);

        }

        // public void static AddToDatatbase operator +(
    }
}


