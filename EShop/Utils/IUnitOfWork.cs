using Eshop.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eshop.Utils
{
    public interface IUnitOfWork
    {
        IRepository<Account> AccountRepository { get; }
        IRepository<Cart> CartRepository { get; }
        IRepository<Invoice> InvoiceRepository { get; }
        IRepository<InvoiceDetail> InvoiceDetailRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<ProductType> ProductTypeRepository { get; }

        int SaveChange();
        void Dispose();

    }
}
