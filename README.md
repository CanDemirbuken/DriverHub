# 🚗 DriverHub

<p align="center">

<img src="https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet" />
<img src="https://img.shields.io/badge/ASP.NET_Core-10-512BD4?style=for-the-badge&logo=dotnet" />
<img src="https://img.shields.io/badge/EF_Core-SQL_Server-6DB33F?style=for-the-badge" />
<img src="https://img.shields.io/badge/CQRS-MediatR-orange?style=for-the-badge" />
<img src="https://img.shields.io/badge/Clean_Architecture-✔-0A66C2?style=for-the-badge" />
<img src="https://img.shields.io/badge/JWT-Authentication-success?style=for-the-badge" />

</p>

<p align="center">

<strong>
A production-oriented car rental backend built with ASP.NET Core, Clean Architecture, CQRS and secure identity management.
</strong>

</p>

---

## 📖 About

DriverHub is a **car rental backend API** designed as a portfolio project around modern .NET backend architecture and real-world application patterns.

The project originally started as a CRUD-focused learning project, but evolved into a more focused rental domain centered around **physical vehicles, fleet management, locations, pricing and reservations**.

Instead of implementing repetitive CRUD operations for unrelated entities, DriverHub focuses on a smaller domain with deeper business behavior.

Current development is divided into two main areas:

- **Admin Fleet Management**
- **Public Rental & Reservation Flow**

---

## 🚀 Technology Stack

### Backend

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

### Architecture

- Clean Architecture
- CQRS + MediatR
- Repository Pattern
- Unit of Work
- Query Services
- Result Pattern
- Dependency Injection

### Security

- ASP.NET Core Identity
- JWT Authentication
- Refresh Token Rotation
- Refresh Token Hashing
- Refresh Token Reuse Detection
- Role-Based Authorization
- Policy-Based Authorization

### Infrastructure

- FluentValidation
- AutoMapper
- Serilog
- Swagger / OpenAPI
- Rate Limiting
- Health Checks
- MailKit

---

## 🏗️ Architecture

DriverHub follows Clean Architecture principles and keeps application rules independent from infrastructure concerns.

```text
                    ┌──────────────────────┐
                    │     Presentation     │
                    │      Web API         │
                    └──────────┬───────────┘
                               │
                               ▼
                    ┌──────────────────────┐
                    │     Application      │
                    │ CQRS / Use Cases     │
                    └──────────┬───────────┘
                               │
                               ▼
                    ┌──────────────────────┐
                    │        Domain        │
                    │ Entities / Enums     │
                    └──────────────────────┘

          ┌────────────────────┐   ┌────────────────────┐
          │    Persistence     │   │   Infrastructure   │
          │ EF Core / Queries  │   │ Identity / JWT     │
          └─────────┬──────────┘   └─────────┬──────────┘
                    │                        │
                    └──────────► Application
```

### Layer Responsibilities

| Layer | Responsibility |
|---|---|
| Domain | Core rental and fleet entities |
| Application | CQRS use cases, validation, contracts and business rules |
| Persistence | EF Core, repositories, database queries and transactions |
| Infrastructure | Identity, JWT, refresh tokens and mail services |
| Presentation | Controllers, middleware, Swagger and HTTP pipeline |

---

## 🚗 Fleet Domain

The current core domain is centered around physical rental vehicles.

```text
Brand
  │
  ▼
Car ───── Category
 │
 ├───── Current Location
 │
 ├───── Car Features
 │
 ├───── Pricing
 │
 └───── Description
```

A `Car` represents a **physical vehicle**, not only a model.

Example:

```text
Toyota Corolla
Plate: 34 ABC 123
VIN: ...
Current Location: Bostancı
Status: Active
```

This allows the system to support real rental scenarios such as vehicle location, status, availability and reservations.

---

## ✨ Admin Fleet Management

Admin-only CQRS operations are currently implemented for:

### Cars

- Create vehicle
- Update vehicle
- Get vehicle details
- Paginated vehicle listing
- Change vehicle status
- Change current location
- Assign vehicle features
- Configure daily / weekly / monthly pricing

### Supporting Data

- Brand management
- Category management
- Location management
- Feature management

Delete operations contain relationship-aware business rules.  
For example, a Brand, Category, Location or Feature cannot be removed while it is actively referenced by vehicles.

---

## 🔐 Identity & Security

DriverHub includes a complete authentication and authorization foundation.

### Authentication

- User Registration
- User Login
- Email Confirmation
- Forgot Password
- Password Reset
- JWT Access Tokens
- Refresh Tokens
- Refresh Token Rotation
- Refresh Token Revocation
- Refresh Token Reuse Detection
- Logout
- Logout From All Sessions

### Authorization

- ASP.NET Core Identity
- Role Management
- User Role Management
- Admin Policy
- Role-Based Authorization
- Policy-Based Authorization
- Fallback Authorization Policy

### Identity Business Rules

- At least one administrator must remain in the system.
- Every user must have at least one role.
- Duplicate role assignments are prevented.
- Protected system roles cannot be removed incorrectly.

---

## ⚙️ Application Patterns

