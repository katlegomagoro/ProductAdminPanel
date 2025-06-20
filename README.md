# ProductAdminPanel

A modular, maintainable Blazor Server admin panel built with Clean Architecture and Entity Framework Core 9. It provides full CRUD functionality for managing Products, Categories, and Suppliers in a structured and scalable way.

## Overview

This system allows administrators to manage product catalogs, supplier data, and product categorization. It is structured using a clean separation of concerns and includes support for UI filtering, enum-driven statuses, nested relationships, and extensibility for future tags and image handling.

## Tech Stack

- .NET 9 (Blazor Server)
- Entity Framework Core 9 (SQL Server)
- C# 13
- Visual Studio 2022
- MudBlazor (UI components)
- Clean Architecture (Presentation, Application, Domain, Infrastructure, DAL layers)

## How to Set Up the Project

1. Clone the repository
2. Ensure `.NET 9 SDK` is installed
3. Open the solution in Visual Studio 2022 or run from terminal:

```bash
dotnet restore
dotnet ef database update
dotnet run --project ProductAdminPanel
```

Ensure your `appsettings.json` contains the following connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ProductAdminDb;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=true"
}
```

---

## Static Data Seeding (Initial Setup)
This project includes a manual SQL script to populate foundational static data for demonstration or testing purposes. This includes pre-configured Suppliers and Categories needed for the application UI to load correctly.

### Instructions:
Ensure the database exists and is updated via EF Core:
```bash
dotnet ef database update
```
Execute the seed script using SQL Server Management Studio (SSMS) or `sqlcmd`:
Ensure the database exists and is updated via EF Core:
```bash
sqlcmd -S localhost -d ProductAdminDb -i Scripts\\SeedStaticData.sql
```

### Alternatively, run the script manually:

```bash
-- SeedStaticData.sql

-- Suppliers
INSERT INTO Suppliers (Id, Name, ContactEmail, ContactNumber, Website, CreatedAt)
VALUES 
('52F5B112-CE1C-40BC-A382-F290572BD710', 'Acme Corp', 'info@acme.com', '0123456789', 'https://acme.com', GETUTCDATE()),
('DCBC2B63-725C-4FB2-9E2C-C391F2A19190', 'BookBazaar', 'contact@bookbazaar.com', '0987654321', 'https://bookbazaar.com', GETUTCDATE()),
('EE596C1A-E261-488B-B981-5973F8210BA7', 'GlobalTech', 'support@globaltech.com', '0112233445', 'https://globaltech.com', GETUTCDATE());

-- Categories
INSERT INTO Categories (Id, Name, Description, CreatedAt)
VALUES 
('4844865B-C2E7-4BD0-B7F5-3375DA75E898', 'Electronics', 'Electronic items', GETUTCDATE()),
('34AC7062-DE85-4981-BA14-02DF9089C36F', 'Home Appliances', 'Appliances for home use', GETUTCDATE()),
('66886479-C010-493E-BD9F-C6DBBF3A1AA7', 'Books', 'Books and stationery', GETUTCDATE());

-- Products
INSERT INTO Products (Id, Name, SKU, Description, Price, CostPrice, StockQuantity, Status, LaunchDate, EndDate, SupplierId, CreatedAt)
VALUES 
(NEWID(), 'Smartphone A10', 'A10-SKU', 'Affordable smartphone with modern features.', 299.99, 200.00, 100, 1, '2024-01-01', '2026-01-01', 'EE596C1A-E261-488B-B981-5973F8210BA7', GETUTCDATE()),
(NEWID(), 'Electric Kettle X1', 'KETTLE-X1', '1.5L electric kettle with auto shut-off.', 45.50, 30.00, 50, 1, '2023-11-15', '2025-11-15', '52F5B112-CE1C-40BC-A382-F290572BD710', GETUTCDATE()),
(NEWID(), 'Modern Literature Set', 'BOOK-ML01', 'Collection of contemporary literature books.', 89.99, 60.00, 25, 1, '2023-08-01', '2024-12-31', 'DCBC2B63-725C-4FB2-9E2C-C391F2A19190', GETUTCDATE());


```

## Pseudocode-Based Architecture Breakdown

### DAL/Models/Product.cs

- Name, SKU, Description, Price, CostPrice, StockQuantity, Status, LaunchDate, EndDate
- FK: Supplier
- Future: Tags, Categories, Images

### DAL/Models/Category.cs

- Self-referencing ParentCategoryId
- Nested categories support

### DAL/Models/Supplier.cs

- Contact info fields
- Products navigation property

### DAL/Configurations

- Proper constraints, relationships, and enum conversions

### DAL/Enums/ProductStatus.cs

- `Draft`, `Active`, `Discontinued`

### DbContext

- `DbSet<Product>`, `DbSet<Category>`, `DbSet<Supplier>`

---

## Services Layer

### Interfaces

- `IProductService`: CRUD for products
- `ICategoryService`: CRUD for categories
- `ISupplierService`: CRUD for suppliers

### Implementations

- Use EF Core with eager loading where necessary
- Validate and save via `SaveChangesAsync`

---

## UI Components (Blazor)

### ProductList.razor

- Table with filters (SKU, Name, Supplier, Status)
- Edit/Delete buttons

### ProductForm.razor

- Form for adding/editing
- Dropdowns for Supplier and Status

### CategoryList.razor & CategoryForm.razor

- View and manage nested categories

### SupplierList.razor & SupplierForm.razor

- Standard supplier details with validations

---

## Pages & Routing

### Products.razor

- Entry point for product list

### MainLayout.razor & NavMenu.razor

- Navigation drawer and app bar setup

### App.razor

- Fallback routing and layout

### Program.cs

- Registers services and DbContext
- Configures MudBlazor and app pipeline

---

## Summary

This admin panel is intended as a solid foundation for more advanced business dashboards. With a clean architecture backbone, extensibility is prioritised making this suitable for teams aiming to grow a modular system with features like file/image uploads, multi-tenancy, or advanced analytics.

---

For feedback or collaboration inquiries, reach out via:

**Name**: Katlego Magoro  
**Email**: katlegomagoro98@gmail.com  
**LinkedIn**: [linkedin.com/in/katlego-magoro-288b08236](https://www.linkedin.com/in/katlego-magoro-288b08236)