FROM mcr.microsoft.com/dotnet/sdk:6.0.100-preview.7-focal-amd64 AS BUILDER
RUN mkdir /build
RUN mkdir /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet publish RepositoryManager/RepositoryManagerNet.API/RepositoryManagerNet.API.csproj --configuration Release --output /buildOutput --runtime linux-x64

FROM mcr.microsoft.com/dotnet/aspnet:6.0.0-preview.7-focal-amd64 AS RUNTIME
RUN mkdir -p /KubernetesObjects/
COPY KubernetesObjects/* /KubernetesObjects/
COPY --from=BUILDER /buildOutput /opt/RepositoryManagerNet.API
ENTRYPOINT ["/opt/RepositoryManagerNet.API/RepositoryManagerNet.API"]
