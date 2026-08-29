using API.Data.Models;
using API.Requests;
using API.Responses;
using AutoMapper;

namespace API.Utilities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Name, PatienNameResponse>().ReverseMap();
        CreateMap<Patient, PatientResponse>().ReverseMap();

        CreateMap<PatientCreateRequest, Patient>().ReverseMap();
        CreateMap<NameCreateRequest, Name>().ReverseMap();

        CreateMap<NameUpdateRequest, Name>().ReverseMap();
        CreateMap<PatientUpdateRequest, Patient>().ReverseMap();
    }
}
