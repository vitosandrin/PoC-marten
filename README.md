# 🚀 C# .NET PoC - Marten

![GitHub top language](https://img.shields.io/github/languages/top/vitosandrin/PoC-marten)
![GitHub last commit](https://img.shields.io/github/last-commit/vitosandrin/PoC-marten)
![GitHub issues](https://img.shields.io/github/issues/vitosandrin/PoC-marten)

## 📖 About the Project

This repository contains a **Proof of Concept (PoC)** to demonstrate the use of **Marten** as an Event Store and ORM in **.NET**.

The main goal is to validate the technical feasibility of **Marten** within an architecture based on **DDD (Domain-Driven Design)**, **Clean Architecture**, and **CQRS (Command Query Responsibility Segregation)**.

## 🛠️ Technologies Used

- **.NET 8**
- **ASP.NET Core**
- **Marten** (Event Store & ORM)
- **Docker**
- **PostgreSQL**
- **MediatR** (for CQRS)
- **Swagger** (API documentation)

---

## 📦 How to Run the Project

### 🔹 **Prerequisites**

Before starting, make sure you have the following dependencies installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/)

### 🔹 **Step-by-Step Guide**



docker-compose.Development.Infrastructure.yaml

1. **Clone this repository**
   ```sh
   git clone https://github.com/vitosandrin/PoC-marten.git
   cd PoC-marten
   ```

2. **Run containers**
   ```sh
   docker compose -f docker-compose.Development.Infrastructure.yaml up -d
   ```

3. **Restore dependencies**
   ```sh
   dotnet restore
   ```

4. **Run the application**
   ```sh
   dotnet run
   ```

5. **Access the API**
   - Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)
   - API: [http://localhost:5000/api](http://localhost:5000/api)

---

## 🧩 Project Structure

```
📦 PoC-Marten
 ┣ 📂 src              # Main source code
 ┃ ┣ 📂 Application   # Business logic and use cases (CQRS)
 ┃ ┣ 📂 Domain        # Entities and domain rules (DDD)
 ┃ ┣ 📂 Infrastructure # Persistence, Repositories, Configurations
 ┃ ┣ 📂 API           # Controllers and endpoints
 ┃ ┗ 📜 Program.cs    # Application entry point
 ┣ 📂 tests           # Unit and integration tests
 ┣ 📜 .gitignore      # Git ignored files
 ┣ 📜 README.md       # Project documentation
 ┣ 📜 docker-compose.yml # Docker configuration
 ┗ 📜 appsettings.json # Application settings
```

---

## 🏗️ Contributing

If you want to contribute, follow these steps:

1. **Fork the repository**
2. **Create a new branch** (`git checkout -b feature-new-feature`)
3. **Make your changes**
4. **Commit your changes** (`git commit -m "Add new feature"`)
5. **Push to the branch** (`git push origin feature-new-feature`)
6. **Open a Pull Request**

---
