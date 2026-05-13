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

## License

This project is for educational/portfolio purposes.
