using Automatonymous;
using MassTransit.Saga;
using System;

namespace SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Transactional
{
    public class OrderState :
        SagaStateMachineInstance,
        ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }
        public string CurrentState { get; set; }
        public int Version { get; set; }
    }
}
