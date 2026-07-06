using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.SubscriptionsBilling.Application.CommandServices;
using nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.OutboundServices;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Aggregates;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Commands;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;
using nextech.biotrack.platform.Shared.Application.Internal.Model;
using nextech.biotrack.platform.Shared.Domain.Repositories;

namespace nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.CommandServices;

public class ReactivateSubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionPlanRepository planRepository,
    IPaymentRepository paymentRepository,
    IInvoiceRepository invoiceRepository,
    IPaymentGatewayAdapter paymentGateway,
    IUnitOfWork unitOfWork)
    : IReactivateSubscriptionCommandService
{
    public async Task<Result<Subscription>> Handle(ReactivateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.SubscriptionId, cancellationToken);
        if (subscription == null)
            return Result<Subscription>.Failure(SubscriptionsBillingError.SubscriptionNotFound, "The specified subscription was not found.");

        if (subscription.UserId != command.RequestingUserId)
            return Result<Subscription>.Failure(SubscriptionsBillingError.AccessDenied, "You do not have permission to access this subscription.");

        if (subscription.Status != SubscriptionStatus.Suspended && subscription.Status != SubscriptionStatus.PendingPayment)
            return Result<Subscription>.Failure(SubscriptionsBillingError.SubscriptionNotEligibleForReactivation,
                "The subscription is not eligible for reactivation.");

        var plan = await planRepository.FindByIdAsync(subscription.PlanId, cancellationToken);
        if (plan == null)
            return Result<Subscription>.Failure(SubscriptionsBillingError.PlanNotFound, "The specified subscription plan was not found.");

        try
        {
            var paymentResult = await paymentGateway.ProcessPaymentAsync(subscription.Id, plan.MonthlyAmount, cancellationToken);

            var payment = new Payment(subscription.Id, DateOnly.FromDateTime(DateTime.UtcNow), plan.MonthlyAmount);
            if (paymentResult.IsApproved)
            {
                payment.Approve(paymentResult.TransactionId, paymentResult.GatewayMessage);
                var nextBillingDate = DateOnly.FromDateTime(DateTime.UtcNow).AddMonths(1);
                subscription.Reactivate(nextBillingDate);

                var invoice = new Invoice(subscription.Id, DateOnly.FromDateTime(DateTime.UtcNow),
                    nextBillingDate, plan.MonthlyAmount);
                await invoiceRepository.AddAsync(invoice, cancellationToken);
            }
            else
            {
                payment.Reject(paymentResult.TransactionId, paymentResult.GatewayMessage);
                await paymentRepository.AddAsync(payment, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<Subscription>.Failure(SubscriptionsBillingError.PaymentRejected, "The reactivation payment was rejected by the payment gateway.");
            }

            await paymentRepository.AddAsync(payment, cancellationToken);
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
