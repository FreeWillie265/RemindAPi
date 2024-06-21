using Microsoft.EntityFrameworkCore;
using Remind.Core.Models;
using Remind.Core.Repositories;
using Remind.Core.Services;

namespace Remind.Services;

public class ObservationService(IUnitOfWork unitOfWork) : IObservationService
{

    public async Task<Observation?> GetById(Guid id)
    {
        return await unitOfWork.Observations.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Observation>> GetAll()
    {
        var allData = await unitOfWork.Observations.GetAllAsync();
        return allData.OrderBy(x => x.DataId);
    }

    public async Task<Observation?> GetNext()
    {
        return await unitOfWork.Observations.Find(o => !o.Assigned)
            .AsQueryable()
            .OrderBy(x => x.DataId)
            .FirstOrDefaultAsync();
    }

    public async Task<Observation> Create(Observation observation)
    {
        await unitOfWork.Observations.AddAsync(observation);
        await unitOfWork.CommitAsync();
        return observation;
    }

    public async Task<Observation> ProcessObservation(Observation observation)
    {
        observation.Assigned = true;
        await unitOfWork.CommitAsync();
        return observation;
    }

    public async Task UpdateObservation(Observation toBeUpdated, Observation observation)
    {
        toBeUpdated.BlockId = observation.BlockId;
        toBeUpdated.BlockSize = observation.BlockSize;
        toBeUpdated.Stratum = observation.Stratum;
        toBeUpdated.Treatment = observation.Treatment;
        toBeUpdated.Assigned = observation.Assigned;

        await unitOfWork.CommitAsync();
    }

    public async Task DeleteObservation(Observation observation)
    {
        unitOfWork.Observations.Remove(observation);
        await unitOfWork.CommitAsync();
    }
}