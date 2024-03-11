# Veterinary Clinic Application

This repository contains the source code for the Veterinary Clinic Application, an ASP.NET Core MVC project designed to manage veterinary clinic operations, including handling owners and their pets.

## Getting Started

These instructions will get you a copy of the project up and running on your local computer for development and testing purposes.

### Prerequisites

- .NET 6.0 SDK or later (8.0 is preferred) 
- Visual Studio 2022 or JetBrains Rider (or any IDE that supports .NET development)
- Git

### Cloning the Repository

To get started with this project, clone the repository to your local computer:

```bash
git clone https://github.com/Semos-C-Project/veterinary-client-app.git
cd veterinary-client-app
```

### Creating a Branch

Before making any changes, create a new branch to work on:

```bash 
git checkout -b feature/your-new-feature
```
Replace feature/your-new-feature with a descriptive name for your branch.

### Installing Dependencies

After cloning the project, navigate to the project directory and restore the required packages:

```bash 
dotnet restore
```

### Setting up the Database

Ensure that the SQLite database is up to date with the latest migrations:

```bash 
dotnet ef database update
```

### Running the Application

To run the application locally:

```bash
dotnet run
```

Navigate to http://localhost:5000 (http://localhost:[yourport]) in your web browser to view the application.

### Pushing Changes

After making changes, add your changes to git, commit, and push them to the repository:

```bash
git add .
git commit -m "Describe your changes here"
git push origin feature/your-new-feature
// NOTE: change the brnach name after the origin -> git push origin [the branch name you created]
```

### Packages and Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- Bootstrap (for UI components)

### What We Have Done (so far)

- Initialized the project with ASP.NET Core MVC.
- Configured Entity Framework Core to use SQLite.
- Created models, views, and controllers for Owners.
- Implemented CRUD operations for managing owners.
- Personalized the landing page and navigation. <-( Needs more work )

### What Is Left To Do?

- Creat models, views, and controllers for Pets.
- Implement CRUD operations for managing pets.
- Implementing user authentication and authorization.
- Advanced model validation.
- Improving UI/UX design.
- Writing unit tests.
- Implementing caching for performance optimization.
- Preparing the application for production deployment.
