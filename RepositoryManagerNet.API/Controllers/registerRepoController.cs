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
    public class registerRepoController : ControllerBase
    {
        [HttpPost]
        public string Post([FromBody] Models.RepoData RepositoryData)
        {
            MySqlConnection con = new MySqlConnection("Server=10.152.183.94;Database=repomanager;Uid=RepoManager;Pwd=RepoManager;");

            string Command = $"INSERT INTO Repositories (Name, PackageType, BaseDomain) VALUES (@Name, @PackageType, @BaseDomain)";
            
            MySqlCommand cmd = new MySqlCommand(Command, con);
            cmd.Parameters.AddWithValue("@Name", RepositoryData.Name);
            cmd.Parameters.AddWithValue("@PackageType", RepositoryData.PackageType);
            cmd.Parameters.AddWithValue("@BaseDomain", RepositoryData.BaseDomain);
            con.Open();

            cmd.ExecuteNonQuery();

            return RepositoryData.Name;
        }
    }
}
