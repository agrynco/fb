using AutoMapper;
using DAL.Abstract.Core;
using DAL.Abstract.Users;
using Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using Services.Core;
using SlimMessageBus;

namespace Services.Users.Authenticate;

public class AuthenticateUserByTokenRequest : IRequest<AuthenticateUserResponse>
{
    public required string IpAddress { get; init; } = default!;
    public required string JwtKey { get; init; } = default!;
    public int RefreshTokenTtl { get; init; }
    public int TokenLiveDuration { get; init; }
    public int UserId { get; set; }
}

public class
    AuthenticateUserByTokenRequestHandler : RequestHandler<AuthenticateUserByTokenRequest, AuthenticateUserResponse>
{
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;

    public AuthenticateUserByTokenRequestHandler(ILogger logger, IUsersRepository usersRepository,
        IJwtUtils jwtUtils, IMapper mapper, IUnitOfWork unitOfWork) : base(logger)
    {
        _usersRepository = usersRepository;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<AuthenticateUserResponse> DoOnHandle(AuthenticateUserByTokenRequest request)
    {
        User user = (await _usersRepository.GetById(request.UserId))!;

        string refreshToken = await HandleRefreshToken(request.IpAddress, request.RefreshTokenTtl, request.UserId);

        return new AuthenticateUserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RefreshToken = refreshToken,
            JwtToken = _jwtUtils.GenerateJwtToken(user.Id, request.JwtKey, request.TokenLiveDuration)
        };
    }

    private async Task<string> HandleRefreshToken(string ipAddress, int refreshTokenTtl, int userId)
    {
        await using IDbContextTransaction transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _usersRepository.RemoveOldRefreshTokens(userId, refreshTokenTtl);

            GenerateRefreshTokenResult generateRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);

            var refreshToken =
                _mapper.Map<RefreshToken>(generateRefreshToken);

            await _usersRepository.AddRefreshTokenToUser(userId, refreshToken);

            await transaction.CommitAsync();

            return generateRefreshToken.Token;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            throw;
        }
    }
}