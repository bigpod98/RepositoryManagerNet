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
    public class registerRepoController : ControllerBase
    {
        [HttpPost]
        public string Post([FromBody] Models.RepoData RepositoryData)
        {
            //add to database

            return RepositoryData.Name;
        }
    }
}
