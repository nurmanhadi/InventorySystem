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

| Component | Technology | Version | Purpose |
|-----------|-----------|---------|---------|
| **Backend Framework** | .NET / ASP.NET Core | 10.0 | Web framework using Minimal APIs pattern |
| **Database** | PostgreSQL | 12+ | Reliable relational database |
| **ORM (Data Access)** | Entity Framework Core | 10.0.7 | Database abstraction and migrations |
| **Database Driver** | Npgsql | 10.0.1 | PostgreSQL provider for EF Core |
| **Password Security** | BCrypt.Net-Next | 4.2.0 | Secure password hashing and verification |
| **Validation** | FluentValidation | 12.1.1 | Fluent API for validation rules |
| **API Documentation** | Swagger / Swashbuckle | 10.1.7 | Interactive API documentation |
| **Logging** | Serilog | 10.0.0 | Structured logging framework |
| **Monitoring** | OpenTelemetry | 1.15+ | Performance monitoring and tracing |
| **CORS** | Built-in ASP.NET Core | Native | Cross-origin request handling |
| **Authentication** | Cookie-based | Native | Built-in ASP.NET Core authentication |
| **Authorization** | Claims-based | Native | Role-based access control |

**Why these technologies?**
- .NET 10.0 is fast, secure, and modern with native support for async operations
- Minimal APIs provide a lightweight alternative to traditional MVC
- PostgreSQL is reliable, scalable, and perfect for structured data
- Serilog provides structured logging with multiple sink options
- OpenTelemetry gives complete observability into application performance
- FluentValidation offers a fluent, readable API for validation rules
- BCrypt ensures passwords are securely hashed with salt

---

## Project Structure

Here's how the code is organized:

```
src/InventorySystem/
├── Features/                - Feature modules organized by domain
│   ├── Auth/                - Authentication (Login/Logout)
│   ├── Categories/          - Category management
│   ├── Products/            - Product CRUD operations
│   ├── Stocks/              - Stock In/Out tracking
│   ├── Reports/             - Inventory reports and summaries
│   └── Users/               - User management (Admin only)
├── Infrastructure/
│   ├── Configs/             - Service configuration (Auth, Database, CORS, Swagger, etc.)
│   ├── Databases/           - Entity Framework DbContext
│   └── Middlewares/         - HTTP middleware (Global exception handling)
├── Shared/
│   ├── Exceptions/          - Custom exception classes
│   ├── Helpers/             - Utility functions and constants
│   ├── Responses/           - API response wrapper classes
│   └── Validations/         - Fluent validation rules
├── Migrations/              - Entity Framework database migrations (auto-generated)
├── Properties/              - Application properties and launch settings
├── Program.cs               - Application entry point and startup configuration
├── InventorySystem.csproj   - Project file with dependencies
└── appsettings.*.json       - Environment-specific configuration files
```

**Architecture Details:**

This project uses **Minimal APIs** pattern with **Feature-Driven Design**:

- **Features** organize code by business domain, each containing:
  - `{Feature}Router.cs` - HTTP endpoint definitions and request routing
  - `{Feature}Service.cs` - Business logic and data operations
  - `{Feature}Dto.cs` - Data transfer objects for API contracts
  - Entity models - Database entity definitions

- **Infrastructure** centralizes cross-cutting concerns:
  - **Configs** bootstrap all services (authentication, database, CORS, monitoring, etc.)
  - **Databases** manages Entity Framework DbContext and data access
  - **Middlewares** handles global concerns like exception handling

- **Shared** contains reusable components:
  - **Exceptions** - Custom error types for consistent error handling
  - **Helpers** - Utility functions and constants
  - **Responses** - Standard API response wrapper
  - **Validations** - FluentValidation rules for data integrity

---

## What Can You Do

