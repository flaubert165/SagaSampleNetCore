using System;

namespace SagaSample.Messages.Safekeeping.Events
{
    public interface IAssetCannotBeProtectedEvent
    {
        Guid CorrelationId { get; set; }
    }
}
