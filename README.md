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