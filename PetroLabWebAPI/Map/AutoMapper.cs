using AutoMapper;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.ServiceDto.Branch.Response;
using PetroLabWebAPI.ServiceDto.Customer.Response;
using PetroLabWebAPI.ServiceDto.Doctor.Response;
using PetroLabWebAPI.ServiceDto.LabStudio.Response;

namespace PetroLabWebAPI.Map;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<Branch, BranchDtoItem>();
        CreateMap<Customer, CustomerDtoItem>();
        CreateMap<Doctor, DoctorDtoItem>();
        CreateMap<Doctor, DoctorDtoItem>();
        CreateMap<LabStudio,LabStudioDtoItem>();
    }
}
