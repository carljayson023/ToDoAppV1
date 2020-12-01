using System.Collections.Generic;
using System.Data.SQLite;
using ToDoApp_v1._2.Model;

namespace ToDoApp_v1._2.Database
{
    public interface IConnectDB
    {
        SQLiteConnection DbConnection();
        List<string> GetAll(string tableName);
        List<Itemlist> GetItem(int id);
        string AddData(string data);
        string AddDataItem(string data);
        string DeleteData(string data);
        
        
       
        string UpdateData(string data);
        string UpdateDataItem(string data);
    }
}