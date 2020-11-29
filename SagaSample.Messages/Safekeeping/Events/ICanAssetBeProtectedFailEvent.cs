using System;

namespace SagaSample.Messages.Safekeeping.Events
{
    public interface ICanAssetBeProtectedFailEvent
    {
        Guid CorrelationId { get; set; }
    }
}
