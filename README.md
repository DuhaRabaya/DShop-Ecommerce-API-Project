# 🛒 DSHOP – E-Commerce Backend API

DSHOP is a full-featured ASP.NET Core Web API for an e-commerce platform.
It provides secure authentication, product management, cart and order handling,
Stripe payments, reviews, localization, and role-based user management.

This project follows clean architecture principles and modern backend best practices.

---

## 🚀 Features

### 🔐 Authentication & Authorization
- JWT authentication
- Refresh tokens
- Email confirmation
- Reset password
- Role-based access control
- Block / unblock users
- Change user roles

### 🧑‍💻 User Management
- Register & login
- Get users & user details
- Secure secrets using User Secrets

### 🛍️ Products & Categories
- Create, update, delete products
- Upload main image & sub-images
- Product translations (localization support)
- Product search
- Pagination, filtering & sorting
- Toggle category status

### ⭐ Reviews
- Add review to product
- Update review
- Remove review
- Get product details with reviews

### 🛒 Cart
- Add products to cart
- Get cart items
- Update item quantity
- Remove item from cart
- Clear cart

### 📦 Orders
- Create orders
- Order items handling
- Decrease product quantities after purchase
- Get orders by status
- Update order status

### 💳 Payments
- Stripe Checkout integration
- Cash & Visa payment methods
- Payment success handling
- Order approval after successful payment
- Email notification after successful payment

### 🌍 Localization
- Multi-language support (EN / AR)
- Language switching via query string

### 🧰 Technical Features
- Repository pattern
- Mapster for object mapping
- Fluent Validation (custom validations)
- Global exception handling middleware
- CORS policy configuration
- Seed data
- Audit fields

---

## 🧱 Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- JWT Authentication
- Stripe Checkout
- Mapster

