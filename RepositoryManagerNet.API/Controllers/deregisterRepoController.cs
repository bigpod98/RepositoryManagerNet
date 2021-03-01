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
    public class deregisterRepoController : ControllerBase
    {
        [HttpDelete]
        public string Delete([FromBody] Models.RepoData RepositoryData)
        {
            //delete from database

            return RepositoryData.Name;
        }
    }
}
