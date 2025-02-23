using Payment.API.Entities;
using Shared.DTOs;

namespace Payment.API.Services.Interfaces;

public interface IPaymentService
{
    Task<ResponseDto<IEnumerable<Transaction>>> GetAllTransactionsAsync();
    Task GetTransactionFromWebhook(TransactionAPIResponse transaction);
}