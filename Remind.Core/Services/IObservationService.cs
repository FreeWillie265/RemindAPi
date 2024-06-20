using Remind.Core.Models;

namespace Remind.Core.Services;

public interface IObservationService
{
    Task<Observation> GetById(String id);
    Task<IEnumerable<Observation>> GetAll();
    Task<Observation?> GetNext();
    Task<Observation> Create(Observation observation);
    Task UpdateObservation(Observation toBeUpdated, Observation observation);
    Task DeleteObservation(Observation observation);
}