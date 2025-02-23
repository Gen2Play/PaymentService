using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Payment.API.Entities;
using Payment.API.Persistence;
using Payment.API.Repositories.Interfaces;

namespace Payment.API.Repositories;

public class PaymentRepository : RepositoryBase<Transaction, int, PaymentContext>, IPaymentRepository
{
    public PaymentRepository(PaymentContext context, IUnitOfWork<PaymentContext> unitOfWork) : base(context, unitOfWork)
    {
        
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync() => await FindAll().ToListAsync();
}