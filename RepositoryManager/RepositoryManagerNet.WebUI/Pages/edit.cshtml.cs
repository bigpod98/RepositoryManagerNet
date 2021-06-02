using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RepositoryManagerNet.WebUI.Pages
{
    
    public class editModel : PageModel
    {
        

        [BindProperty]
        public string ID {get;set;}
        [BindProperty]
        public string Name {get;set;}
        [BindProperty]
        public string PackageType { get; set; }
        [BindProperty]
        public string BaseDomain {get;set;}

        public async Task OnGet(string id)
        {
            ID = id;

            HttpResponseMessage responseMessage = await IndexModel.client.GetAsync($"api/repo/{ID}"); 
            var body = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            Models.RepoData repo = JsonSerializer.Deserialize<Models.RepoData>(body);

            Name = repo.Name;
            PackageType = repo.PackageType;
            BaseDomain = repo.BaseDomain;
        }

        public async Task OnPost()
        {
            Models.RepoData repo = new Models.RepoData(Convert.ToInt32(ID), Name, PackageType, BaseDomain);
            var data = new StringContent(JsonSerializer.Serialize(repo), Encoding.UTF8, "application/json");
            await IndexModel.client.PutAsync($"api/modifyRepo/{ID}", data);
        }
    }
}
