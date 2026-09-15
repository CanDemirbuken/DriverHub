# DriverHub Angular Client — Codex Instructions

These instructions apply to everything under:

```text
Client/
```

DriverHub Client is the Angular administration interface for the DriverHub ASP.NET Core API.

Preserve the existing frontend architecture, UI patterns, and development conventions.

Do not redesign unrelated screens during feature work.

---

## Frontend Stack

The existing frontend uses:

- Angular
- TypeScript
- Standalone Components
- Angular Signals
- Angular Router
- HttpClient
- SCSS
- typed request/response models
- centralized API endpoint definitions
- shared toast notifications

Follow the existing Angular approach.

Do not introduce:

- NgRx
- another state-management library
- another CSS framework
- another HTTP abstraction
- another notification system

unless explicitly requested.

---

## Core Principle

Prefer:

```text
Existing UI pattern
+ typed API contract
+ shared infrastructure
+ small focused change
```

over:

```text
new frontend pattern
+ duplicated service logic
+ broad redesign
+ speculative abstraction
```

Consistency with existing features is preferred over introducing a theoretically cleaner alternative.

---

## Context Efficiency

Do not inspect the entire Angular project by default.

For every task:

1. Identify the affected feature.
2. Inspect the target component.
3. Inspect its service.
4. Inspect its request/response models.
5. Inspect one comparable existing feature.
6. Inspect shared infrastructure only when necessary.
7. Inspect the backend contract only when the API behavior must be confirmed.

Do not repeatedly inspect unrelated components.

Avoid inspecting:

```text
node_modules/
dist/
.angular/
```

unless explicitly necessary.

Do not reread the full frontend README for small implementation tasks unless context is required.

---

## Existing Pattern First

Before implementing a feature, find a comparable implementation.

Useful existing feature areas typically include:

- Cars
- Brands
- Categories
- Locations
- Features

Reuse established patterns for:

- list pages
- detail pages
- create pages
- edit pages
- delete behavior
- loading states
- error states
- routing
- API services
- request/response models
- toast notifications
- page layouts
- buttons and actions

Do not invent a new pattern when an existing feature already demonstrates the required behavior.

---

## Components

Use standalone components according to the existing project.

Components should focus on:

- UI state
- user interaction
- route interaction
- orchestration of service calls
- presentation behavior

Do not put raw HTTP implementation logic directly into feature components.

Do not make components responsible for unrelated shared infrastructure.

Keep feature components focused.

---

## Signals

Use Angular Signals for local component state when consistent with existing code.

Common examples include:

```text
isLoading
isSaving
isDeleting
errorMessage
selectedEntity
form values
page state
```

Use operation-specific state where necessary.

Do not introduce RxJS complexity for simple local UI state.

HTTP services may continue returning Observables according to the existing architecture.

Do not convert working Observable-based API flows to Signals merely for stylistic consistency.

---

## API Services

Backend communication belongs in the established service layer.

Expected flow:

```text
Component
    ↓
Feature/Core Service
    ↓
HttpClient
    ↓
DriverHub API
```

Do not place repeated `HttpClient` calls directly inside components.

Before adding a service method:

1. Inspect the existing service.
2. Inspect similar methods.
3. Reuse centralized endpoint definitions.
4. Use typed request and response models.
5. Follow existing error-handling behavior.

Do not duplicate API logic across components.

---

## API Endpoints

Use the existing centralized API endpoint configuration.

Do not scatter literal endpoint paths throughout components and services.

When adding a backend endpoint to the client:

1. Add it to the appropriate endpoint definition.
2. Use the endpoint through the corresponding service.
3. Keep route/query parameter construction consistent with existing code.

---

## Request / Response Models

Use typed TypeScript contracts.

Do not use `any` to avoid defining an API model.

When a backend contract changes:

1. Update the corresponding TypeScript model.
2. Check the service method.
3. Check all affected components.
4. Check template usage when relevant.

Do not guess backend response shapes.

The backend API contract is authoritative.

---

## ApiResponse

Use the existing shared `ApiResponse<T>` structure.

Do not create feature-specific wrappers when the shared API response model already represents the backend contract.

Follow existing success and error handling patterns.

---

## Backend Contract Awareness

The ASP.NET Core API is authoritative for:

- field names
- required fields
- validation rules
- business rules
- entity relationships
- authorization behavior
- route structure
- enum/state values

Do not work around incorrect backend behavior solely in the frontend.

If backend behavior appears incorrect, identify the problem rather than hiding it with UI logic.

When necessary, inspect only the corresponding backend endpoint/command/query rather than exploring the entire backend.

---

## Forms

Follow the form approach used by comparable existing features.

Do not migrate unrelated forms between form strategies during feature work.

When adding client-side validation:

- improve user experience
- align with relevant backend validation
- provide useful feedback

Frontend validation does not replace backend validation.

Do not duplicate complicated business rules unnecessarily in the frontend.

---

## Routing

Use Angular Router.

Do not manipulate browser URLs manually when router navigation is appropriate.

Preserve established feature navigation patterns.

Where applicable, prefer:

```text
List
→ Detail
→ Edit
```

rather than placing every operation directly on the list page.

Do not create duplicate routes for the same responsibility without a reason.

---

