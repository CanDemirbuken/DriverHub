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

## 📅 Reservation Availability & Preparation

The Admin API supports the following workflow through CQRS/MediatR handlers, FluentValidation, the Result Pattern, Query Services and Repository/Unit of Work:

```text
Availability → Quote → Pricing + Extras / Insurance → Confirmation → Reservation
```

- Availability filters physical vehicles in the database by pickup location, date range and `CarStatus.Active`.
- A reservation blocks availability only when its status is `Pending` or `Confirmed` and `existing.StartDate < requested.EndDate && existing.EndDate > requested.StartDate`. `Cancelled` and `Completed` do not block; adjacent intervals are allowed.
- Quote generation validates the selected vehicle and location without inserting a reservation or holding the vehicle.
- Pricing is calculated on the server using the vehicle's Daily, Weekly and Monthly `CarPricing` entries. Rental duration rounds partial days up to whole days, with a minimum of one day.
- Rental extras use the existing `Extra` model; extras and `InsurancePackage` selection are optional. Selected IDs must exist. Empty catalogs are supported, and unselected extras/insurance contribute zero.
- Extras and insurance are charged by daily price multiplied by rental days. `ReservationExtra` stores the selected extra's unit price and total price snapshot.
- Final creation rechecks vehicle status, pickup location, dates, selected options and pricing, recalculates all totals, and repeats overlap validation inside the save transaction. Client-calculated prices are not part of the create contract.
- Creation persists a `Pending` reservation and its extras together. Return location currently equals pickup location; the authenticated administrator supplies the reservation user identity.
- Availability, quote and create share a five-minute start-time grace period measured from the current UTC minute. Older starts and `end <= start` are rejected; the Angular client applies the matching policy.

Quotes are estimates, not price locks or inventory holds. Creation and lifecycle commands serialize writes on the physical car row with SQL Server `UPDLOCK, HOLDLOCK` inside the existing Unit of Work transaction. Approval rechecks dates, active status, pickup location and overlapping Pending/Confirmed reservations. Pricing uses complete 30-day months, then weeks, then remaining days; existing reservation price snapshots are unchanged.

### Admin reservation management

- `/admin/reservations`: paged history, customer/vehicle search, status and rental-period filters, creation-date ordering, approval/cancellation and detail navigation.
- `/admin/reservations/create`: existing availability/quote/options flow, creating a Pending request for the authenticated admin account. Creating on behalf of another customer is not an existing use case.
- `/admin/reservations/:id`: customer snapshot, vehicle, pickup/return locations, server-calculated rental days, saved pricing and admin processing audit.
- `GET /api/reservations`, `GET /api/reservations/{id}`, `POST /api/reservations/{id}/approve`, `POST /api/reservations/{id}/cancel` all require `AdminOnly`.
- List parameters: `PageNumber` (default 1), `PageSize` (default 10, maximum 100), `Status`, `Search`, `CarId`, `From`, `To`, `OldestFirst`. Date filters select rental periods intersecting `[From, To)`. Dates in responses are UTC; Angular displays local time.
- First/last name, email and optional phone are resolved server-side from the active Identity account and stored as a snapshot. Client-supplied user IDs, prices, customer snapshots and status are not bound to the create command.
- `ProcessedAt` / `ProcessedBy` capture the single allowed admin decision. Repeating the same successful approve/cancel operation returns success without rewriting the audit or creating another notification. Other transitions return Conflict.

```text
Created → Pending
          ├── Approved (existing Confirmed = 2)
          └── Cancelled
Completed = 4 remains a supported stored status; no completion operation is introduced.
```

Migration `20260915073331_AddReservationManagement` adds nullable customer/audit columns, query indexes and `EmailOutboxMessages`. Existing status values and prices are preserved. Legacy customer snapshots are initialized from the current Identity account because reservation-time values are unavailable; historical admin dates/actors remain null. Old migrations are unchanged. Apply the new migration before starting the updated API.

