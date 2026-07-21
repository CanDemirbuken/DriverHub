# 🚗 DriverHub

[![GitHub](https://img.shields.io/badge/GitHub-CanDemirbuken-181717?style=for-the-badge&logo=github)](https://github.com/CanDemirbuken)
![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10-512BD4?style=for-the-badge&logo=dotnet)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-10-6DB33F?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Clean Architecture](https://img.shields.io/badge/Clean_Architecture-✔-0A66C2?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-In_Progress-orange?style=for-the-badge)

---

> 🚧 **Project Status**
>
> DriverHub is currently under active development. The project is being built incrementally with a strong focus on clean architecture, maintainability, scalability, and enterprise-grade backend development practices.

---

# 📖 Overview

**DriverHub** is a production-oriented **ASP.NET Core Clean Architecture** reference project built as a long-term foundation for enterprise backend applications.

Rather than simply following a tutorial, this project focuses on applying modern software architecture principles and best practices, including clean code, separation of concerns, Domain-Driven Design (DDD), and maintainable application design.

The project will continue to evolve by integrating modern backend technologies such as **CQRS**, **MediatR**, **FluentValidation**, **Repository Pattern**, **Unit of Work**, **JWT Authentication**, and many other enterprise patterns.

---

# 🏛️ Solution Architecture

```
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

The project follows the **Clean Architecture** approach, ensuring that business rules remain independent from infrastructure and presentation concerns.

---

# 🚀 Technologies

- ASP.NET Core 10
- Entity Framework Core 10
- SQL Server
- Clean Architecture
- Domain-Driven Design (DDD)
- Dependency Injection
- Options Pattern
- Swagger / OpenAPI
- RESTful API

---

# ✅ Current Features

- Clean Architecture project structure
- Entity Framework Core Code First approach
- SQL Server integration
- Fluent Entity Configurations
- Generic Base Entity
- Automatic Audit Fields (CreatedDate / UpdatedDate)
- Dependency Injection Extensions
- Options Pattern implementation
- Swagger / OpenAPI documentation
- One-to-One relationships
- One-to-Many relationships
- Explicit Many-to-Many relationships
- Automatic Configuration Discovery
- Modular project organization

---

# 🛣️ Roadmap

## Architecture

- [x] Clean Architecture
- [x] Domain Layer
- [x] Persistence Layer
- [x] Entity Configurations
- [x] Dependency Injection
- [x] Options Pattern

## Database

- [x] Entity Framework Core
- [x] SQL Server
- [x] Code First
- [x] Migrations

## API

- [x] REST API
- [x] Swagger

## Application

- [ ] CQRS
- [ ] MediatR
- [ ] FluentValidation
- [ ] AutoMapper
- [ ] Pipeline Behaviors
- [ ] Generic Responses

## Infrastructure

- [ ] Repository Pattern
- [ ] Unit of Work
- [ ] Logging
- [ ] Global Exception Middleware

## Security

- [ ] JWT Authentication
- [ ] Authorization
- [ ] Refresh Token

## Testing

- [ ] Unit Tests
- [ ] Integration Tests

---

# 🎯 Project Goals

The primary objective of DriverHub is to become a **production-ready reference project** that demonstrates modern ASP.NET Core backend development.

The project emphasizes:

- Clean and maintainable architecture
- Separation of concerns
- SOLID principles
- Domain-Driven Design
- Scalable software design
- Enterprise application development
- Best practices over shortcuts

---

# 📚 Learning Objectives

This project is also intended as a personal reference for mastering:

- Clean Architecture
- CQRS
- MediatR
- Entity Framework Core
- ASP.NET Core
- Dependency Injection
- FluentValidation
- Authentication & Authorization
- Modern Backend Development

---

# 📄 License

This repository is created for educational, portfolio, and learning purposes.
