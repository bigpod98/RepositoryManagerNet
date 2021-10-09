using genKubeObjects;

Settings.IngressClass = "nginx";
Settings.KubernetesNamespace = "rmntest";
Settings.StorageClass = "pajdisek-nfs";
Settings.RepositoryHostname = "repository.pajdisek.palocal";
Settings.ContainerImages.InitRPMRepository = "ubuntu:20.04";
Settings.ContainerImages.RPMRepository = "ubuntu:20.04";
Settings.ContainerImages.InitAPTRepository = "ubuntu:20.04";
Settings.ContainerImages.APTRepository = "ubuntu:20.04";
Settings.ContainerImages.PacmanRepository = "ubuntu:20.04";
Settings.ContainerImages.InitPacmanRepository = "ubuntu:20.04";

genKubeObjects.Models.RepoData RepositoryData = new genKubeObjects.Models.RepoData();
RepositoryData.Name = "alpha";
RepositoryData.PackageType = "apt";
RepositoryData.BaseDomain = "repository.pajdisek.palocal";
RepositoryData.ID = 1;

savetofile(deploybyhand.CreateDeployment(RepositoryData));
savetofile(deploybyhand.CreateService(RepositoryData));
savetofile(deploybyhand.CreateIngress(RepositoryData));
savetofile(deploybyhand.CreatePersistentVolumeClaim_a(RepositoryData));
savetofile(deploybyhand.CreatePersistentVolumeClaim_b(RepositoryData));

static void savetofile(string deployment)
{
    Random a = new Random();
    string b = a.Next(1000000, 9999999).ToString();

    string filename = $"{b}.yaml";
    string path = $"/temp/{filename}";
    System.IO.File.WriteAllText(path, deployment);
}