### Reservation email delivery

Create/approve/cancel enqueue an email in the **same database transaction** as the reservation change. A hosted worker sends it using the existing `IMailService` / MailKit SMTP configuration and a central HTML-encoded template. No SMTP request runs inside a reservation transaction.

The worker locks one outbox message per delivery across instances, preserves lifecycle order per reservation, uses a 30-second delivery timeout, and retries failures with exponential delay (up to six hours). State changes stay committed when SMTP fails. Unique `(ReservationId, Status)` prevents duplicate enqueue on retries. Delivery is **at least once**: a crash after SMTP accepts a message but before its receipt commits can cause a duplicate email. Successful receipts retain IDs/timestamps while recipient/body are cleared. Monitor unsent rows, attempt counts and worker warning/error logs; sustained SMTP errors need operator attention. No historical emails are sent by the migration.

Existing `Smtp` settings and secret storage are reused. Keep credentials in user-secrets or environment configuration, never in source control.

### Reservation validation

`Tests/DriverHub.Tests` uses xUnit, real SQL Server transactions/migrations, a local HTTP host and fake mail delivery. It always creates and removes uniquely named `DriverHub_ReservationTests_*` databases and never loads the application's database configuration. Windows defaults to `(localdb)\MSSQLLocalDB`; set `DRIVERHUB_TEST_SQL_SERVER` to a dedicated SQL Server instance with integrated authentication if needed.

```bash
dotnet test Tests/DriverHub.Tests/DriverHub.Tests.csproj
```

Tests cover customer snapshots, Pending creation, valid/invalid/idempotent transitions, authorization and mass assignment, date/vehicle/overlap rules, concurrent requests, pagination, pricing boundaries, clean/upgrade migrations, and notification failure handling. If a running API locks Debug DLLs, use `-p:OutputPath=bin/ReservationValidation/` for build/test validation.

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
- Reservation availability, quote and explicit confirmation workflow
- Route-level lazy loading for admin feature screens
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

The design-time factory lives in `DriverHub.WebApi` and reads JSON configuration, Development User Secrets, environment variables and command-line overrides. Its default environment is Development; use `-- --environment Production` explicitly when needed. Set `SqlOptions:ConnectionString` in the WebApi project's User Secrets or `SqlOptions__ConnectionString` in the environment. An empty connection fails with a configuration error before attempting a database connection.

Visual Studio Package Manager Console:

```powershell
Update-Database -Project DriverHub.Persistence -StartupProject DriverHub.WebApi -Context AppDbContext
```

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
- ✅ Reservation availability and overlap filtering
- ✅ Pre-reservation quote and server-side pricing integration
- ✅ Optional extras / insurance and reservation extra price snapshots
- ✅ Explicit reservation creation with final rule revalidation
- ✅ Shared reservation start-time grace-period validation

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
- ✅ Reservation search, quote, options and confirmation
- ✅ Reservation success state and UI duplicate-submit protection
- ✅ Admin feature route-level lazy loading
- 🚧 Vehicle feature management

### In Progress

- 🚧 Angular Admin Panel
- ✅ Reservation admin history/detail, lifecycle decisions, customer snapshots and durable email delivery

### Planned

- Public rental flow
- Reservation completion operation and public customer flow
- Broader automated test coverage
- Docker
- CI/CD
- Monitoring / metrics

---

## 🗺️ Domain Roadmap

The administrative reservation preparation and creation flow is available:

```text
Location + Rental Dates
          │
          ▼
Available Physical Vehicles
          │
          ▼
Vehicle Selection
          │
          ▼
Quote / Server-Side Pricing
          │
          ▼
Extras / Insurance
          │
          ▼
Explicit Confirmation → Pending Reservation
```

The flow uses the existing domain models:

- Reservation
- Reservation Extras
- Insurance Packages
- Rental Extras (`Extra`)

Next steps include public rental screens, a completion operation, broader test coverage and operational monitoring for the email outbox.

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
