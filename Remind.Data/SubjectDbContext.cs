using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Remind.Core.Models;
using Remind.Data.Configurations;

namespace Remind.Data;

public class SubjectDbContext :  IdentityDbContext<ApplicationUser>
{
    public DbSet<Subject> Subjects { get; set; }

    public SubjectDbContext(DbContextOptions<SubjectDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new SubjectConfiguration());
    }
}