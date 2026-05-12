# FashionShop – E-commerce Website

## Project Overview

FashionShop is a comprehensive online fashion retail system built with a modern **Decoupled Architecture** (fully separated Frontend and Backend). The backend is an **ASP.NET Core Web API** handling all business logic, while the frontend is a **Vanilla JavaScript SPA** served as static files.

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | C# (.NET 8), ASP.NET Core Web API |
| ORM | Entity Framework Core 8 |
| Database | SQL Server |
| Security | JWT (JSON Web Token) – role-based Auth (Admin / User) |
| Frontend | HTML5, CSS3, Vanilla JavaScript (ES6+), Fetch API |
| Architecture | Repository Pattern, DTO, Dependency Injection |
| API Docs | Swagger / OpenAPI |

## Project Structure

```
FashionShopAPI/
├── Controllers/        # API endpoint controllers
├── Data/               # DbContext (FashionShopDbContext)
├── Migrations/         # EF Core database migrations
├── Models/             # Entity models
├── Repositories/
│   ├── Interfaces/     # IProductRepository, IUserRepository, etc.
│   └── Implementations/
├── wwwroot/            # Frontend static files (HTML, CSS, JS)
├── Program.cs          # App configuration & middleware pipeline
└── FashionShopAPI.csproj
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (local or remote instance)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/hbphucc/fashionshop-dotnet.git
cd fashionshop-dotnet
```

### 2. Configure `appsettings.json`

Create or update `appsettings.json` (and `appsettings.Development.json`) with your SQL Server connection string and JWT settings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=FashionShopDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyAtLeast32CharactersLong!",
    "Issuer": "FashionShopAPI",
    "Audience": "FashionShopClient"
  }
}
```

> ⚠️ **Never commit real secrets.** Add `appsettings.json` to `.gitignore` or use [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for local development.

### 3. Apply database migrations

```bash
dotnet ef database update
```

This creates the `FashionShopDB` database and applies all migrations found in the `Migrations/` folder.

### 4. Run the application

```bash
dotnet run
```

The API will start on `https://localhost:5001` (or the port shown in the console).

### 5. Explore the API

Open Swagger UI in your browser:

```
https://localhost:5001/swagger
```

You can authenticate via the **Authorize** button using a Bearer token obtained from the login endpoint.

## API Endpoints (overview)

| Resource | Endpoint prefix | Auth required |
|---|---|---|
| Auth (register/login) | `/api/auth` | No |
| Products | `/api/products` | No (read) / Admin (write) |
| Categories | `/api/categories` | No (read) / Admin (write) |
| Orders | `/api/orders` | User / Admin |
| Admin dashboard | `/api/admin` | Admin |

## Key Features

- RESTful API with Repository Pattern for clean separation of concerns
- JWT authentication with role-based access control (Admin / User)
- Admin dashboard with revenue and sales analytics (LINQ + EF Core)
- Dynamic SPA frontend served from `wwwroot`, state managed via LocalStorage
- Server-side price and stock validation for data integrity

## Known Issues / Code Notes

### `Program.cs` middleware order

The current middleware pipeline has two minor issues worth fixing:

**1. `UseDeveloperExceptionPage` should be environment-gated**

```csharp
// Current (always shows detailed errors – a security risk in production):
app.UseDeveloperExceptionPage();

// Fixed:
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
```

**2. `UseHttpsRedirection` should come before `UseCors`**

The standard ASP.NET Core middleware order is:

```csharp
app.UseHttpsRedirection();   // ← move this before UseCors
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

Placing `UseHttpsRedirection` after `UseCors` means CORS headers may not be included in the redirect response, which can cause browser preflight failures.

## License

This project is for educational/portfolio purposes.
