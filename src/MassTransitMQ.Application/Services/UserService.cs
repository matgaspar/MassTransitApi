using AutoMapper;
using MassTransitMQ.Domain.DTOs.IO.Input;
using MassTransitMQ.Domain.DTOs.IO.Output;
using MassTransitMQ.Domain.Entities;
using MassTransitMQ.Domain.Interfaces.Data;
using MassTransitMQ.Domain.Interfaces.Data.Repositories;
using MassTransitMQ.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace MassTransitMQ.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserOutput>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(page, pageSize, cancellationToken);

        var result = _mapper.Map<IEnumerable<UserOutput>>(users);

        return result;
    }

    public async Task<UserOutput> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var data = await _userRepository.FindByIdAsync(id, cancellationToken);

        var result = _mapper.Map<UserOutput>(data);

        return result;
    }

    public async Task<Guid> AddAsync(UserInput input, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var entity = _mapper.Map<User>(input);

            var id = await _userRepository.AddAsync(entity, cancellationToken);

            await _unitOfWork.Commit();

            return id;
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception(ex.Message, ex?.InnerException);
        }
    }

    public async Task UpdateAsync(Guid id, UserInput input, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var entity = _mapper.Map<User>(input);

            await _userRepository.UpdateAsync(id, entity, cancellationToken);

            await _unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception(ex.Message, ex?.InnerException);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransaction();
            
            await _userRepository.DeleteAsync(id, cancellationToken);

            await _unitOfWork.Commit();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            throw new Exception(ex.Message, ex?.InnerException);
        }
    }
}