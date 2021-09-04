using Microsoft.AspNetCore.Mvc;

//For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositoryManagerNet.UploadAPI.Controllers;
[Route("/api/upload/[controller]")]
[ApiController]
public class Upload : ControllerBase
{
    //POST<UploadController>
   [HttpPost]
    public async Task<string> PostAsync(IFormFile package, string repositoryname)
    {
        HttpClient client = new HttpClient()
        { BaseAddress = new Uri("http://repositorymanagernetapi") };
        HttpResponseMessage responseMessage = await client.GetAsync("api/repo");
        var Repos = await responseMessage.Content.ReadFromJsonAsync<Models.RepoDataList>();
        bool equalstoanything = false;
        string path = "";
        foreach (var i in Repos.repodata)
        {
            if (i.Name.ToLower() == repositoryname.ToLower())
            {
                equalstoanything = true;
                path = $"/repositories/{i.Name}";
                break;
            }
            else
            {
                equalstoanything = false;
            }
        }

        if (!equalstoanything)
            return "Repository name invalid";

        string returnval = $"{package.Name} -- {package.ContentType} -- {package.ToString()}";
        if (package.Length > 0)
        {
            using (var fileStream = System.IO.File.Create(path))
            {
                package.CopyTo(fileStream);
            }
        }
        return returnval;
    }
}
