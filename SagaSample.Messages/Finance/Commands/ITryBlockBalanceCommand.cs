using System;

namespace SagaSample.Messages.Finance.Commands
{
    public interface ITryBlockBalanceCommand
    {
        Guid UserId { get; }

        Guid? OrderId { get; }

        Guid CorrelationId { get; }

        decimal Amount { get; }
    }
}
