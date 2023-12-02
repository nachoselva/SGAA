# SGAA
Sistema de gestión de alquileres autónomos

## Contenedores
### Docker
Para correr el contenedor localmente, ejecutar el siguiente script:
```
docker-compose  -f "C:\Projects\SGAA\docker-compose.yml" --ansi never up -d --build --remove-orphans
```

Guía para instalar docker en AWS
```
https://docs.aws.amazon.com/es_es/prescriptive-guidance/latest/patterns/run-an-asp-net-core-web-api-docker-container-on-an-amazon-ec2-linux-instance.html
```
https://docs.docker.com/build/ci/github-actions/
https://aws.amazon.com/es/blogs/security/use-iam-roles-to-connect-github-actions-to-actions-in-aws/
https://www.youtube.com/watch?v=4jlDDwMT5Nw&ab_channel=Garajedeideas%7CTech

arn:aws:iam::417268992578:oidc-provider/token.actions.githubusercontent.com
## Backend
### DbContext
Para generar las migraciones de contexto, correr el siguiente script:
```
dotnet ef migrations add MiScript --project Backend/SGAA.Repository --startup-project Backend/SGAA.Api
```

Para eliminar la ultima migración, correr el siguiente script:
```
dotnet ef migrations remove --project Backend/SGAA.Repository --startup-project Backend/SGAA.Api
```

Para actualizar la base de datos, correr el siguiente script:
```
dotnet ef database update --project Backend/SGAA.Repository/SGAA.Repository.csproj --startup-project Backend/SGAA.Api/SGAA.Api.csproj --context SGAADbContext --verbose
```

Para abrir swagger:
```
http://localhost:5104/swagger/index.html
```
