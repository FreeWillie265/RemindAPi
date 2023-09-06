using Remind.Core.Models;

namespace Remind.Core.Services;

public interface ISubjectService
{
    Task<Subject> GetById(Guid id);
    Task<IEnumerable<Subject>> GetAll();
    Task<Subject?> GetNext(String ageGroup, String sex);
    Task<Subject> Create(Subject subject);
    Task UpdateSubject(Subject subjectToBeUpdated, Subject subject);
    Task<Subject> ProcessSubject(Subject subject);
    Task DeleteSubject(Subject subject);
}