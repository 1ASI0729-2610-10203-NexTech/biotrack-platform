using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.Iam.Application.CommandServices;
using nextech.biotrack.platform.Iam.Application.Internal.OutboundServices;
using nextech.biotrack.platform.Iam.Domain.Model;
using nextech.biotrack.platform.Iam.Domain.Model.Aggregates;
using nextech.biotrack.platform.Iam.Domain.Model.Commands;
using nextech.biotrack.platform.Iam.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.Iam.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    IHashingService hashingService,
    ITokenService tokenService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    public async Task<Result> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            return Result.Failure(IamError.EmailAlreadyTaken,
                $"The email '{command.Email}' is already registered.");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.FirstName, command.LastName, command.Email, hashedPassword, command.Role);

        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(IamError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, "A database error occurred while registering the user.");
        }
        catch (Exception)
        {
            return Result.Failure(IamError.InternalServerError, "An unexpected error occurred.");
        }
    }

    public async Task<Result<(User user, string token)>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(command.Email, cancellationToken);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(
                IamError.InvalidCredentials, "Invalid email or password.");

        var token = tokenService.GenerateToken(user);
        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result> Handle(VerifyEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByVerificationTokenAsync(command.Token, cancellationToken);

        if (user == null)
            return Result.Failure(IamError.InvalidVerificationToken,
                "The verification token is invalid or has already been used.");

        if (user.EmailVerified)
            return Result.Failure(IamError.EmailAlreadyVerified,
                "The email address has already been verified.");

        user.VerifyEmail();
        userRepository.Update(user);

        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, "A database error occurred while verifying the email.");
        }
    }
}
