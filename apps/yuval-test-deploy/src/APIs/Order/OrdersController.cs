using Microsoft.AspNetCore.Mvc;

namespace YuvalTestDeploy.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
