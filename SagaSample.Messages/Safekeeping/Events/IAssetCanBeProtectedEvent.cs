using System;

namespace SagaSample.Messages.Safekeeping.Events
{
    public interface IAssetCanBeProtectedEvent
    {
        Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }
    }
}
