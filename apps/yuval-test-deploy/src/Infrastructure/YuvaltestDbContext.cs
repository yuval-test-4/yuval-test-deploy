using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yuvaltest.Infrastructure.Models;

namespace Yuvaltest.Infrastructure;

public class YuvaltestDbContext : IdentityDbContext<IdentityUser>
{
    public YuvaltestDbContext(DbContextOptions<YuvaltestDbContext> options)
        : base(options) { }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }
}
