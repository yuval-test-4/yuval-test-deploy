using Microsoft.AspNetCore.Mvc;

namespace Yuvaltest.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
