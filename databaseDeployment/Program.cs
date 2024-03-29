﻿using System;
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
            "CREATE TABLE Settings (setting varchar (64) not null, vsettings varchar(128), PRIMARY KEY (setting));",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"StorageClass\", \"pajdisek-nfs\");",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"KubernetesNamespace\", \"repositorymanagernet\");",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"APTRepository\", \"ghcr.io/bigpod98/aptpackagewatcher:latest\");",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"RPMRepository\", \"ghcr.io/bigpod98/rpmpackagewatcher:latest\");",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"PacmanRepository\", \"ghcr.io/bigpod98/pacmanpackagewatcher:latest\");",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"APTRepository\", \"ghcr.io/bigpod98/rmn-aptrepoinit:latest\");",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"RPMRepository\", \"ghcr.io/bigpod98/rmn-rpmrepoinit:latest\");",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"PacmanRepository\", \"ghcr.io/bigpod98/rmn-pacmanrepoinit:latest\");",
            "INSERT INTO Settings (setting, vsettings) VALUES (\"IngressClass\", \"nginx\");"
            };

con.Open();
foreach (string command in commands)
{
    MySqlCommand cmd = new MySqlCommand(command, con);

    Console.WriteLine(cmd.CommandText);

    cmd.ExecuteNonQuery();
}

