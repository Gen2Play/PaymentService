using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.API.Entities;
using Payment.API.Repositories.Interfaces;
using Payment.API.Services.Interfaces;
using Shared.DTOs;
using ILogger = Serilog.ILogger;

namespace Payment.API.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;
    private readonly ILogger<PaymentService> _logger;
    
    public PaymentService(ILogger<PaymentService> logger, IPaymentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    
    public async Task GetTransactionFromWebhook(TransactionAPIResponse tranaction)
    {
        if (tranaction.data.Count == 0)
        {
            _logger.LogInformation("No new transactions.");
            return;
        }

        var transactions = await _repository.GetAllTransactionsAsync();
        try
        {
            foreach (var tran in tranaction.data)
            {
                var existingTransaction = await _repository.FindByCondition(x => x.Id.Equals(tran.Id)).SingleOrDefaultAsync();
                if (existingTransaction != null)
                    _logger.LogInformation($"Found transaction with id: {tran.Id}.");
                
                var add_transaction = new Transaction
                {
                    Id = tran.Id,
                    Tid = tran.Tid,
                    Description = tran.Description,
                    Amount = tran.Amount,
                    CusumBalance = tran.cusum_balance,
                    When = tran.When,
                    BankSubAccId = tran.bank_sub_acc_id,
                    SubAccId = tran.SubAccId,
                    BankName = tran.BankName,
                    BankAbbreviation = tran.BankAbbreviation,
                    VirtualAccount = tran.VirtualAccount,
                    VirtualAccountName = tran.VirtualAccountName,
                    CorresponsiveName = tran.CorresponsiveName,
                    CorresponsiveAccount = tran.CorresponsiveAccount,
                    CorresponsiveBankId = tran.CorresponsiveBankId,
                    CorresponsiveBankName = tran.CorresponsiveBankName,
                };

                if (!transactions.Any(t => t.Tid == add_transaction.Tid))
                { 
                    await _repository.CreateAsync(add_transaction);
                    await _repository.SaveChangesAsync();
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError("There was an error getting the transactions from the webhook.", e.Message);
            throw;
        }
    }
    
    public async Task<ResponseDto<IEnumerable<Transaction>>> GetAllTransactionsAsync()
    {
        return new ResponseDto<IEnumerable<Transaction>>
            (await _repository.GetAllTransactionsAsync(), "Get all transactions successfully!",
                200);
    }
}