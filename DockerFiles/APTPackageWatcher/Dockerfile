FROM mcr.microsoft.com/dotnet/sdk:6.0.100-preview.7-focal-amd64 AS BUILDER
RUN mkdir /build
RUN mkdir /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet publish RepositoryManager/APTPackageWatcher/APTPackageWatcher.csproj --configuration Release --output /buildOutput --runtime linux-x64

FROM mcr.microsoft.com/dotnet/runtime:6.0.0-preview.7-focal-amd64 AS RUNTIME
RUN apt update && apt upgrade -y
RUN apt install reprepro -y
COPY --from=BUILDER /buildOutput /opt/APTPackageWatcher
ENTRYPOINT ["/opt/APTPackageWatcher/APTPackageWatcher"]
