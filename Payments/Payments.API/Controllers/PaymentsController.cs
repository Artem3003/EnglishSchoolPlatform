using Microsoft.AspNetCore.Mvc;
using Payments.Application.DTOs.Payment;
using Payments.Application.Interfaces;

namespace Payments.API.Controllers;

/// <summary>
/// Controller for managing payments
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PaymentsController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    /// <summary>
    /// Get all payments
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
    {
        var payments = await _paymentService.GetAllPaymentsAsync();
        return Ok(payments);
    }

    /// <summary>
    /// Get payment by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentDto>> GetPaymentById(Guid id)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(id);
        
        if (payment == null)
        {
            return NotFound(new { message = $"Payment with ID {id} not found" });
        }

        return Ok(payment);
    }

    /// <summary>
    /// Get payments by user ID
    /// </summary>
    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByUserId(Guid userId)
    {
        var payments = await _paymentService.GetPaymentsByUserIdAsync(userId);
        return Ok(payments);
    }

    /// <summary>
    /// Get payments by course ID
    /// </summary>
    [HttpGet("course/{courseId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByCourseId(Guid courseId)
    {
        var payments = await _paymentService.GetPaymentsByCourseIdAsync(courseId);
        return Ok(payments);
    }

    /// <summary>
    /// Create a new payment
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentDto>> CreatePayment([FromBody] CreatePaymentDto createPaymentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var payment = await _paymentService.CreatePaymentAsync(createPaymentDto);
        return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
    }

    /// <summary>
    /// Process a pending payment
    /// </summary>
    [HttpPost("{id:guid}/process")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentDto>> ProcessPayment(Guid id, [FromBody] ProcessPaymentDto processPaymentDto)
    {
        processPaymentDto.PaymentId = id;

        try
        {
            var payment = await _paymentService.ProcessPaymentAsync(processPaymentDto);
            return Ok(payment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update payment status
    /// </summary>
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentDto>> UpdatePaymentStatus(Guid id, [FromBody] UpdatePaymentStatusDto updateStatusDto)
    {
        updateStatusDto.PaymentId = id;

        try
        {
            var payment = await _paymentService.UpdatePaymentStatusAsync(updateStatusDto);
            return Ok(payment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Refund a payment
    /// </summary>
    [HttpPost("{id:guid}/refund")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentDto>> RefundPayment(Guid id, [FromBody] RefundPaymentDto refundDto)
    {
        refundDto.PaymentId = id;

        try
        {
            var payment = await _paymentService.RefundPaymentAsync(refundDto);
            return Ok(payment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a payment
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        var result = await _paymentService.DeletePaymentAsync(id);
        
        if (!result)
        {
            return NotFound(new { message = $"Payment with ID {id} not found" });
        }

        return NoContent();
    }
}
