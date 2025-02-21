using API.Models;
using Application.Entities;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessTransaction([FromBody] TransactionRequest request)
        {
            Response<Transaction> response = await _transactionsService.ProcessTransactionAsync(request);

            if (response.Code == Application.Enums.eCodes.Secsses)
            {
                TransactionDTO transactionDTO = new TransactionDTO(response.Data);
                return Ok(transactionDTO);
            }
            return BadRequest(new { Code = response.Code });
        }

        [HttpGet("history/{userId}")]
        public IActionResult GetHistory(string userId)
        {
            var transactions = _transactionsService
                .GetHistoryAsync(userId);

            IAsyncEnumerable<TransactionDTO> transactionDTOs = ConvertTransaction(transactions);

            return Ok(transactionDTOs);
        }

        private async IAsyncEnumerable<TransactionDTO> ConvertTransaction(IAsyncEnumerable<Transaction> transactions)
        {
            await foreach (var transaction in transactions)
            {
                yield return new TransactionDTO(transaction);
            }
        }
    }
}
