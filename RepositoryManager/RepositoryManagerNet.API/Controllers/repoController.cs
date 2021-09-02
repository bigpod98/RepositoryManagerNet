using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using static RepositoryManagerNet.API.staticVariables;

namespace RepositoryManagerNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class repoController : ControllerBase
    { 
        [HttpGet("repoID")]
        public string Get(string repoID)
        {
            MySqlConnection con = new MySqlConnection(conBuilder.GetConnectionString(true));

            string Command = $"SELECT ID, Name, PackageType, BaseDomain FROM Repositories WHERE ID=@repoID;";

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
        public Models.RepoDataList Get()
        {
            MySqlConnection con = new MySqlConnection(conBuilder.GetConnectionString(true));

            string Command = $"SELECT ID, Name, PackageType, BaseDomain FROM Repositories;";

            MySqlCommand cmd = new MySqlCommand(Command, con);
            con.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            var repos = new Models.RepoDataList();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString("Name"));
                repos.Repodata.Add(new Models.RepoData(reader.GetInt32("ID"), reader.GetString("Name"),
                    reader.GetString("PackageType"), reader.GetString("BaseDomain")));
            }
            Console.WriteLine(repos.Repodata[0].Name);
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(repos));
            
            return repos;
        }
    }
}
