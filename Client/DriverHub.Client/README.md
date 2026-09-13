# 🖥️ DriverHub Client

<p align="center">

<img src="https://img.shields.io/badge/Angular-21.2-DD0031?style=for-the-badge&logo=angular" />
<img src="https://img.shields.io/badge/TypeScript-5.9-3178C6?style=for-the-badge&logo=typescript&logoColor=white" />
<img src="https://img.shields.io/badge/SCSS-C6538C?style=for-the-badge&logo=sass&logoColor=white" />
<img src="https://img.shields.io/badge/API-ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet" />

</p>

<p align="center">

<strong>
Angular frontend for the DriverHub car rental platform.
</strong>

</p>

---

## 📖 Overview

DriverHub Client is the Angular frontend of the DriverHub car rental platform.

The frontend is designed around two primary application areas:

- **Admin Panel**
- **Public Rental Experience**

Current development is focused primarily on the **Admin Panel**.

The current milestone provides the core fleet management workflows required for administrators to manage vehicles and the supporting entities used by the vehicle domain.

The frontend communicates with the DriverHub ASP.NET Core Web API and follows a feature-oriented Angular structure built around:

- Standalone components
- Angular Signals
- Typed request and response contracts
- Centralized API endpoint definitions
- Centralized route definitions
- Feature-specific services
- Shared helpers and UI services

The current Admin Fleet Management foundation includes:

- Vehicle listing
- Vehicle detail
- Vehicle editing
- Vehicle media management
- Vehicle status management
- Vehicle location management
- Vehicle pricing management
- Vehicle feature assignment
- Brand management
- Category management
- Location management
- Feature management

---

## 🚀 Technology Stack

### Frontend

- Angular 21.2
- TypeScript 5.9
- Angular Signals
- Angular Router
- HttpClient
- FormsModule
- RxJS
- SCSS
- Standalone Components

### Backend Integration

- ASP.NET Core Web API
- JWT-based authentication
- REST API
- Standardized API responses
- Typed request / response contracts

### Tooling

- Angular CLI 21.2
- npm 11.17

---

# 🏗️ Frontend Architecture

The client separates application-wide infrastructure from feature-specific UI.

```text
src/app
│
├── core
│   ├── constants
│   ├── guards
│   ├── models
│   └── services
│
├── features
│   ├── admin-login
│   ├── forgot-password
│   ├── reset-password
│   ├── dashboard
│   ├── cars
│   ├── brands
│   ├── categories
│   ├── locations
│   └── car-features
│
├── layouts
│   ├── admin-layout
│   └── public-layout
│
├── shared
│   ├── components
│   ├── helpers
│   └── services
│
└── app.routes.ts
```

The main responsibilities are divided as follows.

---

## Core

The `core` layer contains application-wide frontend infrastructure.

Examples include:

- API endpoint definitions
- Route path definitions
- Route link definitions
- Authentication guards
- Feature services
- Request models
- Response models
- Shared API contracts

Feature components do not directly manage infrastructure concerns such as API URL construction.

---

## Features

The `features` directory contains page-level application functionality.

Current Admin Panel features include:

```text
Dashboard

Cars
├── Car List
├── Car Detail
└── Car Edit

Brands
├── Brand List
├── Brand Create
├── Brand Detail
└── Brand Edit

Categories
├── Category List
├── Category Create
├── Category Detail
└── Category Edit

Locations
├── Location List
├── Location Create
├── Location Detail
└── Location Edit

Car Features
├── Feature List
├── Feature Create
├── Feature Detail
└── Feature Edit
```

Deletion workflows for supporting entities are managed directly from their list screens.

---

## Layouts

The application currently defines two layout foundations:

- **Admin Layout**
- **Public Layout**

The Admin Layout contains shared administrative UI such as:

- Sidebar
- Topbar
- Footer
- Global Toast Component
- Router outlet for protected Admin pages

The Public Layout currently provides the foundation for the future customer-facing rental experience.

---

## Shared

Reusable frontend behavior is placed under the shared area.

Current examples include:

- Toast notifications
- Toast service
- Image URL helper
- Shared components
- Reusable frontend utilities

