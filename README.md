# SGAA
Sistema de gestión de alquileres autónomos

## Backend
### DbContext
Para generar las migraciones de contexto, correr el siguiente script
```
dotnet ef migrations add MiScript --project Backend/SGAA.Repository --startup-project Backend/SGAA.Api
```

Para eliminar la ultima migración, correr el siguiente script
```
dotnet ef migrations remove --project Backend/SGAA.Repository --startup-project Backend/SGAA.Api
```
