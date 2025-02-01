<h1 align="center">Employee Management API</h1>
<p align="center">
  <img src="https://readme-typing-svg.herokuapp.com?font=Fira+Code&pause=1000&color=2196F3&center=true&vCenter=true&width=435&lines=Modern+Employee+Management+System;Built+with+.NET+8.0;PostGreSQL+Server+Database;RESTful+API+Architecture" alt="Typing SVG" />
</p>
<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-blue?style=for-the-badge&logo=.net" alt="NET 8.0" />
  <img src="https://img.shields.io/badge/SQL_Server-2019-red?style=for-the-badge&logo=microsoft-sql-server" alt="SQL Server" />
  <img src="https://img.shields.io/badge/C%23-11.0-brightgreen?style=for-the-badge&logo=c-sharp" alt="C# 11.0" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-9.0-purple?style=for-the-badge&logo=entity-framework" alt="EF Core" />
</p>

Welcome to the **Employee Management Web API**! This API allows you to **manage employees**, departments, locations, and more using **C# .NET with PostgreSQL**. 🚀

---

## 🌟 Features
✅ **CRUD operations** for Employees 👥  
✅ **Search Employees** 🔍  
✅ **Database Integration (PostgreSQL)** 🛢️  
✅ **RESTful API with Swagger** 📜  
✅ **Secure and Scalable Architecture** 🔒  
✅ **Added World's All Countries, States & Cities Data in It** 🌍  

---

## 🛠️ Tech Stack

- **Backend:** C# (.NET Core) 🖥️  
- **Database:** PostgreSQL 🛢️  
- **ORM:** Entity Framework Core 🔄  
- **API Documentation:** Swagger 📜  
- **Environment:** Visual Studio Code 💻  

---

## 📂 Folder Structure
```
📦 EmployeeManagementAPI
 ┣ 📂 Controllers
 ┃ ┣ 📜 EmployeeController.cs
 ┣ 📂 Models
 ┃ ┣ 📜 Employee.cs
 ┃ ┣ 📜 Department.cs
 ┃ ┣ 📜 Country.cs
 ┃ ┣ 📜 State.cs
 ┃ ┣ 📜 City.cs
 ┣ 📂 Data
 ┃ ┣ 📜 ApplicationDbContext.cs
 ┃ ┣ 📜 countries+states+cities.json
 ┣ 📜 Program.cs
 ┣ 📜 appsettings.json
 ┣ 📂 Script
 ┃ ┣ 📜 insert_data.py
```

---

## 🚀 Installation & Setup

### 📌 Prerequisites
- Install **.NET Core SDK**
- Install **PostgreSQL**
- Install **Visual Studio Code**

### 📌 Steps to Run
```sh
# Clone the repository
git clone https://github.com/yourusername/EmployeeManagementAPI.git
cd EmployeeManagementAPI

# Install dependencies
dotnet restore

# Update database
dotnet ef database update

# Run the application
dotnet run
```

---

## 🌍 API Endpoints

| HTTP Method | Endpoint | Description |
|-------------|----------|-------------|
| **GET** | `/api/employees` | Get all employees |
| **POST** | `/api/employees` | Add a new employee |
| **PUT** | `/api/employees/{id}` | Update employee |
| **DELETE** | `/api/employees/{id}` | Delete employee |
| **GET** | `/api/employees/search?name=John` | Search employee by name |

---

## 📜 Swagger API Documentation

🔹 Open your browser and go to:  
👉 **`https://localhost:5001/swagger/index.html`**  

---

## 🛠️ Contributing
💡 Contributions are welcome! Feel free to fork this repository and submit pull requests.

1. **Fork the repository**
2. **Create a new branch** (`feature/your-feature`)
3. **Commit your changes**
4. **Push to your branch**
5. **Submit a pull request**

---

## 🤝 Contact
🔗 **GitHub:** [KrupalPatel17](https://github.com/KrupalPatel17)  

---

🎉 **Happy Coding! 🚀**
<p align="center">
  <img src="https://raw.githubusercontent.com/Platane/snk/output/github-contribution-grid-snake.svg" alt="snake animation" />
</p>

<p align="center">Made with ❤️ by Krupal</p>
