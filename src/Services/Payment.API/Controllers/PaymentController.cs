using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Services.Interfaces;
using Shared.DTOs;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTransactionsAsync()
        {
            return Ok(await _paymentService.GetAllTransactionsAsync());
        }
        
        [HttpPost("webhook")]
        public async Task<IActionResult> GetTransactionWebhook(
            [FromBody] TransactionAPIResponse data,
            CancellationToken cancellationToken)
        {
            if (Request.Headers["Secure-Token"] != "phamquangvinh")
                throw new UnauthorizedAccessException();
            
            if (data == null)
            {
                return BadRequest("Invalid payload");
            }

            try
            {
                await _paymentService.GetTransactionFromWebhook(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred processing the webhook");
            }
        }
    }
}
