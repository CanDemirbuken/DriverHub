# DriverHub — Codex Instructions

## Project Purpose

DriverHub is a full-stack car rental application developed as a production-oriented portfolio project.

The backend follows a layered architecture based on:

- .NET 10
- ASP.NET Core Web API
- Clean Architecture
- CQRS
- MediatR
- Entity Framework Core
- SQL Server
- Repository Pattern
- Unit of Work
- Query Services
- FluentValidation
- AutoMapper
- Result Pattern
- ASP.NET Core Identity
- JWT Authentication
- Serilog
- Swagger / OpenAPI

The frontend is an Angular administration client located under `Client/`.

Preserve the existing project architecture and established implementation patterns.

Do not redesign the project unless explicitly requested.

---

## Repository Structure

```text
DriverHub/
├── AGENTS.md
│
├── Client/
│   └── DriverHub.Client/
│
├── Core/
│   ├── DriverHub.Domain/
│   └── DriverHub.Application/
│
├── Infrastructure/
│   ├── DriverHub.Persistence/
│   └── DriverHub.Infrastructure/
│
├── Presentation/
│   └── DriverHub.WebApi/
│
└── DriverHub.slnx
```

Angular-specific instructions are defined in:

```text
Client/AGENTS.md
```

When working under `Client/`, follow both this file and the local Client instructions.

---

## Core Development Principle

Prefer:

```text
Existing pattern
+ smallest safe change
+ correct layer
+ verified behavior
```

over:

```text
large refactor
+ speculative abstraction
+ unrelated cleanup
+ unverified assumptions
```

Consistency with the existing codebase is preferred over introducing a theoretically cleaner alternative.

---

## Context Efficiency

Do not scan the entire repository by default.

For every task:

1. Identify the affected project, layer, or feature.
2. Search for the relevant symbol or implementation.
3. Inspect one or two comparable existing implementations.
4. Read only directly related files.
5. Inspect callers and dependencies only when necessary.
6. Expand the search scope only when required.

Do not repeatedly reread unchanged files.

Avoid inspecting:

```text
bin/
obj/
node_modules/
dist/
.git/
.vs/
```

unless explicitly necessary.

Do not inspect EF Core migrations unless the task involves database schema or migration behavior.

Do not reread full README files for small implementation tasks unless project context is actually required.

---

## General Change Rules

Prefer minimal targeted changes.

Do not:

- refactor unrelated code
- rename unrelated files or symbols
- reformat unrelated files
- move files without a concrete reason
- perform speculative cleanup
- introduce abstractions for hypothetical future requirements
- add dependencies unless required
- upgrade dependencies unless explicitly requested
- change unrelated business behavior
- rewrite working implementations only for stylistic preference

Before creating a new implementation:

1. Search for an existing equivalent.
2. Inspect similar features.
3. Reuse existing abstractions where appropriate.
4. Follow the dominant project pattern.

Do not introduce a second abstraction for a responsibility already covered by the existing architecture.

---

## Backend Architecture

The backend is organized into:

```text
Domain
Application
Persistence
Infrastructure
WebApi
```

Respect existing project references and dependency direction.

Do not introduce reverse dependencies between layers.

---

## Domain

`DriverHub.Domain` contains core domain entities and domain concepts.

Domain must remain independent from:

- ASP.NET Core
- HTTP concerns
- EF Core implementation details
- concrete infrastructure services
- Presentation concerns

Do not place persistence or API-specific behavior inside domain entities without an established project reason.

A `Car` represents a physical rentable vehicle.

Preserve existing domain meaning when modifying vehicle, fleet, availability, or reservation behavior.

---

## Application

`DriverHub.Application` contains application use cases.

Typical responsibilities include:

- Commands
- Queries
- Handlers
- DTOs
- Validators
- Interfaces
- Pipeline behaviors
- Business rules
- Result handling

Before adding a new use case:

1. Find the closest existing feature.
2. Inspect its Command or Query.
3. Inspect its Handler.
4. Inspect its Validator when present.
5. Follow the same structure and naming conventions.

Do not create a new feature organization pattern unless required.

