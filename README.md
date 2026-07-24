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
> DriverHub is currently under active development and is being built as a long-term reference project for modern ASP.NET Core backend development. Every architectural decision is intentionally evaluated and refined with maintainability, scalability, and production-readiness in mind.

---

# 📖 Overview

**DriverHub** is a production-oriented **ASP.NET Core 10 Web API** built using **Clean Architecture**, **CQRS**, and **Vertical Slice Architecture**.

Rather than serving as a simple CRUD application, DriverHub focuses on implementing modern backend architecture, clean code principles, and enterprise software development practices. Every feature is designed with maintainability, scalability, testability, and long-term sustainability in mind.

---

# 🎯 Design Principles

DriverHub is developed around a few fundamental principles:

- Build features through Vertical Slices
- Keep business logic independent from infrastructure
- Separate HTTP contracts from application use-cases
- Follow RESTful API conventions
- Produce consistent HTTP responses using RFC7807 ProblemDetails
- Keep the codebase clean, maintainable, and scalable
- Continuously refactor and improve architectural decisions

Rather than following tutorials step-by-step, the project intentionally evolves by reviewing, refining, and improving every architectural decision throughout the development process.

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

The solution follows the **Clean Architecture** approach where dependencies always point toward the business layer, allowing the Domain and Application layers to remain completely independent from infrastructure and presentation concerns.

---

# 🚀 Technology Stack

- ASP.NET Core 10
- Entity Framework Core 10
- SQL Server
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
- Consistent HTTP Error Contracts
- TraceId Support
- Custom Business Exceptions

## API

- RESTful API Design
- Standardized HTTP Status Codes
- Request / Command Separation
- Swagger / OpenAPI
- ProducesResponseType Documentation

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

- [ ] ASP.NET Core Identity
- [ ] JWT Authentication
- [ ] Authorization
- [ ] Refresh Token
- [ ] Role-Based Authorization

## Infrastructure

- [ ] Logging
- [ ] Serilog
- [ ] Health Checks

## Testing

- [ ] Unit Tests
- [ ] Integration Tests

## Frontend

- [ ] Angular Admin Panel
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
- Production-Ready API Design

Rather than focusing solely on implementing features, DriverHub aims to demonstrate how enterprise backend applications can be designed, structured, and evolved over time.

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
- JWT Authentication
- Enterprise Backend Development

---

# 📄 License

This repository is created for educational, portfolio, and learning purposes.
