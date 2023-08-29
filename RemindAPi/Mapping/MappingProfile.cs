using AutoMapper;
using Remind.Core.Models;
using RemindAPi.Resources;

namespace RemindAPi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SaveSubjectResource, Subject>();
    }
}