using Remind.Core.Repositories;
using Remind.Data.Repositories;

namespace Remind.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SubjectDbContext _context;
    private SubjectRepository _subjectRepository;
    private IObservationRepository _observationRepository;

    public UnitOfWork(SubjectDbContext context)
    {
        this._context = context;
    }

    public ISubjectRepository Subjects => _subjectRepository
        = _subjectRepository ?? new SubjectRepository(_context);

    public IObservationRepository Observations => _observationRepository
        = _observationRepository ?? new ObservationRepository(_context);

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}