### 🔐 Authentication & Authorization
Secure access with role-based permissions
- **Login**: Authenticate using username and password
- **Logout**: Securely end your session
- **Role-Based Access Control**: Different permissions for different user roles
  - **Admin**: Full access to all features
  - **Warehouse Operations**: Read-only access to inventory data
  - **Staff**: Limited access to stock operations only
- **Password Security**: All passwords are securely hashed using BCrypt

### 🏷️ Category Management
Organize your products into groups
- Create new categories (e.g., Electronics, Clothing, Food)
- View all your categories
- Edit category names and details
- Delete categories with soft-delete tracking
- Filter and search categories

### 📦 Product Management
Add and manage your products
- Create new products with name, price, and category
- Assign unique SKU (Stock Keeping Unit) to each product
- View all products with pagination
- Search products by name or keywords
- Filter products by category
- Edit product details (price, category, description)
- Delete products (soft-delete preserves history)
- Track product creation and modification dates

### 📊 Stock Management
Track items moving in and out
- **Stock In**: Record when new items arrive (purchases, transfers)
- **Stock Out**: Record when items leave (sales, adjustments)
- View complete stock history with timestamps
- Track the reason and quantity for each transaction
- Calculate real-time inventory levels
- Identify stock movements by product or time period
- Add notes to stock operations

### 📈 Inventory Reports & Summary
See important statistics
- Total number of products in inventory
- Total inventory value across all products
- Current stock levels by product
- Stock movement history and trends
- Low stock alerts and warnings
- Overall inventory health metrics

### 👥 User Management (Admin Only)
Manage who can access the system
- Create new user accounts with credentials
- Assign user roles (Admin, Warehouse, Staff)
- Edit user information and roles
- Deactivate or remove users
- View user activity history
- Manage role-based permissions

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

The project includes comprehensive test coverage with both **Unit Tests** and **Integration Tests**.

**Test Structure:**

- **Unit Tests** (`tests/InventorySystem.Tests/Unit/`): Test individual components in isolation
  - DTO validation tests
  - Business logic tests
  - Helper function tests

- **Integration Tests** (`tests/InventorySystem.Tests/Integration/`): Test complete API scenarios
  - Category API workflow tests
  - Product API workflow tests
  - Stock tracking scenario tests
  - End-to-end user flows

### Running Tests via Command Line

**Run All Tests:**
```bash
dotnet test
```

**Run Only Unit Tests:**
```bash
dotnet test --filter Category=Unit
```

**Run Only Integration Tests:**
```bash
dotnet test --filter Category=Integration
```

**Run Tests with Coverage:**
```bash
dotnet test /p:CollectCoverage=true
```

**Run a Specific Test Class:**
```bash
dotnet test --filter "FullyQualifiedName~CategoryDtoTests"
```

### Using Swagger (Recommended for Manual Testing)

Once the application is running, go to:
```
https://localhost:5044/swagger/index.html
```

This page shows:
- All available API endpoints
- Required parameters for each endpoint
- Expected request/response formats
- Interactive testing interface ("Try it out" button)

**To test an endpoint:**
1. Click the endpoint you want to test
2. Click "Try it out"
3. Fill in the required parameters
4. Click "Execute"
5. Review the response and status code

**First Test - Authentication:**
1. Find the auth-login endpoint under "/auth"
2. Click it to expand
3. Click "Try it out"
4. Enter test credentials (or use admin account if created)
5. Click "Execute"
6. You'll receive a session token to use for subsequent requests

### Test Guide

For comprehensive testing information, see:
👉 **[Comprehensive Test Guide](./tests/InventorySystem.Tests/COMPREHENSIVE_TEST_GUIDE.md)**

This includes:
- Test architecture and organization
- How to write new tests
- Test data fixtures and scenarios
- Integration test patterns
- Best practices for testing APIs

---

## For Developers

**If you're developing or making changes to the code:**

### Project Architecture

