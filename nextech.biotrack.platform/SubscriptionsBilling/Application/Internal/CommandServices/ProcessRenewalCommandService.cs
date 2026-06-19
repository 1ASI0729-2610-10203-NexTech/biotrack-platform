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

public class ProcessRenewalCommandService(
    ISubscriptionRepository subscriptionRepository,
    ISubscriptionPlanRepository planRepository,
    IPaymentRepository paymentRepository,
    IInvoiceRepository invoiceRepository,
    IPaymentGatewayAdapter paymentGateway,
    IUnitOfWork unitOfWork)
    : IProcessRenewalCommandService
{
    public async Task<Result<Subscription>> Handle(ProcessRenewalCommand command, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.SubscriptionId, cancellationToken);
        if (subscription == null)
            return Result<Subscription>.Failure(SubscriptionsBillingError.SubscriptionNotFound, "The specified subscription was not found.");

        if (subscription.UserId != command.RequestingUserId)
            return Result<Subscription>.Failure(SubscriptionsBillingError.AccessDenied, "You do not have permission to access this subscription.");

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
                var nextBillingDate = subscription.NextBillingDate.AddMonths(1);
                subscription.Activate(nextBillingDate);

                var invoice = new Invoice(subscription.Id, DateOnly.FromDateTime(DateTime.UtcNow),
                    nextBillingDate, plan.MonthlyAmount);
                await invoiceRepository.AddAsync(invoice, cancellationToken);
            }
            else
            {
                payment.Reject(paymentResult.TransactionId, paymentResult.GatewayMessage);
                subscription.SetPendingPayment();
            }

            await paymentRepository.AddAsync(payment, cancellationToken);
            subscriptionRepository.Update(subscription);
            await unitOfWork.CompleteAsync(cancellationToken);

            if (!paymentResult.IsApproved)
                return Result<Subscription>.Failure(SubscriptionsBillingError.PaymentRejected, "The renewal payment was rejected by the payment gateway.");

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
