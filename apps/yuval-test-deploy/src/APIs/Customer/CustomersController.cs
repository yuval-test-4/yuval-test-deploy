using Microsoft.AspNetCore.Mvc;

namespace YuvalTestDeploy.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