This project uses:
- **Minimal APIs**: Lightweight API endpoints without controllers
- **Feature-Driven Structure**: Code organized by business features
- **Dependency Injection**: All services registered in `Program.cs` configuration
- **FluentValidation**: Type-safe, fluent validation rules
- **Entity Framework Core**: Database-first ORM with migrations
- **Structured Logging**: Serilog with detailed request/response logging
- **OpenTelemetry**: Distributed tracing and performance monitoring

### Building the Project
Compiles the code without running it:
```bash
dotnet build
```

### Running the Application
Standard execution with current configuration:
```bash
dotnet run
```

### Running with Auto-Reload
The application restarts automatically when you change code:
```bash
dotnet watch run
```

Perfect for development - just save your file and see changes immediately.

### Running Tests
Execute the complete test suite (unit + integration):
```bash
dotnet test
```

Run specific test categories:
```bash
dotnet test --filter Category=Unit
dotnet test --filter Category=Integration
```

### Adding a Database Migration
When you modify entity models, create a migration:
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Checking for Build Issues
```bash
dotnet build --no-restore
```

### Code Structure Guidelines

**Adding a New Feature:**

1. Create a new folder under `Features/` (e.g., `Features/Orders/`)
2. Add the feature files:
   - `{Feature}Router.cs` - Route definitions (map endpoints)
   - `{Feature}Service.cs` - Business logic (data operations)
   - `{Feature}Dto.cs` - Request/response models
   - Entity model (if needed) - Add to `Infrastructure/Databases/`
3. Register the service in `Infrastructure/Configs/ServiceConfig.cs`
4. Map routes in `Program.cs`
5. Add validation rules in `Shared/Validations/`

**Configuration Pattern:**

All service configuration follows this pattern in `Infrastructure/Configs/`:
```csharp
public static class YourFeatureConfig
{
    public static WebApplicationBuilder AddYourFeature(this WebApplicationBuilder builder)
    {
        // Register services
        return builder;
    }
}
```

**API Response Pattern:**

All API responses use the standardized `WebResponse<T>` wrapper:
```csharp
public class WebResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
}
```

### Debugging

**View Application Logs:**

Serilog logs are output to the console with the format:
```
HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed}ms
```

**Enable Trace Logging:**

In `appsettings.Development.json`:
```json
{
  "Serilog": {
    "MinimumLevel": "Debug"
  }
}
```

**Test an Endpoint:**

Use the Swagger UI at: `https://localhost:5044/swagger/index.html`

### Common Development Tasks

**Reset Database:**
```bash
dotnet ef database drop --force
dotnet ef database update
```

**Check Database Connection:**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your_connection_string"
```

**List Available Endpoints:**

Start the app and visit Swagger: `https://localhost:5044/swagger/index.html`

---

## Configuration

The application supports environment-specific configurations for different scenarios (Development, Production, etc.).

### Configuration Files

**`appsettings.json`** (Base Configuration)
- Default settings applied to all environments
- General application configuration
- Connection string templates

**`appsettings.Development.json`** (Development Settings)
- Used during local development
- Detailed logging and debug output
- Swagger UI enabled
- Relaxed CORS policies for easier testing
- In-memory or local development database

**`appsettings.Production.json`** (Production Settings)
- Used in production deployment
- Minimal logging for performance
- Swagger UI disabled
- Strict CORS policies
- Production database configuration

### Setting Environment Variables

**For Development:**
```bash
# Windows PowerShell
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run

# Windows CMD
set ASPNETCORE_ENVIRONMENT=Development
dotnet run

# Linux/Mac
export ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

**For Production:**
```bash
# Windows PowerShell
$env:ASPNETCORE_ENVIRONMENT = "Production"
dotnet run

# Windows CMD
set ASPNETCORE_ENVIRONMENT=Production
dotnet run

