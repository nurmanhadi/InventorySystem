# InventorySystem

A modern .NET REST API for inventory management, built with .NET 10, PostgreSQL, and OpenTelemetry observability.

## Overview

InventorySystem is a RESTful API backend for managing products, stock levels, categories, and inventory transactions. It provides real-time stock tracking, transaction history, and inventory summaries for small to medium-sized businesses.

## Key Features

- 📦 **Product Management** - Create, update, and manage products with categories
- 🏷️ **Category Management** - Organize products by categories
- 📊 **Stock Tracking** - Track stock in/out transactions with full history
- 📈 **Dashboard Summaries** - Real-time inventory statistics and insights
- 🔍 **Search & Filter** - Advanced search and filtering capabilities
- 📝 **Transaction Logging** - Complete audit trail of all stock movements
- 📡 **Distributed Tracing** - OpenTelemetry integration for observability
- 🔐 **Input Validation** - FluentValidation for robust data validation

## Technology Stack

- **.NET 10.0** - Latest .NET runtime
- **C# 13** - Modern language features
- **PostgreSQL** - Reliable relational database
- **Entity Framework Core 10** - Object-relational mapping
- **OpenTelemetry** - Distributed tracing and metrics
- **Serilog** - Structured logging
- **FluentValidation** - Data annotation validation
- **Swagger/Swashbuckle** - Interactive API documentation

## Project Structure

```
Routers/           - API endpoint definitions (routes & handlers)
  ├── CategoryRouter.cs
  ├── ProductRouter.cs
  ├── StockRouter.cs
  └── SummaryRouter.cs

Services/          - Business logic layer
  ├── CategoryService.cs
  ├── ProductService.cs
  ├── StockService.cs
  └── SummaryService.cs

Models/            - Domain entities & DbContext
  ├── Category.cs
  ├── Product.cs
  ├── Stock.cs
  └── DbInitiate.cs

DTOs/              - Data transfer objects for API contracts
  ├── CategoryDto.cs
  ├── ProductDto.cs
  ├── StockDto.cs
  └── SummaryDto.cs

Validations/       - Request validation rules
  ├── CategoryValidation.cs
  ├── ProductValidation.cs
  └── StockValidation.cs

Helpers/           - Utility classes & shared logic
  ├── WebResponse.cs (standard response wrapper)
  ├── StockType.cs (stock transaction types)
  └── HistoryStockPeriod.cs (time period enums)

Middlewares/       - Request/response interceptors
  └── GlobalException.cs (centralized error handling)

Migrations/        - Database schema versioning
```

## API Endpoints

### Category Management (`/categories`)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/` | Create a new category |
| GET | `/{id}` | Get category by ID |
| GET | `/` | List all categories |
| PUT | `/{id}` | Update category |
| DELETE | `/{id}` | Delete category |

### Product Management (`/products`)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/` | Create a new product |
| GET | `/{id}` | Get product by ID with category |
| GET | `/` | List products (with pagination, search, category filter) |
| PUT | `/{id}` | Update product |
| DELETE | `/{id}` | Delete product |

**Query Parameters for GET `/products`:**
- `page` (int, default=1) - Page number
- `size` (int, default=10) - Items per page
- `search` (string, optional) - Product name search
- `categoryId` (long, optional) - Filter by category

### Stock Management (`/stocks`)
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/in` | Record incoming stock |
| POST | `/out` | Record outgoing stock |
| GET | `/history` | Get stock transaction history |
| GET | `/{id}` | Get current stock level |

**Query Parameters for GET `/stocks/history`:**
- `page` (int, default=1) - Page number
- `size` (int, default=10) - Items per page
- `period` (HistoryStockPeriod, default=CURRENT) - Time period filter
- `productId` (long, optional) - Filter by product
- `type` (StockType, optional) - Filter by transaction type (IN/OUT)

### Summary & Analytics (`/summary`)
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/` | Get dashboard summary statistics |

## Installation & Setup

### Prerequisites
- .NET 10.0 SDK
- PostgreSQL 12+
- Visual Studio Code or Visual Studio 2022+

### 1. Clone the Repository
```bash
git clone <repository-url>
cd InventorySystem
```

### 2. Install Dependencies
```bash
dotnet restore
```

### 3. Configure Database Connection
Edit `appsettings.json` with your PostgreSQL connection string:
```json
{
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=myhost;Username=myname;Password=mypass;Database=mydb"
  },
  "Cors": {
    "AllowedOrigins": [
      "*"
    ]
  },
  "Serilog": {
    "Using": [
      "Serilog.Sinks.Console"
    ],
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "Console"
      }
    ]
  },
  "Otel": {
    "ServiceName": "InventorySystemAPI",
    "Exporter": {
      "Otlp": {
        "Endpoint": "example.com"
      }
    }
  }
}
```

### 4. Run Database Migrations
```bash
dotnet ef database update
```

To create a new migration:
```bash
dotnet ef migrations add MigrationName
```

### 5. Run the Application
```bash
dotnet run
```

The API will be available at `https://localhost:5044` (or configured URL).

## API Documentation

Swagger/OpenAPI documentation is available at:
```
https://localhost:5044/swagger/index.html
```

## Development

### Build
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

### Watch Mode (auto-restart on changes)
```bash
dotnet watch run
```

## Configuration Files

- `appsettings.json` - Default configuration
- `appsettings.Development.json` - Development overrides
- `appsettings.Production.json` - Production overrides

## Observability

### Distributed Tracing
OpenTelemetry tracing is configured to export traces to an OTLP collector. Traces include:
- HTTP request/response tracking
- Database queries via PostgreSQL instrumentation
- Custom application traces

### Structured Logging
Serilog is configured for structured logging with:
- Console output in development
- JSON formatting for production
- Automatic request/response logging

## Error Handling

The API uses a standardized `WebResponse<T>` wrapper for all responses:

```json
{
    "data": { /* response data */ },
    "message": "Operation successful"
}
```

Errors are caught by the global exception middleware and returned with appropriate HTTP status codes and error messages.

## Response Format

All API responses follow this structure:
```csharp
public class WebResponse<T>
{
    public string Message { get; set; }
    public T Data { get; set; }
    public int StatusCode { get; set; }
}
```

## Common HTTP Status Codes

- `200` - Success
- `201` - Created (POST requests)
- `400` - Bad Request (validation error)
- `404` - Not Found
- `500` - Internal Server Error

## Deployment

### Docker Deployment
```bash
docker build -t inventory-system .
docker run -p 5000:5000 inventory-system
```

### Published Release Build
```bash
dotnet publish -c Release -o ./publish
```

Deploy the contents of the `./publish` folder to your hosting environment.

## Contributing

1. Create a feature branch from `main`
2. Make your changes with clear commit messages
3. Ensure all tests pass
4. Submit a pull request with description

## License

MIT License - See LICENSE file for details
