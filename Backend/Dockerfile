FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
ARG RELATIVE_PATH=''    
WORKDIR /build
ADD "${RELATIVE_PATH}." /build
# Update the runtime to linux-arm64 if you're running it on a machine with ARM64 chip
RUN dotnet publish SGAA.Api/SGAA.Api.csproj -r linux-x64 -o bin -c Release

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=publish /build/bin ./
EXPOSE 80
ENTRYPOINT ["dotnet", "SGAA.Api.dll"]