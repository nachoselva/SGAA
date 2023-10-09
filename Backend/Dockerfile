#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SGAA.Api/SGAA.Api.csproj", "SGAA.Api/"]
COPY ["SGAA.Repository/SGAA.Repository.csproj", "SGAA.Repository/"]
COPY ["SGAA.Domain/SGAA.Domain.csproj", "SGAA.Domain/"]
COPY ["SGAA.Utils/SGAA.Utils.csproj", "SGAA.Utils/"]
COPY ["SGAA.Service/SGAA.Service.csproj", "SGAA.Service/"]
RUN dotnet restore "SGAA.Api/SGAA.Api.csproj"
COPY . .
WORKDIR "/src/SGAA.Api"
RUN dotnet build "SGAA.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGAA.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGAA.Api.dll"]