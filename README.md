# 🚗 DriverHub

<p align="center">

<img src="https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet" />
<img src="https://img.shields.io/badge/ASP.NET_Core-10-512BD4?style=for-the-badge&logo=dotnet" />
<img src="https://img.shields.io/badge/Entity_Framework_Core-6DB33F?style=for-the-badge" />
<img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver" />
<img src="https://img.shields.io/badge/CQRS-MediatR-orange?style=for-the-badge" />
<img src="https://img.shields.io/badge/Clean_Architecture-✔-0A66C2?style=for-the-badge" />
<img src="https://img.shields.io/badge/JWT-Authentication-success?style=for-the-badge" />

</p>

<p align="center">

<strong>
Production-oriented ASP.NET Core Web API built with Clean Architecture, CQRS, JWT Authentication, Refresh Token Rotation and Policy-Based Authorization.
</strong>

</p>

---

# 📖 About

DriverHub is a **production-oriented backend Web API** developed to demonstrate modern .NET backend architecture and enterprise software development practices.

Instead of focusing only on CRUD operations, the project emphasizes scalable architecture, maintainability, clean code principles and secure authentication mechanisms.

The application is built around **Clean Architecture**, **CQRS**, **ASP.NET Core Identity**, **JWT Authentication**, **Refresh Token Rotation**, **Policy-Based Authorization**, and several production-ready backend patterns.

DriverHub is continuously evolving by introducing new security features, architectural improvements and production-level best practices.

---

# 🚀 Technology Stack

### Backend

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server

### Architecture

- Clean Architecture
- CQRS
- MediatR
- Repository Pattern
- Unit of Work
- Query Services
- Result Pattern

### Security

- ASP.NET Core Identity
- JWT Authentication
- Refresh Token Rotation
- Refresh Token Reuse Detection
- Role-Based Authorization
- Policy-Based Authorization

### Infrastructure

- FluentValidation
- AutoMapper
- Serilog
- Swagger / OpenAPI

---

# 🏗️ Architecture

DriverHub follows **Clean Architecture** to separate business rules from infrastructure concerns.

```
Presentation
      │
      ▼
Application
      │
      ▼
Domain
      │
      ▼
Persistence
      │
      ▼
Infrastructure
```

### Layer Responsibilities

| Layer | Responsibility |
|--------|----------------|
| Presentation | HTTP endpoints, Controllers, Middleware |
| Application | Business use cases, CQRS, Validation, Contracts |
| Domain | Core business entities and domain rules |
| Persistence | Entity Framework Core, Repositories, Query Services |
| Infrastructure | Identity, JWT, Mail, External Services |

---

## Architectural Highlights

- Clean Architecture
- CQRS with MediatR
- Repository Pattern
- Unit of Work
- Query Services
- Result Pattern-based Error Handling
- Global Exception Middleware
- FluentValidation Pipeline
- AutoMapper
- Dependency Injection

---

# ✨ Features

## 🔐 Authentication

- ✅ User Registration
- ✅ User Login
- ✅ JWT Authentication
- ✅ Refresh Token Support
- ✅ Refresh Token Rotation
- ✅ Refresh Token Revocation
- ✅ Refresh Token Reuse Detection
- ✅ Logout
- ✅ Logout From All Sessions

---

## 🛡️ Authorization

- ✅ ASP.NET Core Identity
- ✅ Role-Based Authorization
- ✅ Policy-Based Authorization
- ✅ Fallback Authorization Policy
- ✅ AllowAnonymous Support
- ✅ Admin Policy

---

## 👥 Role Management

- ✅ Role CRUD
- ✅ Role Query Service
- ✅ Duplicate Role Validation
- ✅ System Role Protection

---

## 👤 User Role Management

- ✅ Assign Role to User
- ✅ Remove Role from User
- ✅ Last Administrator Protection
- ✅ Last User Role Protection

---

## 🚗 Entity Management

Implemented using **CQRS** and **MediatR**.

- Cars
- Brands
- Categories
- Features
- Banners
- Contacts
- About

---

## ⚙️ Infrastructure

- Global Exception Middleware
- Result Pattern-based Error Handling
- FluentValidation Pipeline
- AutoMapper Profiles
- Query Services
- Standardized API Responses
- Swagger Documentation
- Serilog Logging

---

# 🔒 Security

DriverHub focuses on secure authentication and authorization mechanisms commonly used in modern backend applications.

## Authentication

- JWT Access Token
- Refresh Token
- Refresh Token Hashing
- Refresh Token Rotation
- Refresh Token Revocation
- Refresh Token Reuse Detection

---

## Authorization

- Policy-Based Authorization
- Role-Based Authorization
- Admin-Only Endpoints
- Secure Refresh Token Lifecycle

---

## Business Rules

- At least one administrator must always remain in the system.
- Every user must always have at least one assigned role.
- Duplicate role assignments are prevented.
- Duplicate role creation is prevented.
- System roles are protected from invalid operations.

---

# 📂 Solution Structure

