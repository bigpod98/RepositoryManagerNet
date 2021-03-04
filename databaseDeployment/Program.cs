using System;
using MySql.Data.MySqlClient;

namespace databaseDeployment
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection con = new MySqlConnection("Server=10.152.183.94;Database=repomanager;Uid=RepoManager;Pwd=RepoManager;");
            string commandAPT = "CREATE TABLE RepositoriesAPT (ID int not NULL AUTO_INCREMENT,  Name varchar(30) not null, PRIMARY KEY (ID));";
            string commandRPM = "CREATE TABLE RepositoriesRPM (ID int not NULL AUTO_INCREMENT,  Name varchar(30) not null, PRIMARY KEY (ID));";
            string commandARCH = "CREATE TABLE RepositoriesARCH (ID int not NULL AUTO_INCREMENT,  Name varchar(30) not null, PRIMARY KEY (ID));";

            MySqlCommand cmdAPT = new MySqlCommand(commandAPT, con);
            MySqlCommand cmdRPM = new MySqlCommand(commandRPM, con);
            MySqlCommand cmdARCH = new MySqlCommand(commandARCH, con);
            con.Open();

            cmdAPT.ExecuteNonQuery();
            cmdRPM.ExecuteNonQuery();
            cmdARCH.ExecuteNonQuery();
        }
    }
}