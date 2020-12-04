using Automatonymous;
using SagaSample.Messages.Finance.Events;
using SagaSample.Messages.Safekeeping.Events;
using SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Contracts;
using SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Transactional.Activities;
using System;

namespace SagaSample.OrderSagaCoordinator.Infrastructure.Events.Clients.RabbitMQ.Events.Transactional
{
    public class OrderStateMachine :
        MassTransitStateMachine<OrderState>
    {
        public State OrderRequested { get; private set; }
        public State SafekeepingValidated { get; private set; }
        public State BalanceValidated { get; private set; }
        public State OrderRejected { get; private set; }
        public State OrderCompleted { get; private set; }

        public Event<IOrderSubmitted> OrderSubmittedEvent { get; private set; }

        #region Events Finance
        public Event<ITryBlockBalanceSuccessfullyEvent> TryBlockBalanceSuccessfullyEvent { get; private set; }
        public Event<ITryBlockBalanceFailedEvent> TryBlockBalanceFailedEvent { get; private set; }
        public Event<ITryUnblockBalanceFailedEvent> TryUnblockBalanceFailedEvent { get; private set; }
        public Event<ITryUnblockBalanceSuccessfullyEvent> TryUnblockBalanceSuccessfullyEvent { get; private set; }

        # endregion

        #region Events Safekeeping
        public Event<IAssetCanBeProtectedEvent> AssetCanBeProtectedEvent { get; private set; }
        public Event<IAssetCannotBeProtectedEvent> AssetCannotBeProtectedEvent { get; private set; }
        public Event<ICanAssetBeProtectedFailEvent> CanAssetBeProtectedFailEvent { get; private set; }

        #endregion

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);
            ConfigureCorrelationIds();

            Initially(
                When(OrderSubmittedEvent)
                .Then(x => x.Instance.OrderId = x.Data.OrderId)
                .Then(x => Console.WriteLine($"Order {x.Instance.OrderId} submitted"))
                .Activity(x => x.OfType<ValidateCustomerAssetsActivity>())
                .TransitionTo(SafekeepingValidated));

            During(SafekeepingValidated,
                When(AssetCanBeProtectedEvent)
                .Then(x => Console.WriteLine($"Safekeeping {x.Instance.OrderId} validated"))
                .Activity(x => x.OfType<ValidateAndTryBlockBalanceActivity>())
                .TransitionTo(BalanceValidated)
                .TransitionTo(OrderCompleted)
                .Finalize());

            //DuringAny(
            //    When(AssetCannotBeProtectedEvent)
            //        .Then(x => Console.WriteLine($"Safekeeping {x.Instance.OrderId} validate failed"))
            //        .Activity(x => x.OfType<>())
            //        .TransitionTo(OrderRejected)
            //        .Finalize());

            //DuringAny(
            //     When(CanAssetBeProtectedFailEvent)
            //         .Then(x => Console.WriteLine($"Balance {x.Instance.OrderId} validate failed"))
            //         .Activity(x => x.OfType<>())
            //         .TransitionTo(OrderRejected)
            //         .Finalize());

            //DuringAny(
            //     When(TryBlockBalanceFailedEvent)
            //         .Then(x => Console.WriteLine($"Balance {x.Instance.OrderId} validate failed"))
            //         .Activity(x => x.OfType<>())
            //         .TransitionTo(OrderRejected)
            //         .Finalize());

            //DuringAny(
            //     When(TryUnblockBalanceFailedEvent)
            //         .Then(x => Console.WriteLine($"Balance {x.Instance.OrderId} validate failed"))
            //         .Activity(x => x.OfType<>())
            //         .TransitionTo(OrderRejected)
            //         .Finalize());

            //DuringAny(
            //     When(TryUnblockBalanceSuccessfullyEvent)
            //         .Then(x => Console.WriteLine($"Balance {x.Instance.OrderId} validate failed"))
            //         .Activity(x => x.OfType<>())
            //         .TransitionTo(OrderRejected)
            //         .Finalize());
        }

        private void ConfigureCorrelationIds()
        {
            Event(() => OrderSubmittedEvent, x => x.CorrelateById(x => x.Message.CorrelationId).SelectId(c => c.Message.CorrelationId));
            Event(() => AssetCanBeProtectedEvent, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => AssetCannotBeProtectedEvent, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => TryBlockBalanceSuccessfullyEvent, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => TryBlockBalanceFailedEvent, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => TryUnblockBalanceFailedEvent, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => TryUnblockBalanceSuccessfullyEvent, x => x.CorrelateById(x => x.Message.CorrelationId));
        }
    }
}
