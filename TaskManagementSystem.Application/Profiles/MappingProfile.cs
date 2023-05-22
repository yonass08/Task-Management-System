using AutoMapper;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Features.UserTask.DTO;
using TaskManagementSystem.Domain;
using TaskManagementSystem.Application.Models.Identity;

namespace TaskManagementSystem.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        #region  UserTask
        CreateMap<UserTask, CreateUserTaskDto>().ReverseMap();
        CreateMap<UserTask, UpdateUserTaskDto>().ReverseMap();
        CreateMap<UserTask, GetUserTaskListDto>().ReverseMap();
        CreateMap<UserTask, GetUserTaskDetailDto>().ReverseMap();
        #endregion

        #region  CheckList
        CreateMap<CheckList, CreateCheckListDto>().ReverseMap();
        CreateMap<CheckList, UpdateCheckListDto>().ReverseMap();
        CreateMap<CheckList, GetCheckListListDto>().ReverseMap();
        CreateMap<CheckList, GetCheckListDetailDto>().ReverseMap();
        #endregion

    }
}
