using Contracts.Common.Interfaces;
using PaymentService.Entities;
using PaymentService.Persistence;

namespace PaymentService.Repositories.Interfaces;

public interface IPaymentRepository : IRepositoryBaseAsync<Transaction, int, PaymentContext>
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
}