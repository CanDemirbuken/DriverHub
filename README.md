# 🚗 DriverHub

<p align="center">

<img src="https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet" />
<img src="https://img.shields.io/badge/Angular-22-DD0031?style=for-the-badge&logo=angular" />
<img src="https://img.shields.io/badge/ASP.NET_Core-10-512BD4?style=for-the-badge&logo=dotnet" />
<img src="https://img.shields.io/badge/EF_Core-SQL_Server-6DB33F?style=for-the-badge" />
<img src="https://img.shields.io/badge/CQRS-MediatR-orange?style=for-the-badge" />
<img src="https://img.shields.io/badge/Clean_Architecture-✔-0A66C2?style=for-the-badge" />
<img src="https://img.shields.io/badge/JWT-Authentication-success?style=for-the-badge" />

</p>

<p align="center">

<strong>
A full-stack car rental platform built with ASP.NET Core, Angular, Clean Architecture, CQRS and secure identity management.
</strong>

</p>

---

## 📖 About

DriverHub is a **car rental platform** designed as a portfolio project around modern .NET backend architecture, Angular frontend development and real-world application patterns.

The project originally started as a CRUD-focused learning project, but evolved into a more focused rental domain centered around **physical vehicles, fleet management, locations, pricing and reservations**.

Instead of implementing repetitive CRUD operations for unrelated entities, DriverHub focuses on a smaller domain with deeper business behavior.

The application is divided into two main areas:

- **Admin Fleet Management**
- **Public Rental & Reservation Flow**

The backend provides the application, domain, security and persistence foundation, while the Angular client provides the administrative and future public-facing user interfaces.

---

## 🚀 Technology Stack

### Backend

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

### Frontend

- Angular
- TypeScript
- Angular Signals
- Angular Router
- HttpClient
- SCSS
- Standalone Components

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

## 🏗️ Backend Architecture

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

A `Car` represents a **physical vehicle**, not only a vehicle model.

Example:

```text
Toyota Corolla
Plate: 34 ABC 123
VIN: ...
Current Location: Bostancı
Status: Active
```

This allows the system to support real rental scenarios such as vehicle location, operational status, availability, pricing and reservations.

---

## ✨ Admin Fleet Management

Admin-only operations are implemented around the core fleet domain.

### Cars

- Create vehicle
- Update vehicle
- Get vehicle details
- Paginated vehicle listing
- Change vehicle status
- Change current location
- Assign vehicle features
- Configure daily / weekly / monthly pricing
- Upload and manage vehicle images

### Supporting Data

- Brand management
- Category management
- Location management
- Feature management

Delete operations contain relationship-aware business rules.

For example, a Brand, Category, Location or Feature cannot be removed while it is actively referenced by vehicles.

---

## 🖥️ Angular Client

DriverHub includes an Angular client for the Admin Panel and future public rental experience.

The current Admin Panel communicates directly with the ASP.NET Core Web API and includes:

- Standalone Angular architecture
- Admin and public layouts
- Route guards
- API service layer
- Standardized API response models
- Car listing
- Car detail screen
- Car creation and editing
- Vehicle image upload and preview
- Inline vehicle status management
- Inline vehicle location management
- Vehicle pricing management
- Global toast notifications
- Shared helpers and reusable UI infrastructure

Detailed frontend documentation is available in:

```text
Client/DriverHub.Client/README.md
```

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

The Angular client follows similar separation principles through:

- Feature components
- Core API services
- Shared UI components
- Shared helpers
- Typed request / response models
- Centralized route definitions
- Signal-based local UI state

---

## 📂 Repository Structure

```text
DriverHub

├── Client
│   └── DriverHub.Client
│
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

## ⚙️ Backend Getting Started

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

### Run API

```bash
dotnet run --project Presentation/DriverHub.WebApi
```

Swagger documentation is available in the **Development** environment.

---

## 🌐 Frontend Getting Started

Navigate to the Angular client:

```bash
cd Client/DriverHub.Client
```

Install dependencies:

```bash
npm install
```

Start the development server:

```bash
ng serve
```

For detailed Angular client documentation, see:

```text
Client/DriverHub.Client/README.md
```

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

### Backend Completed

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

### Angular Admin Panel

- ✅ Admin layout and navigation
- ✅ Angular routing foundation
- ✅ Route guards
- ✅ API integration foundation
- ✅ Car listing
- ✅ Car details
- ✅ Car creation
- ✅ Car editing
- ✅ Vehicle media upload
- ✅ Global toast notifications
- ✅ Vehicle status management
- ✅ Vehicle location management
- ✅ Vehicle pricing management
- 🚧 Vehicle feature management

### In Progress

- 🚧 Angular Admin Panel
- 🚧 Reservation use cases
- 🚧 Vehicle availability checks
- 🚧 Rental price calculation
- 🚧 Extras and insurance flow

### Planned

- Public rental flow
- Reservation lifecycle management
- Automated tests
- Docker
- CI/CD
- Monitoring / metrics

---

## 🗺️ Domain Roadmap

The next major domain milestone is the rental flow:

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
- Clear frontend/backend separation
- User-oriented administrative workflows

The goal is not to demonstrate how many CRUD endpoints or screens can be written, but how an application can evolve from a simple project into a structured rental platform.

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

Built with ❤️ using ASP.NET Core, Angular, Clean Architecture and CQRS.

</p>
