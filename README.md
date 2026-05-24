# MiApi - API REST de Gestión de Productos y Categorías

Una API RESTful moderna desarrollada con **.NET 8** y **SQL Server**, containerizada con **Docker**, e implementada con **CI/CD automático** usando **Azure Pipelines**. El proyecto proporciona un sistema completo de gestión de productos organizados por categorías con integración de base de datos, pruebas unitarias y despliegue automático.

---

## 📋 Descripción del Proyecto

**MiApi** es una aplicación backend robusta que implementa operaciones CRUD completas sobre dos entidades principales:

- **Categorías**: Clasificaciones de productos con descripción
- **Productos**: Artículos con precio, stock, imagen y referencia a categoría

La API está diseñada para ser escalable, mantenible y fácil de desplegar mediante contenedores Docker y automatización de CI/CD.

---

## 🏗️ Arquitectura y Tecnologías

| Componente | Tecnología |
|-----------|-----------|
| **Framework** | .NET 8.0 |
| **ORM** | Entity Framework Core 8.0 |
| **Base de Datos** | SQL Server |
| **Documentación API** | Swagger/OpenAPI |
| **Containerización** | Docker & Docker Compose |
| **CI/CD** | Azure Pipelines |
| **Testing** | xUnit |

---

## 📋 Requisitos Previos

