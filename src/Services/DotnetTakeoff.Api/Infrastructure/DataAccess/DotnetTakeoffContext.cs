using System.Reflection;
using DotnetTakeoff.Api.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetTakeoff.Api.Infrastructure.DataAccess;

internal sealed class DotnetTakeoffContext : DbContext
{
    private DotnetTakeoffContext()
    {
    }

    public DotnetTakeoffContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
