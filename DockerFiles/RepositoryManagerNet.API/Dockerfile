FROM mcr.microsoft.com/dotnet/sdk:6.0.100-preview.7-focal-amd64 AS BUILDER
RUN mkdir /build
RUN mkdir /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet publish RepositoryManager/RepositoryManagerNet.API/RepositoryManagerNet.API.csproj --configuration Release --output /buildOutput --runtime linux-x64

FROM ubuntu:focal AS KUBECTLINSTALL
RUN apt update && apt upgrade -y && apt install curl -y
RUN curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
RUN chmod +x /kubectl

FROM mcr.microsoft.com/dotnet/aspnet:6.0.0-preview.7-focal-amd64 AS RUNTIME
RUN apt update && apt upgrade -y
RUN mkdir -p /KubernetesObjects/
COPY --from=KUBECTLINSTALL /kubectl /usr/bin/kubectl
COPY KubernetesObjects/* /KubernetesObjects/
COPY --from=BUILDER /buildOutput /opt/RepositoryManagerNet.API
ENTRYPOINT ["/opt/RepositoryManagerNet.API/RepositoryManagerNet.API"]
