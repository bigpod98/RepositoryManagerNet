using System;
using static System.Environment;
using MySql.Data.MySqlClient;

namespace RepositoryManagerNet.API
{
    public static class Settings
    {
        public static string MYSQL_DBName;
        public static string MYSQL_UserName;
        public static string MYSQL_Password;
        public static string MYSQL_IP;
        public static string MYSQL_PORT;
        public static string StorageClass;
        public static string KubernetesNamespace;

        public static class ContainerImages
        {
            public static string APTRepository;
            public static string RPMRepository;
            public static string PacmanRepository;
        }

        public static void getSettingsENV()
        {
            string noValueENV = GetEnvironmentVariable("dspldfsgjdafdgsdfagrf");
            MYSQL_DBName = GetEnvironmentVariable("MYSQL_DATABASE_NAME");
            MYSQL_UserName = GetEnvironmentVariable("MYSQL_USERNAME");
            MYSQL_Password = GetEnvironmentVariable("MYSQL_PASSWORD");
            MYSQL_IP = GetEnvironmentVariable("MYSQL_IP");
            MYSQL_PORT = GetEnvironmentVariable("MYSQL_PORT");

            if (MYSQL_DBName == noValueENV)
            {
                Console.WriteLine("No Database name defined");
            }
            if (MYSQL_UserName == noValueENV)
            {
                Console.WriteLine("No Username defined");
            }
            if (MYSQL_Password == noValueENV)
            {
                Console.WriteLine("No Password defined");
            }
            if (MYSQL_IP == noValueENV)
            {
                Console.WriteLine("No Database IP defined");
            }
            if (MYSQL_PORT == noValueENV)
            {
                Console.WriteLine("No Database port defined");
            }
        }

        public static void getSettingsDB()
        {
            MySqlConnectionStringBuilder conBuilder = new MySqlConnectionStringBuilder()
            {
                Server = Settings.MYSQL_IP,
                Database = Settings.MYSQL_DBName,
                UserID = Settings.MYSQL_UserName,
                Password = Settings.MYSQL_Password,
                Port = Convert.ToUInt32(Settings.MYSQL_PORT)
            };

            MySqlConnection con = new MySqlConnection(conBuilder.GetConnectionString(true));

            string Command = $"SELECT setting, vsetting FROM Settings;";

            MySqlCommand cmd = new MySqlCommand(Command, con);
            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string storageKey = reader.GetString("setting");
                switch (storageKey)
                {
                    case "StorageClass":
                        StorageClass = reader.GetString("vsetting");
                        break;
                    case "KubernetesNamespace":
                        KubernetesNamespace = reader.GetString("vsetting");
                        break;
                    case "APTRepository":
                        ContainerImages.APTRepository = reader.GetString("vsetting");
                        break;
                    case "RPMRepository":
                        ContainerImages.RPMRepository = reader.GetString("vsetting");
                        break;
                    case "PacmanRepository":
                        ContainerImages.PacmanRepository = reader.GetString("vsetting");
                        break;
                }
            }

            if (StorageClass == "")
            {
                Console.WriteLine("No Storage Class defined");
            }
            if (KubernetesNamespace == "")
            {
                Console.WriteLine("No Kubernetes Namespace defined");
            }
            if (ContainerImages.APTRepository == "")
            {
                Console.WriteLine("No APT Repository image defined");
            }
            if (ContainerImages.RPMRepository == "")
            {
                Console.WriteLine("No RPM Repository image defined");
            }
            if (ContainerImages.PacmanRepository == "")
            {
                Console.WriteLine("No Pacman Repository image defined");
            }
        }
    }
}