> 📘 A complete library management system built with **ASP.NET Core MVC + Web API**, applying clean architecture principles and modern .NET development practices.
# 📚 Full Stack Library Management System (ASP.NET Core MVC + Web API)
---

## 🚀 Project Overview

The Library Management System allows managing books, categories, and users with role-based access control.  
It includes CRUD operations, validation, authentication, and integration between MVC and Web API projects.

---

## 🧩 Technologies & Tools

### 🖥️ Front-End
- HTML5  
- CSS3  
- Bootstrap 5  
- JavaScript (ES6)  
- jQuery  

### 🗄️ Back-End
- C#  
- ASP.NET Core MVC  
- ASP.NET Core Web API  
- Entity Framework Core  
- AutoMapper  
- Dependency Injection  
- Repository Pattern  

### 🧰 Database
- Microsoft SQL Server 2014+  
- SQL Management Studio  

### 🔐 Security
- Microsoft Identity  
- JWT Authentication  
- Role-Based Authorization  

### 🧪 API Testing
- Swagger UI  
- Postman  

---

## 🧱 Project Architecture

This project follows a **3-Layer Architecture** for scalability, maintainability, and separation of concerns:

📁 LibraryManagementSystem/
┣ 📂 LMS.DAL/ # Data Access Layer
┃ ┣ 📂 Entities/ # Entity classes (Book, Category, User, etc.)
┃ ┣ 📂 Database/ # DbContext and configuration
┃ ┗ 📜 DbInitializer.cs # Database seeding

┣ 📂 LMS.BLL/ # Business Logic Layer
┃ ┣ 📂 Interfaces/ # Interfaces for services and repositories
┃ ┣ 📂 GenericRepository/ # Generic repository implementation
┃ ┣ 📂 CustomRepository/ # Custom repository logic for specific entities
┃ ┣ 📂 Models/DTOs/ # Data Transfer Objects
┃ ┣ 📂 Services/ # Service layer handling business rules
┃ ┣ 📂 Helpers/ # Helper methods and utilities
┃ ┗ 📂 AutoMapperProfiles/ # Mapping configuration between Entities and DTOs

┣ 📂 LMS.PL/ # Presentation Layer (UI)
┃ ┣ 📂 LMS.MVC/ # MVC Web Application (Views + Controllers)
┃ ┗ 📂 LMS.API/ # Web API Project (API Controllers + Swagger)

┣ 📜 appsettings.json # Configuration file
┣ 📜 Program.cs / Startup.cs # Application startup and DI setup
┗ 📜 README.md # Project documentation

## 🧩 Layer Explanation

### 🧱 Data Access Layer (DAL)
- Defines all **Entities** (tables in the database).  
- Contains the **DbContext** to communicate with SQL Server.  
- Handles database creation, migrations, and seeding.  

### ⚙️ Business Logic Layer (BLL)
- Contains **Interfaces**, **Repositories**, and **Services**.  
- Implements the **Repository Pattern** and **Dependency Injection**.  
- Uses **AutoMapper** to convert between Entities and DTOs.  
- Includes **Helper Methods** for reusable logic.  

### 🖥️ Presentation Layer (UI)
Consists of **two projects**:
- **MVC Project:** User-friendly interface with views and controllers.  
- **API Project:** Provides RESTful APIs with Swagger documentation.  
  Used for testing and integration with other systems.


## ⚙️ Features

✅ Full CRUD operations (Create, Read, Update, Delete)  
✅ Entity Framework Core with Code-First  
✅ AutoMapper for object mapping  
✅ Microsoft Identity for user management  
✅ JWT Authentication for APIs  
✅ Swagger UI for API testing  
✅ File Upload and Delete functionality  
✅ Responsive Admin Dashboard (AdminLTE)  
✅ Globalization & Localization  
✅ Dependency Injection and SOLID principles  


## 🧠 Learned Concepts

- Web Technologies: HTML5, CSS3, JavaScript, jQuery, Bootstrap  
- Backend: C#, ASP.NET Core MVC, APIs, EF Core  
- Database: SQL Server, ERD, Queries, Stored Procedures, Joins  
- Design Patterns: Repository, Dependency Injection  
- Software Engineering: SDLC, Agile, UML, System Design  


## 🧑‍💻 Developed By

**Abd El-Masih Khamis Habib Abou El-Yamein**  
Faculty of Computers and Information – Minia University  
Full Stack .NET Diploma Student  
