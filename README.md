# Shopify

# Overview
Shopify is a simple project designed to view products and add them to cart with CRUD operations. <br>
It supports paginated views of products with filters and integrates with an ASP.NET Core Web API for backend operations. <br>
Project uses clean architecture principles with domain driven design with the use of Entity Framework Core, MediatR for CQRS and asynchronous operations.

# Project Structure
```bash
Shopify
├── Shopify.Web/               # ASP.NET Core Web API (also hosts React SPA)
│   └── ClientApp/             # React frontend with Tailwind, Zustand, TanStack Query
├── Shopify.Application/       # CQRS logic (commands, validators)
├── Shopify.Domain/            # Core domain entities, models and enums
├── Shopify.Infrastructure/    # EF Core, services, repositories, CQRS logic (Queries)
```

# Setup Instructions:

Install Tools (if not already installed):
- .NET 9.0
- React
- Vite
- pnpm

## 1. Open project
open Shopify.sln file in editor

## 2. Install dependencies and build the project
Install pnpm dependencies
```bash
cd Shopify.Web/ClientApp
pnpm install or pnpm i
```

Build .Net project
```bash
dotnet build
```

## 3. Database Setup
- Ensure SQL Server is running.
- [Optional] Update the connection string in <b>appsettings.Development.json</b> in the <b>Shopify.Web</b> project.

## 4. Run the Application
- To start make <b>Shopify.Web</b> as startup project in Rider, Visual Studio or other IDE.
- In <b>launchSettings.json</b> you can start Swagger or Web profile.
- You can start them using the following commands:
```bash
# Swagger
cd src/Shopify.Web
dotnet run
# open https://localhost:7090/swagger/index.html

# Web
cd src/Shopify.Web
dotnet run
# open https://localhost:7089
```

## 5. Migrations
EF Core migrations run automatically on Shopify.Web startup (DatabaseInitializer.cs)

# Features
### Authentication
- Register
- Login
- JWT validation

### Products
- Fetch all products
- Fetch product details
- Search
- Filter
- Sort
- Paginated
- In-memory caching for performance

### Cart
- Add/remove items
- Sync with backend
- Zustand + backend persistence

### Favorites
- Add/remove favorite products
- User-specific favorites

### CI
- GitHub Actions workflow for Pull Request verification (lint, build) - PR Verify

# Project Components
## Shopify.Web
    React project for managing products, user and cart through a UI.

## Shopify.Application
    Contains application logic, including commands, interfaces, DTOs.
    Implements the CQRS pattern with MediatR.

## Shopify.Domain
    Contains domain entities, models and core interfaces.
    Represents the core business logic and rules.

## Shopify.Infrastructure
    Provides database context, repositories, and persistence logic.
    Uses Entity Framework Core.