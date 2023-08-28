using Remind.Core.Models;
using Remind.Core.Repositories;

namespace Remind.Data.Repositories;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(SubjectDbContext context) : base(context)
    {}
    
}