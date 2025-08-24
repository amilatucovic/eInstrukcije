# 📚 eInstrukcije

**eInstrukcije** is a web application developed as part of the *Software Development I* course at the Faculty of Information Technologies. It serves as a platform that connects students and tutors for easy session scheduling, communication, and learning management.

---

## 🛠️ Technologies Used

- ASP.NET Core (Web API)
- Angular (Frontend)
- Entity Framework Core (ORM)
- SQL Server
- SignalR (Real-time messaging)
- JWT Authentication
- Git / Azure DevOps / GitHub

---

## 🚀 Key Features

- 👤 User roles: Student, Tutor, and Admin
- 📅 Schedule management: students can request sessions
- ✅ Tutors can confirm or reject requests
- 💬 Real-time chat between students and tutors (SignalR)
- 🔍 Browse available tutors and their profiles
- 🔐 Role-based access control
- 🧾 Admin module for managing tutors and users

---

## 👥 Test Users

Use the following credentials to log in and explore the application:

### 🎓 Student

- **Username:** emmaStudent  
- **Password:** `emma123`

### 📘 Tutor

- **Username:** oliviaTutor  
- **Password:** `olivia123`

### 🔐 Admin

- **Username:** AdminAdmin  
- **Password:** `admin123`

---

## 🖥️ Getting Started

### 🔧 Backend (ASP.NET Core)

1. Open the `eInstrukcije.sln` solution in Visual Studio
2. Run database migrations using the Package Manager Console:
    ```
    Update-Database
    ```
3. Start the backend server (`F5` or `dotnet run`)

### 🌐 Frontend (Angular)

```bash
cd frontend
npm install
ng serve



