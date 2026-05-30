# InventorySystem

A simple, modern web application for managing your business inventory. Track products, stock levels, and transactions all in one place.

## What is This?

InventorySystem helps businesses keep track of their products and inventory. Whether you're managing a small shop or a growing warehouse, this application lets you:
- Store and organize all your products
- Track how much stock you have
- Record when items come in or go out
- See reports and statistics about your inventory

## What Can You Do?

- 📦 **Add and Manage Products** - Create product listings and organize them by category
- 🏷️ **Organize by Categories** - Group similar products together for easy management
- 📊 **Track Stock** - Keep records of items added or removed from inventory
- 📈 **View Reports** - See statistics about your inventory at a glance
- 🔍 **Search Products** - Easily find products by name or filter by category
- 📝 **Keep History** - View a complete record of all stock movements
- 🔐 **Validate Data** - Ensure data is correct before saving

## Technology Stack

**What's Under the Hood?** (You don't need to know this to use the app, but if you're curious...)

- **.NET 10.0** - The framework used to build this application
- **PostgreSQL** - The secure database where all your data is stored
- **Entity Framework Core** - The system that manages communication between the app and database
- **Swagger** - The tool that provides interactive documentation for the API
- **OpenTelemetry** - Monitors the application's performance and tracks how requests move through the system
- **Serilog** - Records detailed logs of everything that happens, helpful for finding problems
- **FluentValidation** - Checks that all your data is correct and complete before saving it

## File Organization

Here's how the project is organized (don't worry if this seems confusing - you won't need to touch most of these):

```
Routers/           - Handles incoming requests from users
Routers/CategoryRouter.cs - Manages category requests
Routers/ProductRouter.cs - Manages product requests
Routers/StockRouter.cs - Manages stock requests

Services/          - The brain of the app (where the work happens)
Services/CategoryService.cs - Handles category operations
Services/ProductService.cs - Handles product operations
Services/StockService.cs - Handles stock operations

Models/            - The blueprint for your data
Models/Category.cs - What a category looks like
Models/Product.cs - What a product looks like
Models/Stock.cs - What a stock transaction looks like

DTOs/              - How data is formatted when sent/received
Validations/       - Rules to check if data is correct
Helpers/           - Useful tools and utilities
Migrations/        - Record of database changes over time
```

## Using the Application

The application provides different sections for different tasks:

### Managing Categories
Create and organize product categories (e.g., Electronics, Clothing, Food)
- **Add** a new category
- **View** all your categories
- **Edit** a category
- **Delete** a category

### Managing Products
Add your products to the system
- **Add** a new product with name, description, price, and category
- **View** all products or search by name
- **View details** of a specific product
- **Edit** product information
- **Delete** a product

### Tracking Stock
Keep track of inventory changes
- **Add stock** when new items arrive
- **Remove stock** when items are sold
- **View history** of all stock movements
- **Check current levels** of each product

### Viewing Reports
See important statistics
- **Total products** in your system
- **Total stock value** of all inventory
- **Stock levels** by product

## Getting Started

### What You Need First

Before you can run this application, make sure you have:
1. **.NET 10.0** - Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)
2. **PostgreSQL** - A database program. Download from [postgresql.org](https://www.postgresql.org/download/)
3. **A code editor** - Visual Studio Code (free) or Visual Studio 2022

### Step 1: Get the Code

Open your terminal and run:
```bash
git clone https://github.com/nurmanhadi/InventorySystem.git
cd InventorySystem
```

### Step 2: Install Requirements

Let .NET download everything it needs:
```bash
dotnet restore
```

### Step 3: Connect to Your Database

Open the file named `appsettings.json` and update it with your PostgreSQL details:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Username=postgres;Password=your_password;Database=inventory_db"
  }
}
```

**Replace:**
- `your_password` with your PostgreSQL password
- `inventory_db` with the name you want for your database

### Step 4: Set Up the Database

Run this command to create all the necessary tables:
```bash
dotnet ef database update
```

### Step 5: Run the Application

Start the application:
```bash
dotnet run
```

**You should see:** "Now listening on: https://localhost:5044"

Open your web browser and go to: `https://localhost:5044/swagger`

Here you can test all the features!

## Testing the Application

Once the application is running, you can test it at:
```
https://localhost:5044/swagger/index.html
```

This page shows all available features and lets you try them out right there in your browser.

## For Developers

### Building the Project
```bash
dotnet build
```

### Starting the App with Auto-Reload
Useful during development - the app restarts automatically when you change code:
```bash
dotnet watch run
```

## Configuration

The application has different settings for different environments:

- `appsettings.json` - Default settings (general)
- `appsettings.Development.json` - Settings when developing (testing)
- `appsettings.Production.json` - Settings for the live version

## Understanding Responses

When you interact with the application, it always gives you a response with:
- **Message** - A description of what happened
- **Data** - The information you requested
- **Status Code** - A number indicating success or error

### Status Codes Explained

- `200` ✓ Success - Everything worked
- `201` ✓ Created - New item was successfully added
- `400` ✗ Bad Request - Something in your request was wrong
- `404` ✗ Not Found - The item you're looking for doesn't exist
- `500` ✗ Server Error - Something went wrong on the server

## Sharing Your Application

### Using Docker (Advanced)

If you want to run the application in a container:

```bash
docker build -t inventory-system .

docker run \
-e ASPNETCORE_ENVIRONMENT=Production \
-v ./appsettings.Production.json:/App/appsettings.Production.json \
-p 5000:5000 inventory-system
```

### Preparing for Production

To prepare a final version for deployment:

```bash
dotnet publish -c Release -o ./publish
```

The files in the `publish` folder are ready to upload to your server.

## Want to Help Improve This?

If you find bugs or have ideas for improvements:

1. Create a new branch with your changes
2. Make sure everything works
3. Submit a pull request describing your improvements

## License

This project is licensed under the MIT License - See the LICENSE file for details.
