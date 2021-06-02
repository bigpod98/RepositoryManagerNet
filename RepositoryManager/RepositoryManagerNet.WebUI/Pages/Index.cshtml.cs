using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RepositoryManagerNet.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        public static HttpClient client = new HttpClient()
        {BaseAddress = new Uri("http://127.0.0.1:6000")};


        [BindProperty]
        public string Name {get;set;}
        [BindProperty]
        public string PackageType { get; set; }
        [BindProperty]
        public string BaseDomain {get;set;}
        
        public string[] PackageManagerTypes = new[] {"APT", "Pacman", "RPM"};
        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            Models.RepoData Repository = new Models.RepoData(0, Name,PackageType,BaseDomain);
            var Content = new StringContent(JsonSerializer.Serialize(Repository),
                                            Encoding.UTF8,
                                            "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync("api/registerRepo", Content); 
            
        }
    }
}
