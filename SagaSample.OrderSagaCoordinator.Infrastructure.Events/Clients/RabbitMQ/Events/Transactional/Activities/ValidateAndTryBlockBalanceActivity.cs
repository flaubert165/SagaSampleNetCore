using Automatonymous;
using GreenPipes;
using SagaSample.Common;
using SagaSample.Messages.Finance.Commands;
using SagaSample.Messages.Safekeeping.Events;
using System;
using System.Threading.Tasks;

namespace SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Transactional.Activities
{
    public class ValidateAndTryBlockBalanceActivity
        : Activity<OrderState, IAssetCanBeProtectedEvent>
    {
        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<OrderState, IAssetCanBeProtectedEvent> context, Behavior<OrderState, IAssetCanBeProtectedEvent> next)
        {
            var uri = QueueNames.GetMessageUri(nameof(ITryBlockBalanceCommand));

            var sendEndpoint = await context.GetSendEndpoint(uri);

            await sendEndpoint.Send<ITryBlockBalanceCommand>(new
            {
                OrderId = context.Data.OrderId,
                Amount = 100m
            });

            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<OrderState, IAssetCanBeProtectedEvent, TException> context, Behavior<OrderState, IAssetCanBeProtectedEvent> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("validate-balance");
        }
    }
}
