
using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Queries;

public class GetCheckListListQueryHandler : IRequestHandler<GetCheckListListQuery, List<GetCheckListListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCheckListListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetCheckListListDto>> Handle(GetCheckListListQuery request, CancellationToken cancellationToken)
    {
        var CheckLists = await _unitOfWork.CheckListRepository.GetAll();
        var CheckListDtos = _mapper.Map<List<GetCheckListListDto>>(CheckLists);
        return CheckListDtos;
    }
}



