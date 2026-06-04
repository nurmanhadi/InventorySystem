# InventorySystem

A simple, modern web application for managing your business inventory. Track products, stock levels, and transactions all in one place.

---

## Table of Contents

1. [Overview](#overview)
2. [Key Features](#key-features)
3. [Technology Stack](#technology-stack)
4. [Project Structure](#project-structure)
5. [What Can You Do](#what-can-you-do)
6. [Getting Started](#getting-started)
   - [Requirements](#requirements)
   - [Installation Steps](#installation-steps)
   - [Database Setup](#database-setup)
7. [Running the Application](#running-the-application)
8. [Testing the Application](#testing-the-application)
9. [For Developers](#for-developers)
10. [Configuration](#configuration)
11. [API Documentation](#api-documentation)
12. [Deployment](#deployment)
13. [Contributing](#contributing)
14. [License](#license)

---

## Overview

InventorySystem is a web-based application designed to help businesses keep track of their products and inventory. Whether you're managing a small shop or a growing warehouse, this application lets you:

- Store and organize all your products in one central place
- Track how much stock you have at any moment
- Record when items arrive or leave your warehouse
- See important reports and statistics about your inventory
- Search and filter products by name or category
- Maintain a complete history of all stock movements

**Who is this for?**
- Shop owners managing multiple products
- Warehouse managers tracking inventory levels
- Businesses that need to monitor stock movements
- Anyone who needs organized product and inventory management

---

## Key Features

- 📦 **Product Management** - Create, view, update, and delete products
- 🏷️ **Category Organization** - Organize products into categories for better management
- 📊 **Stock Tracking** - Record items coming in and going out of inventory
- 📈 **Inventory Reports** - View statistics and summaries of your inventory
- 🔍 **Product Search** - Easily find products by name or filter by category
- 📝 **Stock History** - Complete record of all stock movements with timestamps
- 🔐 **Data Validation** - Ensures all data is correct before saving
- 👥 **User Management** - Create and manage different user roles with specific permissions
- 🔐 **Secure Authentication** - Password-protected login with role-based access control

---

## Technology Stack

**What Powers This Application?**

This application is built with modern, reliable technologies:

| Component | Technology | Purpose |
|-----------|-----------|---------|
| **Backend Framework** | .NET 10.0 | The main framework that runs the application |
| **Database** | PostgreSQL | Secure database that stores all your data |
| **ORM (Data Access)** | Entity Framework Core | Makes it easy to work with the database |
| **Password Security** | BCrypt.Net-Next | Safely encrypts and stores user passwords |
| **API Documentation** | Swagger | Shows all available features and lets you test them |
| **Data Validation** | FluentValidation | Checks that all your data is correct before saving |
| **Performance Monitoring** | OpenTelemetry | Tracks how the application performs and responds |
| **Logging** | Serilog | Records detailed information about what happens in the app |

**Why these technologies?**
- .NET is fast, secure, and widely used for business applications
- PostgreSQL is reliable and can handle large amounts of data
- Swagger makes it easy to understand and use the API
- OpenTelemetry helps identify and fix performance issues

---

## Project Structure

Here's how the code is organized:

```
src/InventorySystem/
├── Routers/                 - Handles incoming requests and routes them to services
├── Services/                - Contains business logic and core operations
├── Models/                  - Database entity models and context
├── DTOs/                    - Data transfer objects for API requests/responses
├── Validations/             - Data validation rules for incoming requests
├── Configs/                 - Application configuration and setup
├── Helpers/                 - Utility functions and constants
├── Exceptions/              - Custom exception classes
├── Middlewares/             - HTTP middleware components
├── Migrations/              - Database migration history (auto-generated)
├── Properties/              - Application properties and launch settings
├── Program.cs               - Application entry point and startup configuration
├── InventorySystem.csproj   - Project file with dependencies
└── appsettings.*.json       - Environment-specific configuration files
```

**What this means:**
- **Routers** receive requests from users and direct them to the right place
- **Services** do all the actual work (creating, updating, deleting items)
- **Models** define what your data looks like in the database
- **DTOs** format the data for sending and receiving via the API
- **Validations** check that incoming data meets requirements
- **Configs** set up authentication, database, CORS, and other services
- **Helpers** provide utility functions and constants used across the application
- **Exceptions** handle custom error scenarios
- **Middlewares** process HTTP requests/responses globally

---

## What Can You Do

### 🏷️ Category Management
Organize your products into groups
- Create new categories (e.g., Electronics, Clothing, Food)
- View all your categories
- Edit category names
- Delete categories you no longer need

### 📦 Product Management
Add and manage your products
- Create new products with name, price, and category
- Assign a unique SKU (Stock Keeping Unit) to each product
- View all products or search by name
- Filter products by category
- Edit product details
- Delete products

### 📊 Stock Management
Track items moving in and out
- **Stock In**: Record when new items arrive at your warehouse
- **Stock Out**: Record when items are sold or removed
- View complete stock history with dates and times
- Filter history by product, type, or time period
- Add notes to stock movements for reference

### 📈 Inventory Reports
See important statistics
- Total number of products
- Total value of all inventory
- Number of items in low stock
- Total number of items in stock

### 👥 User Management (Admin Only)
Manage who can access the system
- Create new user accounts
- Set user roles and permissions
- Edit user information
- Remove users from the system

---

## Getting Started

### Requirements

**What You Need Installed First:**

Before you can run this application, make sure you have:

1. **.NET 10.0 SDK** (or later)
   - Download from: [dotnet.microsoft.com](https://dotnet.microsoft.com/download)
   - This is the runtime environment for the application

2. **PostgreSQL Database** (version 12 or later)
   - Download from: [postgresql.org](https://www.postgresql.org/download/)
   - This is where all your data will be stored

3. **A Code Editor** (choose one)
   - **Visual Studio Code** (free, lightweight) - Recommended for beginners
   - **Visual Studio Community** (free, feature-rich) - Recommended for developers
   - Any text editor if you're comfortable with the command line

4. **Git** (optional, for cloning the repository)
   - Download from: [git-scm.com](https://git-scm.com/)

**How to Check If You Have Them:**

Open your terminal/command prompt and run:
```bash
dotnet --version
psql --version
```

Both should show version numbers. If not, you need to install them.

### Installation Steps

**Step 1: Get the Code**

Open your terminal and run:
```bash
git clone https://github.com/yourusername/InventorySystem.git
cd InventorySystem
```

Or if you don't have Git, download the ZIP file from GitHub and extract it.

**Step 2: Restore Dependencies**

This downloads all the code packages the application needs:
```bash
dotnet restore
```

**Step 3: Create the Database**

Using PostgreSQL, create a new database:
```bash
createdb inventory_db
```

Or use pgAdmin (PostgreSQL's graphical tool) if you prefer clicking instead of typing.

### Database Setup

**Step 1: Configure Database Connection**

Open the file `appsettings.json` in your project and find this section:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Username=postgres;Password=your_password;Database=inventory_db"
  }
}
```

**Replace these values:**
- `your_password` → Your PostgreSQL password
- `inventory_db` → Name of your database (must match what you created)
- `localhost` → Your database server address (usually `localhost` for local development)
- `postgres` → Your PostgreSQL username (usually `postgres` by default)

**Example:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Username=postgres;Password=mySecurePassword123;Database=inventory_system"
  }
}
```

**Step 2: Apply Database Migrations**

This creates all the necessary tables in your database:
```bash
dotnet ef database update
```

You should see messages showing the tables being created. If you see "Done." at the end, you're good!

**What are migrations?**
Migrations are automatic scripts that set up your database structure. They ensure all users have the same database layout.

---

## Running the Application

**Starting the Application:**

```bash
dotnet run
```

**What you'll see:**
```
Now listening on: https://localhost:5044
Now listening on: http://localhost:5000
Press Ctrl+C to quit
```

This means the application is running! Your local server is ready to use.

**Access the Application:**

Open your web browser and go to:
```
https://localhost:5044
```

You should see the Swagger documentation page which shows all available features.

---

## Testing the Application

**Using Swagger (Recommended for Beginners)**

Once the application is running, go to:
```
https://localhost:5044/swagger/index.html
```

This page shows:
- All available endpoints (features)
- What data each endpoint needs
- What response you'll get back
- A button to try each feature directly in your browser

**To test an endpoint:**
1. Click on the endpoint you want to test
2. Click "Try it out"
3. Fill in the required information
4. Click "Execute"
5. See the response at the bottom

**First Test - Login:**
1. Find the "auth-login" endpoint
2. Click it to expand
3. Click "Try it out"
4. Enter a username and password
5. Click "Execute"
6. If you see a 400 error, that's normal - you probably don't have a user yet

---

## For Developers

**If you're developing or making changes to the code:**

### Building the Project
Compiles the code without running it:
```bash
dotnet build
```

### Running with Auto-Reload
The application restarts automatically when you change code:
```bash
dotnet watch run
```

Perfect for development - just save your file and see changes immediately.

### Running Tests (If Available)
```bash
dotnet test
```

### Checking for Issues
```bash
dotnet build --no-restore
```

### Creating a Migration
When you change the database structure:
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

---

## Configuration

The application has different settings for different situations:

### Configuration Files

**`appsettings.json`** (Default Settings)
- Used by default
- Contains general configuration

**`appsettings.Development.json`** (Development Settings)
- Used when you're developing and testing
- More detailed logging
- Easier debugging

**`appsettings.Production.json`** (Production Settings)
- Used when the app is live
- More secure settings
- Less detailed logging to improve performance

### Environment Variables

You can control which configuration file is used:

**For Development:**
```bash
set ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

**For Production:**
```bash
set ASPNETCORE_ENVIRONMENT=Production
dotnet run
```

---

## API Documentation

For detailed information about all available API endpoints, request/response formats, and examples, please refer to:

👉 **[API Documentation](./docs/api-doc.md)**

This comprehensive guide includes:
- Authentication endpoints
- Product management endpoints
- Category management endpoints
- Stock tracking endpoints
- Inventory summary endpoints
- User management endpoints
- Status codes and error handling
- Request/response examples
- Role-based access control information

---

## Deployment

### Preparing for Production

**Create a Release Build:**

This creates an optimized version ready for your live server:

```bash
dotnet publish -c Release -o ./publish
```

The files in the `publish` folder are what you upload to your server.

### Using Docker (Advanced)

If you want to run the application in a container:

**Step 1: Build the Docker Image**
```bash
docker build -t inventory-system .
```

**Step 2: Run the Container**
```bash
docker run \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -v ./appsettings.Production.json:/App/appsettings.Production.json \
  -p 5000:5000 \
  inventory-system
```

**What does this do?**
- Creates a container with the application
- Uses Production settings
- Exposes the application on port 5000
- Uses your production configuration file

### Deploying to a Server

**General Steps:**

1. Prepare a Release build
2. Upload files to your server
3. Configure database connection
4. Install .NET Runtime on the server
5. Start the application
6. Set up a reverse proxy (nginx, IIS, etc.)
7. Enable HTTPS

**Recommended Hosting Providers:**
- Azure (Microsoft)
- AWS
- DigitalOcean
- Heroku
- Any server with .NET Runtime installed

---

## Contributing

We welcome improvements! Here's how to help:

**Before Making Changes:**
1. Create a new branch with a descriptive name
2. Make your improvements
3. Test everything thoroughly
4. Make sure the code is clean and follows the project style

**Submitting Your Changes:**
1. Commit your changes with clear messages
2. Push to your branch
3. Open a Pull Request describing what you changed and why
4. Wait for review

**What Makes a Good Contribution:**
- Bug fixes with explanations
- New features that help users
- Documentation improvements
- Code quality improvements
- Tests for new features

---

## License

This project is licensed under the **MIT License**.

This means:
- ✓ You can use this code freely
- ✓ You can modify it
- ✓ You can distribute it
- ✓ You can use it in commercial projects

**The only requirement:** Include the original license file when you distribute it.

See the [LICENSE.md](LICENSE.md) file for full details.

---

## Need Help?

### Common Issues

**"Database connection failed"**
- Check your PostgreSQL is running
- Verify the connection string in `appsettings.json`
- Make sure the database exists

**"Port 5044 is already in use"**
- Change the port in `Properties/launchSettings.json`
- Or stop the application using that port

**"Database migrations failed"**
- Ensure PostgreSQL is running
- Delete the database and try `dotnet ef database update` again
- Check your connection string is correct

### Getting More Help

- Check the [API Documentation](Docs/api-doc.md) for endpoint details
- Review the [Project Structure](#project-structure) section
- Check application logs for detailed error messages