﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PlantHealth.Api/PlantHealth.Api.csproj", "PlantHealth.Api/"]
RUN dotnet restore "PlantHealth.Api/PlantHealth.Api.csproj"
COPY . .
WORKDIR "/src/PlantHealth.Api"
RUN dotnet build "PlantHealth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlantHealth.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlantHealth.Api.dll"]
