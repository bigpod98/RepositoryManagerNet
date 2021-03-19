using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace RepositoryManagerNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class repoController : ControllerBase
    {
        [HttpGet("repoID")]
        public string Get(string repoID)
        {
            MySqlConnection con = new MySqlConnection("Server=10.152.183.94;Database=repomanager;Uid=RepoManager;Pwd=RepoManager;");

            string Command = $"SELECT * FROM Repositories WHERE ID=@repoID;";

            MySqlCommand cmd = new MySqlCommand(Command, con);
            if (Int32.TryParse(repoID, out _))
            {
                cmd.Parameters.AddWithValue("@repoID", repoID);
            }
            else
            {
                return "Error: RepoID not a intiger";
            }
            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            Models.RepoData repo;
            if (reader.Read())
            {
                repo = new Models.RepoData(reader.GetInt32("ID"), reader.GetString("Name"),
                    reader.GetString("PackageType"), reader.GetString("BaseDomain"));
            }
            else{
                repo = new Models.RepoData();
            }
            return JsonSerializer.Serialize(repo);
        }

        [HttpGet()]
        public List<Models.RepoData> Get()
        {
            MySqlConnection con = new MySqlConnection("Server=10.152.183.94;Database=repomanager;Uid=RepoManager;Pwd=RepoManager;");

            string Command = $"SELECT * FROM Repositories;";

            MySqlCommand cmd = new MySqlCommand(Command, con);
            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            List<Models.RepoData> repos = new List<Models.RepoData>();
            while (reader.Read())
            {
                repos.Add(new Models.RepoData(reader.GetInt32("ID"), reader.GetString("Name"),
                    reader.GetString("PackageType"), reader.GetString("BaseDomain")));
            }

            return repos;
        }
    }
}
