using AutoMapper;
using courseProject.Core.Models.DTO.FeedbacksDTO;
using courseProject.Core.Models;

namespace courseProject.MappingProfile
{
    public class MappingForFeedback : Profile
    {
        public MappingForFeedback()
        {
            CreateMap<FeedbackDTO, Feedback>();
            CreateMap<Feedback, FeedbackForRetriveDTO>()
                .ForMember(x => x.name, o => o.MapFrom(y => y.User.userName + " " + y.User.student.LName))
                .ForMember(x => x.imageUrl, o => o.MapFrom(y => y.User.student.ImageUrl));

            CreateMap<Feedback, AllFeedbackForRetriveDTO>()
                .ForMember(x => x.name, o => o.MapFrom(y => y.User.userName + " " + y.User.student.LName))
                .ForMember(x => x.imageUrl, o => o.MapFrom(y => y.User.student.ImageUrl));

        }
    }
}
