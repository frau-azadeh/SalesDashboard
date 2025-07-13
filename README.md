# 📊 Sales Dashboard Admin Panel

A clean and modern role-based admin panel built with **ASP.NET MVC**, **TailwindCSS**, and **RESTful APIs**.  
Designed for internal teams such as Sales, Marketing, Purchasing, and Accounting to manage company operations efficiently.

---

## 🚀 Features

- 🔐 Login system with role-based access

- 🧑‍💼 Admin sees all pages; users see their own modules

- 📦 Products module with API-based data handling

- 📊 Dashboard ready for chart integrations (sales, orders, returns)

- 📝 CRM functionality for marketing leads (coming soon)

- 🧾 Invoice and order management system (planned)

- 💬 Real-time notification support (planned)

---

## ⚙️ Tech Stack

- 💻 ASP.NET Core MVC

- 🎨 Tailwind CSS

- 🛠 RESTful API (custom controller-based)

- 🧠 Session-based authentication

- 📊 (Upcoming) Chart.js or ApexCharts for data visualization

---

## 🛠️ Getting Started

1. **Clone the repository**
  
     git clone https://github.com/frau-azadeh/salesdashboard.git

2. Open in Visual Studio

	Run the project (with IIS Express or Kestrel)

	Login with sample users:

	admin / admin1234

	sales / sales1234

	marketing / marketing1234

	account / account1234

	purches / purches1234

## Project Structure

```
	SalesDashboard/
├── Controllers/
│   ├── AccountController.cs
│   ├── DashboardController.cs
│   └── Api/
│       └── ProductsController.cs
├── Models/
│   └── Product.cs
├── Views/
│   ├── Account/Login.cshtml
│   ├── Dashboard/Index.cshtml
│   └── Products/Index.cshtml
├── wwwroot/
│   └── css/output.css
├── Program.cs
└── README.md

```

## 📌 Milestones


✅ Phase 1: Basic Login with Role Support

✅ Phase 2: Product Module via REST API

⏳ Phase 3: Orders, Invoices, and CRM

⏳ Phase 4: Charts & Realtime Notifications

## 🧠 License

This project is under active development. Feel free to fork or contribute.

## 🌻Developed by

Azadeh Sharifi Soltani
