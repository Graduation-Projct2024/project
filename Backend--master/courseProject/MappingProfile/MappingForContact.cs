using AutoMapper;
using courseProject.Core.Models.DTO.ContactUsDTO;
using courseProject.Core.Models;
using Sieve.Models;

namespace courseProject.MappingProfile
{
    public class MappingForContact : Profile
    {
        public MappingForContact()
        {

            CreateMap<CreateMessageContactDTO, Contact>();

            CreateMap<CustomSieveModel, SieveModel>();
        }
    }
}
