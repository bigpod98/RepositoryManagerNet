using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using static System.Environment;

MySqlConnectionStringBuilder conBuilder;

if (args[0] == "--env-var")
{
    conBuilder = new MySqlConnectionStringBuilder()
    {
        Server = GetEnvironmentVariable("MYSQL_IP"),
        Database = GetEnvironmentVariable("MYSQL_DATABASE_NAME"),
        UserID = GetEnvironmentVariable("MYSQL_USERNAME"),
        Password = GetEnvironmentVariable("MYSQL_PASSWORD"),
        Port = Convert.ToUInt32(GetEnvironmentVariable("MYSQL_PORT"))
    };
}
else
{
    conBuilder = new MySqlConnectionStringBuilder()
    {
        Server = args[0],
        Database = args[1],
        UserID = args[2],
        Password = args[3],
        Port = Convert.ToUInt32(args[4])
    };
}
MySqlConnection con = new MySqlConnection(conBuilder.GetConnectionString(true));

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