This prevents feature components from duplicating application-wide behavior.

---

# 🧭 Routing

Routing is centrally managed through Angular Router.

Route path definitions and browser navigation links are separated through:

```text
RoutePaths
RouteLinks
```

This avoids duplicated route strings throughout feature components.

The current Admin route structure is conceptually:

```text
/admin
│
├── login
├── forgot-password
├── reset-password
│
└── Protected Admin Layout
    │
    ├── dashboard
    │
    ├── cars
    │   ├── create
    │   ├── :id
    │   └── :id/edit
    │
    ├── brands
    │   ├── create
    │   ├── :id
    │   └── :id/edit
    │
    ├── categories
    │   ├── create
    │   ├── :id
    │   └── :id/edit
    │
    ├── locations
    │   ├── create
    │   ├── :id
    │   └── :id/edit
    │
    ├── features
    │   ├── create
    │   ├── :id
    │   └── :id/edit
    │
    └── reservations
        └── create
```

Protected Admin routes are placed under the Admin Layout and guarded before navigation is allowed.

Admin fleet route components and the reservation creation screen use route-level `loadComponent` lazy loading to reduce the initial production bundle. The Admin Layout, Sidebar, Topbar, Footer, Toast and authentication guards remain eager. Static routes such as `create` precede dynamic `:id` routes, preserving existing navigation behavior.

---

# 🛡️ Authentication and Route Guards

The Admin area contains authentication-related screens for:

- Login
- Forgot Password
- Reset Password

The routing infrastructure uses separate guards for authenticated and guest scenarios.

Conceptually:

```text
Unauthenticated User
        │
        ▼
Admin Login
        │
        ▼
Authentication
        │
        ▼
Admin Auth Guard
        │
        ▼
Protected Admin Layout
```

Authentication and authorization-related navigation logic therefore remains outside individual business feature components.

---

# 🌐 API Integration

Backend communication is handled through Angular services using `HttpClient`.

Feature components do not construct backend URLs directly.

The general communication flow is:

```text
Feature Component
       │
       ▼
Feature Service
       │
       ▼
ApiEndpoints
       │
       ▼
environment.apiUrl
       │
       ▼
ASP.NET Core Web API
```

Current service responsibilities include areas such as:

- Authentication
- Cars
- Brands
- Categories
- Locations
- Features
- Media

Each feature keeps its request and response contracts explicitly represented through TypeScript interfaces.

---

# 📦 Standardized API Responses

The Angular application mirrors the standardized response structure returned by the DriverHub API.

This allows feature components to consistently process:

- Successful responses
- Validation failures
- Business rule errors
- Not found responses
- Missing data
- Unexpected failures

Backend error messages can therefore be surfaced to the administrator when appropriate.

A simplified flow is:

```text
ASP.NET Core API
       │
       ▼
ApiResponse<T>
       │
       ▼
Angular Service
       │
       ▼
Feature Component
       │
       ├── Success State
       │
       └── Error / Toast Feedback
```

---

# 🚗 Car Management

Car Management is the primary operational area of the current Admin Panel.

The current frontend provides:

- Car listing
- Car detail
- Car editing
- Image management
- Status management
- Location management
- Pricing management
- Feature assignment

---

## 📋 Car Listing

The Car List screen provides the administrative fleet overview.

It consumes the paginated Car API and displays vehicle information required for fleet management.

Administrators can navigate from the list to the detail screen of a specific physical vehicle.

The detail-oriented navigation model keeps operational vehicle management centralized around the Car Detail screen.

---

## 🔎 Car Detail

The Car Detail screen acts as the primary operational management screen for an individual physical vehicle.

It displays information such as:

- Brand
- Model
- Model year
- Plate
- VIN
- Category
- Current location
- Vehicle status
- Transmission
- Fuel type
- Mileage
- Seat capacity
- Luggage capacity
- Color
- Features
- Pricing
- Vehicle images

The page also provides access to vehicle editing and several inline operational actions.

---

## ➕ Car Creation

The car list links to `/admin/cars/create`. The standalone creation screen provides Brand, Category and Location selections backed by lookup services, vehicle fields, and cover/big image uploads with previews.

