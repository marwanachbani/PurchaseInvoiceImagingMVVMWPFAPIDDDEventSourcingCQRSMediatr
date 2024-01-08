using Data.SQLServer.Events;
using Data.SQLServer.Invoices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=(local);Database=PurchasesIMagine;Trusted_Connection=true ;MultipleActiveResultSets=True ;TrustServerCertificate=True;Pooling=True;Max Pool Size=100;", sqlServerOptionsAction: sqlOptions =>
            {
                
                
            });
            
            
        }

      
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceContent> InvoiceContents { get; set; }
        public DbSet<InvoiceCalculation> InvoiceCalculations { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Event> Events { get; set; }
    }
    

    

}
