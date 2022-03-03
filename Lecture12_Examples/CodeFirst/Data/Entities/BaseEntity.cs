using System.ComponentModel.DataAnnotations;

namespace CodeFirstExample.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Uid { get; set; }
    }
}