Typed create models and `CarService` use the centralized API endpoint definitions. Required-field validation, lookup/upload/save loading states, API errors and shared `ToastService` notifications follow the existing form patterns. Successful creation navigates to the new car's detail screen.

---

## ✏️ Car Editing

Vehicle master information can be edited through the dedicated Car Edit screen.

The flow is:

```text
Car Detail
    │
    ▼
Edit
    │
    ▼
Car Edit Form
    │
    ▼
Update API
    │
    ▼
Toast Feedback
```

Existing vehicle data is loaded before editing.

The edit screen also integrates supporting data such as:

- Brands
- Categories
- Locations

This keeps relational selections synchronized with the available supporting entities in the system.

---

# 🖼️ Vehicle Media Management

The Admin Panel supports vehicle image uploads.

Supported media formats currently include:

- JPG
- JPEG
- PNG
- WEBP

Two primary vehicle images are handled.

### Cover Image

Used primarily by vehicle listing-oriented views.

### Big Image

Used for larger vehicle presentation and detail-oriented views.

The media upload UI supports:

- File selection
- Upload loading state
- Image preview
- Existing image preview
- Upload feedback
- API-provided media paths

The frontend sends the selected file through `FormData` to the backend media infrastructure.

Conceptually:

```text
Browser File
     │
     ▼
FormData
     │
     ▼
Media API
     │
     ▼
Stored Media
     │
     ▼
Returned Path
     │
     ▼
Vehicle Update
```

---

# 🔗 Image URL Handling

Stored backend media paths and browser-ready display URLs are kept separate.

A shared `ImageUrlHelper` resolves the path returned by the API into an image URL suitable for the browser.

Conceptually:

```text
Stored API Path
      │
      ▼
ImageUrlHelper
      │
      ▼
Browser-ready Image URL
```

This prevents Car List, Car Detail and Car Edit from implementing their own media URL construction logic.

---

# 🚦 Vehicle Status Management

Vehicle status can be changed directly from the Car Detail screen.

Supported statuses include:

- Active
- Maintenance
- Out Of Service
- Damaged
- Retired

Status changes are treated as operational actions rather than general vehicle editing.

The flow is:

```text
Select Status
      │
      ▼
PATCH Status API
      │
      ▼
Update Local Car State
      │
      ▼
Toast Feedback
```

The frontend prevents unnecessary API requests when the selected value already matches the current vehicle status.

---

# 📍 Vehicle Location Management

A vehicle's current physical location can be changed directly from the Car Detail screen.

Available locations are loaded from the Location API.

The update flow is:

```text
Load Locations
      │
      ▼
Select Location
      │
      ▼
PATCH Location API
      │
      ▼
Update Local Car State
      │
      ▼
Toast Feedback
```

This allows fleet movements to be performed without opening the complete Car Edit form.

---

# 💰 Vehicle Pricing Management

Pricing is managed directly from the Car Detail screen.

The current pricing model supports:

- Daily
- Weekly
- Monthly

Editable pricing values are stored separately from the original API response.

Conceptually:

```text
API Pricing
     │
     ▼
Editable Pricing State
     │
     ▼
User Changes
     │
     ▼
PUT Pricing API
     │
     ▼
Updated Car State
```

This prevents the persisted vehicle state from being mutated before the backend confirms the operation.

---

# ✨ Vehicle Feature Assignment

Vehicle feature assignment is implemented directly inside the Car Detail screen.

Administrators can load the available feature definitions and manage which features belong to the selected vehicle.

The UI keeps an editable feature selection separate from the persisted vehicle state.

Conceptually:

```text
Available Features
       │
       ▼
Current Car Features
       │
       ▼
Editable Feature Selection
       │
       ├── Save
       │
       └── Cancel
       │
       ▼
Set Car Features API
       │
       ▼
Updated Local Car State
```

This allows administrators to modify the feature selection without immediately mutating the original vehicle data.

Changes are synchronized only after the backend confirms the operation.

---

# 🏷️ Supporting Entity Management

Vehicle management depends on several supporting entities.

The Admin Panel currently provides management workflows for:

