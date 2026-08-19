# 🖥️ DriverHub Client

<p align="center">

<img src="https://img.shields.io/badge/Angular-22-DD0031?style=for-the-badge&logo=angular" />
<img src="https://img.shields.io/badge/TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white" />
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

The frontend is designed to support two primary application areas:

- **Admin Panel**
- **Public Rental Experience**

Current development is focused on the **Admin Panel**, where administrators can manage the vehicle fleet and interact with the DriverHub ASP.NET Core Web API.

The frontend is built using standalone Angular components and follows a feature-oriented structure with separate core services, shared components, helpers and typed API contracts.

---

## 🚀 Technology Stack

- Angular
- TypeScript
- Angular Signals
- Angular Router
- HttpClient
- FormsModule
- SCSS
- Standalone Components

Backend integration:

- ASP.NET Core Web API
- JWT-based authentication
- REST API
- Standardized API responses

---

## 🏗️ Frontend Architecture

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
│   ├── authentication
│   ├── dashboard
│   ├── cars
│   └── ...
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

The main responsibilities are divided as follows:

### Core

Contains application-level infrastructure such as:

- API services
- Request / response models
- Route constants
- Guards
- Shared API contracts

### Features

Contains page-level application functionality.

Examples:

- Car List
- Car Detail
- Car Create
- Car Edit
- Dashboard
- Authentication screens

### Layouts

Defines the primary visual shells of the application.

Current layouts:

- Admin Layout
- Public Layout

### Shared

Contains reusable frontend functionality such as:

- Toast notifications
- Image URL helpers
- Reusable components
- Shared services

---

## 🧭 Routing

Routing is centrally managed through Angular Router.

Route path definitions are separated from navigation links to avoid duplicated route strings throughout the application.

The application currently contains separate routing areas for:

```text
Public
└── Public Layout

Admin
├── Authentication
└── Admin Layout
    ├── Dashboard
    └── Car Management
```

Protected Admin routes use route guards to prevent unauthorized navigation.

---

## 🛡️ Route Guards

Admin routes are protected through Angular route guards.

The guard layer is responsible for determining whether the user can navigate to protected application areas.

This keeps authorization-related navigation logic outside individual feature components.

---

## 🌐 API Integration

Backend communication is handled through Angular services using `HttpClient`.

Feature components do not construct API URLs directly.

Instead, communication follows a structure similar to:

```text
Component
    │
    ▼
Feature Service
    │
    ▼
API Endpoint Definition
    │
    ▼
ASP.NET Core Web API
```

Example service responsibilities include:

- `CarService`
- `LocationService`
- Media service
- Authentication services

Request and response contracts are represented through TypeScript interfaces.

---

## 📦 Standardized API Responses

The Angular application mirrors the standardized response structure returned by the DriverHub API.

This allows components to consistently handle:

- Successful responses
- API errors
- Validation errors
- Missing data
- Unexpected failures

Error messages returned by the backend can therefore be surfaced directly to the user when appropriate.

---

# 🚗 Car Management

Car Management is currently the most developed Admin Panel feature.

The frontend integrates directly with the Admin Car API.

---

## 📋 Car Listing

The Car List screen provides the administrative fleet overview.

It consumes the paginated Car API and displays vehicle information required for fleet management.

The screen provides navigation to individual vehicle detail and management screens.

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

The page also provides access to vehicle editing and inline operational actions.

---

## ✏️ Car Editing

Vehicle information can be edited through the dedicated Car Edit screen.

The edit flow:

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

The edit screen integrates with supporting data such as:

- Brands
- Categories
- Locations

Existing vehicle data is loaded into the form before editing.

---

## 🖼️ Vehicle Media Management

The Admin Panel supports vehicle image uploads.

Currently supported image formats include:

- JPG
- JPEG
- PNG
- WEBP

Two primary vehicle images are supported:

### Cover Image

Used primarily in vehicle listing cards.

### Big Image

Used as the larger vehicle image for detail-oriented views.

The upload UI provides:

- File selection
- Upload loading state
- Image preview
- Upload status feedback
- Existing image support

Uploaded image paths returned by the API are stored as part of the vehicle data.

---

## 🔗 Image URL Handling

Backend media paths and frontend display URLs are separated.

A shared `ImageUrlHelper` is responsible for resolving stored image paths into URLs that can be displayed by the browser.

This prevents individual components from manually constructing media URLs.

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

---

## 🚦 Vehicle Status Management

Vehicle status can be updated directly from the Car Detail screen.

Supported statuses currently include:

- Active
- Maintenance
- Out Of Service
- Damaged
- Retired

Status management uses an inline workflow rather than requiring navigation to the full Car Edit form.

This separates operational changes from general vehicle information editing.

The update flow is:

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

The UI prevents unnecessary requests when the selected status is already the vehicle's current status.

---

## 📍 Vehicle Location Management

The vehicle's current location can also be changed directly from the Car Detail screen.

Available locations are retrieved from the Location API.

The currently assigned location is synchronized with the vehicle detail state.

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

This allows operational fleet movements to be performed without opening the complete vehicle edit form.

---

## 💰 Vehicle Pricing Management

Pricing is managed from the Car Detail screen.

The pricing model currently supports:

- Daily
- Weekly
- Monthly

