
namespace Data.Entities
{
    public abstract class BaseEntity
{
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        public bool isDeleted { get; set; }
    }
}
