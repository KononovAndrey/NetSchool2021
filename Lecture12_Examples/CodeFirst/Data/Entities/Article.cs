using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstExample.Data.Entities
{
    [Table("articles")]
    public class Article : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("text")]
        public string Text { get; set; }

        [NotMapped]
        public string SomeIgnoredField { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
