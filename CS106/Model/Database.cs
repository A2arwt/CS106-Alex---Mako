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
        

        public void login(string username,string password)
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


        // public void static AddToDatatbase operator +(
    }
}


