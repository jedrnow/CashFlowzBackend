namespace CashFlowzBackend.Data.Models
{
    public class Expense : BaseEntity
    {
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public decimal Amount { get; private set; }

        public Expense(decimal amount)
        {
            Amount = amount;
        }
        public Expense(int transactionId,  decimal amount)
        {
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}
