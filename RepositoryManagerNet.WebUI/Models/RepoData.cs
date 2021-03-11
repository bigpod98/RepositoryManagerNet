namespace RepositoryManagerNet.WebUI.Models
{
    public class RepoData
    {
        public RepoData(int iD, string name, string packageType, string baseDomain)
        {
            ID = iD;
            Name = name;
            PackageType = packageType;
            BaseDomain = baseDomain;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string PackageType { get; set; }
        public string BaseDomain {get;set;}
    }
}