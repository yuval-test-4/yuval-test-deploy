using Yuvaltest.Infrastructure;

namespace Yuvaltest.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(YuvaltestDbContext context)
        : base(context) { }
}
