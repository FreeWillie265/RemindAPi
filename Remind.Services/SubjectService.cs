﻿using Remind.Core.Models;
using Remind.Core.Repositories;
using Remind.Core.Services;

namespace Remind.Services;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubjectService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Subject> GetById(Guid id)
    {
        return await _unitOfWork.Subjects.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Subject>> GetAll()
    {
        return await _unitOfWork.Subjects.GetAllAsync();
    }

    public async Task<Subject> Create(Subject subject)
    {
        await _unitOfWork.Subjects.AddAsync(subject);
        await _unitOfWork.CommitAsync();
        return subject;
    }

    public async Task UpdateSubject(Subject subjectToBeUpdated, Subject subject)
    {
        subjectToBeUpdated.AgeGroup = subject.AgeGroup;
        subjectToBeUpdated.Sex = subject.Sex;
        subjectToBeUpdated.BlockId = subject.BlockId;
        subjectToBeUpdated.BlockSize = subject.BlockSize;
        subjectToBeUpdated.ClinicName = subject.ClinicName;
        subjectToBeUpdated.District = subject.District;
        subjectToBeUpdated.Clerk = subject.Clerk;
        subjectToBeUpdated.Etc = subject.Etc;
        subjectToBeUpdated.Traversed = subject.Traversed;

        await _unitOfWork.CommitAsync();

    }

    public async Task DeleteSubject(Subject subject)
    {
        _unitOfWork.Subjects.Remove(subject);
        await _unitOfWork.CommitAsync();
    }
}