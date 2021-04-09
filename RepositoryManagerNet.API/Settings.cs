using System;
using static System.Environment;
using MySql.Data.Common;

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
            MYSQL_DBName = GetEnvironmentVariable("MYSQL_DATABASE_NAME");
            MYSQL_UserName = GetEnvironmentVariable("MYSQL_USERNAME");
            MYSQL_Password = GetEnvironmentVariable("MYSQL_PASSWORD");
            MYSQL_IP = GetEnvironmentVariable("MYSQL_IO");
            MYSQL_PORT = GetEnvironmentVariable("MYSQL_PORT");

            if(MYSQL_DBName == " ")
            {
                Console.WriteLine("No Database name defined");
            }
            if(MYSQL_UserName == " ")
            {
                Console.WriteLine("No Username defined");
            }
            if(MYSQL_Password == " ")
            {
                Console.WriteLine("No Password defined");
            }
            if(MYSQL_IP == " ")
            {
                Console.WriteLine("No Database IP defined");
            }
            if(MYSQL_PORT == " ")
            {
                Console.WriteLine("No Database port defined");
            }
        }

        public static void getSettingsDB()
        {
            

            string storageKey = "";
            switch(storageKey)
            {
                case "StorageClass":
                StorageClass = "";
                break;
                case "KubernetesNamespace":
                KubernetesNamespace = "";
                break;
                case "APTRepository":
                ContainerImages.APTRepository = "";
                break;
                case "RPMRepository":
                ContainerImages.RPMRepository = "";
                break;
                case "PacmanRepository":
                ContainerImages.PacmanRepository = "";
                break;
            }

            if(StorageClass == "")
            {
                Console.WriteLine("No Storage Class defined");
            }
            if(KubernetesNamespace == "")
            {
                Console.WriteLine("No Kubernetes Namespace defined");
            }  
            if(ContainerImages.APTRepository == "")
            {
                Console.WriteLine("No APT Repository image defined");
            }  
            if(ContainerImages.RPMRepository == "")
            {
                Console.WriteLine("No RPM Repository image defined");
            }  
            if(ContainerImages.PacmanRepository == "")
            {
                Console.WriteLine("No Pacman Repository image defined");
            }            
        }
    }
}