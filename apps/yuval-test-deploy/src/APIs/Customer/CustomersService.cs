using Yuvaltest.Infrastructure;

namespace Yuvaltest.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(YuvaltestDbContext context)
        : base(context) { }
}
