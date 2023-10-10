namespace CashFlowzBackend.Data.Models
{
    public class Income:BaseEntity
    {
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public decimal Amount { get; private set; }

        public Income(decimal amount)
        {
            Amount = amount;
        }
        public Income(int transactionId, decimal amount)
        {
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}
