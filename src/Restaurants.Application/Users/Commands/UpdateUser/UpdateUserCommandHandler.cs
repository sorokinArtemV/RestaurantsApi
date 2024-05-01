using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Core.Domain.Identity;
using Restaurants.Core.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(
    ILogger<UpdateUserCommandHandler> logger,
    IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Updating user {Id}: {@Users}", user!.Id, request);

        var dbUser = await userStore.FindByIdAsync(user.Id, cancellationToken);

        if (dbUser is null) throw new NotFoundException(nameof(User), user.Id);

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}