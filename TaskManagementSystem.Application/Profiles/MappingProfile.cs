using AutoMapper;
using TaskManagementSystem.Application.Features.CheckList.DTO;
using TaskManagementSystem.Application.Features.UserTask.DTO;
using TaskManagementSystem.Application.Features.User.DTO;
using TaskManagementSystem.Domain;

namespace TaskManagementSystem.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region  User
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();
        CreateMap<User, GetUserListDto>().ReverseMap();
        CreateMap<User, GetUserDetailDto>().ReverseMap();
        #endregion

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
