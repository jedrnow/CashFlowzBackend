namespace CashFlowzBackend.Data.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }

        public Category(string name)        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void Delete()
        {
            base.Delete();
        }
    }
}
