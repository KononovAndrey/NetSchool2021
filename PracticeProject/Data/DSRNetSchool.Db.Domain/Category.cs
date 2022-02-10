namespace DSRNetSchool.Db.Domain;

public class Category : BaseEntity
{
    public string Title { get; set; }
    public virtual ICollection<Book> Books { get; set; }
    public virtual ICollection<BooksCategories> BooksCategories { get; set; }
}
