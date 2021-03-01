using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RepositoryManagerNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class repoController : ControllerBase
    {
        [HttpGet("repoID")]
        public string Get(string repoID)
        {
            //get single instance from database
            return "";
        }

        [HttpGet()]
        public List<Models.RepoData> Get()
        {
            //get all instances from database

            return new List<Models.RepoData>();
        }
    }
}
