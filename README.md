# 🚗 RentCar – Full-Stack Car Rental Application

**RentCar** is an end-to-end car rental web application developed using **ASP.NET Core Razor Pages**, **ASP.NET Core Web API**, and **SQL Server**. The application enables users to browse available vehicles, filter by manufacturing year, perform date-range availability searches, and make car reservations with real-time price sorting and dynamic pagination.

---

## 🌟 Key Features

- **Vehicle Search & Filter:** Filter cars by manufacturing year, search by pickup and return dates, and sort by rental price.
- **User Authentication:** Cookie and JWT-based authentication for registration and login handling.
- **Rental History:** View past and active car rental transactions with automated total price calculations.
- **Decoupled Architecture:** Client-side Web App communicating with a standalone RESTful Web API service.
- **Dynamic Pagination:** Server-side pagination for optimal data presentation.

---

## 🛠️ Tech Stack

- **Front-End:** C#, ASP.NET Core Razor Pages, HTML5, CSS3, JavaScript
- **Back-End:** ASP.NET Core Web API, C#
- **Database:** Microsoft SQL Server (SSMS)
- **Authentication:** Cookie & JWT Authentication

---

## 📂 Project Structure

```text
├── PRojectRentcarwebclident/  # ASP.NET Core Razor Pages Web Client
├── RentCarApi/                # ASP.NET Core RESTful Web API Service
└── queryrentcar.sql           # Database schema & initial seed scripts

Prerequisites
1. .NET SDK

2. Microsoft SQL Server & SSMS

Database Setup
1. Open SSMS and execute the SQL script in queryrentcar.sql to generate the database schema and table records.

2. Update the connection string in RentCarApi/appsettings.json to match your local SQL Server instance.

Running the Project
1. start the API
cd RentCarApi
dotnet run

2. start the web client
cd PRojectRentcarwebclident
dotnet run
