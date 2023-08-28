using Microsoft.EntityFrameworkCore;
using Remind.Core.Models;
using Remind.Data.Configurations;

namespace Remind.Data;

public class SubjectDbContext : DbContext
{
    public DbSet<Subject> Subjects { get; set; }

    public SubjectDbContext(DbContextOptions<SubjectDbContext> options)
    : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SubjectConfiguration());
    }
}