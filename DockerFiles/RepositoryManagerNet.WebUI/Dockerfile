FROM mcr.microsoft.com/dotnet/sdk:6.0.100-preview.7-focal-amd64 AS BUILDER
RUN mkdir /build
RUN mkdir /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet publish RepositoryManager/RepositoryManagerNet.WebUI/RepositoryManagerNet.WebUI.csproj --configuration Release --output /buildOutput --runtime linux-x64

FROM mcr.microsoft.com/dotnet/aspnet:6.0.0-preview.7-focal-amd64 AS RUNTIME
COPY --from=BUILDER /buildOutput /opt/RepositoryManagerNet.WebUI
WORKDIR /opt/RepositoryManagerNet.WebUI
ENTRYPOINT ["/opt/RepositoryManagerNet.WebUI/RepositoryManagerNet.WebUI"]
