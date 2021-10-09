using System;
using static System.Environment;

namespace genKubeObjects
{
    public static class Settings
    {
        public static string StorageClass;
        public static string KubernetesNamespace;
        public static string RepositoryHostname;
        public static string IngressClass;


        public static class ContainerImages
        {
            public static string APTRepository;
            public static string RPMRepository;
            public static string PacmanRepository;
            public static string InitAPTRepository;
            public static string InitRPMRepository;
            public static string InitPacmanRepository;
        }
    }
}