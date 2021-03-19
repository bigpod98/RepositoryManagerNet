using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace RepositoryManagerNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class deregisterRepoController : ControllerBase
    {
        [HttpDelete]
        public string Delete([FromBody] Models.RepoData RepositoryData)
        {
            MySqlConnection con = new MySqlConnection("Server=10.152.183.94;Database=repomanager;Uid=RepoManager;Pwd=RepoManager;");

            string Command = $"DELETE FROM Repositories WHERE ID=@RepoID";
            
            MySqlCommand cmd = new MySqlCommand(Command, con);
            cmd.Parameters.AddWithValue("@RepoID", RepositoryData.Name);
            con.Open();

            cmd.ExecuteNonQuery();

            return RepositoryData.Name;
        }
    }
}
