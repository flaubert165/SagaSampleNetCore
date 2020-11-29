namespace SagaSample.Messages.Finance.Events
{
    public enum TryBlockBalanceFailureReason
    {
        TradingAccountNotFound = 1,
        BalanceNotFound = 2,
        NotEnoughBalanceToBlock = 3
    }
}
