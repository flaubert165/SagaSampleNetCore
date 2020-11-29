using System;

namespace SagaSample.Messages.Finance.Events
{
    public interface ITryUnblockBalanceFailedEvent
    {
        Guid CorrelationId { get; set; }
    }
}
