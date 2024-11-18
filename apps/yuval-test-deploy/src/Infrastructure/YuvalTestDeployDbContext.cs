using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YuvalTestDeploy.Infrastructure.Models;

namespace YuvalTestDeploy.Infrastructure;

public class YuvalTestDeployDbContext : IdentityDbContext<IdentityUser>
{
    public YuvalTestDeployDbContext(DbContextOptions<YuvalTestDeployDbContext> options)
        : base(options) { }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }
}
