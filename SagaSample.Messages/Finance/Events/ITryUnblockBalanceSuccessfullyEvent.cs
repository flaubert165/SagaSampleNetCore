using System;

namespace SagaSample.Messages.Finance.Events
{
    public interface ITryUnblockBalanceSuccessfullyEvent
    {
        Guid CorrelationId { get; set; }
    }
}
