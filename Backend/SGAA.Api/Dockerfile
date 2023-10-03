#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Backend/SGAA.Api/SGAA.Api.csproj", "Backend/SGAA.Api/"]
COPY ["Backend/SGAA.Repository/SGAA.Repository.csproj", "Backend/SGAA.Repository/"]
COPY ["Backend/SGA.Domain/SGAA.Domain.csproj", "Backend/SGA.Domain/"]
COPY ["Backend/SGAA.Utils/SGAA.Utils.csproj", "Backend/SGAA.Utils/"]
COPY ["Backend/SGAA.Service/SGAA.Service.csproj", "Backend/SGAA.Service/"]
RUN dotnet restore "Backend/SGAA.Api/SGAA.Api.csproj"
COPY . .
WORKDIR "/src/Backend/SGAA.Api"
RUN dotnet build "SGAA.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGAA.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGAA.Api.dll"]