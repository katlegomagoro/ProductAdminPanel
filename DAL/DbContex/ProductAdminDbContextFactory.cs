﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProductAdminPanel.DAL.DbContex;
using System.IO;

namespace ProductAdminPanel.DAL.DbContext
{
    public class ProductAdminDbContextFactory : IDesignTimeDbContextFactory<ProductAdminDbContext>
    {
        public ProductAdminDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ProductAdminDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new ProductAdminDbContext(optionsBuilder.Options);
        }
    }
}