- Brands
- Categories
- Locations
- Features

These entities use a consistent administrative pattern.

```text
List
 │
 ├── Create
 │
 ├── Detail
 │
 ├── Edit
 │
 └── Delete
```

Common UI behavior includes:

- Loading states
- Error states
- Empty states
- Typed API communication
- Toast feedback
- Detail navigation
- Edit navigation
- Delete confirmation
- Backend business rule feedback

---

## 🏷️ Brand Management

Brand Management provides administrative workflows for vehicle manufacturers.

Supported workflows include:

- List brands
- Create brand
- View brand detail
- Edit brand
- Delete brand

Brand records are used by vehicles through the Car domain.

---

## 🗂️ Category Management

Category Management provides administrative workflows for vehicle categories.

Supported workflows include:

- List categories
- Create category
- View category detail
- Edit category
- Delete category

Category records can be selected while managing vehicle information.

---

## 📍 Location Management

Location Management represents the physical branches used by the rental fleet.

Supported workflows include:

- List locations
- Create location
- View location detail
- Edit location
- Delete location

Locations are also used by the operational vehicle location management flow.

---

## ✨ Feature Management

Feature Management defines the reusable vehicle features that can later be assigned to individual vehicles.

Supported workflows include:

- List features
- Create feature
- View feature detail
- Edit feature
- Delete feature

Feature definitions and vehicle feature assignment are intentionally separate concerns.

Conceptually:

```text
Feature Management
       │
       ▼
Defines Available Features
       │
       ▼
Car Detail
       │
       ▼
Assign Features to Vehicle
```

This prevents feature definition management from being coupled directly to an individual vehicle.

---

# 🗑️ Delete Confirmation and Business Rules

Supporting entity list screens use explicit delete confirmation before destructive operations.

The general flow is:

```text
Delete Action
     │
     ▼
Confirmation Modal
     │
     ├── Cancel
     │
     └── Confirm
            │
            ▼
        DELETE API
            │
            ├── Success
            │     │
            │     ▼
            │ Local List Update
            │
            └── Business Rule Error
                  │
                  ▼
             Toast Feedback
```

When deletion succeeds, the frontend can remove the entity from local Signal state without requiring a complete list reload.

If the backend rejects deletion because the entity is currently referenced by another domain object, the backend error is surfaced through the global toast system.

Business rules therefore remain enforced by the backend while the frontend provides clear user feedback.

---

# 📅 Reservation Creation

The lazy-loaded `/admin/reservations/create` screen keeps preparation and confirmation on one page:

```text
Search Criteria → Available Cars → Select Car → Reservation Quote
→ Extras → Insurance → Price Summary → Confirmation → Success State
```

- Select a pickup location and local start/end date-time, then explicitly request availability. Dates are sent to the API as UTC ISO strings; changing criteria clears the displayed results and quote without automatically searching again.
- Selecting a vehicle requests a backend-generated quote; it does not create a reservation. Users can return to vehicle selection or change the search criteria.
- Optional extra checkboxes and an optional insurance package selection refresh the quote through the API. The price summary displays returned base, extras, insurance and total amounts; Angular does not calculate authoritative prices.
- The date input minimum and action validation follow the backend's five-minute grace period at minute precision. Older starts and an end at or before the start are rejected.
- `Rezervasyonu Onayla` is the explicit create action. Busy-state checks and disabled controls prevent duplicate submissions from the screen while confirmation is in progress.
- On success, the quote/form is replaced by a success panel with the reservation ID, `Yeni Rezervasyon Oluştur` and `Araçlara Dön` actions. Vehicle, quote, options, criteria and operation error state are cleared; a success toast remains additional feedback.

The screen uses standalone components, local Signals, typed API models, centralized endpoint definitions and the shared `ToastService`. Loading and API errors are handled separately from the success state. Availability and quoted prices can change before confirmation; the API remains authoritative. Reservation list/detail and lifecycle screens are not yet provided.

---

# 🔔 Global Toast Notifications

The application contains a reusable global toast notification system.

Toast notifications are rendered at layout level rather than being duplicated inside individual feature pages.

The shared `ToastService` manages:

