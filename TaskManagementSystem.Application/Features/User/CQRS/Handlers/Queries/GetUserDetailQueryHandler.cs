using AutoMapper;
using MediatR;
using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Application.Exceptions;
using TaskManagementSystem.Application.Features.User.CQRS.Requests.Queries;
using TaskManagementSystem.Application.Features.User.DTO;

namespace TaskManagementSystem.Application.Features.User.CQRS.Handlers.Queries;


public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, GetUserDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetUserDetailDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var Exists = await _unitOfWork.UserRepository.Exists(request.Id);
        if (Exists == false)
            throw new NotFoundException(nameof(Domain.User), request.Id);

        var user = await _unitOfWork.UserRepository.Get(request.Id);
        var userDto = _mapper.Map<GetUserDetailDto>(user);
        return userDto;
    }
}