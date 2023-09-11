using Remind.Core.Models;

namespace Remind.Core.Repositories;

public interface ISubjectRepository : IRepository<Subject>
{
    
    Task<IEnumerable<String>> GetAgeGroups();
}