---

## CQRS / MediatR

Commands modify state.

Queries retrieve data.

Keep these responsibilities separate.

Use MediatR according to the existing project conventions.

Handlers should orchestrate application behavior.

Do not:

- put HTTP-specific concerns inside handlers
- call another handler through `IMediator.Send()` merely to reuse code
- place EF Core query logic directly in controllers

Extract reusable behavior only when genuine reuse exists.

---

## Result Pattern

Expected business failures must use the existing Result Pattern where appropriate.

Examples include:

- entity not found
- duplicate resource
- invalid credentials
- invalid entity state
- relationship restriction
- authentication failure
- business rule failure

Do not throw exceptions for normal expected business outcomes.

Unexpected technical failures should flow through the existing global exception-handling mechanism.

Do not introduce another Result abstraction.

Do not redesign the existing Result Pattern unless explicitly requested.

---

## FluentValidation

Use FluentValidation according to the existing validation pipeline.

Do not duplicate validator rules manually inside:

- controllers
- handlers
- services

when those rules belong in an existing validator.

Business rules requiring database state may remain inside the appropriate handler or service when consistent with the existing feature pattern.

Follow neighboring implementations.

---

## Web API

Controllers must remain thin.

Expected flow:

```text
HTTP Request
    ↓
Controller
    ↓
MediatR
    ↓
Command / Query
    ↓
Handler
    ↓
Application abstraction
    ↓
Persistence / Infrastructure
```

Do not place directly inside controllers:

- EF Core queries
- business rules
- repository orchestration
- token-generation logic
- password logic
- database persistence logic

Preserve existing API response and Result translation conventions.

---

## Persistence

`DriverHub.Persistence` owns database concerns.

Typical responsibilities include:

- DbContext
- EF Core configurations
- repositories
- Unit of Work
- Query Services
- transactions
- migrations

Do not place application business logic inside repositories or EF Core configuration classes.

---

## EF Core

For read-only queries:

- prefer `AsNoTracking()`
- filter before materialization
- project only required fields where appropriate
- avoid unnecessary `Include()`
- avoid unnecessary database round trips
- keep filtering on the database side

Prefer:

```text
Filter
→ Project
→ Materialize
```

instead of:

```text
Materialize
→ Filter in memory
```

Do not optimize queries speculatively.

Prioritize correctness, readability, and consistency first.

Do not introduce raw SQL unless there is a demonstrated reason.

---

## Repository / Unit of Work

Repository and Unit of Work abstractions already exist.

Reuse them.

Do not introduce another persistence abstraction for the same responsibility.

Before adding a repository method, check whether:

- an equivalent already exists
- a Query Service is more appropriate
- the behavior already exists elsewhere

Follow existing transaction and `SaveChangesAsync()` conventions.

Do not scatter persistence calls across unrelated layers.

---

## AutoMapper

AutoMapper is part of the existing architecture.

Use existing mapping profiles and conventions.

Before adding a mapping:

1. Search existing profiles.
2. Check whether an equivalent mapping exists.
3. Add the mapping to the appropriate profile.

Do not globally replace AutoMapper with manual mapping.

For update operations, follow the dominant neighboring feature pattern.

---

## Infrastructure

`DriverHub.Infrastructure` contains external and technical implementations such as:

- ASP.NET Core Identity
- JWT handling
- refresh-token infrastructure
- email services
- external technical services

Application should depend on abstractions rather than concrete infrastructure implementations.

Do not leak infrastructure-specific implementation types into Domain or Application without a demonstrated reason.

---

## Authentication & Security

DriverHub includes authentication and session functionality such as:

- registration
- login
- email confirmation
- forgot password
- password reset
- JWT access tokens
- refresh tokens
- refresh-token hashing
- refresh-token rotation
- refresh-token revocation
- refresh-token reuse detection
- logout
- logout from all sessions
- role management
- policy-based authorization

Security-sensitive behavior must not be weakened.

Never:

- store plaintext passwords
- store plaintext refresh tokens
- log passwords
- log access tokens
- log refresh tokens
- hard-code secrets
- commit credentials
- weaken validation for convenience

