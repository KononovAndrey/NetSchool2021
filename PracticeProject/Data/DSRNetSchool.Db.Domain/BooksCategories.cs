namespace DSRNetSchool.Db.Domain;

public class BooksCategories : BaseEntity
{
    public int BookId { get; set; }
    public virtual Book Book { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public DateTime Created { get; set; }
}
