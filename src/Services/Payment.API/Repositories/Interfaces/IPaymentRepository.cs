using Contracts.Common.Interfaces;
using Payment.API.Entities;
using Payment.API.Persistence;

namespace Payment.API.Repositories.Interfaces;

public interface IPaymentRepository : IRepositoryBaseAsync<Transaction, int, PaymentContext>
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
}