# Linux/Mac
export ASPNETCORE_ENVIRONMENT=Production
dotnet run
```

### Configuring Services

**Database Configuration** (`DatabaseConfig.cs`)
- PostgreSQL connection string
- Entity Framework options
- Connection pooling settings
- Automatic migration on startup

**Authentication Configuration** (`AuthenticationConfig.cs`)
- Cookie authentication settings
- Session timeout
- Secure cookie options

**Authorization Configuration** (`AuthorizationConfig.cs`)
- Role-based policies (Admin, Warehouse, Staff)
- Authorization requirements per endpoint

**CORS Configuration** (`CorsConfig.cs`)
- Allowed origins for cross-origin requests
- Allowed methods and headers
- Development vs. production policies

**Swagger Configuration** (`SwaggerConfig.cs`)
- API documentation generation
- Security definitions for authentication
- Enabled only in Development environment

**OpenTelemetry Configuration** (`OpentelemetryConfig.cs`)
- Distributed tracing setup
- Performance monitoring
- Export configuration

### Connection String Configuration

Edit `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Username=postgres;Password=your_password;Database=inventory_db;Port=5432"
  }
}
```

**Parameters:**
- `Host` - PostgreSQL server address (localhost for local development)
- `Port` - PostgreSQL port (default 5432)
- `Username` - PostgreSQL username (default postgres)
- `Password` - PostgreSQL password
- `Database` - Database name

### Using User Secrets (Recommended)

Store sensitive data like passwords without committing to version control:

```bash
# Set a secret
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Username=postgres;Password=your_secure_password;Database=inventory_db"

# List all secrets
dotnet user-secrets list

# Clear all secrets
dotnet user-secrets clear
```

Secrets are stored in:
- **Windows**: `%APPDATA%\Microsoft\UserSecrets\`
- **Linux/Mac**: `~/.microsoft/usersecrets/`

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

This section covers preparing your application for production deployment.

### Prerequisites for Deployment

Ensure your server has:
- .NET 10.0 Runtime installed
- PostgreSQL database server
- HTTPS/SSL certificate
- Reverse proxy (nginx, IIS, or similar)

### Preparing a Release Build

Create an optimized, production-ready build:

```bash
dotnet publish -c Release -o ./publish
```

This creates a `publish` folder containing:
- Application binaries (.dll files)
- Runtime configuration
- Dependencies
- Everything needed to run the application

**Result:** The files in `./publish` are what you deploy to your server.

### Publishing to a Server

**1. On Your Development Machine:**
```bash
dotnet publish -c Release -o ./publish
```

**2. Transfer to Server:**
```bash
# Via SCP (Linux/Mac)
scp -r ./publish user@your-server:/var/www/inventory-app

# Via SFTP (Windows/GUI)
# Use your SFTP client to upload the publish folder
```

**3. On Your Server:**
```bash
# Set permissions
chmod +x /var/www/inventory-app/InventorySystem

# Start the application
cd /var/www/inventory-app
./InventorySystem
# Or on Windows: InventorySystem.exe
```

### Using Docker (Recommended for Containerized Deployment)

**Build Docker Image:**
```bash
docker build -t inventory-system:1.0 .
```

**Run Container:**
```bash
docker run \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ConnectionStrings__DefaultConnection="Host=db;Username=postgres;Password=your_password;Database=inventory_db" \
  -p 5000:8080 \
  --name inventory-app \
  inventory-system:1.0
```

**Using Docker Compose (Recommended):**

Create `docker-compose.yml`:
```yaml
version: '3.8'

services:
  app:
    build: .
    environment:
      ASPNETCORE_ENVIRONMENT: Production
      ConnectionStrings__DefaultConnection: "Host=db;Username=postgres;Password=your_password;Database=inventory_db"
    ports:
      - "5000:8080"
    depends_on:
      - db

  db:
    image: postgres:16
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: your_password
      POSTGRES_DB: inventory_db
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:
```

**Start Services:**
```bash
docker-compose up -d
```

### Setting Up Reverse Proxy

**nginx Configuration Example:**

Create `/etc/nginx/sites-available/inventory-app`:

```nginx
server {
    listen 443 ssl http2;
    server_name your-domain.com;

    ssl_certificate /path/to/cert.pem;
    ssl_certificate_key /path/to/key.pem;

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }
}

