using Eshop.Models;
using EShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data
{
    public class EshopContext:DbContext
    {
        public EshopContext(DbContextOptions<EshopContext> options):base(options)
        {
           
        }

        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
      
    }
}
