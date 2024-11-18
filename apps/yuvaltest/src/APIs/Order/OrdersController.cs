using Microsoft.AspNetCore.Mvc;

namespace Yuvaltest.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
