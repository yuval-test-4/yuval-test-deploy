using Microsoft.AspNetCore.Mvc;
using YuvalTestDeploy.APIs.Common;
using YuvalTestDeploy.Infrastructure.Models;

namespace YuvalTestDeploy.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
