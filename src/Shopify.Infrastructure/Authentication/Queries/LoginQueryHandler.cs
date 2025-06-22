using ErrorOr;
using MediatR;
using Shopify.Application.Authentication.DTO;
using Shopify.Domain.Common.Interfaces;
using Shopify.Domain.Users;
using Shopify.Domain.Users.Repositories;
using Shopify.Infrastructure.Authentication.Errors;

namespace Shopify.Infrastructure.Authentication.Queries;

internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationDto>>
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public LoginQueryHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationDto>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(query.Email);

        if (user is null || !passwordHasher.IsCorrectPassword(query.Password, user.Password))
        {
            return AuthenticationErrors.InvalidCredentials;
        }

        string token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationDto(user.Email, token);
    }
}