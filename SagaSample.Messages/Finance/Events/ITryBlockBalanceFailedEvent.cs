using System;

namespace SagaSample.Messages.Finance.Events
{
    public interface ITryBlockBalanceFailedEvent
    {
        Guid UserId { get; }

        Guid? OrderId { get; }

        Guid CorrelationId { get; }

        decimal Amount { get; }

        TryBlockBalanceFailureReason Reason { get; }
    }
}
