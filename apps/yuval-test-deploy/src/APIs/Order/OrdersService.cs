using YuvalTestDeploy.Infrastructure;

namespace YuvalTestDeploy.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(YuvalTestDeployDbContext context)
        : base(context) { }
}
