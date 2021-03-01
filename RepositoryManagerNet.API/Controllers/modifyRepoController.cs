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
    public class modifyRepoController : ControllerBase
    {
        [HttpPut("repoID")]
        public string Put(string repoID, [FromBody] Models.RepoData RepositoryData)
        {
            //modify in database

            return RepositoryData.Name;
        }
    }
}
