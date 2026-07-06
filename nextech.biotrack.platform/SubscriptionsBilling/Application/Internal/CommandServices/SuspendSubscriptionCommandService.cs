using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.SubscriptionsBilling.Application.CommandServices;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.CommandServices;

public class SuspendSubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork)
    : ISuspendSubscriptionCommandService
{
    public async Task<Result<Subscription>> Handle(SuspendSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.SubscriptionId, cancellationToken);
        if (subscription == null)
            return Result<Subscription>.Failure(SubscriptionsBillingError.SubscriptionNotFound, "The specified subscription was not found.");

        if (subscription.UserId != command.RequestingUserId)
            return Result<Subscription>.Failure(SubscriptionsBillingError.AccessDenied, "You do not have permission to access this subscription.");

        if (subscription.Status == SubscriptionStatus.Suspended)
            return Result<Subscription>.Failure(SubscriptionsBillingError.SubscriptionNotSuspended, "The subscription is already suspended.");

        try
        {
            subscription.Suspend();
            subscriptionRepository.Update(subscription);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Subscription>.Success(subscription);
        }
        catch (OperationCanceledException)
        {
            return Result<Subscription>.Failure(SubscriptionsBillingError.OperationCancelled, "The operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<Subscription>.Failure(SubscriptionsBillingError.DatabaseError, "A database error occurred.");
        }
        catch (Exception)
        {
            return Result<Subscription>.Failure(SubscriptionsBillingError.InternalServerError, "An unexpected error occurred.");
        }
    }
}
