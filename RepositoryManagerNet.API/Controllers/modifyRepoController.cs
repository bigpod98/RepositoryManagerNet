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
    public class modifyRepoController : ControllerBase
    {
        [HttpPut("repoID")]
        public string Put(string repoID, [FromBody] Models.RepoData RepositoryData)
        {
            // MySqlConnection con = new MySqlConnection("Server=10.152.183.94;Database=repomanager;Uid=RepoManager;Pwd=RepoManager;");

            MySqlConnectionStringBuilder conBuilder = new MySqlConnectionStringBuilder()
            {
                Server=Settings.MYSQL_IP,
                Database=Settings.MYSQL_DBName,
                UserID=Settings.MYSQL_UserName,
                Password=Settings.MYSQL_Password,
                Port=Convert.ToUInt32(Settings.MYSQL_PORT) 
            };

            MySqlConnection con = new MySqlConnection(conBuilder.GetConnectionString(true));

            string Command = $"UPDATE Repositories SET Name=@Name, PackageType=@PackageType, BaseDomain=@BaseDomain WHERE ID=@RepoID;";
            
            MySqlCommand cmd = new MySqlCommand(Command, con);
            cmd.Parameters.AddWithValue("@Name", RepositoryData.Name);
            cmd.Parameters.AddWithValue("@PackageType", RepositoryData.PackageType);
            cmd.Parameters.AddWithValue("@RepoID", RepositoryData.ID);
            cmd.Parameters.AddWithValue("@BaseDomain", RepositoryData.BaseDomain);
            con.Open();

            cmd.ExecuteNonQuery();

            return RepositoryData.Name;
        }
    }
}