- Visibility
- Message
- Notification type
- Automatic closing
- Timer replacement

Supported notification types currently include:

```text
success
error
```

Feature components only request notification behavior:

```text
Feature Component
      │
      ▼
ToastService
      │
      ▼
Global Toast Component
```

This keeps notification rendering separate from feature behavior.

---

## ⏱️ Toast Timer Management

Toast timers are centrally controlled.

When another toast is opened before the previous timeout expires, the previous timer is cleared.

This prevents an older notification timer from closing a newer notification unexpectedly.

---

# 🧠 State Management

The application currently uses Angular Signals for local and feature-oriented UI state.

Examples include:

- Loading state
- Error messages
- Current entity
- Entity collections
- Vehicle status selection
- Vehicle location selection
- Editable pricing
- Editable vehicle features
- Upload state
- Delete confirmation state
- Toast state

Conceptually:

```text
API Data
   │
   ▼
Signal State
   │
   ▼
Angular Template
```

The application currently avoids introducing a global state management library when local Signals and application services are sufficient.

---

# 🔄 Local State Synchronization

Several operations synchronize only the affected portion of frontend state after a successful backend request.

Examples include:

- Vehicle status updates
- Vehicle location updates
- Vehicle pricing updates
- Vehicle feature updates
- Supporting entity deletions
- Supporting entity edits

Conceptually:

```text
User Action
    │
    ▼
API Request
    │
    ▼
Successful Response
    │
    ▼
Signal Update
    │
    ▼
Immediate UI Synchronization
```

This reduces unnecessary GET requests and avoids full page reloads.

---

# 🎨 UI Design

The Admin Panel uses custom SCSS rather than depending entirely on a UI component framework.

Current UI patterns include:

- Admin sidebar
- Topbar
- Footer
- Dashboard shell
- List cards
- Detail cards
- Create forms
- Edit forms
- Status controls
- Location controls
- Pricing controls
- Feature controls
- Image upload controls
- Loading states
- Error states
- Empty states
- Delete confirmation modals
- Toast notifications
- Responsive layout behavior

Brand, Category, Location and Feature management intentionally follow a consistent visual language.

The goal is to provide predictable administration workflows while keeping control over the application's UI structure.

---

# 📂 Client Structure

The Angular application is located under:

```text
DriverHub
└── Client
    └── DriverHub.Client
```

The project uses Angular standalone components rather than a traditional NgModule-oriented application structure.

---

# ⚙️ Getting Started

## Prerequisites

Make sure the following tools are available:

- Node.js
- npm
- Angular CLI

The DriverHub ASP.NET Core API should also be running for API-integrated functionality.

---

## Install Dependencies

From the Angular project directory:

```bash
npm install
```

---

## Development Server

Run:

```bash
npm start
```

or:

```bash
ng serve
```

The Angular development server will start the application.

---

## Build

Create a production build with:

```bash
npm run build
```

or:

```bash
ng build
```

Build artifacts are generated under:

```text
dist/
```

---

## Tests

Run the configured Angular test command with:

```bash
npm test
```

---

# 🔧 Environment Configuration

API base URLs are configured through Angular environment configuration.

Feature services combine:

```text
environment.apiUrl
        +
ApiEndpoints
        │
        ▼
Final API URL
```

Endpoint definitions remain centralized rather than being hardcoded inside components.

This keeps backend addressing consistent across the application.

---

# 🧩 Current Admin Panel Status

## Fleet Management Foundation

The basic Admin Fleet Management foundation is now complete.

### Completed

