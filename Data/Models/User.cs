namespace CashFlowzBackend.Data.Models
{
    public class User : BaseEntity
    {
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public virtual ICollection<Budget> Budgets { get; private set;}
        public virtual ICollection<Transaction> Transactions { get; private set;}

        public User(string login, string passwordHash, string email, string firstName, string lastName)
        {
            Login= login;
            PasswordHash= passwordHash;
            Email= email;
            FirstName= firstName;
            LastName= lastName;
        }

        public void Update(string login, string email, string firstName, string lastName)
        {
            Login= login;
            Email= email;
            FirstName= firstName;
            LastName = lastName;
        }

        public void Delete()
        {
            foreach(Budget budget in Budgets)
            {
                budget.Delete();
            }

            foreach(Transaction transaction in Transactions.Where(x => !x.Deleted))
            {
                transaction.Delete();
            }

            base.Delete();
        }
    }
}
