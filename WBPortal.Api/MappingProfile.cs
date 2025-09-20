using AutoMapper;
using WBPortal.Api.Models;
using WBPortal.Api.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserResponseDTO>();
        CreateMap<UserCreateDTO, User>();
        CreateMap<UserUpdateDTO, User>();

        // Project
        CreateMap<Project, ProjectResponseDTO>()
            .ForMember(dest => dest.ProjectManagerName, opt => opt.MapFrom(src => src.ProjectManager.Username));
        CreateMap<ProjectCreateDTO, Project>();
        CreateMap<ProjectUpdateDTO, Project>();

        // Feedback
        CreateMap<Feedback, FeedbackResponseDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));
        CreateMap<FeedbackCreateDTO, Feedback>();
    }
}