server {
    listen 80;
    server_name your-domain.com;
    return 301 https://$server_name$request_uri;
}
```

**Enable Site:**
```bash
sudo ln -s /etc/nginx/sites-available/inventory-app /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl restart nginx
```

### Database Deployment

**1. Create Database on Production Server:**
```bash
createdb -U postgres inventory_db
```

**2. Apply Migrations:**
```bash
cd /var/www/inventory-app
dotnet ef database update --configuration Release
```

**3. Seed Initial Data (Optional):**
Create an admin user and initial categories if needed.

### Health Checks

Monitor your application:

```bash
# Check if running
curl https://your-domain.com/health

# View logs
docker logs inventory-app
```

### Monitoring & Logging

**View OpenTelemetry Metrics:**
- Metrics are exported based on your `appsettings.Production.json` configuration
- Configure exporters in `OpentelemetryConfig.cs`

**Application Logs:**
- Structured logs via Serilog
- Check system logs or configured log sinks
- Example: `journalctl -u inventory-app -f` (systemd)

### Recommended Hosting Providers

- **Azure App Service** - Microsoft's managed .NET hosting
- **AWS Elastic Beanstalk** - AWS managed application platform
- **DigitalOcean App Platform** - Simple container deployment
- **Heroku** - Easy deployment (though may require buildpack)
- **Self-hosted VPS** - Full control (AWS EC2, DigitalOcean, Linode, etc.)

### SSL/TLS Certificate

Use Let's Encrypt for free SSL certificates:

```bash
# Using Certbot
sudo certbot certonly --standalone -d your-domain.com
```

Certificates are typically located at:
- `/etc/letsencrypt/live/your-domain.com/fullchain.pem`
- `/etc/letsencrypt/live/your-domain.com/privkey.pem`

### Environment-Specific Configuration for Production

Update `appsettings.Production.json`:

```json
{
  "Serilog": {
    "MinimumLevel": "Warning"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=prod-db-server;Username=app_user;Password=secure_password;Database=inventory_prod"
  },
  "Cors": {
    "AllowedOrigins": ["https://your-domain.com"]
  }
}
```

### Backup & Recovery

**Database Backups:**
```bash
# Backup PostgreSQL
pg_dump -U postgres inventory_db > backup.sql