### Para ejecutar con Docker:
- Docker Desktop instalado ([Descargar](https://www.docker.com/products/docker-desktop))

### Para desarrollo local:
- .NET 8 SDK ([Descargar](https://dotnet.microsoft.com/download))
- SQL Server Management Studio (opcional)
- Visual Studio Code o Visual Studio

---

## 🚀 Inicio Rápido

### 1. Clonar el repositorio
```bash
git clone https://github.com/daviddlv007/diplo.git
cd diplo
```

### 2. Opción A: Ejecutar con Docker Compose (Recomendado)
```bash
docker-compose up --build
```

**Servicios disponibles después del inicio:**
- 🐳 **SQL Server**: `localhost:1433`
- 🌐 **API .NET**: `http://localhost:8080`
- 📚 **Swagger UI**: `http://localhost:8080/swagger`
- **Usuario SQL**: `sa`
- **Contraseña SQL**: `Admin12345*`

### 2. Opción B: Ejecución local sin Docker

```bash
# Restaurar dependencias
dotnet restore

# Aplicar migraciones (crea la BD automáticamente al iniciar)
dotnet ef database update

# Ejecutar la aplicación
dotnet run
```

La API estará disponible en `https://localhost:5001` y Swagger en `https://localhost:5001/swagger`

---

## 📝 Configuración

### Variables de Entorno y Cadena de Conexión

**En Docker (automático via `docker-compose.yml`):**
```yaml
Database: MiApiDB
User: sa
Password: Admin12345*
Server: sqlserver:1433
```

**Localmente (`appsettings.json`):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MiApiDB;Trusted_Connection=True;"
  }
}
```

**En desarrollo (`appsettings.Development.json`):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=sqlserver;Database=MiApiDB;User Id=sa;Password=Admin12345*;"
  }
}
```

---

## 📚 Estructura del Proyecto

```
MiApi/
├── Controllers/          # Controladores de API
│   ├── ProductosController.cs
│   └── CategoriasController.cs
├── Models/              # Entidades de dominio
│   ├── Producto.cs
│   ├── Categoria.cs
│   └── DTOs.cs         # Data Transfer Objects
├── Data/               # Contexto de Entity Framework
│   └── AppDbContext.cs
├── Migrations/         # Migraciones de BD
├── Properties/         # Configuración del proyecto
├── Program.cs          # Punto de entrada y configuración
├── docker-compose.yml  # Orquestación de contenedores
├── Dockerfile         # Imagen Docker de la aplicación
├── MiApi.csproj       # Archivo de proyecto
├── appsettings.json   # Configuración general
└── azure-pipelines.yml # Definición de CI/CD
```

---

## 🔌 Endpoints de la API

### Categorías

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/categorias` | Obtener todas las categorías |
| `GET` | `/api/categorias/{id}` | Obtener categoría con sus productos |
| `POST` | `/api/categorias` | Crear nueva categoría |
| `PUT` | `/api/categorias/{id}` | Actualizar categoría |
| `DELETE` | `/api/categorias/{id}` | Eliminar categoría |

**Ejemplo POST /api/categorias:**
```json
{
  "nombre": "Electrónica",
  "descripcion": "Productos electrónicos y accesorios"
}
```

### Productos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/productos` | Obtener todos los productos con categoría |
| `GET` | `/api/productos/{id}` | Obtener un producto específico |
| `POST` | `/api/productos` | Crear nuevo producto |
| `PUT` | `/api/productos/{id}` | Actualizar producto |
| `DELETE` | `/api/productos/{id}` | Eliminar producto |

**Ejemplo POST /api/productos:**
```json
{
  "nombre": "Laptop HP",
  "descripcion": "Laptop de 15 pulgadas",
  "imagenUrl": "https://ejemplo.com/laptop.jpg",
  "precio": 899.99,
  "stock": 50,
  "categoriaId": 1
}
```

---

## 🔄 CI/CD con Azure Pipelines

### Overview del Pipeline

El proyecto implementa un **pipeline de 3 etapas** completamente automatizado:

```
COMMIT → CI (Build) → Testing → Delivery (Publish)
```

### 1️⃣ Etapa CI - Integración Continua

**Trigger**: Cambios en rama `main`

**Pasos:**
```yaml
- Restaurar dependencias (.NET packages)
- Compilar la solución en configuración Release
```

**Output**: Validación de código compilable

### 2️⃣ Etapa Testing - Pruebas Unitarias

**Dependencia**: CI completada exitosamente

**Pasos:**
```yaml
- Restaurar dependencias
- Ejecutar pruebas unitarias (xUnit)
- Publicar resultados de pruebas
```

**Ubicación de pruebas**: `tests/MiApi.Tests/`

**Output**: Reporte de cobertura en formato VSTest (`.trx`)

### 3️⃣ Etapa Delivery - Entrega y Publicación

**Dependencia**: Testing completado exitosamente

**Pasos:**
```yaml
- Restaurar dependencias
- Publicar aplicación (Release build)
- Generar artefacto final para despliegue
```

**Artefacto generado**: `miapi-drop`
**Ubicación**: Azure Container Registry / Azure DevOps Artifacts

### Flujo Completo de CI/CD

```
Commit a main → Stage: CI
  ├─ Restore NuGet
  ├─ Build Release
  └─ ¿Éxito? → Sí → Stage: Testing
                ├─ Run Unit Tests
                ├─ Publish Results
                └─ ¿Todos OK? → Sí → Stage: Delivery
                                ├─ Publish App
                                ├─ Create Artifact
                                └─ ✅ Ready for Deploy
```

### Configuración en `azure-pipelines.yml`

```yaml
trigger:
  - main                    # Se ejecuta en cada push a main

pool:
  name: Default             # Pool de agentes Azure DevOps

variables:
  buildConfiguration: 'Release'  # Compilación en Release

stages:
  - stage: CI               # Build
  - stage: Testing          # Unit Tests (depends on CI)
  - stage: Delivery         # Publish (depends on Testing)
```

### Ejecución y Monitoreo

1. **Iniciar**: Cada commit a `main` dispara automáticamente el pipeline
2. **Monitorear**: En Azure DevOps > Pipelines > MiApi-Pipeline
3. **Resultados**: Artefactos listos en `miapi-drop` tras éxito
4. **Logs**: Disponibles en cada etapa para debugging

### Condiciones de Éxito

✅ **CI**: Compilación sin errores
✅ **Testing**: Todas las pruebas pasan
✅ **Delivery**: Artefacto publicado correctamente

Si alguna etapa falla, el pipeline se detiene y se notifica al equipo.

---

## 🗄️ Base de Datos

### Modelos

**Categoria**
```csharp
public class Categoria
{
    public int CategoriaId { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public ICollection<Producto> Productos { get; set; }
}
```

**Producto**
```csharp
public class Producto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? ImagenUrl { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
```

### Migraciones

Las migraciones se aplican **automáticamente** al iniciar la aplicación:

```csharp
// En Program.cs
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Crea/actualiza la BD automáticamente
}
```

---

## 🧪 Testing

### Ejecutar Pruebas Localmente

```bash
dotnet test tests/MiApi.Tests/MiApi.Tests.csproj
```

### Ubicación de Tests
- `tests/MiApi.Tests/UnitTest1.cs`

### Framework
- **xUnit**: Framework de testing
- **Entity Framework Core**: Contexto de BD para pruebas
- **Coverlet**: Cobertura de código

---

## 🐳 Docker

### Construir Imagen Local

```bash
docker build -t miapi:latest .
```

### Ejecutar Contenedor Individual

```bash
docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development miapi:latest
```

### Docker Compose Services

```yaml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server
    ports:
      - "1433:1433"
    environment:
      SA_PASSWORD: Admin12345*

  api:
    build: .
    ports:
      - "8080:80"
    depends_on:
      - sqlserver
```

---

## 📂 Archivos Importantes

| Archivo | Propósito |
|---------|-----------|
| `Program.cs` | Configuración de la aplicación y Dependency Injection |
| `appsettings.json` | Configuración de producción |
| `appsettings.Development.json` | Configuración de desarrollo |
| `docker-compose.yml` | Definición de servicios Docker |
| `Dockerfile` | Imagen de la aplicación |
| `azure-pipelines.yml` | Pipeline de CI/CD |
| `MiApi.csproj` | Dependencias y propiedades del proyecto |

---

## 🤝 Contribuir

1. Fork el repositorio
2. Crear rama de feature (`git checkout -b feature/AmazingFeature`)
3. Commit cambios (`git commit -m 'Add AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir Pull Request

### Checklist antes de hacer PR:
- [ ] Código compilable
- [ ] Todas las pruebas pasan
- [ ] Documentación actualizada
- [ ] Cambios basados en rama `main` actual

---

## 📄 Licencia

Este proyecto está bajo licencia MIT. Consultar `LICENSE` para más detalles.

---

## 👤 Autor

Desarrollado como proyecto final del **Diplomado en Desarrollo .NET**
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
| `GET http://localhost:8080/swagger` | Documentación Swagger |
 
---
 
## 📞 Referencias
 
- Entity Framework Core: https://docs.microsoft.com/en-us/ef/core/
- Docker: https://docs.docker.com/