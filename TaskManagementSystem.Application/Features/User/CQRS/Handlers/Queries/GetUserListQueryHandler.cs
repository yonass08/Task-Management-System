
using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.User.DTO;

namespace TaskManagementSystem.Application.Features.User.CQRS.Handlers.Queries;

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<GetUserListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetUserListDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAll();
        var userDtos = _mapper.Map<List<GetUserListDto>>(users);
        return userDtos;
    }
}