```text
Application Foundation
├── Standalone Angular architecture
├── Admin Layout
├── Public Layout foundation
├── Sidebar
├── Topbar
├── Footer
├── Centralized routing
├── Admin feature route-level lazy loading
├── Route constants
├── API endpoint constants
├── Admin route protection
├── Guest route protection
├── HttpClient integration
├── Typed API contracts
├── Standard API response handling
├── Global Toast Service
├── Global Toast Component
└── Shared Image URL Helper

Authentication
├── Admin Login
├── Forgot Password
└── Reset Password

Car Management
├── Car List
├── Car Create
├── Car Detail
├── Car Edit
├── Vehicle Media Upload
├── Image Preview
├── Vehicle Status Management
├── Vehicle Location Management
├── Vehicle Pricing Management
└── Vehicle Feature Assignment

Reservation Creation
├── Availability Search and Vehicle Selection
├── Backend-Generated Quote and Price Summary
├── Optional Extras and Insurance
├── Explicit Confirmation
├── Matching Start-Time Grace-Period Validation
└── Success State and UI Duplicate-Submit Protection

Brand Management
├── List
├── Create
├── Detail
├── Edit
└── Delete

Category Management
├── List
├── Create
├── Detail
├── Edit
└── Delete

Location Management
├── List
├── Create
├── Detail
├── Edit
└── Delete

Feature Management
├── List
├── Create
├── Detail
├── Edit
└── Delete
```

---

## Planned

Future frontend milestones include:

- Reservation list/detail and lifecycle management UI
- Additional authentication UX improvements
- Public vehicle listing
- Public vehicle detail
- Public rental search and reservation flow with extras / insurance
- Customer-facing rental experience
- Broader responsive UI refinement

---

# 🗺️ Frontend Roadmap

The current Admin Fleet Management foundation follows this structure:

```text
Admin Authentication
        │
        ▼
Admin Layout
        │
        ▼
Fleet Management
        │
        ├── Cars
        │   ├── List
        │   ├── Detail
        │   ├── Edit
        │   ├── Status
        │   ├── Location
        │   ├── Pricing
        │   └── Features
        │
        ├── Brands
        ├── Categories
        ├── Locations
        └── Feature Definitions
```

Availability and reservation creation now build on this fleet foundation; broader reservation management and the rental lifecycle remain planned:

```text
Fleet Management
        │
        ▼
Availability
        │
        ▼
Reservation Management
        │
        ▼
Rental Lifecycle
```

After the administrative rental workflows are established, development can move toward the public rental experience:

```text
Location + Dates
       │
       ▼
Available Vehicles
       │
       ▼
Vehicle Detail
       │
       ▼
Pricing
       │
       ▼
Extras / Insurance
       │
       ▼
Reservation
```

---

# 💡 Frontend Design Philosophy

The DriverHub frontend follows several principles:

- Keep feature components focused on feature behavior.
- Keep API communication inside services.
- Keep request and response contracts explicit.
- Centralize API endpoint definitions.
- Centralize route definitions.
- Avoid duplicated route strings.
- Use standalone Angular components.
- Prefer Angular Signals for local state.
- Avoid unnecessary global state infrastructure.
- Synchronize frontend state after confirmed API operations.
- Keep destructive operations explicit through confirmation.
- Surface backend business rules clearly to administrators.
- Separate operational vehicle actions from general vehicle editing.
- Separate reusable feature definitions from vehicle-level feature assignments.
- Keep shared UI behavior inside shared components and services.
- Preserve consistency across related administrative workflows.

The objective is not only to build individual screens, but to maintain a frontend structure capable of growing together with the DriverHub domain.

---

# 🎯 Current Milestone

With Brand, Category, Location and Feature management completed alongside the operational Car Detail workflows, the **basic Fleet Management Admin foundation is complete**.

The frontend now has an established pattern for:

```text
List
Create
Detail
Edit
Delete
API Integration
Typed Contracts
Loading State
Error State
Toast Feedback
Business Rule Feedback
Local State Synchronization
```

This pattern also supports the reservation preparation workflow described above and provides a foundation for future administrative domains.

---

# 🔗 Related Project

DriverHub Client is part of the main DriverHub repository.

The ASP.NET Core backend, Clean Architecture implementation, Identity infrastructure and complete project documentation can be found in the repository root:

[DriverHub Root README](../../README.md)

---

# 👨‍💻 Author

### Yaşarcan Demirbüken

Software Engineer

GitHub  
> https://github.com/CanDemirbuken

LinkedIn  
> https://www.linkedin.com/in/ya%C5%9Farcan-demirb%C3%BCken-09095b205/

---

<p align="center">

Built with ❤️ using Angular and TypeScript as part of DriverHub.

</p>
