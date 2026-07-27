# 🚗 DriverHub

[![GitHub](https://img.shields.io/badge/GitHub-CanDemirbuken-181717?style=for-the-badge&logo=github)](https://github.com/CanDemirbuken)
![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10-512BD4?style=for-the-badge&logo=dotnet)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-10-6DB33F?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![CQRS](https://img.shields.io/badge/CQRS-MediatR-FF6F00?style=for-the-badge)
![AutoMapper](https://img.shields.io/badge/AutoMapper-Enabled-BE202D?style=for-the-badge)
![JWT](https://img.shields.io/badge/JWT-Authentication-000000?style=for-the-badge&logo=jsonwebtokens)
![Clean Architecture](https://img.shields.io/badge/Clean_Architecture-✔-0A66C2?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-In_Progress-orange?style=for-the-badge)

---

> 🚧 **Project Status**
>
> DriverHub is currently under active development and is being built as a long-term reference project for modern ASP.NET Core backend development.
>
> The project intentionally prioritizes architectural quality over implementation speed. Design decisions are continuously reviewed and refined to improve maintainability, readability, scalability, security, testability, and production readiness.
>
> The current backend includes a standardized CRUD architecture, CQRS, Result Pattern, AutoMapper, FluentValidation pipelines, ASP.NET Core Identity, JWT authentication, secure refresh token hashing, and refresh token rotation.

---

# 📖 Overview

**DriverHub** is a production-oriented **ASP.NET Core 10 Web API** built using **Clean Architecture**, **Vertical Slice Architecture**, and **CQRS**.

Rather than serving as a simple CRUD application, DriverHub focuses on implementing modern backend architecture, explicit application contracts, secure authentication flows, consistent API responses, clean code principles, and enterprise-oriented development practices.

The primary objective is not only to implement features, but to implement them in a way that remains:

- Maintainable
- Readable
- Testable
- Secure
- Scalable
- Extensible
- Production-oriented

---

# 🎯 Design Principles

DriverHub is built around the following principles:

- Clean Architecture
- Vertical Slice Architecture
- CQRS with MediatR
- Separation of Concerns
- Feature-Based Organization
- Explicit Application Contracts
- Result Pattern for Expected Outcomes
- Centralized Validation
- Centralized API Response Mapping
- RESTful API Standards
- Query Projection
- Secure Authentication
- Continuous Refactoring
- Pragmatic Abstraction

Architectural patterns and libraries are not applied blindly. Each tool is used only where it provides clear value.

For example:

- AutoMapper is used for command-to-entity and entity-to-response mappings.
- LINQ projection is preserved for optimized read queries.
- Expected business outcomes are handled through Result objects.
- Exceptions remain reserved for validation pipeline failures and unexpected technical conditions.
- Refresh tokens are stored as hashes instead of plain text.
- Route parameters are treated as the single source of truth for update identifiers.

---

# 🏛️ Solution Architecture

```text
DriverHub
│
├── Core
│   ├── DriverHub.Domain
│   └── DriverHub.Application
│
├── Infrastructure
│   ├── DriverHub.Persistence
│   └── DriverHub.Infrastructure
│
└── Presentation
    └── DriverHub.WebApi
```

The solution follows Clean Architecture dependency rules.

```text
Domain
   ↑
Application
   ↑
Persistence / Infrastructure
   ↑
WebApi
```

## Layer Responsibilities

### DriverHub.Domain

Contains:

- Domain entities
- Base entity abstractions
- Entity relationships
- Framework-independent domain models

The Domain layer does not depend on Application, Persistence, Infrastructure, or WebApi.

### DriverHub.Application

Contains:

- Commands and queries
- MediatR handlers
- FluentValidation validators
- Result Pattern
- Application contracts
- Repository interfaces
- Query service interfaces
- Authentication interfaces
- AutoMapper profiles
- Pipeline behaviors
- Shared application models
- Constants and response contracts

### DriverHub.Persistence

Contains:

- Entity Framework Core DbContext
- Generic repository implementation
- Query service implementations
- Entity configurations
- ASP.NET Core Identity entities
- Refresh token persistence
- Database migrations
- Identity seeding
- Application data seeding
- Audit field management

### DriverHub.Infrastructure

Contains:

- Authentication service implementation
- JWT access token generation
- Refresh token generation
- Refresh token hashing
- Infrastructure options
- Authentication and security-related service implementations

### DriverHub.WebApi

Contains:

- API controllers
- Standardized API response models
- Base controller
- Swagger / OpenAPI configuration
- Middleware registration
- Dependency injection composition
- Authentication pipeline configuration
- HTTP response mapping

---

# 🚀 Technology Stack

- .NET 10
- ASP.NET Core 10 Web API
- Entity Framework Core 10
- SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication
- MediatR
- FluentValidation
- AutoMapper
- Serilog
- Swagger / OpenAPI
- Clean Architecture
- Vertical Slice Architecture
- CQRS
- Repository Pattern
- Unit of Work
- Result Pattern
- RESTful API Design

---

# 📁 Feature Organization

Application use cases are organized by feature and operation.

```text
Features
└── CarFeatures
    ├── Commands
    │   ├── CreateCar
    │   │   ├── CreateCarCommand.cs
    │   │   ├── CreateCarCommandHandler.cs
    │   │   ├── CreateCarCommandResponse.cs
    │   │   └── CreateCarCommandValidator.cs
    │   │
    │   ├── UpdateCar
    │   │   ├── UpdateCarCommand.cs
    │   │   ├── UpdateCarCommandHandler.cs
    │   │   └── UpdateCarCommandValidator.cs
    │   │
    │   └── RemoveCar
    │       ├── RemoveCarCommand.cs
    │       ├── RemoveCarCommandHandler.cs
    │       └── RemoveCarCommandValidator.cs
    │
    └── Queries
        ├── GetCarByIdWithBrand
        │   ├── GetCarByIdWithBrandQuery.cs
        │   ├── GetCarByIdWithBrandQueryHandler.cs
        │   ├── GetCarByIdWithBrandQueryResponse.cs
        │   └── GetCarByIdWithBrandQueryValidator.cs
        │
        └── GetPagedCarsWithBrand
            ├── GetPagedCarsWithBrandQuery.cs
            ├── GetPagedCarsWithBrandQueryHandler.cs
            ├── GetPagedCarsWithBrandQueryResponse.cs
            └── GetPagedCarsWithBrandQueryValidator.cs
```

This structure keeps each use case self-contained and makes the codebase easier to navigate, review, test, and extend.

---

# ✅ Implemented Features

## Architecture

- Clean Architecture
- Vertical Slice Architecture
- CQRS with MediatR
- Feature-Based Organization
- Repository Pattern
- Unit of Work
- Result Pattern
- AutoMapper Profiles
- Centralized Dependency Injection
- Explicit Application Contracts
- Separation of read and write responsibilities

## Validation

- FluentValidation
- Validation Pipeline Behavior
- Automatic Validator Discovery
- Centralized Validation Handling
- Standardized Validation Responses
- Reusable identifier and pagination validation rules

## Error Handling

- Global Exception Middleware
- RFC7807 ProblemDetails
- TraceId Support
- Result-Based Expected Error Handling
- Standardized API Error Contracts
- Consistent HTTP Status Code Handling
- Identity Error Mapping
- Client cancellation handling

## Data Access

- Entity Framework Core Code First
- SQL Server
- Generic Repository
- Unit of Work
- Fluent API Configurations
- Automatic Entity Configuration Discovery
- Automatic Audit Fields
- Pagination
- Query Projection
- No-Tracking Queries
- Entity-specific Query Services
- Ordered and deterministic paginated queries

## Mapping

- AutoMapper Integration
- Feature-Specific Mapping Profiles
- Command-to-Entity Mapping
- Entity-to-Response Mapping
- Existing Entity Update Mapping
- Protected Id and Audit Fields
- LINQ Projection for optimized list queries

## Authentication

- ASP.NET Core Identity
- User Registration
- User Login
- JWT Access Token Generation
- Refresh Token Generation
- Secure Refresh Token Hashing
- Refresh Token Persistence
- Refresh Token Rotation
- Previous Token Revocation
- Replacement Token Tracking
- Account Lockout
- Identity Error Mapping
- Identity Seeding
- Role Seeding
- Claims-Based Authentication Infrastructure
- Role-Based Authentication Infrastructure
- JWT Issuer Validation
- JWT Audience Validation
- JWT Lifetime Validation
- JWT Signature Validation
- Zero Clock Skew
- Cryptographically Secure Refresh Tokens

## API

- RESTful API Design
- Standardized HTTP Status Codes
- Standardized ApiResponse Model
- Centralized Result-to-HTTP Mapping
- Swagger / OpenAPI
- ProducesResponseType Documentation
- Explicit Request / Response Contracts
- Route-Based Update Identifiers
- Clean Update Request Bodies
- CancellationToken Support

## Logging

- Serilog Integration
- Structured Application Logging
- Request Pipeline Logging
- Centralized Error Logging

---

# 🔄 Result Pattern

Expected business outcomes are represented using Result objects instead of exceptions.

Examples include:

- Record not found
- Duplicate entity
- Invalid login credentials
- Locked user account
- Invalid or expired refresh token
- Role assignment failure
- Inactive or deleted users

Example flow:

```text
Handler
   ↓
Result.Success(...)
or
Result.Failure(...)
   ↓
BaseController
   ↓
Standardized ApiResponse
```

This keeps expected application behavior explicit and prevents exceptions from being used as normal control flow.

---

# 🗺️ Mapping Strategy

DriverHub uses different mapping strategies depending on the use case.

## AutoMapper

AutoMapper is used for in-memory object transformations:

```text
Create Command → Entity
Update Command → Existing Entity
Entity → Detail Response
```

## Query Projection

Read-heavy and paginated queries use direct LINQ projection:

```text
Database Query → Response DTO
```

This prevents unnecessary entity materialization and allows Entity Framework Core to select only the required database columns.

AutoMapper is therefore used where it improves readability, while projection is preserved where it improves query efficiency.

---

# 🔐 Authentication

DriverHub uses **ASP.NET Core Identity** together with **JWT Bearer Authentication**.

## Login Flow

```text
Login Request
   ↓
User Lookup
   ↓
Active / Deleted User Check
   ↓
Password Verification
   ↓
Account Lockout Check
   ↓
Role Lookup
   ↓
JWT Access Token Generation
   ↓
Refresh Token Generation
   ↓
Refresh Token Hashing
   ↓
Hashed Token Persistence
   ↓
Access Token + Refresh Token Response
```

## Refresh Token Flow

```text
Refresh Token Request
   ↓
Token Hashing
   ↓
Stored Token Lookup
   ↓
Expiration and Revocation Check
   ↓
User Validation
   ↓
Role Lookup
   ↓
New Access Token Generation
   ↓
New Refresh Token Generation
   ↓
Old Token Revocation
   ↓
Replacement Token Hash Assignment
   ↓
New Token Persistence
   ↓
New Token Pair Response
```

## Security Decisions

- Plain refresh tokens are never stored in the database.
- Only refresh token hashes are persisted.
- Refresh tokens are generated using cryptographically secure random bytes.
- Old refresh tokens are revoked during rotation.
- Replacement token hashes are stored for token chain tracking.
- Reused or revoked refresh tokens are rejected.
- Login failures use generic messages to reduce account enumeration risk.
- Account lockout is enabled for repeated failed login attempts.
- JWT validation includes issuer, audience, lifetime, and signing key checks.

---

# 🌐 API Response Standard

All endpoints return a consistent response structure.

Example successful response:

```json
{
  "isSuccess": true,
  "data": {
    "id": "00000000-0000-0000-0000-000000000000"
  },
  "errors": []
}
```

Example failure response:

```json
{
  "isSuccess": false,
  "data": null,
  "errors": [
    {
      "field": null,
      "message": "The requested record was not found."
    }
  ]
}
```

Successful update and delete operations return:

```http
204 No Content
```

---

# 🛣️ Roadmap

## Architecture

- [x] Clean Architecture
- [x] Vertical Slice Architecture
- [x] CQRS
- [x] MediatR
- [x] FluentValidation
- [x] Repository Pattern
- [x] Unit of Work
- [x] Result Pattern
- [x] AutoMapper
- [x] Validation Pipeline Behavior
- [x] RFC7807 ProblemDetails
- [x] Global Exception Handling
- [x] RESTful API Standards
- [x] HTTP Contract Standardization
- [x] Standardized API Responses
- [x] Route-Based Update Identifiers

## Data Access

- [x] Entity Framework Core
- [x] SQL Server
- [x] Code First
- [x] Migrations
- [x] Pagination
- [x] Query Projection
- [x] No-Tracking Queries
- [x] Automatic Audit Fields
- [ ] Response Caching
- [ ] Database Constraint Hardening

## Security

- [x] ASP.NET Core Identity
- [x] JWT Authentication
- [x] Identity Seeding
- [x] Role Seeding
- [x] Refresh Token Infrastructure
- [x] Secure Refresh Token Hashing
- [x] Refresh Token Rotation
- [x] Refresh Token Revocation
- [x] Account Lockout
- [x] JWT Configuration Validation
- [ ] Refresh Token Family Reuse Detection
- [ ] Authorization Policies
- [ ] Role-Based Endpoint Authorization
- [ ] Logout
- [ ] Logout From All Devices
- [ ] Email Confirmation
- [ ] Password Reset
- [ ] Session Management

## Infrastructure

- [x] Structured Logging
- [x] Serilog
- [ ] Health Checks
- [ ] Rate Limiting
- [ ] Distributed Caching
- [ ] Monitoring
- [ ] Metrics
- [ ] Docker
- [ ] CI/CD

## Testing

- [ ] Unit Tests
- [ ] Integration Tests
- [ ] AutoMapper Configuration Tests
- [ ] Authentication Integration Tests
- [ ] Authorization Integration Tests
- [ ] Refresh Token Rotation Tests

## Frontend

- [ ] Angular Admin Panel
- [ ] Login Screen
- [ ] Authentication Interceptor
- [ ] Route Guards
- [ ] Role Guards
- [ ] Car Management Screens
- [ ] Pagination UI
- [ ] Public Website

---

# 🎯 Goals

The primary objective of DriverHub is to become a production-ready backend reference project demonstrating modern ASP.NET Core application architecture.

The project prioritizes:

- Maintainable Code
- Scalable Architecture
- Testability
- Readability
- SOLID Principles
- Separation of Concerns
- Secure Authentication
- Secure Authorization
- Production-Oriented API Design
- Explicit Business Outcomes
- Consistent Coding Standards
- Continuous Architectural Improvement

Rather than focusing solely on implementing features, DriverHub demonstrates how enterprise backend applications evolve through continuous architectural refinement.

---

# 📚 Learning Focus

DriverHub also serves as a long-term personal reference project for mastering:

- ASP.NET Core
- Clean Architecture
- Vertical Slice Architecture
- CQRS
- MediatR
- Entity Framework Core
- FluentValidation
- AutoMapper
- Result Pattern
- Repository Pattern
- Unit of Work
- ASP.NET Core Identity
- Authentication and Authorization
- JWT Authentication
- Refresh Token Security
- Query Projection
- API Contract Design
- Enterprise Backend Development
- Production Hardening

---

# 📈 Latest Progress

## July 2026

- ✅ Standardized Result Pattern across application features
- ✅ Replaced legacy mapping extensions with AutoMapper
- ✅ Added mapping profiles for all implemented entities
- ✅ Preserved LINQ projection for optimized read queries
- ✅ Standardized controller responses through BaseController
- ✅ Added consistent ApiResponse contracts
- ✅ Standardized route-based identifiers for update endpoints
- ✅ Removed identifiers from update request bodies
- ✅ Authentication architecture implemented
- ✅ ASP.NET Core Identity integrated
- ✅ JWT access token generation implemented
- ✅ Refresh token generation separated from JWT generation
- ✅ Secure refresh token hashing implemented
- ✅ Hashed refresh token persistence implemented
- ✅ Refresh token rotation implemented
- ✅ Previous refresh tokens revoked during rotation
- ✅ Replacement token hash tracking implemented
- ✅ Identity error mapping added
- ✅ Identity and role seeding implemented
- ✅ Account lockout support added
- ✅ Authentication contracts reorganized
- ✅ Token contracts separated into access and refresh token structures
- ✅ AutoMapper registered through Application dependency injection
- ✅ CRUD endpoints verified through Swagger
- ✅ Authentication and refresh token flows verified through Swagger

---

# 🧭 Current Project State

DriverHub currently provides a strong foundation for a modular monolithic backend application.

The implemented architecture includes:

```text
WebApi
   ↓
MediatR
   ↓
Validation Pipeline
   ↓
Command / Query Handler
   ↓
Result Pattern
   ↓
Repository or Query Service
   ↓
Entity Framework Core
   ↓
SQL Server
```

Authentication is integrated into the same API as a dedicated application module, avoiding unnecessary distributed system complexity while preserving clear boundaries between authentication, infrastructure, persistence, and business features.

The project is currently considered:

> **Production-oriented, but not yet production-ready.**

The remaining work mainly focuses on authorization, automated tests, monitoring, health checks, rate limiting, frontend integration, and production hardening.

---

# 📄 License

This repository is created for educational, portfolio, and learning purposes.

The source code may be reviewed and used as a reference for modern ASP.NET Core backend architecture.
