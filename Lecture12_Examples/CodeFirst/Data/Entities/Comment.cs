namespace CodeFirstExample.Data.Entities
{
    public class Comment : BaseEntity
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public string Author { get; set; }
        public string Text { get; set; }
    }
}
