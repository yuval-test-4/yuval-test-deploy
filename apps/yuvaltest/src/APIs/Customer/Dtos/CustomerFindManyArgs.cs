using Microsoft.AspNetCore.Mvc;
using Yuvaltest.APIs.Common;
using Yuvaltest.Infrastructure.Models;

namespace Yuvaltest.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