When modifying authentication or session behavior, inspect the connected token/session lifecycle before changing implementation.

Preserve existing:

- token hashing
- expiration
- rotation
- revocation
- replacement-token relationships
- reuse detection

---

## Identity Business Rules

Preserve existing identity invariants.

Examples include:

- at least one administrator must remain
- every user must have at least one role
- duplicate role assignments must be prevented
- protected system roles must not be removed incorrectly

Do not bypass these rules from new commands, handlers, or endpoints.

---

## Error Handling

Unexpected exceptions should use the existing global exception handling mechanism.

Do not add repetitive `try/catch` blocks simply to log and rethrow.

Catch exceptions only when:

- recovery is possible
- translation is required
- meaningful handling exists

Expected business failures should normally use Result rather than exceptions.

---

## Logging

Use existing logging abstractions and Serilog.

Prefer structured logging.

Good:

```csharp
_logger.LogInformation(
    "Car {CarId} updated successfully",
    carId);
```

Avoid:

```csharp
_logger.LogInformation(
    $"Car {carId} updated successfully");
```

Never log sensitive authentication or credential information.

Do not add noisy logging to frequently executed paths without a reason.

---

## Async / CancellationToken

Use asynchronous APIs for real asynchronous I/O.

Propagate `CancellationToken` where existing APIs support it.

Avoid:

```text
.Result
.Wait()
```

Do not wrap normal ASP.NET Core server work in `Task.Run()`.

Follow neighboring implementations when determining async conventions.

---

## API Contract Changes

The Angular client consumes backend API contracts.

When changing:

- request models
- response models
- property names
- data types
- route paths
- validation behavior
- enum values

check the corresponding Angular implementation under `Client/`.

Do not introduce silent breaking contract changes.

Only inspect the affected frontend feature; do not scan the entire Angular application.

---

## Database Changes

Schema changes must be intentional.

When schema changes are required:

1. Modify the entity.
2. Modify EF Core configuration when necessary.
3. Create a new migration.
4. Inspect the generated migration.
5. Confirm it represents the intended schema change.

Do not modify old migrations to represent new development unless explicitly requested.

Do not create migrations for changes that do not affect database schema.

Never run migrations against an unknown or production database unless explicitly requested.

---

## Dependency Injection

Use existing layer-specific registration patterns.

Before adding a service:

1. Find similar registrations.
2. Register it in the correct layer.
3. Choose lifetime intentionally.

Do not manually resolve ordinary application services using `IServiceProvider`.

---

## Package Management

Do not install a new NuGet package unless required.

Before adding a package:

1. Check whether existing dependencies already provide the needed functionality.
2. Confirm the package is necessary.
3. Confirm compatibility with the current project.

Do not perform package upgrades during unrelated work.

---

## Validation Workflow

After backend changes, validate the affected scope.

Preferred full backend validation:

```bash
dotnet build DriverHub.slnx
```

Use narrower validation when it is sufficient.

For schema work, validate the generated migration as well.

Do not claim:

- the project builds
- tests pass
- the migration works
- the endpoint works

unless the corresponding validation actually succeeded.

Do not fix unrelated warnings or errors unless they prevent validation of the requested task.

---

## Git Safety

Do not automatically:

- commit
- push
- pull
- rebase
- reset
- force push
- delete branches

unless explicitly requested.

Preserve existing uncommitted user changes.

Never discard unrelated modifications merely to obtain a clean working tree.

---

## Documentation

The root `README.md` describes the project architecture, capabilities, and development status.

Use it when project-level context is required.

Do not reread or rewrite README documentation for every implementation task.

After substantial architectural or user-visible feature work, check whether documentation has become outdated.

Do not update documentation for trivial implementation details.

---

## Final Response

Keep task-completion responses concise.

Report:

1. What changed
2. Files changed
3. Important implementation decision
4. Validation performed
5. Remaining issue, only if relevant

Do not:

- repeat the original task
- provide line-by-line explanations unless requested
- paste entire modified files unless requested
- describe unchanged code