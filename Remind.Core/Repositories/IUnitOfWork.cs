namespace Remind.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    ISubjectRepository Subjects { get; }
    IObservationRepository Observations { get; }
    Task<int> CommitAsync();
}