using ErrorOr;
using MediatR;
using Shopify.Application.Authentication.DTO;
using Shopify.Application.Authentication.Mappings;
using Shopify.Application.Common.Interfaces;
using Shopify.Domain.Common.Interfaces;
using Shopify.Domain.Users;
using Shopify.Domain.Users.Repositories;

namespace Shopify.Application.Authentication.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationDto>>
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPasswordHasher passwordHasher;
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
        this.passwordHasher = passwordHasher;
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationDto>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email))
        {
            return Error.Conflict(description: "User with given email already exists");
        }

        ErrorOr<string> hashPasswordResult = passwordHasher.HashPassword(command.Password);

        if (hashPasswordResult.IsError)
        {
            return hashPasswordResult.Errors;
        }

        User user = command.MapToDomain(hashPasswordResult.Value);

        await userRepository.AddAsync(user);
        await unitOfWork.CommitChangesAsync(cancellationToken);

        string token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationDto(user.Email, token);
    }
}