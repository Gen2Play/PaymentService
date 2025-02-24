using PaymentService.Entities;
using Shared.DTOs;

namespace PaymentService.Services.Interfaces;

public interface IPaymentService
{
    Task<ResponseDto<IEnumerable<Transaction>>> GetAllTransactionsAsync();
    Task GetTransactionFromWebhook(TransactionAPIResponse transaction);
}