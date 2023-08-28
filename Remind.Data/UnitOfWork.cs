using Remind.Core.Repositories;
using Remind.Data.Repositories;

namespace Remind.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SubjectDbContext _context;
    private SubjectRepository _subjectRepository;

    public UnitOfWork(SubjectDbContext context)
    {
        this._context = context;
    }

    public ISubjectRepository Subjects => _subjectRepository
        = _subjectRepository ?? new SubjectRepository(_context);

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}