
using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Queries;

public class GetCheckListListQueryHandler : IRequestHandler<GetCheckListListQuery, List<GetCheckListListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly IAuthorizationService _authService;

    public GetCheckListListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork;
        _authService = authorizationService;
        _mapper = mapper;
    }


    public async Task<List<GetCheckListListDto>> Handle(GetCheckListListQuery request, CancellationToken cancellationToken)
    {
        var result = await _authService.IsAdmin(request.UserId);

        if(result == false)
            throw new UnauthorizedException($"route authorized only for admins");

        var CheckLists = await _unitOfWork.CheckListRepository.GetAll();
        var UserId = request.UserId;
        var CheckListDtos = _mapper.Map<List<GetCheckListListDto>>(CheckLists);


        return CheckListDtos;
    }
}



