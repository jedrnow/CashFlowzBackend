namespace CashFlowzBackend.Data.Models
{
    public class Budget : BaseEntity
    {
        public string Name { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly? EndDate { get; private set; }
        public decimal? Goal { get; private set; }
        public decimal Amount { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public virtual ICollection<Transaction> Transactions { get; private set; }

        public Budget(string name, DateOnly startDate, DateOnly? endDate, decimal? goal, int userId, int categoryId)
        {
            Name= name;
            StartDate= startDate;
            EndDate= endDate;
            Goal= goal;
            Amount= decimal.Zero;
            UserId= userId;
            CategoryId= categoryId;
            Transactions= new List<Transaction>();
        }

        public void Update(string name, DateOnly startDate, DateOnly? endDate, decimal? goal, int categoryId)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Goal = goal;
            CategoryId = categoryId;
        }

        public void Delete()
        {
            foreach(Transaction transaction in Transactions)
            {
                transaction.Delete();
            }

            base.Delete();
        }
    }
}
