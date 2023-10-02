namespace CashFlowzBackend.Data.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public bool Deleted { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            Deleted=false;
            CreatedAt=DateTime.UtcNow;
            UpdatedAt=DateTime.UtcNow;
        }

        public void Delete()
        {
            Deleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkUpdated()
        {
            UpdatedAt= DateTime.UtcNow;
        }
    }
}
