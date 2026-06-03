# Inventory System API Documentation

## Overview

The **Inventory System API** is a web service that helps you manage products, categories, stock levels, and users in a warehouse or inventory system. It allows you to track what items you have, add new products, manage stock movements (incoming and outgoing items), and generate summaries of your inventory.

### Key Features

- **Authentication**: Secure login and logout for users
- **Product Management**: Create, view, update, and delete products
- **Category Management**: Organize products into categories
- **Stock Management**: Track items coming in and going out of inventory
- **Stock History**: View all stock movement history
- **Summary Reports**: Get an overview of your inventory
- **User Management**: Manage system users and their permissions

---

## Table of Contents

1. [Base URL](#base-url)
2. [Authentication Overview](#authentication)
   - [What You Need to Know](#what-you-need-to-know)
   - [Login Information](#login)
3. [API Endpoints](#api-endpoints)
   - [1. Authentication](#1-authentication)
     - [1.1 Login](#11-login)
     - [1.2 Logout](#12-logout)
   - [2. Product Management](#2-product-management)
     - [2.1 Add a New Product](#21-add-a-new-product)
     - [2.2 Get a Specific Product](#22-get-a-specific-product)
     - [2.3 Get All Products](#23-get-all-products)
     - [2.4 Update a Product](#24-update-a-product)
     - [2.5 Delete a Product](#25-delete-a-product)
   - [3. Category Management](#3-category-management)
     - [3.1 Add a New Category](#31-add-a-new-category)
     - [3.2 Get a Specific Category](#32-get-a-specific-category)
     - [3.3 Get All Categories](#33-get-all-categories)
     - [3.4 Update a Category](#34-update-a-category)
     - [3.5 Delete a Category](#35-delete-a-category)
   - [4. Stock Management](#4-stock-management)
     - [4.1 Add Stock (Stock In)](#41-add-stock-stock-in)
     - [4.2 Remove Stock (Stock Out)](#42-remove-stock-stock-out)
     - [4.3 Get Stock History](#43-get-stock-history)
   - [5. Summary](#5-summary)
     - [5.1 Get Inventory Summary](#51-get-inventory-summary)
   - [6. User Management](#6-user-management)
     - [6.1 Get All Users](#61-get-all-users)
     - [6.2 Get a Specific User](#62-get-a-specific-user)
     - [6.3 Create a New User](#63-create-a-new-user)
     - [6.4 Update a User](#64-update-a-user)
     - [6.5 Delete a User](#65-delete-a-user)
4. [User Roles Explained](#user-roles-explained)
   - [Admin Only](#admin-only)
   - [Warehouse Operations](#warehouse-operations)
   - [Staff Only](#staff-only)
5. [Common Response Status Codes](#common-response-status-codes)
6. [Error Response Format](#error-response-format)
7. [Tips for Using the API](#tips-for-using-the-api)
8. [Quick Start Example](#quick-start-example)
9. [Support and Questions](#support-and-questions)

---

## Base URL

```
http://your-server-address/
```

## Authentication

### What You Need to Know

This API uses **role-based access control**, which means different users have different permissions based on their role:

- **Admin Only**: Full system access - create/delete users, categories, and products
- **Warehouse Operations**: Can view products and stock information
- **Staff Only**: Can add and remove stock from inventory

### Login

Before you can use the API, you need to log in with your username and password. The system will keep you logged in for **60 minutes**.

---

## API Endpoints

### 1. Authentication

#### 1.1 Login
Logs you into the system. After successful login, your session will be active for 60 minutes.

**Endpoint:**
```
POST /auth/login
```

**Required Access**: None (anyone can login)

**What to Send:**
```json
{
  "username": "your_username",
  "password": "your_password"
}
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Login successful",
  "data": {
    "id": 1,
    "username": "john_doe",
    "role": "AdminOnly"
  }
}
```

**Possible Errors:**
- **400**: Invalid username or password
- **500**: Server error

---

#### 1.2 Logout
Logs you out of the system and ends your session.

**Endpoint:**
```
POST /auth/logout
```

**Required Access**: Warehouse Operations level

**What to Send**: Nothing (your session information is sent automatically)

**What You Get Back (Success - 200):**
```json
{
  "message": "Logout successful"
}
```

**Possible Errors:**
- **500**: Server error

---

### 2. Product Management

#### 2.1 Add a New Product
Create a new product in the system.

**Endpoint:**
```
POST /products
```

**Required Access**: Admin Only

**What to Send:**
```json
{
  "name": "Laptop Dell XPS",
  "sku": "DELL-XPS-001",
  "price": 1200.50,
  "categoryId": 1
}
```

**Field Explanations:**
- **name**: The product name
- **sku**: Stock Keeping Unit - a unique identifier for the product
- **price**: The price of the product
- **categoryId**: Which category this product belongs to

**What You Get Back (Success - 201):**
```json
{
  "message": "Product created successfully",
  "data": {
    "id": 123,
    "name": "Laptop Dell XPS",
    "sku": "DELL-XPS-001",
    "stock": 0,
    "price": 1200.50
  }
}
```

**Possible Errors:**
- **400**: Missing required fields or invalid data
- **404**: Category not found
- **500**: Server error

---

#### 2.2 Get a Specific Product
Retrieve details of a single product by its ID.

**Endpoint:**
```
GET /products/{id}
```

**Required Access**: Warehouse Operations level

**Example:**
```
GET /products/123
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Product retrieved successfully",
  "data": {
    "id": 123,
    "name": "Laptop Dell XPS",
    "sku": "DELL-XPS-001",
    "stock": 5,
    "price": 1200.50,
    "category": {
      "id": 1,
      "name": "Electronics"
    }
  }
}
```

**Possible Errors:**
- **404**: Product not found
- **500**: Server error

---

#### 2.3 Get All Products
Retrieve a list of all products with optional filtering and pagination.

**Endpoint:**
```
GET /products
```

**Required Access**: Warehouse Operations level

**Optional Query Parameters:**
- **page**: Page number (default: 1)
- **size**: Number of products per page (default: 10)
- **search**: Search by product name
- **categoryId**: Filter by category ID

**Examples:**
```
GET /products?page=1&size=10
GET /products?search=Laptop
GET /products?categoryId=1
GET /products?page=2&size=20&search=Dell&categoryId=1
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Products retrieved successfully",
  "data": {
    "items": [
      {
        "id": 123,
        "name": "Laptop Dell XPS",
        "sku": "DELL-XPS-001",
        "stock": 5,
        "price": 1200.50
      },
      {
        "id": 124,
        "name": "Monitor Dell 24\"",
        "sku": "DELL-MON-024",
        "stock": 10,
        "price": 350.00
      }
    ],
    "page": 1,
    "size": 10,
    "totalItems": 2,
    "totalPages": 1
  }
}
```

**Possible Errors:**
- **400**: Invalid page number or size
- **500**: Server error

---

#### 2.4 Update a Product
Modify details of an existing product.

**Endpoint:**
```
PUT /products/{id}
```

**Required Access**: Admin Only

**Example:**
```
PUT /products/123
```

**What to Send:**
```json
{
  "name": "Laptop Dell XPS 15\"",
  "sku": "DELL-XPS-001",
  "price": 1300.00,
  "categoryId": 1
}
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Product updated successfully",
  "data": {
    "id": 123,
    "name": "Laptop Dell XPS 15\"",
    "sku": "DELL-XPS-001",
    "stock": 5,
    "price": 1300.00
  }
}
```

**Possible Errors:**
- **400**: Invalid data
- **404**: Product not found
- **500**: Server error

---

#### 2.5 Delete a Product
Remove a product from the system.

**Endpoint:**
```
DELETE /products/{id}
```

**Required Access**: Admin Only

**Example:**
```
DELETE /products/123
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Product deleted successfully"
}
```

**Possible Errors:**
- **404**: Product not found
- **500**: Server error

---

### 3. Category Management

#### 3.1 Add a New Category
Create a new product category.

**Endpoint:**
```
POST /categories
```

**Required Access**: Admin Only

**What to Send:**
```json
{
  "name": "Electronics"
}
```

**What You Get Back (Success - 201):**
```json
{
  "message": "Category created successfully",
  "data": {
    "id": 1,
    "name": "Electronics"
  }
}
```

**Possible Errors:**
- **400**: Missing category name or invalid data
- **500**: Server error

---

#### 3.2 Get a Specific Category
Retrieve details of a single category.

**Endpoint:**
```
GET /categories/{id}
```

**Required Access**: Warehouse Operations level

**Example:**
```
GET /categories/1
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Category retrieved successfully",
  "data": {
    "id": 1,
    "name": "Electronics"
  }
}
```

**Possible Errors:**
- **404**: Category not found
- **500**: Server error

---

#### 3.3 Get All Categories
Retrieve a list of all product categories.

**Endpoint:**
```
GET /categories
```

**Required Access**: Warehouse Operations level

**What You Get Back (Success - 200):**
```json
{
  "message": "Categories retrieved successfully",
  "data": [
    {
      "id": 1,
      "name": "Electronics"
    },
    {
      "id": 2,
      "name": "Furniture"
    },
    {
      "id": 3,
      "name": "Office Supplies"
    }
  ]
}
```

**Possible Errors:**
- **500**: Server error

---

#### 3.4 Update a Category
Change the name of an existing category.

**Endpoint:**
```
PUT /categories/{id}
```

**Required Access**: Admin Only

**Example:**
```
PUT /categories/1
```

**What to Send:**
```json
{
  "name": "Computer Equipment"
}
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Category updated successfully"
}
```

**Possible Errors:**
- **400**: Invalid data
- **404**: Category not found
- **500**: Server error

---

#### 3.5 Delete a Category
Remove a category from the system.

**Endpoint:**
```
DELETE /categories/{id}
```

**Required Access**: Admin Only

**Example:**
```
DELETE /categories/1
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Category deleted successfully"
}
```

**Possible Errors:**
- **404**: Category not found
- **500**: Server error

---

### 4. Stock Management

Stock management allows you to record when items are added to or removed from your inventory.

#### 4.1 Add Stock (Stock In)
Record when new items arrive and are added to inventory.

**Endpoint:**
```
POST /stocks/in
```

**Required Access**: Staff Only

**What to Send:**
```json
{
  "productId": 123,
  "quantity": 10,
  "notes": "Received from supplier ABC"
}
```

**Field Explanations:**
- **productId**: The product being added to stock
- **quantity**: How many items to add
- **notes**: Optional notes about this stock movement

**What You Get Back (Success - 200):**
```json
{
  "message": "Stock in successful"
}
```

**Possible Errors:**
- **400**: Invalid quantity or missing fields
- **404**: Product not found
- **500**: Server error

---

#### 4.2 Remove Stock (Stock Out)
Record when items are removed from inventory (sold, damaged, etc.).

**Endpoint:**
```
POST /stocks/out
```

**Required Access**: Staff Only

**What to Send:**
```json
{
  "productId": 123,
  "quantity": 2,
  "notes": "Sold to customer XYZ"
}
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Stock out successful"
}
```

**Possible Errors:**
- **400**: Not enough stock available or invalid quantity
- **404**: Product not found
- **500**: Server error

---

#### 4.3 Get Stock History
View all stock movements (additions and removals) with optional filtering.

**Endpoint:**
```
GET /stocks/history
```

**Required Access**: Warehouse Operations level

**Optional Query Parameters:**
- **page**: Page number (default: 1)
- **size**: Number of records per page (default: 10)
- **period**: Time period to show (default: Current) - options: Current, Daily, Weekly, Monthly
- **productId**: Filter by specific product
- **type**: Filter by type - options: In (stock added), Out (stock removed)

**Examples:**
```
GET /stocks/history?page=1&size=10
GET /stocks/history?productId=123
GET /stocks/history?type=In&period=Weekly
GET /stocks/history?page=2&size=20&productId=123&type=Out
```

**What You Get Back (Success - 200):**
```json
{
  "message": "Stock history retrieved successfully",
  "data": {
    "items": [
      {
        "id": 1,
        "product": {
          "id": 123,
          "name": "Laptop Dell XPS"
        },
        "quantity": 10,
        "type": "In",
        "notes": "Received from supplier",
        "createdAt": "2026-06-03T10:30:00Z"
      },
      {
        "id": 2,
        "product": {
          "id": 123,
          "name": "Laptop Dell XPS"
        },
        "quantity": 2,
        "type": "Out",
        "notes": "Sold to customer",
        "createdAt": "2026-06-03T14:15:00Z"
      }
    ],
    "page": 1,
    "size": 10,
    "totalItems": 2,
    "totalPages": 1
  }
}
```

**Possible Errors:**
- **400**: Invalid parameters
- **500**: Server error

---

### 5. Summary

#### 5.1 Get Inventory Summary
Get an overview of your entire inventory including total products, total stock value, and other key metrics.

**Endpoint:**
```
GET /summary
```

**Required Access**: Admin Only

**What You Get Back (Success - 200):**
```json
{
  "message": "Summary retrieved successfully",
  "data": {
    "totalProducts": 25,
    "totalStockValue": 50000.00,
    "lowStockItems": 3,
    "totalCategories": 5,
    "totalInStock": 250
  }
}
```

**Possible Errors:**
- **500**: Server error

---

### 6. User Management

#### 6.1 Get All Users
Retrieve a list of all system users.

**Endpoint:**
```
GET /users
```

**Required Access**: Admin Only

**What You Get Back (Success - 200):**
```json
{
  "message": "Users retrieved successfully",
  "data": [
    {
      "id": 1,
      "username": "john_admin",
      "role": "AdminOnly"
    },
    {
      "id": 2,
      "username": "jane_warehouse",
      "role": "WarehouseOperations"
    },
    {
      "id": 3,
      "username": "bob_staff",
      "role": "StaffOnly"
    }
  ]
}
```

**Possible Errors:**
- **500**: Server error

---

#### 6.2 Get a Specific User
Retrieve details of a single user.

**Endpoint:**
```
GET /users/{id}
```

**Required Access**: Admin Only

**Example:**
```
GET /users/1
```

**What You Get Back (Success - 200):**
```json
{
  "message": "User retrieved successfully",
  "data": {
    "id": 1,
    "username": "john_admin",
    "role": "AdminOnly"
  }
}
```

**Possible Errors:**
- **404**: User not found
- **500**: Server error

---

#### 6.3 Create a New User
Add a new user to the system.

**Endpoint:**
```
POST /users
```

**Required Access**: Admin Only

**What to Send:**
```json
{
  "username": "new_user",
  "password": "secure_password",
  "role": "StaffOnly"
}
```

**Field Explanations:**
- **username**: The login username
- **password**: The user's password (should be strong and secure)
- **role**: The user's role (AdminOnly, WarehouseOperations, or StaffOnly)

**What You Get Back (Success - 201):**
```json
{
  "message": "User created successfully",
  "data": {
    "id": 4,
    "username": "new_user",
    "role": "StaffOnly"
  }
}
```

**Possible Errors:**
- **400**: Invalid data or username already exists
- **500**: Server error

---

#### 6.4 Update a User
Modify a user's information (username, password, or role).

**Endpoint:**
```
PUT /users/{id}
```

**Required Access**: Admin Only

**Example:**
```
PUT /users/1
```

**What to Send:**
```json
{
  "username": "john_admin_updated",
  "password": "new_secure_password",
  "role": "AdminOnly"
}
```

**What You Get Back (Success - 200):**
```json
{
  "message": "User updated successfully"
}
```

**Possible Errors:**
- **400**: Invalid data
- **404**: User not found
- **500**: Server error

---

#### 6.5 Delete a User
Remove a user from the system.

**Endpoint:**
```
DELETE /users/{id}
```

**Required Access**: Admin Only

**Example:**
```
DELETE /users/1
```

**What You Get Back (Success - 200):**
```json
{
  "message": "User deleted successfully"
}
```

**Possible Errors:**
- **404**: User not found
- **500**: Server error

---

## User Roles Explained

### Admin Only
- Can create, update, and delete products
- Can create, update, and delete categories
- Can create, update, and delete users
- Can view inventory summary
- Can view all products and stock history

### Warehouse Operations
- Can view products and categories
- Can view stock history
- Can view all products
- Cannot modify products or categories
- Cannot perform stock in/out operations

### Staff Only
- Can perform stock in operations (add items to inventory)
- Can perform stock out operations (remove items from inventory)
- Cannot view products list directly
- Cannot create or modify products

---

## Common Response Status Codes

| Code | Meaning |
|------|---------|
| 200 | Success - The request was successful |
| 201 | Created - A new resource was successfully created |
| 400 | Bad Request - The request was invalid or incomplete |
| 401 | Unauthorized - The request requires authentication or the credentials are invalid |
| 403 | Forbidden - You don't have permission to access this resource |
| 404 | Not Found - The requested resource doesn't exist |
| 500 | Server Error - Something went wrong on the server |

---

## Error Response Format

When an error occurs, you'll receive a response like this:

```json
{
  "message": "Error description",
  "data": null,
  "statusCode": 400
}
```

---

## Tips for Using the API

1. **Always Login First**: Make sure to login before making any other requests
2. **Check User Role**: Make sure your role has permission to access the endpoint
3. **Session Timeout**: Your session expires after 60 minutes of inactivity
4. **Pagination**: When getting lists, use pagination to avoid loading too much data at once
5. **Search Filters**: Use search and filter parameters to find what you need quickly

---

## Quick Start Example

Here's a simple sequence of API calls to get started:

```
1. POST /auth/login
   - Login with your credentials

2. GET /categories
   - View all product categories

3. GET /products?categoryId=1
   - View products in a specific category

4. GET /stocks/history?period=Weekly
   - Check recent stock movements

5. POST /auth/logout
   - Logout when done
```

---

## Support and Questions

If you have questions about the API or encounter issues, please contact your system administrator or development team.
