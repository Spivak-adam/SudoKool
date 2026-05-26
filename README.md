# SudoKool

A web-based Sudoku game with an Angular frontend, ASP.NET C# backend, and SQL Server database.

---

## Prerequisites

Before running the project locally, make sure you have the following installed:

### 1. Visual Studio Code

Install Visual Studio Code if you do not already have it.

Recommended VS Code extensions:

- Angular Language Service
- C# Dev Kit
- C#
- SQL Server / MSSQL extension

The Angular extension is not required to run the project, but it helps with Angular files, templates, and autocomplete.

---

### 2. Node.js and npm

Angular uses Node.js and npm.

After installing Node.js, verify it is installed:

```bash
node -v
npm -v
```

---

### 3. Angular CLI

Install the Angular CLI globally:

```bash
npm install -g @angular/cli
```

Verify it installed correctly:

```bash
ng version
```

If `ng` is not recognized, you can run Angular using:

```bash
npx ng serve
```

---

### 4. .NET SDK

Install the .NET SDK so the ASP.NET C# backend can run.

Verify it is installed:

```bash
dotnet --version
```

You will also need the Entity Framework CLI tools for database migrations:

```bash
dotnet tool install --global dotnet-ef
```

If it is already installed, update it with:

```bash
dotnet tool update --global dotnet-ef
```

Verify it works:

```bash
dotnet ef
```

---

### 5. SQL Server Express

Install SQL Server Express on your laptop.

SQL Server Express is the local database server that will run the SudoKool database.

During installation, you can use the default options. If you are using LocalDB, the server name will usually be:

```text
(localdb)\MSSQLLocalDB
```

If you are using SQL Server Express, the server name may be:

```text
localhost\SQLEXPRESS
```

or:

```text
.\SQLEXPRESS
```

---

### 6. SQL Server Management Studio

Install SQL Server Management Studio, also known as SSMS.

SSMS is the program used to connect to SQL Server, create databases, and view tables.

---

## Database Setup

### 1. Open SQL Server Management Studio

Open SQL Server Management Studio.

When the connection window opens, use one of the following server names depending on your installation:

```text
(localdb)\MSSQLLocalDB
```

or:

```text
localhost\SQLEXPRESS
```

or:

```text
.\SQLEXPRESS
```

Use:

```text
Authentication: Windows Authentication
```

Then click **Connect**.

---

### 2. Create the SudoKool Database

In SSMS:

1. Right-click **Databases**
2. Click **New Database**
3. Name the database:

```text
SudokoolDb
```

4. Click **OK**

This creates the local database for the project.

---

### 3. Check the Backend Connection String

In the ASP.NET backend project, check the connection string in `appsettings.json`.

It should look similar to this if you are using LocalDB:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=SudokoolDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

If you are using SQL Server Express, it may look like this:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=SudokoolDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

The important part is that the `Database` value matches:

```text
SudokoolDb
```

---

## How to Start the Game Locally

### 1. Start the Angular Frontend

Open one terminal and run:

```bash
cd 'angular/sodukool'
npm install
ng serve
```

If `ng serve` does not work, use:

```bash
npx ng serve
```

The frontend will usually run at:

```text
http://localhost:4200
```

---

### 2. Start the ASP.NET Backend

Open a second terminal and run:

```bash
cd '.\C#\SudoKool Server\'
dotnet restore
dotnet run
```

The backend will usually run at:

```text
http://localhost:5278
```

---

### 3. Update the Database

If this is your first time running the project, or if there are new migrations, run this from the backend project folder:

```bash
dotnet ef database update
```

This applies the database migrations and creates the needed tables inside `SudokoolDb`.

---

### 4. Play the Game

Open the Angular app in your browser:

```text
http://localhost:4200
```

Click **Start Game** to generate a new Sudoku board.

Click **Load Game** to choose a saved game by ID.

---

## Common Issues

### `ng` is not recognized

If you see:

```text
ng is not recognized as the name of a cmdlet
```

Run:

```bash
npm install -g @angular/cli
```

Then close and reopen the terminal.

You can also use:

```bash
npx ng serve
```

---

### SQL Server connection error

If SSMS cannot connect, make sure SQL Server Express or LocalDB is installed.

For LocalDB, try:

```text
(localdb)\MSSQLLocalDB
```

For SQL Server Express, try:

```text
localhost\SQLEXPRESS
```

or:

```text
.\SQLEXPRESS
```

Also make sure the database name is placed in the **Database** field or connection string, not the **Server Name** field.

---

### `dotnet ef` is not recognized

Install the EF Core CLI tools:

```bash
dotnet tool install --global dotnet-ef
```

Then close and reopen the terminal.

---

## Project Structure

```text
SudoKool
│
├── angular/
│   └── sodukool/
│       └── Angular frontend
│
└── C#/
    └── SudoKool Server/
        └── ASP.NET C# backend
```

---

## Local Startup Summary

Run the frontend:

```bash
cd 'angular/sodukool'
npm install
ng serve
```

Run the backend:

```bash
cd '.\C#\SudoKool Server\'
dotnet restore
dotnet run
```

Update the database when needed:

```bash
dotnet ef database update
```
