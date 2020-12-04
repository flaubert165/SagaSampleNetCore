using Automatonymous;
using GreenPipes;
using SagaSample.Common;
using SagaSample.Messages.Safekeeping.Events;
using SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Contracts;
using System;
using System.Threading.Tasks;

namespace SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Transactional.Activities
{
    public class ValidateCustomerAssetsActivity
        : Activity<OrderState, IOrderSubmitted>
    {
        public ValidateCustomerAssetsActivity()
        {

        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<OrderState, IOrderSubmitted> context, Behavior<OrderState, IOrderSubmitted> next)
        {
            var uri = QueueNames.GetMessageUri(nameof(IAssetCanBeProtectedEvent));

            var sendEndpoint = await context.GetSendEndpoint(uri);

            await sendEndpoint.Send<IAssetCanBeProtectedEvent>(new
            {
                OrderId = context.Data.OrderId
            });

            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<OrderState, IOrderSubmitted, TException> context, Behavior<OrderState, IOrderSubmitted> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("submit-order");
        }
    }
}
