FROM mcr.microsoft.com/dotnet/sdk:5.0 AS BUILDER
RUN mkdir /build
RUN mkdir /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet publish --configuration Release --output /buildOutput --runtime linux-x64

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS RUNTIME
RUN mkdir -p /KubernetesObjects/
COPY KubernetesObjects/* /KubernetesObjects/
COPY --from=BUILDER /buildOutput /opt/RepositoryManagerNet.API
ENTRYPOINT ["/opt/RepositoryManagerNet.API/RepositoryManagerNet.API"]
