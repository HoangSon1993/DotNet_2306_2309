using Eshop.Data;
using Eshop.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Utils
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EshopContext _context;


        // Lazy initalization
        private Lazy<IRepository<Account>> _accountRepository;
        private Lazy<IRepository<Cart>> _cartRepository;
        private Lazy<IRepository<Invoice>> _invoiceRepository;
        private Lazy<IRepository<InvoiceDetail>> _invoiceDetailRepository;
        private Lazy<IRepository<Product>> _productRepository;
        private Lazy<IRepository<ProductType>> _productTypeRepository;


        public UnitOfWork(EshopContext context)
        {
            _context = context;
            _accountRepository = new Lazy<IRepository<Account>>(() => new Repository<Account>(_context));
            _cartRepository = new Lazy<IRepository<Cart>>(() => new Repository<Cart>(_context));
            _invoiceRepository = new Lazy<IRepository<Invoice>>(() => new Repository<Invoice>(_context));
            _invoiceDetailRepository = new Lazy<IRepository<InvoiceDetail>>(() => new Repository<InvoiceDetail>(_context));
            _productRepository = new Lazy<IRepository<Product>>(() => new Repository<Product>(_context));
            _productTypeRepository = new Lazy<IRepository<ProductType>>(() => new Repository<ProductType>(_context));
        }
        
        


        public IRepository<Account> AccountRepository => _accountRepository.Value;
        public IRepository<Cart> CartRepository => _cartRepository.Value;
        public IRepository<Invoice> InvoiceRepository => _invoiceRepository.Value;
        public IRepository<InvoiceDetail> InvoiceDetailRepository => _invoiceDetailRepository.Value;
        public IRepository<Product> ProductRepository => _productRepository.Value;
        public IRepository<ProductType> ProductTypeRepository => _productTypeRepository.Value;
        public int SaveChange()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                // Xử lý lỗi xung đột khi nhều người dùng đồng thời cập nhật dữ liệu
                // Log the detailed error
                Console.WriteLine("Concurrency error occurred while saving changes to the database: " + ex.Message);

                // Provide a more user-friendly message or take other appropriate actions
                throw new InvalidOperationException(
                    "A concurrency error occurred while updating the database. Your changes may have been overwritten by another user.",
                    ex);
            }
            catch (DbUpdateException ex)
            {
                // Xảy ra lỗi trong quá trình cập nhật CSDL (vd: Vi phạm ràng buộc khoá)
                // Log the detailed error
                Console.WriteLine("Error occurred while saving changes to the database: " + ex.Message);

                // Log inner exception details if available
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }

                // Provide a more user-friendly message or take other appropriate actions
                throw new InvalidOperationException("An error occurred while updating the database. Please try again or contact support.", ex);
            }
            catch (Exception ex)
            {
                // Các lỗi k mong đợi khác
                // Log the unexpected error
                Console.WriteLine("Unexpected error occurred while saving changes to the database: " + ex.Message);

                // Provide a more user-friendly message or take other appropriate actions
                throw new InvalidOperationException("An unexpected error occurred while saving changes. Please try again or contact support.", ex);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
