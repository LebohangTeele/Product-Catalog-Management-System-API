# Solution Design – Product Catalog Management System

## Overview

This document describes the architecture, design decisions, and implementation details for the Product Catalog Management System. The solution is a full-stack application built with ASP.NET Core Web API (.NET 9) for the backend and Angular SPA for the frontend. It enables administrators to manage products and categories, including CRUD operations, search, filtering, and hierarchical category management.

---

## Architecture

- **Backend:** ASP.NET Core Web API (.NET 9)
- **Frontend:** Angular SPA
- **Data Storage:** In-memory repositories 
- **API Documentation:** Swagger/OpenAPI

---

## Backend Design

### Data Models

- **Product:**  
  - Properties: `Id`, `Name`, `Description`, `SKU`, `Price`, `Quantity`, `CategoryId`, `CreatedAt`, `UpdatedAt`
  - Implemented as a C# record type with `IComparable` for sorting.

- **Category:**  
  - Properties: `Id`, `Name`, `Description`, `ParentCategoryId`
  - Implemented as a C# record type.

### Repository Pattern

- **IRepository<T>:** Generic interface for CRUD operations.
- **InMemoryRepository<T>:** In-memory implementation using `Dictionary<Guid, T>`.
- **Dependency Injection:** Registered as singleton services for both `Product` and `Category`.

### Services

- **ProductSearchEngine:**  
  - Handles product search by name and category.
  - Implements caching for search results using a dictionary.

- **CategoryTreeBuilder:**  
  - Builds a hierarchical tree structure from flat category data.

### Extensions

- **ProductFilterExtensions:**  
  - Custom LINQ extension methods for filtering products by category and searching by name.

### Middleware

- **RequestLoggingMiddleware:**  
  - Logs incoming HTTP requests for monitoring and debugging.

### Controllers

- **ProductController:**  
  - Endpoints for CRUD, search, pagination, and filtering.
  - Validates input using pattern matching.
  - Manual model binding and custom JSON serialization demonstrated.

- **CategoryController:**  
  - Endpoints for flat and hierarchical category lists, and category creation.

### CORS

- Configured to allow requests from the Angular frontend (`http://localhost:4200`).

### API Documentation

- Swagger UI available at `/swagger` for interactive API exploration.

---

## Frontend Design

### Angular SPA Structure

- **Product List Component:**  
  - Displays products in a grid/table.
  - Integrates search bar and category filter.
  - Supports delete with confirmation.

- **Product Form Component:**  
  - Used for both adding and editing products.
  - Includes validation for required fields.

- **Search Bar Component:**  
  - Allows searching products by name.

- **Category Filter Dropdown:**  
  - Filters products by selected category.

- **Category Management Component:**  
  - Lists categories and allows adding new categories.

- **Loading Indicator & Error Message Components:**  
  - Provide user feedback during data fetch and error scenarios.

### Routing

- `/products` – Product listing
- `/products/add` – Add product
- `/products/edit/:id` – Edit product
- `/categories` – Category management

### API Integration

- Communicates with backend via HTTP services.
- Handles CORS and error scenarios gracefully.

---

## Design Decisions

- **Separation of Concerns:**  
  - Clear separation between data access, business logic, and presentation.
- **Extensibility:**  
  - Easily replace in-memory repositories with persistent storage.
- **Modern C# Features:**  
  - Record types, pattern matching, nullable reference types.
- **Scalability:**  
  - Modular Angular components and services.
- **User Experience:**  
  - Loading indicators, error handling, and confirmation dialogs.

---

## How to Build, Test, and Run

See [README.md](./README.md) for step-by-step instructions to build, test, and run both backend and frontend locally.




