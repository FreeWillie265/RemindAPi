namespace Remind.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    ISubjectRepository Subjects { get; }
    Task<int> CommitAsync();
}