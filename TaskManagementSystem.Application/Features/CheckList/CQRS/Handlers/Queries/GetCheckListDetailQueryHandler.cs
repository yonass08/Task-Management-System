using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.CheckList.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.CheckList.DTO;

namespace TaskManagementSystem.Application.Features.CheckList.CQRS.Handlers.Queries;


public class GetCheckListDetailQueryHandler : IRequestHandler<GetCheckListDetailQuery, GetCheckListDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCheckListDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetCheckListDetailDto> Handle(GetCheckListDetailQuery request, CancellationToken cancellationToken)
    {
        var Exists = await _unitOfWork.CheckListRepository.Exists(request.Id);
        if (Exists == false)
            throw new NotFoundException(nameof(Domain.CheckList), request.Id);

        var CheckList = await _unitOfWork.CheckListRepository.Get(request.Id);
        var CheckListDto = _mapper.Map<GetCheckListDetailDto>(CheckList);
        return CheckListDto;
    }
}