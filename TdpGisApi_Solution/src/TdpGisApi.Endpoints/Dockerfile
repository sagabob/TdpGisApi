#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/TdpGisApi.Endpoints/TdpGisApi.Endpoints.csproj", "src/TdpGisApi.Endpoints/"]
COPY ["src/TdpGisApi.Application/TdpGisApi.Application.csproj", "src/TdpGisApi.Application/"]
RUN dotnet restore "src/TdpGisApi.Endpoints/TdpGisApi.Endpoints.csproj"
COPY . .
WORKDIR "/src/src/TdpGisApi.Endpoints"
RUN dotnet build "TdpGisApi.Endpoints.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TdpGisApi.Endpoints.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TdpGisApi.Endpoints.dll"]