DriverHub uses several reusable backend patterns:

- CQRS with MediatR
- FluentValidation pipeline
- Result Pattern
- Standardized API responses
- Global Exception Middleware
- AutoMapper
- Generic Repository
- Unit of Work
- Query Services
- Database transactions
- EF Core configurations
- User Secrets for local sensitive configuration

---

## 📂 Solution Structure

```text
DriverHub

├── Core
│   ├── DriverHub.Domain
│   └── DriverHub.Application
│       ├── Behaviors
│       ├── Common
│       ├── Interfaces
│       └── Features
│           ├── Entities
│           │   ├── Brands
│           │   ├── Cars
│           │   ├── Categories
│           │   ├── Features
│           │   └── Locations
│           │
│           └── Identity
│               ├── AccountFeatures
│               ├── AuthenticationFeatures
│               ├── RoleFeatures
│               ├── SessionFeatures
│               └── UserRoleFeatures
│
├── Infrastructure
│   ├── DriverHub.Persistence
│   └── DriverHub.Infrastructure
│
└── Presentation
    └── DriverHub.WebApi
```

---

## ⚙️ Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server
- Visual Studio 2022 / Rider / VS Code
- Git

### Clone

```bash
git clone https://github.com/CanDemirbuken/DriverHub.git
cd DriverHub
```

### Configure Local Secrets

DriverHub uses **ASP.NET Core User Secrets** for local sensitive configuration.

Run the commands from:

```text
Presentation/DriverHub.WebApi
```

Initialize User Secrets:

```bash
dotnet user-secrets init
```

Configure the required values:

```bash
dotnet user-secrets set "SqlOptions:ConnectionString" "YOUR_CONNECTION_STRING"
dotnet user-secrets set "Jwt:SecretKey" "YOUR_JWT_SECRET"
dotnet user-secrets set "IdentitySeed:AdminEmail" "YOUR_ADMIN_EMAIL"
dotnet user-secrets set "IdentitySeed:AdminPassword" "YOUR_ADMIN_PASSWORD"
dotnet user-secrets set "Smtp:UserName" "YOUR_SMTP_USERNAME"
dotnet user-secrets set "Smtp:Password" "YOUR_SMTP_PASSWORD"
```

### Apply Migrations

```bash
dotnet ef database update \
  --project Infrastructure/DriverHub.Persistence \
  --startup-project Presentation/DriverHub.WebApi
```

### Run

```bash
dotnet run --project Presentation/DriverHub.WebApi
```

Swagger documentation is available in the **Development** environment.

---

## 📬 API Documentation

Swagger/OpenAPI includes:

- JWT Bearer authentication
- Organized Admin / Identity endpoints
- Request and response contracts
- HTTP status documentation
- Interactive endpoint testing

---

## 🧪 Current Status

### Completed

- ✅ Clean Architecture foundation
- ✅ CQRS + MediatR
- ✅ Repository + Unit of Work
- ✅ Result Pattern
- ✅ Validation Pipeline
- ✅ Global Exception Handling
- ✅ JWT Authentication
- ✅ Refresh Token lifecycle
- ✅ Email Confirmation
- ✅ Forgot / Reset Password
- ✅ Role Management
- ✅ User Role Management
- ✅ Policy-Based Authorization
- ✅ Rate Limiting
- ✅ Health Checks
- ✅ Swagger organization
- ✅ Admin Car Backend
- ✅ Brand Management
- ✅ Category Management
- ✅ Location Management
- ✅ Feature Management

### In Progress

- 🚧 Reservation use cases
- 🚧 Vehicle availability checks
- 🚧 Rental price calculation
- 🚧 Extras and insurance flow
- 🚧 Angular Admin Panel integration

### Planned

- Public rental flow
- Reservation lifecycle management
- Automated tests
- Docker
- CI/CD
- Monitoring / metrics

---

## 🗺️ Domain Roadmap

The next major backend milestone is the rental flow:

```text
Location + Rental Dates
          │
          ▼
Available Vehicle Models
          │
          ▼
Vehicle Selection
          │
          ▼
Pricing Calculation
          │
          ▼
Extras / Insurance
          │
          ▼
Reservation
```

The domain already contains the initial foundation for:

- Reservation
- Reservation Extras
- Insurance Packages
- Rental Extras

These models will evolve together with the upcoming reservation and availability use cases.

---

## 💡 Design Philosophy

DriverHub is intentionally built around **use cases rather than entity count**.

The project prioritizes:

- Clear domain boundaries
- Real business behavior
- Readable architecture
- Secure defaults
- Explicit validation
- Maintainable code
- Small and meaningful abstractions

The goal is not to demonstrate how many CRUD endpoints can be written, but how a backend can evolve from a simple application into a structured rental system.

---

## 👨‍💻 Author

### Yaşarcan Demirbüken

Software Engineer

GitHub  
> https://github.com/CanDemirbuken

LinkedIn  
> https://www.linkedin.com/in/ya%C5%9Farcan-demirb%C3%BCken-09095b205/

---

<p align="center">

Built with ❤️ using ASP.NET Core, Clean Architecture and CQRS.

</p>