## Detail / Edit Pattern

Keep entity detail and edit responsibilities separate when the existing feature follows that pattern.

A common DriverHub workflow is:

```text
List
→ Detail
→ Edit
```

The detail page may act as the primary entry point for entity-specific actions.

Follow comparable features rather than inventing a different interaction model.

---

## Loading States

Asynchronous user-visible operations should have intentional loading state when appropriate.

Examples include:

```text
isLoading
isSaving
isDeleting
```

Always reset operation-specific loading state on both:

- success
- error

Do not leave buttons or spinners indefinitely active after failed operations.

Do not reuse a single loading flag when separate operations require independent state.

---

## Error Handling

Handle API errors consistently.

Use:

- existing shared API error information
- meaningful page-level error messages
- `ToastService` when appropriate
- persistent feature-local errors when the page requires them

Do not silently swallow HTTP errors.

Do not expose unnecessary technical backend details directly to end users.

---

## Toast Notifications

Use the existing `ToastService`.

Do not create another notification implementation.

Toasts are appropriate for events such as:

- successful creation
- successful update
- successful deletion
- relevant operation failure

Avoid excessive notifications for normal navigation or passive data loading.

---

## Delete Operations

Before implementing deletion:

1. Inspect a comparable existing delete flow.
2. Preserve confirmation behavior if one exists.
3. Use a dedicated deletion/loading state.
4. Handle backend relationship restrictions.
5. Update the visible list/state after success.
6. Reset loading state after success or failure.

Do not assume deletion will succeed.

Backend relationship and business rules are authoritative.

---

## Styling

Use the existing SCSS conventions and visual language.

Before creating styles:

1. Inspect the closest comparable page.
2. Reuse established spacing.
3. Reuse typography patterns.
4. Reuse card/layout patterns.
5. Reuse button/action patterns.
6. Reuse status/badge styles where appropriate.

Do not redesign unrelated screens.

Do not introduce another design system or CSS framework during feature work.

Keep responsive behavior consistent with nearby pages.

---

## HTML Templates

Keep templates readable.

Prefer semantic HTML and clear class naming.

Avoid complex business logic directly inside templates.

Move non-trivial calculations or decisions into TypeScript.

Avoid unnecessary deeply nested conditional markup.

Do not duplicate large sections of markup when an existing reusable pattern already exists.

---

## TypeScript Style

Follow existing project conventions.

Prefer:

- explicit types
- clear method names
- small focused methods
- `readonly` dependencies where consistent
- Signals for local UI state
- typed API models
- early returns where they improve readability

Avoid:

- `any`
- unnecessary casts
- duplicated constants
- deeply nested callbacks
- unused imports
- speculative generic abstractions

Do not rewrite existing working code simply to change stylistic preferences.

---

## Dependency Injection

Use Angular dependency injection according to existing project style.

Do not manually instantiate injectable services.

Preserve existing constructor/injection conventions unless the requested task specifically changes them.

---

## Authentication

Use existing authentication/session infrastructure.

Do not:

- duplicate authentication state logic
- manually handle tokens inside arbitrary feature components
- create additional token storage
- bypass existing route or authorization behavior

Authentication concerns belong in the established core authentication infrastructure.

When authentication contracts change, verify the corresponding backend contract.

---

## Shared Infrastructure

Before creating a new shared helper, service, model, or component, search for an existing equivalent.

Examples include:

- ToastService
- API endpoint definitions
- shared API models
- authentication services
- route infrastructure
- shared layout components
- common styles

Do not duplicate existing shared functionality.

Only move behavior into shared infrastructure when real reuse exists.

---

## Feature Development

For a normal management feature, a useful implementation order is:

```text
API models
→ endpoint definition
→ service method
→ routes
→ list/detail/create/edit behavior
→ deletion behavior
→ validation
→ UI polish
```

This is guidance, not a requirement to create every layer/page.

Implement only what the requested feature requires.

---

## Scope Control

Do not:

- redesign unrelated pages
- rewrite working components
- migrate state-management approaches
- reorganize folders during unrelated work
- rename broad sets of files
- introduce generic abstractions without demonstrated reuse
- change backend contracts merely to simplify frontend code
- add dependencies for functionality already available

Keep changes focused on the requested feature.

---

## Validation Workflow

After Angular changes, validate from the Angular project directory.

Typical location:

```text
Client/DriverHub.Client
```

Use the project's existing npm scripts.

When appropriate, run:

```bash
npm run build
```

Use lint/test scripts only when they exist and are relevant.

Do not claim the Angular project builds unless the command actually completed successfully.

Do not fix unrelated warnings or build issues unless they prevent validation of the requested change.

---

## Cross-Layer Changes

If the frontend task requires an API contract change:

1. Identify the exact backend endpoint.
2. Inspect the related Command/Query/response contract.
3. Make only the required backend adjustment.
4. Update the Angular model/service/component.
5. Validate both affected scopes.

Do not turn a small frontend feature into broad backend refactoring.

---

## Documentation

Frontend-specific documentation may exist under `Client/`.

Update documentation after substantial user-visible or architectural changes when it has become outdated.

Do not update documentation for trivial implementation changes.

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
- explain obvious code line by line
- paste full files unless requested
- describe unrelated code