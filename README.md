

# 🛍️ E-Commerce API

A clean and scalable **E-Commerce Backend API** built using **ASP.NET Core**, following **N-Tier Architecture**, **Clean Code principles**, and various **Design Patterns** for maintainability and flexibility.

---

## 🧩 Architecture Overview

This project is structured using the **N-Tier Architecture**, separating responsibilities into three main layers:

1. **Presentation Layer (PL)** — Handles API endpoints and user interactions.
2. **Business Logic Layer (BLL)** — Contains business rules, validation, and service logic.
3. **Data Access Layer (DAL)** — Manages database interactions using Entity Framework Core.

This structure ensures:

* High scalability and testability.
* Clear separation of concerns.
* Easier maintenance and extension.

---

## 🧠 Design Patterns Implemented

* **Repository Pattern** – For cleaner data access logic.
* **Unit of Work Pattern** – To manage transactions efficiently.
* **Dependency Injection (DI)** – For better modularity and testing.
* **DTO Pattern** – For safe and efficient data transfer between layers.

---

## 🔐 Authentication & Authorization

The API uses **JWT (JSON Web Token)** for secure authentication and authorization.

### User Roles:

* 🧑‍💼 **SuperAdmin** – Full access to all operations and management.
* 👨‍💻 **Admin** – Can manage products, brands, and categories.
* 🛒 **Customer** – Can browse, purchase, and manage their own cart.

### Auth Endpoints:

* `POST /api/Auth/Register` – Register a new user.
* `POST /api/Auth/Login` – Sign in with JWT token response.
* `GET /api/Auth/ConfirmEmail` – Confirm user’s email via token link.
* `POST /api/Auth/ResetPassword` – Request password reset.
* `POST /api/Auth/ChangePassword` – Change user password.

---

## 💳 Payment Integration

Integrated with **Stripe API** for secure and smooth payment processing.

### Features:

* Create checkout sessions.
* Handle payment confirmations.
* Support for multiple payment methods.

---

## 🛠️ Main Endpoints

### 🏷️ Categories

* `GET /api/Categories` – Get all categories.
* `GET /api/Categories/{id}` – Get category by ID.
* `POST /api/Categories` – Add new category.
* `PUT /api/Categories/{id}` – Update existing category.
* `DELETE /api/Categories/{id}` – Delete category.

### 🏭 Brands

* `GET /api/Brands` – Get all brands.
* `GET /api/Brands/{id}` – Get brand by ID.
* `POST /api/Brands` – Add new brand.
* `PUT /api/Brands/{id}` – Update existing brand.
* `DELETE /api/Brands/{id}` – Delete brand.

### 📦 Products

* `GET /api/Products` – Get all products.
* `GET /api/Products/{id}` – Get product details.
* `POST /api/Products` – Add new product.
* `PUT /api/Products/{id}` – Update product details.
* `DELETE /api/Products/{id}` – Delete product.

---

## 🛒 Cart & Checkout Operations

* `POST /api/Cart/Add` – Add product to cart.
* `GET /api/Cart` – View user’s cart.
* `DELETE /api/Cart/{productId}` – Remove product from cart.
* `POST /api/Checkout` – Complete order & process Stripe payment.

---

## 📧 Email Operations

* Email confirmation after registration.
* Password reset emails.
* Token-based secure verification links.

---

## 🧹 Clean Code Practices

* Consistent naming conventions.
* DTOs for request/response validation.
* Centralized error handling & response wrappers.
* Logging integrated for better debugging.
* Dependency injection for all services and repositories.

---

## ⚙️ Technologies Used

* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **Stripe API**
* **JWT Authentication**
* **AutoMapper**
* **Dependency Injection (DI)**

---

## 🚀 Getting Started

### Prerequisites

* .NET SDK 8 or later
* SQL Server
* Stripe Account (for payment testing)

### Steps

1. Clone the repository

   ```bash
   git clone https://github.com/yourusername/ecommerce-api.git
   ```
2. Navigate to the project folder

   ```bash
   cd ecommerce-api
   ```
3. Update the `appsettings.json` with your:

   * Database connection string
   * JWT secret key
   * Stripe secret key
4. Run database migrations

   ```bash
   dotnet ef database update
   ```
5. Run the project

   ```bash
   dotnet run
   ```

---

