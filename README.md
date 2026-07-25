# 🚗 DriverHub

[![GitHub](https://img.shields.io/badge/GitHub-CanDemirbuken-181717?style=for-the-badge&logo=github)](https://github.com/CanDemirbuken)
![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10-512BD4?style=for-the-badge&logo=dotnet)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-10-6DB33F?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![CQRS](https://img.shields.io/badge/CQRS-MediatR-FF6F00?style=for-the-badge)
![Clean Architecture](https://img.shields.io/badge/Clean_Architecture-✔-0A66C2?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-In_Progress-orange?style=for-the-badge)

---

> 🚧 **Project Status**
>
> DriverHub is currently under active development and is being built as a long-term reference project for modern ASP.NET Core backend development.
>
> The project intentionally prioritizes architectural quality over implementation speed. Every design decision is evaluated, challenged, and continuously refined to achieve maintainability, scalability, security, and production readiness.
>
> Authentication infrastructure, JWT authentication, and refresh token support have been implemented. More advanced security features such as refresh token rotation, reuse detection, and email confirmation are planned as the project evolves.

---

# 📖 Overview

**DriverHub** is a production-oriented **ASP.NET Core 10 Web API** built using **Clean Architecture**, **Vertical Slice Architecture**, and **CQRS**.

Rather than serving as a simple CRUD application, DriverHub focuses on implementing modern backend architecture, clean code principles, security best practices, and enterprise software development techniques.

The primary objective is not simply to build features, but to build them in a way that remains maintainable, testable, scalable, and production-ready.

---

# 🎯 Design Principles

DriverHub is built around several core principles:

- Vertical Slice Architecture
- Clean Architecture
- CQRS with MediatR
- Separation of Concerns
- Feature-Based Organization
- Explicit Application Contracts
- RESTful API Standards
- RFC7807 ProblemDetails
- Security First
- Continuous Refactoring

Rather than blindly following tutorials, every architectural decision is reviewed, discussed, challenged, and improved throughout the project's lifecycle.

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
├── Presentation
│   └── DriverHub.WebApi
│
└── Tests
    └── DriverHub.UnitTest
```

The solution follows **Clean Architecture**, where dependencies always point toward the business layer, allowing the Domain and Application layers to remain completely independent from infrastructure and presentation concerns.

---

# 🚀 Technology Stack

- ASP.NET Core 10
- Entity Framework Core 10
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- Clean Architecture
- Vertical Slice Architecture
- CQRS
- MediatR
- FluentValidation
- RESTful API
- Swagger / OpenAPI

---

# ✅ Implemented Features

## Architecture

- Clean Architecture
- Vertical Slice Architecture
- CQRS with MediatR
- Feature-Based Organization
- Repository Pattern
- Unit of Work
- Mapping Extensions

## Validation

- FluentValidation
- Validation Pipeline Behavior
- ValidationProblemDetails Responses

## Error Handling

- Global Exception Middleware
- RFC7807 ProblemDetails
- TraceId Support
- Custom Business Exceptions
- Consistent HTTP Error Contracts

## Data Access

- Entity Framework Core Code First
- SQL Server
- Generic Repository
- Unit of Work
- Fluent API Configurations
- Automatic Configuration Discovery
- Automatic Audit Fields
- Pagination
- Query Projection
- No-Tracking Queries

## Authentication

- ASP.NET Core Identity
- JWT Authentication
- Refresh Token Infrastructure
- Secure Refresh Token Hashing
- Identity Error Mapping
- Identity Seeding
- Account Lockout
- JWT Claims-Based Authentication
- Role-Based Authentication Infrastructure

## API

- RESTful API Design
- Standardized HTTP Status Codes
- Swagger / OpenAPI
- ProducesResponseType Documentation
- Explicit Request / Response Contracts

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
- [x] Validation Pipeline Behavior
- [x] RFC7807 ProblemDetails
- [x] Global Exception Handling
- [x] RESTful API Standards
- [x] HTTP Contract Standardization

## Data Access

- [x] Entity Framework Core
- [x] SQL Server
- [x] Code First
- [x] Migrations
- [x] Pagination
- [x] Query Projection
- [x] No-Tracking Queries
- [ ] Response Caching

## Security

- [x] ASP.NET Core Identity
- [x] JWT Authentication
- [x] Identity Seeding
- [x] Refresh Token Infrastructure
- [ ] Refresh Token Rotation
- [ ] Refresh Token Reuse Detection
- [ ] Authorization
- [ ] Role-Based Authorization
- [ ] Email Confirmation
- [ ] Password Reset

## Infrastructure

- [ ] Logging
- [ ] Serilog
- [ ] Health Checks
- [ ] Rate Limiting

## Testing

- [ ] Unit Tests
- [ ] Integration Tests

## Frontend

- [ ] Angular Admin Panel
- [ ] Public Website

---

# 🔐 Authentication

DriverHub uses **ASP.NET Core Identity** together with **JWT Bearer Authentication** to provide a modern authentication infrastructure.

Implemented security features include:

- JWT Access Tokens
- Refresh Token Infrastructure
- Secure Refresh Token Hashing
- ASP.NET Core Identity
- Identity Error Mapping
- Account Lockout
- Identity Seeding
- Claims-Based Authentication

Upcoming improvements:

- Refresh Token Rotation
- Refresh Token Reuse Detection
- Email Confirmation
- Password Reset
- Session Management

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
- Secure Authentication & Authorization
- Production-Ready API Design

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
- ASP.NET Core Identity
- Authentication & Authorization
- JWT Authentication
- Refresh Token Security
- Enterprise Backend Development

---

# 📈 Latest Progress

### July 2026

- ✅ Authentication architecture implemented
- ✅ ASP.NET Core Identity integrated
- ✅ JWT Authentication implemented
- ✅ Refresh Token infrastructure implemented
- ✅ Secure refresh token hashing implemented
- ✅ Identity error mapping added
- ✅ Identity seeding implemented
- ✅ Account lockout support added
- ✅ Authentication contracts standardized

---

# 📄 License

This repository is created for educational, portfolio, and learning purposes.
