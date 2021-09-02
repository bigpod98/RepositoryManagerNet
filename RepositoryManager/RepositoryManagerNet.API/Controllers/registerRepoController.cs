using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using k8s;
using static RepositoryManagerNet.API.staticVariables;
using System.Diagnostics;

namespace RepositoryManagerNet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class registerRepoController : ControllerBase
    {
        [HttpPost]
        public string Post([FromBody] Models.RepoData RepositoryData)
        {
            MySqlConnection con = new MySqlConnection(conBuilder.GetConnectionString(true));

            string Command = $"INSERT INTO Repositories (Name, PackageType, BaseDomain) VALUES (@Name, @PackageType, @BaseDomain)";

            MySqlCommand cmd = new MySqlCommand(Command, con);
            cmd.Parameters.AddWithValue("@Name", RepositoryData.Name);
            cmd.Parameters.AddWithValue("@PackageType", RepositoryData.PackageType);
            cmd.Parameters.AddWithValue("@BaseDomain", RepositoryData.BaseDomain);
            con.Open();

            cmd.ExecuteNonQuery();

            #region noncode
            //KubeClient.CreateNamespacedDeployment(Deployment(RepositoryData), Settings.KubernetesNamespace);
            //KubeClient.CreateNamespacedService(Service(RepositoryData), Settings.KubernetesNamespace);
            //KubeClient.CreateNamespacedIngress(Ingress(RepositoryData), Settings.KubernetesNamespace);
            //KubeClient.CreateNamespacedPersistentVolumeClaim(PVC(RepositoryData), Settings.KubernetesNamespace);
            //KubeClient.CreateNamespacedPersistentVolumeClaim(PVCIncoming(RepositoryData), Settings.KubernetesNamespace);

            //var  x = KubeClient.ReadNamespacedDeployment("repositorymanagernetuploadapi", Settings.KubernetesNamespace);
            //var y = x.Spec.Template.Spec.Containers[0].VolumeMounts[0];
            //y.MountPath = $"/incoming/{RepositoryData.Name}";
            //y.Name = $"{RepositoryData.Name}-incoming";
            //x.Spec.Template.Spec.Containers[0].VolumeMounts.Add(y);
            //var z = x.Spec.Template.Spec.Volumes[0];
            //z.Name = $"{RepositoryData.Name}-incoming";
            //z.PersistentVolumeClaim.ClaimName = $"{RepositoryData.Name}-incoming";
            //x.Spec.Template.Spec.Volumes.Add(z);
            //KubeClient.ReplaceNamespacedDeployment(x, "repositorymanagernetuploadapi", Settings.KubernetesNamespace);    
            #endregion

            deploybyhand.deployProcess(deploybyhand.CreateDeployment(RepositoryData));
            deploybyhand.deployProcess(deploybyhand.CreateService(RepositoryData));
            deploybyhand.deployProcess(deploybyhand.CreateIngress(RepositoryData));
            deploybyhand.deployProcess(deploybyhand.CreatePersistentVolumeClaim_a(RepositoryData));
            deploybyhand.deployProcess(deploybyhand.CreatePersistentVolumeClaim_b(RepositoryData));
            return RepositoryData.Name;
        }

        #region nonworking code
        public static k8s.Models.V1Deployment Deployment(Models.RepoData RepoData)
        {
            k8s.Models.V1Deployment KubeObject = k8s.Yaml.LoadFromFileAsync<k8s.Models.V1Deployment>("/KubernetesObjects/Deployment.yaml").Result;

            KubeObject.Metadata.Name = RepoData.Name;
            KubeObject.Metadata.NamespaceProperty = Settings.KubernetesNamespace;
            KubeObject.Spec.Selector.MatchLabels["app"] = RepoData.Name;
            KubeObject.Spec.Template.Metadata.Labels["app"] = RepoData.Name;
            KubeObject.Spec.Template.Spec.InitContainers[0].Name = $"{RepoData.Name}-init";
            KubeObject.Spec.Template.Spec.InitContainers[0].Image = getPackageType(RepoData.PackageType, false);
            KubeObject.Spec.Template.Spec.InitContainers[0].Env[0].Value = "";
            KubeObject.Spec.Template.Spec.Containers[0].Name = RepoData.Name;
            KubeObject.Spec.Template.Spec.Containers[0].Image = getPackageType(RepoData.PackageType, true);
            KubeObject.Spec.Template.Spec.Containers[1].Name = $"{RepoData.Name}-server";
            KubeObject.Spec.Template.Spec.ImagePullSecrets[0].Name = "";
            KubeObject.Spec.Template.Spec.Volumes[0].PersistentVolumeClaim.ClaimName = $"{RepoData.Name}-pvc";
            KubeObject.Spec.Template.Spec.Volumes[1].PersistentVolumeClaim.ClaimName = $"{RepoData.Name}-incoming-pvc";


            return KubeObject;
        }

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

        public static k8s.Models.V1Service Service(Models.RepoData RepoData)
        {
            k8s.Models.V1Service KubeObject = k8s.Yaml.LoadFromFileAsync<k8s.Models.V1Service>("/KubernetesObjects/Service.yaml").Result;

            KubeObject.Metadata.Name = RepoData.Name;
            KubeObject.Metadata.NamespaceProperty = Settings.KubernetesNamespace;
            KubeObject.Spec.Selector["app"] = RepoData.Name;

            return KubeObject;
        }

        public static k8s.Models.V1Ingress Ingress(Models.RepoData RepoData)
        {
            k8s.Models.V1Ingress KubeObject = k8s.Yaml.LoadFromFileAsync<k8s.Models.V1Ingress>("/KubernetesObjects/Ingress.yaml").Result;

            KubeObject.Metadata.Name = RepoData.Name;
            KubeObject.Metadata.NamespaceProperty = Settings.KubernetesNamespace;
            KubeObject.Spec.Rules[0].Host = Settings.RepositoryHostname;
            KubeObject.Spec.Rules[0].Http.Paths[0].Path = $"/Repository/{RepoData.Name}(/|$)(.*)";
            KubeObject.Spec.Rules[0].Http.Paths[0].Backend.Service.Name = RepoData.Name;

            return KubeObject;
        }

        public static k8s.Models.V1PersistentVolumeClaim PVC(Models.RepoData RepoData)
        {
            k8s.Models.V1PersistentVolumeClaim KubeObject = k8s.Yaml.LoadFromFileAsync<k8s.Models.V1PersistentVolumeClaim>("/KubernetesObjects/PVC.yaml").Result;

            KubeObject.Metadata.Name = $"{RepoData.Name}-pvc";
            KubeObject.Metadata.NamespaceProperty = Settings.KubernetesNamespace;
            KubeObject.Spec.StorageClassName = Settings.StorageClass;

            return KubeObject;
        }
        public static k8s.Models.V1PersistentVolumeClaim PVCIncoming(Models.RepoData RepoData)
        {
            k8s.Models.V1PersistentVolumeClaim KubeObject = k8s.Yaml.LoadFromFileAsync<k8s.Models.V1PersistentVolumeClaim>("/KubernetesObjects/PVC.yaml").Result;

            KubeObject.Metadata.Name = $"{RepoData.Name}-incoming-pvc";
            KubeObject.Metadata.NamespaceProperty = Settings.KubernetesNamespace;
            KubeObject.Spec.StorageClassName = Settings.StorageClass;

            return KubeObject;
        }
        #endregion
        
        public static class deploybyhand
        {
            public static string CreateDeployment(Models.RepoData RepositoryData)
            {
                string deploy = System.IO.File.ReadAllText("/KubernetesObjects/Deployment.yaml");
                deploy = deploy.Replace("nameofrepo", RepositoryData.Name);
                deploy = deploy.Replace("namespacetemplate", Settings.KubernetesNamespace);
                deploy = deploy.Replace("claimnameRV", $"{RepositoryData.Name}-pvc");
                deploy = deploy.Replace("claimnameID", $"{RepositoryData.Name}-incoming-pvc");
                deploy = deploy.Replace("imagepullsecrettemplate", "iimagepullsecrettemplate");

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

    }
}
