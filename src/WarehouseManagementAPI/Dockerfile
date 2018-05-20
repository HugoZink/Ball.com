FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WarehouseManagementAPI/WarehouseManagementAPI.csproj WarehouseManagementAPI/
RUN dotnet restore WarehouseManagementAPI/WarehouseManagementAPI.csproj
COPY . .
WORKDIR /src/WarehouseManagementAPI
RUN dotnet build WarehouseManagementAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WarehouseManagementAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WarehouseManagementAPI.dll"]
