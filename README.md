# SudoKool

A web-based Sudoku game with an Angular frontend, ASP.NET C# backend, and SQL Server database.

## How to Start the Game Locally

### 1. Start the Angular Frontend

Open one terminal and run:

```bash
cd 'angular/sodukool'
ng serve
```

The frontend will usually run at:

```text
http://localhost:4200
```

### 2. Start the ASP.NET Backend

Open a second terminal and run:

```bash
cd '.\C#\SudoKool Server\'
dotnet run
```

The backend will usually run at:

```text
http://localhost:5278
```

### 3. Update the Database

If this is your first time running the project, or if there are new migrations, run:

```bash
dotnet ef database update
```

### 4. Play

Open the Angular app in your browser:

```text
http://localhost:4200
```

Click **Start Game** to generate a new Sudoku board.

Click **Load Game** to choose a saved game by ID.
