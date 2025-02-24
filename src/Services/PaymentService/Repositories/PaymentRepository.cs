using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using PaymentService.Entities;
using PaymentService.Persistence;
using PaymentService.Repositories.Interfaces;

namespace PaymentService.Repositories;

public class PaymentRepository : RepositoryBase<Transaction, int, PaymentContext>, IPaymentRepository
{
    public PaymentRepository(PaymentContext context, IUnitOfWork<PaymentContext> unitOfWork) : base(context, unitOfWork)
    {
        
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync() => await FindAll().ToListAsync();
}