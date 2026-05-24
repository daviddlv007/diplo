# MiApi - .NET 8 API con SQL Server en Docker
 
## 📋 Requisitos Previos
 
- Docker Desktop instalado
- .NET 8 SDK (para desarrollo local)
- SQL Server Management Studio (opcional, para conectarse a la BD)
---
 
## 🚀 Inicio Rápido
 
### 1. Clonar el proyecto
```bash
https://github.com/daviddlv007/diplo.git

cd diplo
```
 
### 2. Instalar dependencias locales (opcional, solo si no estás en Docker)
```bash
dotnet restore
```
 
### 3. Crear migraciones de Base de Datos (primera vez)
```bash
dotnet ef migrations add InitialCreate
```
 
### 4. Levantar todo con Docker Compose
```bash
docker-compose up --build
```
 
Esto levantará:
- 🐳 **SQL Server**: `localhost:1433`
- 🌐 **API .NET**: `http://localhost:8080`
- 📚 **Swagger UI**: `http://localhost:8080/swagger`
---
 
## 📝 Configuración
 
### Variables de Entorno
El archivo `docker-compose.yml` configura automáticamente:
- **Base de Datos**: `MiApiDB`
- **Usuario SQL Server**: `sa`
- **Contraseña**: `Admin12345*`
- **Conexión**: `Server=sqlserver;Database=MiApiDB;...`
### Cadena de Conexión
Definida en `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=sqlserver;Database=MiApiDB;User Id=sa;Password=Admin12345*;TrustServerCertificate=True;"
}
```
 
---
 
## 🐳 Comandos Docker
 
| Acción | Comando |
|--------|---------|
| Levantar y construir | `docker-compose up --build` |
| Levantar sin reconstruir | `docker-compose up` |
| Detener servicios | `docker-compose down` |
| Ver logs de la API | `docker-compose logs -f mi-api` |
| Ver logs de SQL Server | `docker-compose logs -f sqlserver` |
| Eliminar todo (contenedores + volúmenes) | `docker-compose down -v` |
 
---
 
## 💻 Desarrollo Local (sin Docker)
 
### 1. Actualizar la cadena de conexión en `appsettings.Development.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MiApiDB;User Id=sa;Password=Admin12345*;TrustServerCertificate=True;"
}
```
 
### 2. Aplicar migraciones
```bash
dotnet ef database update
```
 
### 3. Ejecutar la API
```bash
dotnet run
```
 
Disponible en: `https://localhost:7041` o `http://localhost:5031`
 
---
 
## 🔌 Conectarse a SQL Server desde fuera de Docker
 
**Desde SQL Server Management Studio:**
- **Server**: `localhost,1433`
- **Username**: `sa`
- **Password**: `Admin12345*`
**Connection String:**
```
Server=localhost,1433;Database=MiApiDB;User Id=sa;Password=Admin12345*;TrustServerCertificate=True;
```
 
---
 
## 🏗️ Estructura del Proyecto
 
```
.
├── Controllers/          # Controladores API
├── Data/                 # DbContext y configuración de BD
├── Models/               # Modelos y DTOs
├── Migrations/           # Migraciones de EntityFramework
├── Properties/           # Configuración de lanzamiento
├── appsettings.json      # Configuración general
├── docker-compose.yml    # Orquestación de contenedores
├── Dockerfile            # Imagen Docker de la API
└── MiApi.csproj          # Archivo del proyecto
```
 
---
 
## ⚙️ Tecnologías
 
| Componente | Tecnología |
|------------|------------|
| Framework | .NET 8.0 |
| ORM | Entity Framework Core 8.0 |
| Base de Datos | SQL Server 2022 |
| Containerización | Docker & Docker Compose |
| Documentación API | Swagger / OpenAPI |
 
---
 
## 🔧 Troubleshooting
 
### Error: "Container sqlserver is unhealthy"
```bash
docker-compose down -v
docker-compose up --build
```
 
### Error: "Connection refused to sqlserver:1433"
Espera 30–45 segundos para que SQL Server se inicialice completamente.
 
### Error: "El proyecto no fue encontrado" (en Docker)
Asegúrate de que `MiApi.csproj` esté en la raíz del proyecto.
 
### Limpiar y resetear todo
```bash
docker-compose down -v
rm -rf bin/ obj/
dotnet clean
docker-compose up --build
```
 
---
 
## 📚 Endpoints
 
| Endpoint | Descripción |
|----------|-------------|
| `GET http://localhost:8080/health` | Health Check |
| `GET http://localhost:8080/swagger` | Documentación Swagger |
 
---
 
## 📞 Referencias
 
- Entity Framework Core: https://docs.microsoft.com/en-us/ef/core/
- Docker: https://docs.docker.com/