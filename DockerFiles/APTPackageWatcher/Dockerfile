FROM mcr.microsoft.com/dotnet/sdk:6.0.100-preview.7-focal-amd64 AS BUILDER
RUN mkdir /build
RUN mkdir /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet publish --configuration Release --output /buildOutput --runtime linux-x64

FROM mcr.microsoft.com/dotnet/runtime:6.0.0-preview.7-focal-amd64 AS RUNTIME
COPY --from=BUILDER /buildOutput /opt/APTPackageWatcher
ENTRYPOINT ["/opt/APTPackageWatcher/APTPackageWatcher"]
