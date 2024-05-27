using Eshop.Data;
using Eshop.Models;
using Eshop.Utils;

namespace Eshop.Services
{
    public class AccountService
    {
        private readonly EshopContext _context;

        public AccountService(EshopContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            UnitOfWork unitOfWork = new UnitOfWork(_context);
            var accounts = unitOfWork.AccountRepository.GetAll().ToList();

            return accounts;
        }
    }
}
