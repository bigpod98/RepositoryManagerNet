using System;
using MySql.Data.MySqlClient;

namespace databaseDeployment
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection con = new MySqlConnection("Server=10.152.183.94;Database=repomanager;Uid=RepoManager;Pwd=RepoManager;");
            string commandAPT = "CREATE TABLE Repositories (ID int not NULL AUTO_INCREMENT,  Name varchar(30) not null, PackageType varchar(10) not null, BaseDomain varchar(50) not null, PRIMARY KEY (ID));";

            MySqlCommand cmdAPT = new MySqlCommand(commandAPT, con);
            con.Open();

            cmdAPT.ExecuteNonQuery();
        }
    }
}