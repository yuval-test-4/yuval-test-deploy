using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yuvaltest.APIs;
using Yuvaltest.APIs.Common;
using Yuvaltest.APIs.Dtos;
using Yuvaltest.APIs.Errors;

namespace Yuvaltest.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one customer
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one customer
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many customers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one customer
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one customer
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple orders records to customer
    /// </summary>
    [HttpPost("{Id}/orders")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectOrders(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.ConnectOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple orders records from customer
    /// </summary>
    [HttpDelete("{Id}/orders")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectOrders(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.DisconnectOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple orders records for customer
    /// </summary>
    [HttpGet("{Id}/orders")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Order>>> FindOrders(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] OrderFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindOrders(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple orders records for customer
    /// </summary>
    [HttpPatch("{Id}/orders")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrders(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.UpdateOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
