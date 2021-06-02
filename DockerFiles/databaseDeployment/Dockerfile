FROM mcr.microsoft.com/dotnet/sdk:5.0 AS BUILDER
RUN mkdir /build
RUN mkdir /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet publish --configuration Release --output /buildOutput --runtime linux-x64

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS RUNTIME
COPY --from=BUILDER /buildOutput /opt/databaseDeployment
ENTRYPOINT ["/opt/databaseDeployment/databaseDeployment"]
