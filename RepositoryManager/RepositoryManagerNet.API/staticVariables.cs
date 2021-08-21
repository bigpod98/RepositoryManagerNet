using k8s;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace RepositoryManagerNet.API
{
    public static class staticVariables
    {
        public static MySqlConnectionStringBuilder conBuilder;
        public static Kubernetes KubeClient;
    }
}
