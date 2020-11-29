using System;

namespace SagaSample.Messages.Finance.Events
{
    public interface ITryBlockBalanceSuccessfullyEvent
    {
        Guid UserId { get; }

        Guid? OrderId { get; }

        Guid CorrelationId { get; }

        decimal Amount { get; }
    }
}
