using AutoMapper;
using DAL.Abstract.Core;
using DAL.Abstract.Users;
using Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using Services.Core;
using Services.Users.Authenticate.Exceptions;

namespace Services.Users.Authenticate;

public class AuthenticateUserByCredentialsRequestHandler : RequestHandler<AuthenticateUserByCredentialsRequest,
    AuthenticateUserResponse>
{
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;

    public AuthenticateUserByCredentialsRequestHandler(ILogger logger, IUsersRepository usersRepository,
        IJwtUtils jwtUtils, IMapper mapper, IUnitOfWork unitOfWork) : base(logger)
    {
        _usersRepository = usersRepository;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<AuthenticateUserResponse> DoOnHandle(AuthenticateUserByCredentialsRequest request)
    {
        User? user = await _usersRepository.GetByUsername(request.Username);

        if (user == null)
        {
            throw new IncorrectUsernameException(request.Username);
        }

        if (!user.Activated)
        {
            throw new AccountIsNotActivatedException(request.Username);
        }

        if (!PasswordUtils.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new IncorrectPasswordException(request.Username);
        }

        string refreshToken = await HandleRefreshToken(request.IpAddress, request.RefreshTokenTtl, user.Id);

        return new AuthenticateUserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RefreshToken = refreshToken,
            JwtToken = _jwtUtils.GenerateJwtToken(user.Id, request.JwtKey,
                request.TokenLiveDuration)
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