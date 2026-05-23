# ContactsApi

API REST de libreta de contactos desarrollada en **ASP.NET Core (.NET 8)** con **Entity Framework Core** y **SQLite**. Protegida mediante API Key personalizada con middleware.

## Tecnologías

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQLite
- Swagger / OpenAPI

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Estructura del proyecto

```
ContactsApi/
├── Controllers/
│   └── ContactsController.cs     # Endpoint GET /api/contacts
├── Data/
│   └── AppDbContext.cs           # Contexto de base de datos
├── Middleware/
│   └── ApiKeyMiddleware.cs       # Protección por API Key
├── Models/
│   └── Contact.cs                # Modelo de contacto
├── Program.cs                    # Configuración y arranque
├── appsettings.json              # API Key y configuración
└── ContactsApi.csproj
```

## Configuración

La API Key se configura en `appsettings.json`:

```json
{
  "ApiKey": "mi-clave-secreta-12345"
}
```

Cambiá este valor por una clave segura antes de publicar en producción.

## Cómo correr el proyecto

```bash
# 1. Restaurar paquetes NuGet
dotnet restore

# 2. Correr la API
dotnet run
```

Al iniciar por primera vez, la API:
1. Crea automáticamente la base de datos SQLite (`contactos.db`)
2. Crea la tabla `Contacts`
3. Inserta 6 contactos de ejemplo

## Endpoints

| Método | Ruta               | Descripción              |
|--------|--------------------|--------------------------|
| GET    | `/api/contacts`    | Retorna todos los contactos |

### Ejemplo de request

```http
GET /api/contacts
X-Api-Key: mi-clave-secreta-12345
```

### Ejemplo de response

```json
[
  { "id": 1, "nombre": "Juan Pérez",       "telefono": "8888-1111" },
  { "id": 2, "nombre": "María González",   "telefono": "7777-2222" },
  { "id": 3, "nombre": "Carlos Rodríguez", "telefono": "6666-3333" }
]
```

## Protección por API Key

Cada request debe incluir el header `X-Api-Key` con el valor configurado en `appsettings.json`.

- Sin header → `401 Unauthorized`
- Header incorrecto → `403 Forbidden`
- Header correcto → acceso al endpoint

## Base de datos

Se usa **SQLite** mediante Entity Framework Core. El archivo `contactos.db` se genera automáticamente en la raíz del proyecto al correr la aplicación por primera vez. No requiere instalar ningún motor de base de datos externo.

## Documentación interactiva

Con la API corriendo, accedé a Swagger en:

```
http://localhost:<puerto>/swagger
```

## CORS

Configurado para aceptar requests desde `http://localhost:4200` (Angular en desarrollo).

## Proyecto relacionado

Frontend Angular: [contacts-angular](https://github.com/TU_USUARIO/contacts-angular)
