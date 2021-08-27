using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositoryManagerNet.UploadAPI.Controllers;
[Route("/api/upload/[controller]")]
[ApiController]
public class Upload : ControllerBase
{
    // POST <UploadController>
    [HttpPost]
    public string Post(IFormFile package)
    {
        string returnval = $"{package.Name} -- {package.ContentType} -- {package.ToString()}";
        if (package.Length > 0)
        {
            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                package.CopyTo(stream);
                using (var fileStream = System.IO.File.Create(""))
                {
                    package.CopyTo(fileStream);
                }
            }
        }
        return returnval;
    }
}
