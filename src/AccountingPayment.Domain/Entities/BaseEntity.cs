using System.ComponentModel.DataAnnotations;

namespace AccountingPayment.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool Deleted { get; set; }
        public void SetLastModified() => UpdateAt = DateTime.UtcNow;
        public void SoftDeleteEntity() { Deleted = true; UpdateAt = DateTime.UtcNow; }
    }
}
