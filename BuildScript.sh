docker build -t ghcr.io/bigpod98/aptpackagewatcher:latest -f DockerFiles/APTPackageWatcher/Dockerfile . --no-cache
docker build -t ghcr.io/bigpod98/databasedeployment:latest -f DockerFiles/databaseDeployment/Dockerfile . --no-cache
docker build -t ghcr.io/bigpod98/pacmanpackagewatcher:latest -f DockerFiles/PacmanPackageWatcher/Dockerfile . --no-cache
docker build -t ghcr.io/bigpod98/repositorymanagernetapi:latest -f DockerFiles/RepositoryManagerNet.API/Dockerfile . --no-cache
docker build -t ghcr.io/bigpod98/repositorymanagernetwebui:latest -f DockerFiles/RepositoryManagerNet.WebUI/Dockerfile . --no-cache
docker build -t ghcr.io/bigpod98/rpmpackagewatcher:latest -f DockerFiles/RPMPackageWatcher/Dockerfile . --no-cache
docker push ghcr.io/bigpod98/aptpackagewatcher:latest
docker push ghcr.io/bigpod98/databasedeployment:latest
docker push ghcr.io/bigpod98/pacmanpackagewatcher:latest
docker push ghcr.io/bigpod98/repositorymanagernetapi:latest
docker push ghcr.io/bigpod98/repositorymanagernetwebui:latest
docker push ghcr.io/bigpod98/rpmpackagewatcher:latest
