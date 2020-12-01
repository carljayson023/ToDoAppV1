using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using ToDoApp_v1._2.Model;

namespace ToDoApp_v1._2.Database
{
    public class ConnectDB : IConnectDB
    {
        public SQLiteConnection DbConnection() // Set SQLITE Connection
        {
            string dbConnection = @"Data Source=datalist.db";
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnection);
            return sqliteCon;

        }
        public string AddData(string data) // Insert Datalist
        {
            string[] datas = data.Split('&');
            SQLiteConnection connect = DbConnection();
            string Query = "INSERT INTO Datalists(Name, Description) VALUES('"+datas[0]+ "', '" + datas[1] + "')";

            SQLiteCommand cmd = new SQLiteCommand(Query, connect);
            cmd.ExecuteNonQuery();
            return "Done Save";
        }

        public string AddDataItem(string data) // -----> Insert Item
        {
            string[] datas = data.Split('&');
            SQLiteConnection connect = DbConnection();
            //string Query = "INSERT INTO Itemlists(Name, Detailed,Status,DatalistId) VALUES('" + datas[0] + "', '" + datas[1] + "',)";

            SQLiteCommand cmd = new SQLiteCommand( connect);
            cmd.CommandText = "INSERT INTO Itemlists(Name, Detailed,Status,DatalistId) VALUES(@name,@detailed,@status,@datalistId)";
            cmd.Parameters.AddWithValue("@name", datas[0]);
            cmd.Parameters.AddWithValue("@detailed", datas[1]);
            cmd.Parameters.AddWithValue("@status", datas[2]);
            cmd.Parameters.AddWithValue("@datalistId", datas[3]);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            return "Done Save";
        }

        

        public string DeleteData(string data) // { ID ,Table Name }   DELETE
        {
            string[] datas = data.Split('&');
            var id = datas[0];
            var tableName = datas[1];
            var idName = "" ;
            if (tableName == "Itemlists")
            {
                idName = "ItemlistId";
            }
            else { idName = "DatalistId"; }
            SQLiteConnection connect = DbConnection();
    
            SQLiteCommand cmd = new SQLiteCommand(connect);
            cmd.CommandText = "INSERT INTO @tablename where @idName = @id ";
            cmd.Parameters.AddWithValue("@tablename", tableName);
            cmd.Parameters.AddWithValue("@idName", idName);
            cmd.Parameters.AddWithValue("@id", id);
            return "Successfuly Deleted!";
        }
        public List<string> GetAll(string tableName) // ------> Get All DAta
        {
            
            SQLiteConnection connect = DbConnection();
            connect.Open();
            List<string> listFile = new List<string>();
            //var listFile = new List<listdata>();

            connect.Open();
            string Query = "Select * from "+ tableName + "";
            SQLiteCommand cmd = new SQLiteCommand(Query, connect);
            SQLiteDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                try
                { listFile.Add(Convert.ToString(rd["FileName"])); }
                catch {}
            }
            connect.Close();
            return listFile;
        }

        public List<Itemlist> GetItem(int id) //get by ID
        {
            var listFile = new List<Itemlist>();
            SQLiteConnection connect = DbConnection();
            connect.Open();
            string Query = "SELECT * From Itemlists Where DatalistId = " + id;

            SQLiteCommand cmd = new SQLiteCommand(Query, connect);
            SQLiteDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                //try
                //{ listFile.Add(Convert.ToString(rd["FileName"])); }
                //catch { }
                listFile.Add(new Itemlist()
                {
                    ItemlistId = rd.GetInt32(0),
                    Name = rd.GetString(1),
                    Detailed = rd.GetString(2),
                    Status = rd.GetString(3)
                    // etc... (0, 1 refer to the column index)
                });
            }
            connect.Close();
            return listFile;
        }

        public string UpdateData(string data) // Update List
        {
            string[] datas = data.Split('&');
            int id = Convert.ToInt32(datas[0]);
            string name = datas[1];
            string des = datas[2];
            SQLiteConnection connect = DbConnection();
            connect.Open();
            SQLiteCommand cmd = new SQLiteCommand(connect);
            cmd.CommandText = "UPDATE Datalists SET Name = @name , Description = @des WHERE DatalistId = " + id;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@des", des);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            connect.Close();
            return "Done Update";
        }

        public string UpdateDataItem(string data) // Update Item
        {
            string[] datas = data.Split('&');
            int id = Convert.ToInt32(datas[0]);
            string name = datas[1];
            string des = datas[2];
            string status = datas[3];
            SQLiteConnection connect = DbConnection();
            //string Query = "INSERT INTO Itemlists(Name, Detailed,Status,DatalistId) VALUES('" + datas[0] + "', '" + datas[1] + "',)";

            SQLiteCommand cmd = new SQLiteCommand(connect);
            cmd.CommandText = "UPDATE Itemlists SET Name = @name , Detailed = @des, Status = @status WHERE DatalistId = " + id;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@des", des);
            cmd.Parameters.AddWithValue("@status", status);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            return "Done Update";
        }
    }
}