```text
src

├── DriverHub.Domain
│
├── DriverHub.Application
│
│   ├── Behaviors
│   ├── Common
│   ├── Contracts
│   ├── Interfaces
│   ├── Features
│   │
│   ├── Entities
│   │   ├── AboutFeatures
│   │   ├── BannerFeatures
│   │   ├── BrandFeatures
│   │   ├── CarFeatures
│   │   ├── CategoryFeatures
│   │   ├── ContactFeatures
│   │   └── FeatureFeatures
│   │
│   └── Identity
│       ├── AccountFeatures
│       ├── AuthenticationFeatures
│       ├── RoleFeatures
│       ├── SessionFeatures
│       └── UserRoleFeatures
│
├── DriverHub.Persistence
│
├── DriverHub.Infrastructure
│
└── DriverHub.WebApi
```

---

# ⚙️ Getting Started

## Prerequisites

Before running the project, make sure the following tools are installed:

- .NET 10 SDK
- SQL Server
- Visual Studio 2022 or JetBrains Rider
- Git

---

## Installation

Clone the repository.

```bash
git clone https://github.com/CanDemirbuken/DriverHub.git
```

Navigate to the project.

```bash
cd DriverHub
```

Configure the database connection inside **appsettings.json**.

```json
"ConnectionStrings": {
  "SqlServer": "YOUR_CONNECTION_STRING"
}
```

Apply Entity Framework migrations.

```bash
dotnet ef database update
```

Run the application.

```bash
dotnet run
```

The API will be available at:

```
https://localhost:5001/swagger
```

---

# 📬 API Documentation

Swagger/OpenAPI is enabled in the Development environment and provides:

- JWT Authorization Support
- Interactive Endpoint Testing
- Request / Response Models
- Authentication Integration
- API Documentation

---

# ⭐ Key Highlights

DriverHub demonstrates many concepts commonly found in enterprise backend applications.

- Production-oriented Clean Architecture
- CQRS + MediatR
- Repository + Unit of Work
- Query Services
- Result Pattern
- JWT Authentication
- Refresh Token Rotation
- Refresh Token Reuse Detection
- Secure Session Management
- Policy-Based Authorization
- Role-Based Authorization
- Role Management
- User Role Management
- Global Exception Middleware
- FluentValidation Pipeline
- Standardized API Responses

---

# 🧪 Current Project Status

### Architecture

- ✅ Clean Architecture
- ✅ CQRS
- ✅ Repository Pattern
- ✅ Unit of Work
- ✅ Query Services
- ✅ Result Pattern

### Identity

- ✅ ASP.NET Core Identity
- ✅ JWT Authentication
- ✅ Refresh Token Rotation
- ✅ Refresh Token Revocation
- ✅ Refresh Token Reuse Detection
- ✅ Logout
- ✅ Logout From All Sessions
- ✅ Role Management
- ✅ User Role Management
- ✅ Policy-Based Authorization

### Infrastructure

- ✅ FluentValidation Pipeline
- ✅ AutoMapper
- ✅ Global Exception Middleware
- ✅ Swagger
- ✅ Serilog

---

# 🗺️ Roadmap

## Identity

- [x] User Registration
- [x] User Login
- [x] JWT Authentication
- [x] Refresh Token Rotation
- [x] Refresh Token Revocation
- [x] Refresh Token Reuse Detection
- [x] Logout
- [x] Logout All Sessions
- [x] Role CRUD
- [x] User Role Management
- [x] Policy-Based Authorization

- [ ] Email Confirmation
- [ ] Forgot Password
- [ ] Password Reset
- [ ] User Profile
- [ ] Change Password
- [ ] Change Email

---

## Testing

- [ ] Unit Tests
- [ ] Integration Tests
- [ ] Authentication Integration Tests
- [ ] CRUD Integration Tests
- [ ] AutoMapper Configuration Tests

---

## Production

- [ ] Health Checks
- [ ] Response Caching
- [ ] Rate Limiting
- [ ] Monitoring & Metrics
- [ ] Docker Support
- [ ] CI/CD Pipeline
- [ ] Production Hardening

---

# 💡 Design Goals

DriverHub is built with the goal of demonstrating a modern, scalable and maintainable backend architecture.

The project focuses on:

- Clean and maintainable code
- Clear separation of responsibilities
- Scalable application architecture
- Secure authentication and authorization
- Production-ready backend practices
- Consistent coding standards
- High readability

---

# 📌 Project Philosophy

DriverHub is intentionally designed around **use-case driven architecture** rather than simple CRUD operations.

Every feature is developed with emphasis on:

- Single Responsibility Principle
- Separation of Concerns
- Dependency Inversion
- Security First
- Production Readiness
- Long-term Maintainability

---

# 🤝 Contributing

Contributions, suggestions and feedback are always welcome.

Feel free to fork the repository and submit a Pull Request.

---

# 📄 License

This project is developed for educational, learning and portfolio purposes.

---

# 👨‍💻 Author

## Yaşarcan Demirbüken

Software Engineer

GitHub

> https://github.com/CanDemirbuken

LinkedIn

> [https://www.linkedin.com/in/yasarcandemirbuken](https://www.linkedin.com/in/ya%C5%9Farcan-demirb%C3%BCken-09095b205/)

---

<p align="center">

Built with ❤️ using ASP.NET Core, Clean Architecture and CQRS.

If you found this project helpful, consider giving it a ⭐.

</p>
