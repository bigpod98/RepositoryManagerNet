using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace databaseDeployment
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder conBuilder = new MySqlConnectionStringBuilder()
            {
                Server = args[0],
                Database = args[1],
                UserID = args[2],
                Password = args[3],
                Port = Convert.ToUInt32(args[4])
            };
            MySqlConnection con = new MySqlConnection("Server=10.152.183.94;Database=repomanager;Uid=RepoManager;Pwd=RepoManager;");

            List<string> commands = new List<string>()
            {
            "CREATE TABLE Repositories (ID int not NULL AUTO_INCREMENT, Name varchar(30) not null, PackageType varchar(10) not null, BaseDomain varchar(50) not null, PRIMARY KEY (ID));",
            "CREATE TABLE Settings (setting varchar (64) not null, vsettings varchar(128) not null, PRIMARY KEY (setting)"
                
            };

            foreach (string command in commands)
            {
                MySqlCommand cmd = new MySqlCommand(command, con);
                con.Open();

                cmd.ExecuteNonQuery();
            }
            
        }
    }
}