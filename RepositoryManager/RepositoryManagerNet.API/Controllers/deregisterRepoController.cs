using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using static RepositoryManagerNet.API.staticVariables;

namespace RepositoryManagerNet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class deregisterRepoController : ControllerBase
    {
        [HttpDelete]
        public string Delete([FromBody] Models.RepoData RepositoryData)
        {
            MySqlConnection con = new MySqlConnection(conBuilder.GetConnectionString(true));

            string Command = $"DELETE FROM Repositories WHERE ID=@RepoID";
            
            MySqlCommand cmd = new MySqlCommand(Command, con);
            cmd.Parameters.AddWithValue("@RepoID", RepositoryData.Name);
            con.Open();

            cmd.ExecuteNonQuery();

            KubeClient.DeleteNamespacedDeployment(RepositoryData.Name, Settings.KubernetesNamespace);
            KubeClient.DeleteNamespacedService(RepositoryData.Name, Settings.KubernetesNamespace);
            KubeClient.DeleteNamespacedIngress(RepositoryData.Name, Settings.KubernetesNamespace);
            KubeClient.DeleteNamespacedPersistentVolumeClaim(RepositoryData.Name, Settings.KubernetesNamespace);

            return RepositoryData.Name;
        }
    }
}
