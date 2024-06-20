using Microsoft.EntityFrameworkCore;
using Remind.Core.Models;
using Remind.Core.Repositories;

namespace Remind.Data.Repositories;

public class ObservationRepository(SubjectDbContext context) : Repository<Observation>(context), IObservationRepository
{
}