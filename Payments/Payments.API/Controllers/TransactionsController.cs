using Microsoft.AspNetCore.Mvc;
using Payments.Application.DTOs.Transaction;
using Payments.Application.Interfaces;

namespace Payments.API.Controllers;

/// <summary>
/// Controller for managing transactions
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransactionsController(ITransactionService transactionService) : ControllerBase
{
    private readonly ITransactionService _transactionService = transactionService;

    /// <summary>
    /// Get all transactions
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAllTransactions()
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();
        return Ok(transactions);
    }

    /// <summary>
    /// Get transaction by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionDto>> GetTransactionById(Guid id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);
        
        if (transaction == null)
        {
            return NotFound(new { message = $"Transaction with ID {id} not found" });
        }

        return Ok(transaction);
    }

    /// <summary>
    /// Get transactions by payment ID
    /// </summary>
    [HttpGet("payment/{paymentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactionsByPaymentId(Guid paymentId)
    {
        var transactions = await _transactionService.GetTransactionsByPaymentIdAsync(paymentId);
        return Ok(transactions);
    }

    /// <summary>
    /// Create a new transaction
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TransactionDto>> CreateTransaction([FromBody] CreateTransactionDto createTransactionDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var transaction = await _transactionService.CreateTransactionAsync(createTransactionDto);
        return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
    }
}
