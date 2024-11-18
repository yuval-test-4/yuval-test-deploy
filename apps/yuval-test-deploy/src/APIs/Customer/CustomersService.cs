using YuvalTestDeploy.Infrastructure;

namespace YuvalTestDeploy.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(YuvalTestDeployDbContext context)
        : base(context) { }
}