# Restore from backup
psql -U postgres inventory_db < backup.sql
```

**Application Files:**
- Keep copies of published binaries
- Maintain version history for rollback capability
- Use version control for configuration changes

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

### Common Issues and Solutions

**"ConnectionRefusedException" when running the application**

**Symptoms:** Application starts but fails to connect to database

**Solutions:**
1. Verify PostgreSQL is running:
   ```bash
   # Windows
   Get-Service postgresql*
   
   # Linux
   sudo systemctl status postgresql
   ```

2. Check your connection string in `appsettings.json`:
   - Verify Host, Port, Username, Password, and Database name
   - Ensure database exists: `createdb inventory_db`
   
3. Test the connection manually:
   ```bash
   psql -U postgres -h localhost -d inventory_db
   ```

---

**"Port 5044 is already in use"**

**Symptoms:** Application fails to start on the configured port

**Solutions:**
1. Find what's using the port:
   ```bash
   # Windows
   netstat -ano | findstr :5044
   
   # Linux/Mac
   lsof -i :5044
   ```

2. Change the port in `Properties/launchSettings.json`:
   ```json
   "http": {
     "commandName": "Project",
     "launchBrowser": true,
     "applicationUrl": "http://localhost:5045",
     "environmentVariables": {
       "ASPNETCORE_ENVIRONMENT": "Development"
     }
   }
   ```

3. Or kill the process using that port and restart

---

**"Migrations failed" or "Database update failed"**

**Symptoms:** Error during `dotnet ef database update`

**Solutions:**
1. Verify database exists:
   ```bash
   createdb inventory_db
   ```

2. Check connection string is correct

3. Reset the database (development only):
   ```bash
   dotnet ef database drop --force
   dotnet ef database update
   ```

4. View migration errors in detail:
   ```bash
   dotnet ef database update -v
   ```

---

**"Build fails with NuGet errors"**

**Symptoms:** Package restore or compilation fails

**Solutions:**
1. Clear NuGet cache:
   ```bash
   dotnet nuget locals all --clear
   ```

2. Restore packages:
   ```bash
   dotnet restore
   ```

3. Clean and rebuild:
   ```bash
   dotnet clean
   dotnet build
   ```

---

**"Swagger shows 'Unable to fetch definition'"**

**Symptoms:** Swagger UI displays error when loading API documentation

**Solutions:**
1. Verify you're in Development environment:
   ```bash
   # Should show "Development"
   $env:ASPNETCORE_ENVIRONMENT
   ```

2. Restart the application with Swagger disabled/enabled:
   ```bash
   dotnet clean
   dotnet build
   dotnet run
   ```

3. Clear browser cache and refresh

---

**"Authentication failures" or "401 Unauthorized"**

**Symptoms:** Cannot login or authorized requests return 401

**Solutions:**
1. Verify user exists in database:
   ```sql
   SELECT * FROM public."User";
   ```

2. Check password is correct (passwords are hashed with BCrypt)

3. Verify authentication is enabled in `Program.cs`

4. Check cookies are enabled in your browser

---

**"CORS errors when making requests from frontend"**

**Symptoms:** Browser shows CORS error when calling API from different origin

**Solutions:**
1. Check CORS policy in `appsettings.json`:
   ```json
   "Cors": {
     "AllowedOrigins": ["http://localhost:3000"],
     "AllowedMethods": ["GET", "POST", "PUT", "DELETE"],
     "AllowedHeaders": ["*"]
   }
   ```

2. Add your frontend URL to AllowedOrigins

3. Verify CORS middleware is added in `Program.cs`

4. Use `app.UseCors("CorsPolicy")` before routing

---

**Performance is slow**

**Symptoms:** Application responses are taking too long

**Solutions:**
1. Check database connection is not the bottleneck:
   ```bash
   # Monitor database queries
   dotnet build -c Release
   ```

2. Enable OpenTelemetry monitoring to identify slowness

3. Check application logs for errors:
   ```bash
   # Set minimum log level to Debug
   # In appsettings.Development.json:
   "Serilog": {
     "MinimumLevel": "Debug"
   }
   ```

4. Review long-running queries in production:
   - Check the `OpentelemetryConfig.cs` for export configuration
   - Review traces in your telemetry backend

---

### Getting More Help

1. **Check the API Documentation:**
   👉 [API Documentation](./docs/api-doc.md) - Complete endpoint reference

2. **Review Test Examples:**
   👉 [Comprehensive Test Guide](./tests/InventorySystem.Tests/COMPREHENSIVE_TEST_GUIDE.md) - See test patterns and examples

3. **Check Application Logs:**
   - Look for detailed error messages and stack traces
   - Serilog logs to console in Development mode

4. **Swagger Interactive Documentation:**
   - Start the app: `dotnet run`
   - Visit: `https://localhost:5044/swagger/index.html`
   - Test endpoints directly in your browser

5. **Search Project Issues:**
   - Check existing GitHub issues for similar problems
   - Include full error message and steps to reproduce

6. **Enable Debug Logging:**
   ```bash
   # Set environment to Development
   $env:ASPNETCORE_ENVIRONMENT = "Development"
   
   # Run with verbose output
   dotnet run -v
   ```