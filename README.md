# 🛒 StoreApp — Multi-Layered Store Management System

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-13.0-239120?style=flat-square&logo=csharp)
![Architecture](https://img.shields.io/badge/Architecture-N--Tier%20Layered-0A66C2?style=flat-square)
![Async](https://img.shields.io/badge/Concurrency-Async%2FAwait-orange?style=flat-square)

A modular, multi-layered Store Management Console Application built with **C# (.NET 9)**. Designed around **Separation of Concerns (SoC)**, **Dependency Abstraction**, and **Pluggable Persistence**, enabling seamless switching between JSON file storage, flat text files, and databases without altering business logic.

---

## 🎯 Key Architectural Highlights

* **N-Tier Layered Architecture:** Cleanly isolates Presentation (`StoreApp`), Business Logic (`BL`), Data Access (`DAL`), and Core Entities (`Domains`) into distinct projects.
* **Pluggable Storage Mechanism:** Abstracts persistence behind repository interfaces, currently operating with **JSON file storage** while allowing runtime or configuration-level switching across alternative `StorageType` providers (Text Files / Databases).
* **Asynchronous I/O Operations:** Utilizes `async` / `await` patterns across file operations and service calls to prevent blocking execution and prepare the data layer for database integration.
* **Generic Validation & Utilities:** Includes reusable validation helpers (`Validator<T>`) and structured file utilities (`FileHelper`) for clean error handling and consistent console formatting.
* **Object-Oriented Console UI:** Built on a polymorphic menu hierarchy inheriting from `BaseMenu`, providing standardized navigation, screen rendering, and user input validation.

---

## 🏗️ Solution Architecture

```text
StoreApp/
├── StoreApp/                  # Presentation Layer (Console UI & Menus)
│   ├── Menus/
│   │   ├── BaseMenu.cs        # Abstract base class for console screen rendering & navigation
│   │   ├── MainMenu.cs        # Primary application entry menu
│   │   ├── CustomerMenu.cs    # Customer CRUD & management workflows
│   │   ├── ItemMenu.cs        # Inventory & product catalog management
│   │   ├── OrderMenu.cs       # Order creation linking customers and items
│   │   └── ReportMenu.cs      # Sales & store analytics views
│   └── Program.cs             # Application bootstrap & storage configuration
├── BL/                        # Business Logic Layer (Services, Rules & Generic Validators)
├── DAL/                       # Data Access Layer (JSON, Text & Pluggable Storage Repositories)
└── Domains/                   # Core Domain Models (Customer, Item, Order, OrderItem)
```

---

## ✨ Core Features

* **Customer Management:** Register, update, search, and list store customers with input validation.
* **Inventory Management:** Manage store items, pricing, and stock quantities.
* **Order Processing:** Create multi-item orders linked to registered customers with automatic total calculation.
* **Reporting Module:** Generate structured console reports summarizing orders, customer activity, and inventory status.
* **Configurable Persistence:** Default JSON serialization/deserialization storage with modular support for switching storage backends.

---

## 🚀 Getting Started

### Prerequisites
* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* Visual Studio 2022 (v17.12+) or .NET CLI

### Installation & Execution
1. **Clone the repository:**
   ```bash
   git clone [https://github.com/MoThabet20/StoreApp.git](https://github.com/MoThabet20/StoreApp.git)
   cd StoreApp
   ```
2. **Restore dependencies and build the solution:**
   ```bash
   dotnet restore
   dotnet build
   ```
3. **Run the application:**
   ```bash
   dotnet run --project StoreApp
   ```

---

## 🗺️ Future Roadmap
* [ ] Implement SQL Server / Entity Framework Core provider as an additional `StorageType` option.
* [ ] Add unit testing project (`xUnit` / `Moq`) for Business Logic services and validators.
* [ ] Expose Business Logic via an ASP.NET Core Web API layer.

---

## 👨‍💻 Author
**Mohamed Thabet Hussein**  
Full Stack .NET Developer  
[GitHub](https://github.com/MoThabet20)