Editable pricing state is kept separately from the original API response.

This allows the user to modify pricing values before submitting them to the backend.

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

This separation avoids mutating the original vehicle state before the API confirms the update.

---

## ✨ Vehicle Feature Management

Vehicle feature management is currently being developed.

The intended flow allows administrators to select multiple available features for a vehicle.

Examples may include:

- Air Conditioning
- Cruise Control
- Rear View Camera
- Sunroof
- Heated Seats

The feature management flow is designed around a multi-selection model:

```text
Available Features
       │
       ▼
Editable Feature Selection
       │
       ▼
Save
       │
       ▼
Set Car Features API
       │
       ▼
Updated Car State
```

Feature selection is kept separate from the persisted vehicle state until the backend confirms the operation.

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

Feature components only request a notification:

```text
Feature Component
      │
      ▼
ToastService
      │
      ▼
Global Toast Component
```

This keeps notification rendering separate from business-oriented components.

---

## ⏱️ Toast Timer Management

Toast timers are centrally controlled.

When another toast is opened before the previous timeout expires, the previous timer is cleared.

This prevents an older notification timer from accidentally closing a newer notification.

---

# 🧠 State Management

The current application uses Angular Signals for local UI state.

Examples include:

- Loading state
- Error messages
- Selected vehicle status
- Selected location
- Editable pricing
- Upload state
- Toast state
- Current vehicle data

For example:

```text
API Data
   │
   ▼
Signal State
   │
   ▼
Template
```

The application currently avoids introducing a global state management library where local signals and services are sufficient.

---

## 🔄 Local State Synchronization

Operational updates such as status, location and pricing do not require a complete page reload.

After the backend confirms an update, the relevant portion of the local Car state is synchronized.

This provides immediate UI feedback while avoiding unnecessary GET requests.

---

# 🎨 UI Design

The Admin Panel uses custom SCSS rather than relying entirely on a component library.

Current UI patterns include:

- Admin sidebar
- Topbar
- Footer
- Dashboard layout
- Form cards
- Detail cards
- Status controls
- Location controls
- Pricing controls
- Image upload controls
- Loading indicators
- Toast notifications
- Responsive layout behavior

The goal is to keep the interface consistent while still maintaining control over the application's visual structure.

---

# 📂 Client Structure

The Angular application is located under:

```text
DriverHub
└── Client
    └── DriverHub.Client
```

The application uses standalone components rather than a traditional NgModule-oriented application structure.

---

# ⚙️ Getting Started

## Prerequisites

Make sure the following tools are installed:

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
ng serve
```

The application will start using the Angular development server.

---

## Build

Create a production build with:

```bash
ng build
```

Build artifacts are generated under:

```text
dist/
```

---

# 🔧 Environment Configuration

API base URLs are configured through Angular environment configuration.

Feature services build their request URLs using centralized endpoint definitions rather than hardcoded endpoint strings inside components.

Conceptually:

```text
environment.apiUrl
        +
ApiEndpoints
        │
        ▼
Final API URL
```

This keeps API addressing consistent throughout the application.

---

# 🧩 Current Admin Panel Status

## Completed

- ✅ Angular application foundation
- ✅ Standalone component structure
- ✅ Admin Layout
- ✅ Public Layout foundation
- ✅ Sidebar
- ✅ Topbar
- ✅ Footer
- ✅ Centralized routing
- ✅ Admin route protection
- ✅ HttpClient integration
- ✅ Standard API response handling
- ✅ Car API service
- ✅ Location API service
- ✅ Media upload integration
- ✅ Car listing
- ✅ Car detail
- ✅ Car creation
- ✅ Car editing
- ✅ Vehicle image upload
- ✅ Image preview
- ✅ Shared Image URL Helper
- ✅ Global Toast Service
- ✅ Global Toast Component
- ✅ Vehicle status management
- ✅ Vehicle location management
- ✅ Vehicle pricing management

## In Progress

- 🚧 Vehicle feature management
- 🚧 Admin fleet management improvements

## Planned

- Complete supporting entity management UI
- Authentication integration improvements
- Reservation management UI
- Availability management
- Public vehicle listing
- Public vehicle detail
- Rental search flow
- Reservation flow
- Extras and insurance selection
- Responsive UI improvements

---

# 🗺️ Frontend Roadmap

The Admin Panel is being developed first.

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
        ├── Brands
        ├── Categories
        ├── Locations
        └── Features
        │
        ▼
Reservation Management
```

After the administrative workflows are established, development will move toward the public rental experience:

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

The DriverHub frontend is built around several principles:

- Keep feature components focused on feature behavior.
- Keep API communication inside services.
- Use typed request and response contracts.
- Avoid duplicated route strings.
- Keep reusable UI behavior inside shared components and services.
- Prefer local Signal state when global state management is unnecessary.
- Synchronize UI state after successful API operations.
- Provide immediate feedback for user actions.
- Separate operational actions from full entity editing.
- Keep backend contracts and frontend models explicit.

The objective is not only to build screens, but to create a frontend structure that can grow together with the DriverHub domain.

---

# 🔗 Related Project

DriverHub Client is part of the main DriverHub repository.

The ASP.NET Core backend, Clean Architecture implementation, Identity infrastructure and complete project documentation can be found in the repository root:

```text
/README.md
```

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
