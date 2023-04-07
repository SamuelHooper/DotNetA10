namespace DotNetA10.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        // Navigation Properties
        public virtual Blog Blog { get; set; }

        public Post(string title, string content, int blogId)
        {
            Title = title;
            Content = content;
            BlogId = blogId;
        }
    }
}