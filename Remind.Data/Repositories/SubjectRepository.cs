using Microsoft.EntityFrameworkCore;
using Remind.Core.Models;
using Remind.Core.Repositories;

namespace Remind.Data.Repositories;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    private readonly SubjectDbContext _context;

    public SubjectRepository(SubjectDbContext context) : base(context)
    {
        _context = context;
    }
    
    private SubjectDbContext SubjectDbContext { get {return Context as SubjectDbContext;}}
    public async Task<IEnumerable<string>> GetAgeGroups()
    {
        return await _context.Subjects
            .Select(x => x.AgeGroup)
            .Distinct()
            .ToListAsync();
    }
}