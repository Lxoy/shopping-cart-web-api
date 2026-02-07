# 🛒 ShoppingCart API

![.NET](https://img.shields.io/badge/.NET-10.0-blue?style=flat-square)
![EF Core](https://img.shields.io/badge/EFCore-10.0-lightgrey?style=flat-square)
![Node.js](https://img.shields.io/badge/Node.js-18+-green?style=flat-square)
![npm](https://img.shields.io/badge/npm-yellow?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)

A RESTful web API for managing articles and user shopping carts. <br />
Built with **ASP.NET Core Web API** and **Entity Framework Core**, following a clean layered architecture approach.

---

## ✨ Features
📦 **Articles**
- Get all articles
- Get article by ID
- Create new article
- Update article (partial update)
- Soft delete article

🛍️ **Cart**
- Get user cart
- Add article to cart
- Update item quantity
- Remove item from cart
- Clear entire cart
- Validation for invalid (deleted) articles

⚙️ **Additional Features**
- Global exception handling middleware
- DTO pattern (separating API models from domain models)
- Soft delete implementation
- Business rule validation
- EF Core Fluent API configurations

---

## 🏗️ Architecture
This project is structured into multiple layers:
- ShoppingCart.API – controllers, request/response models, exception handling
- ShoppingCart.Services – business logic, validations, DTOs, custom exceptions
- ShoppingCart.Data – entity models, DbContext, Fluent API configurations
- ShoppingCart.Frontend – React + Vite frontend
---

## 🗄️ Database
Uses Entity Framework Core with a Code First approach.

**Entities**
- User
- Article
- Cart
- CartItem

**Relationships**
- User <-> Cart (1:1)
- Cart <-> CartItems (1:N)
- Article <-> CartItems (1:N)

---

## 🚀 Running the Project
1. Clone the repository
```bash
git clone <repository-url>
```

2. Configure the connection string in appsettings.json
```json
{
    "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=shoppingcart;Username=postgres;Password=postgres"
    }
}

```

3. Apply migrations
```bash
dotnet ef database update
```

4. Run the application
```bash
dotnet run
```

> 5. (Optional) Run swagger
> ```
> https://localhost:7148/swagger/index.html
> ```

---

| Feature | Status |
|---------|--------|
| JWT Auth | ![Todo](https://img.shields.io/badge/status-todo-red) |
| Unit Tests | ![Todo](https://img.shields.io/badge/status-todo-red) |
| Caching | ![Todo](https://img.shields.io/badge/status-todo-red) |

## 🌐 ShoppingCart.Frontend

Frontend application for the Shopping Cart system, built with React (Vite).

## ⚡ Setup & Run

1. Navigate to the frontend project:
```
cd ShoppingCart.Frontend
```

2. Create a .env file in the root of the frontend project and add:
```
VITE_API_URL=http://localhost:5247/api
```

3. Install dependencies:
```
npm install
```

4. Start the development server:
```
npm run dev
```

5. The application will be available at:
```
http://localhost:5173
```

> 💡 **Note:**  
> - The backend must be running for the application to work correctly
> - The API URL can be changed inside the `.env` file depending on the environment.

