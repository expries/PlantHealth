﻿FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Kafka.Producer/Kafka.Producer.csproj", "Kafka.Producer/"]
RUN dotnet restore "Kafka.Producer/Kafka.Producer.csproj"
COPY . .
WORKDIR "/src/Kafka.Producer"
RUN dotnet build "Kafka.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kafka.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kafka.Producer.dll"]
