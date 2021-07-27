# RepositoryManagerNet(WORKING NAME) (WIP, NOT YET WORKING)

Im making this project during my streams on youtube.

RepositoryManagerNet(WORKING NAME) is a set of software written in dotnet C# and other languages mainly targeting kubernetes clusters for deployment of Linux repositories and other types of repositories in future.
It mainly uses DotNet 6, ASP.NET Core 6 and C# but might in future feature other languages and technologies to support future features.



## Architecture

Current Architecture is verry much reliant on containerisation and kubernetes. It uses WebAPI and WebUI built on ASP.Net Core 5 framework to control other containers inside the cluster using KubeAPI. WebUI serves as an FrontEnd while API serves as a backend directing traffic from frontends (webUI and just simple api calls) to kubeapi or in future could support other hosting systems like virtual machines (libvirtd-kvm) or docker.
PackageWatcher Programs are used for Well watching for incomming packages from uploaders(currently no uploaders are implemented but NFS based uploader can be easily implemented) and add them to repository which is then served from nginx image based container(referd to as server or package server). Each Repository gets its own PackageWatcher and server so they are isolated from other repositories. PackageWatcher images are made specificly for type of repository used. (apt gets ubuntu/debian based image, Pacman gets arch based image ...). Currently supported Linux Package Repositories are for deb packages, RPM packages and Arch packages but in future more will be added.
