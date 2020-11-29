using System;

namespace SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Contracts
{
    public interface IOrderSubmitted
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }
    }
}
