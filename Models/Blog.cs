using System.Collections.Generic;

namespace DotNetA10.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        // Navigation Properties
        public virtual List<Post> Posts { get; set; }

        public Blog(string name)
        {
            Name = name;
        }
    }
}