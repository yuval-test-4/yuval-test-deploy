using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YuvalTestDeploy.Infrastructure;

public class YuvalTestDeployDbContext : IdentityDbContext<IdentityUser>
{
    public YuvalTestDeployDbContext(DbContextOptions<YuvalTestDeployDbContext> options)
        : base(options) { }
}
