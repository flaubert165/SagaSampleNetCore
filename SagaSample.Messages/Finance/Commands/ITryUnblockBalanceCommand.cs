using System;

namespace SagaSample.Messages.Finance.Commands
{
    public interface ITryUnblockBalanceCommand
    {
        Guid UserId { get; }

        Guid? OrderId { get; }

        Guid CorrelationId { get; }

        decimal Amount { get; }
    }
}
