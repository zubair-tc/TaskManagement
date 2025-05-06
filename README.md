📌 Overview

A lightweight RESTful API for managing tasks built with ASP.NET Core 8.0.1 and SQLite. Provides full CRUD functionality with search, filtering, and pagination capabilities.
🛠 Tech Stack

⏳ Development Note: This project took approximately 2 hours to develop and includes a DemoVideo.webm in the root directory for quick functionality overview.


    Backend Framework: ASP.NET Core 8.0.1

    Database: Entity Framework Core 8.0.1

    Database Provider: SQLite (embedded database)

    API Documentation: Swagger/OpenAPI

    Package Manager: NuGet

🚀 How to Run the App
Prerequisites

    .NET 8.0 SDK

    Visual Studio 2022 (or VS Code with C# extension)

    (No SQL Server required - uses SQLite)

    Clone the repository

    Restore dependencies

    Run the application:

🔧 Key Features

    Zero-config SQLite database (no separate installation needed)

    Automatic database migrations

    Lightweight and portable solution

    Fast development setup


🧠 Assumptions Made

    Development Focus: SQLite chosen for developer convenience

    Scalability: Suitable for small to medium workloads

    Concurrency: Basic EF Core concurrency handling

    Portability: Entire database stored in single file

    Migrations: Automatic migrations enabled for development

🔄 Migrating to Production Database

To switch to SQL Server or PostgreSQL:

    Change the connection string

    Add the appropriate EF Core provider package

    Run dotnet ef migrations add InitialCreate --context TaskDbContext

    Run dotnet ef database update

🌟 Benefits of This Setup

    Rapid development - Get started immediately

    Self-contained - No external dependencies

    Cross-platform - Works on Windows, Linux, macOS

    Easy to test - Reset database by deleting the .db file

📡 API Endpoints Reference
Task Management Endpoints

All endpoints are relative to the base URL (e.g., https://localhost:7249/api/tasks).
Method	Endpoint	Description	Parameters/Headers
GET	/api/tasks	Get paginated/sorted tasks	?page=1&pageSize=10&sortBy=Name
GET	/api/tasks/{id}	Get task by ID	id (int, in URL path)
POST	/api/tasks	Create new task	Content-Type: application/json + Request Body
PUT	/api/tasks/{id}	Update task	id (int) + Content-Type: application/json
DELETE	/api/tasks/{id}	Delete task	id (int)
GET	/api/tasks/search	Search tasks	?query=string&isCompleted=true







Note: For production environments with higher traffic, consider switching to SQL Server or PostgreSQL by changing the provider in the DbContext configuration.

