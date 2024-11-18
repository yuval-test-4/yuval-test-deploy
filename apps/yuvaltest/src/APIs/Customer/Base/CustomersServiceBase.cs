using Microsoft.EntityFrameworkCore;
using Yuvaltest.APIs;
using Yuvaltest.APIs.Common;
using Yuvaltest.APIs.Dtos;
using Yuvaltest.APIs.Errors;
using Yuvaltest.APIs.Extensions;
using Yuvaltest.Infrastructure;
using Yuvaltest.Infrastructure.Models;

namespace Yuvaltest.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly YuvaltestDbContext _context;

    public CustomersServiceBase(YuvaltestDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one customer
    /// </summary>
    public async Task<Customer> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new CustomerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Orders != null)
        {
            customer.Orders = await _context
                .Orders.Where(order => createDto.Orders.Select(t => t.Id).Contains(order.Id))
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CustomerDbModel>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one customer
    /// </summary>
    public async Task DeleteCustomer(CustomerWhereUniqueInput uniqueId)
    {
        var customer = await _context.Customers.FindAsync(uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many customers
    /// </summary>
    public async Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.Orders)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Meta data about customer records
    /// </summary>
    public async Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs)
    {
        var count = await _context.Customers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one customer
    /// </summary>
    public async Task<Customer> Customer(CustomerWhereUniqueInput uniqueId)
    {
        var customers = await this.Customers(
            new CustomerFindManyArgs { Where = new CustomerWhereInput { Id = uniqueId.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one customer
    /// </summary>
    public async Task UpdateCustomer(
        CustomerWhereUniqueInput uniqueId,
        CustomerUpdateInput updateDto
    )
    {
        var customer = updateDto.ToModel(uniqueId);

        if (updateDto.Orders != null)
        {
            customer.Orders = await _context
                .Orders.Where(order => updateDto.Orders.Select(t => t).Contains(order.Id))
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple orders records to customer
    /// </summary>
    public async Task ConnectOrders(
        CustomerWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Orders);

        foreach (var child in childrenToConnect)
        {
            parent.Orders.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple orders records from customer
    /// </summary>
    public async Task DisconnectOrders(
        CustomerWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Orders?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple orders records for customer
    /// </summary>
    public async Task<List<Order>> FindOrders(
        CustomerWhereUniqueInput uniqueId,
        OrderFindManyArgs customerFindManyArgs
    )
    {
        var orders = await _context
            .Orders.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return orders.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple orders records for customer
    /// </summary>
    public async Task UpdateOrders(
        CustomerWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Orders = children;
        await _context.SaveChangesAsync();
    }
}
