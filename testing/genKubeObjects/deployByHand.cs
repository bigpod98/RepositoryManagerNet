using System.Diagnostics;

namespace genKubeObjects;
public static class deploybyhand
{
    public static string getPackageType(string type, bool isWatcher)
    {
        string x = "";
        if (isWatcher)
        {
            switch (type)
            {
                case "APT":
                    x = Settings.ContainerImages.APTRepository;
                    break;
                case "Pacman":
                    x = Settings.ContainerImages.PacmanRepository;
                    break;
                case "RPM":
                    x = Settings.ContainerImages.RPMRepository;
                    break;
                default:
                    x = Settings.ContainerImages.APTRepository;
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case "APT":
                    x = Settings.ContainerImages.InitAPTRepository;
                    break;
                case "Pacman":
                    x = Settings.ContainerImages.InitPacmanRepository;
                    break;
                case "RPM":
                    x = Settings.ContainerImages.InitRPMRepository;
                    break;
                default:
                    x = Settings.ContainerImages.InitAPTRepository;
                    break;
            }
        }

        return x;
    }

    public static string CreateDeployment(Models.RepoData RepositoryData)
    {
        string deploy = System.IO.File.ReadAllText("/KubernetesObjects/Deployment.yaml");
        deploy = deploy.Replace("nameofrepo", RepositoryData.Name);
        deploy = deploy.Replace("namespacetemplate", Settings.KubernetesNamespace);
        deploy = deploy.Replace("claimnameRV", $"{RepositoryData.Name}-pvc");
        deploy = deploy.Replace("claimnameID", $"{RepositoryData.Name}-incoming-pvc");
        deploy = deploy.Replace("imagepullsecrettemplate", "iimagepullsecrettemplate");
        deploy = deploy.Replace("initimage", getPackageType(RepositoryData.PackageType, false));
        deploy = deploy.Replace("containerimage", getPackageType(RepositoryData.PackageType, true));
        Console.WriteLine(deploy);

        return deploy;
    }
    public static string CreateService(Models.RepoData RepositoryData)
    {
        string deploy = System.IO.File.ReadAllText("/KubernetesObjects/Service.yaml");

        deploy = deploy.Replace("nameofrepo", RepositoryData.Name);
        deploy = deploy.Replace("namespacetemplate", Settings.KubernetesNamespace);

        return deploy;
    }
    public static string CreateIngress(Models.RepoData RepositoryData)
    {
        string deploy = System.IO.File.ReadAllText("/KubernetesObjects/Ingress.yaml");

        deploy = deploy.Replace("nameofrepo", RepositoryData.Name);
        deploy = deploy.Replace("namespacetemplate", Settings.KubernetesNamespace);
        deploy = deploy.Replace("hostlink", RepositoryData.BaseDomain);
        deploy = deploy.Replace("repopath", $"/repository/linux/{RepositoryData.Name}");

        return deploy;
    }
    public static string CreatePersistentVolumeClaim_a(Models.RepoData RepositoryData)
    {
        string deploy = System.IO.File.ReadAllText("/KubernetesObjects/PVC.yaml");

        deploy = deploy.Replace("nameofrepo-pvc", $"{RepositoryData.Name}-pvc");
        deploy = deploy.Replace("namespacetemplate", Settings.KubernetesNamespace);
        deploy = deploy.Replace("storageclassnamex", Settings.StorageClass.Replace("\"", ""));
        deploy = deploy.Replace("\"", "");

        return deploy;
    }
    public static string CreatePersistentVolumeClaim_b(Models.RepoData RepositoryData)
    {
        string deploy = System.IO.File.ReadAllText("/KubernetesObjects/PVC.yaml");

        deploy = deploy.Replace("nameofrepo-pvc", $"{RepositoryData.Name}-incoming-pvc");
        deploy = deploy.Replace("namespacetemplate", Settings.KubernetesNamespace);
        deploy = deploy.Replace("storageclassnamex", Settings.StorageClass.Replace("\"",""));
        deploy = deploy.Replace("\"", "");

        return deploy;
    }

    public static void deployProcess(string x)
    {
        Console.WriteLine("deployprocess");
        Random a = new Random();
        string b = a.Next(1000000, 9999999).ToString();

        string filename = $"{b}.yaml";
        string path = $"/temp/{filename}";
        System.IO.File.WriteAllText(path, x);

        Process p = new Process();
        p.StartInfo.FileName = "kubectl";
        p.StartInfo.Arguments = $"apply -f {path}";
        p.StartInfo.UseShellExecute = true;
        p.Start();
        p.WaitForExit();

        System.IO.File.Delete(path);

    }
}