using DotNetA10.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;

namespace DotNetA10
{
    class Program
    {
        public static void Main(string[] args)
        {
            string menuOption;

            do
            {
                Console.WriteLine("\n1) Display Blogs.");
                Console.WriteLine("2) Add Blog.");
                Console.WriteLine("3) Display Posts.");
                Console.WriteLine("4) Add Post.");
                Console.WriteLine("Enter any other key to exit.\n");
                menuOption = Console.ReadLine();
                if (menuOption == "1")
                {
                    Console.WriteLine("Blogs:");
                    DisplayBlogs();
                }
                else if (menuOption == "2")
                {
                    AddBlog();
                }
                else if (menuOption == "3")
                {
                    DisplayPosts();
                }
                else if (menuOption == "4")
                {
                    AddPost();
                }
            } while (menuOption == "1" || menuOption == "2" || menuOption == "3" || menuOption == "4");

            Console.WriteLine("Shutting Down...");
        }

        private static void DisplayBlogs()
        {
            using var db = new BlogContext();
            foreach (var b in db.Blogs)
            {
                Console.WriteLine($"\t{b.BlogId}: {b.Name}");
            }
        }

        private static void AddBlog()
        {
            string blogName;

            Console.WriteLine("Enter your Blog name:");
            while (true)
            {
                blogName = Console.ReadLine();
                if (!blogName.IsNullOrEmpty())
                {
                    break;
                }
                Console.WriteLine("Blog name cannot be null!");
            }

            var blog = new Blog(blogName);
            using (var db = new BlogContext())
            {
                db.Add(blog);
                db.SaveChanges();
            }
        }

        private static void DisplayPosts()
        {
            Console.WriteLine("Enter the Blog Id of the Blog whose posts you wish to view:");
            var blogId = SelectBlogId();

            using var db = new BlogContext();
            var blog = db.Blogs.Where(x => x.BlogId == blogId).FirstOrDefault();
            foreach (var post in blog.Posts)
            {
                Console.WriteLine($"{blog.Name} | {post.Title}\n\t{post.Content}");
            }
        }

        private static void AddPost()
        {
            string postTitle;
            string postContent;

            Console.WriteLine("Enter the Blog Id of the Blog you wish to add a post to:");
            var blogId = SelectBlogId();

            Console.WriteLine("Enter your Post title:");
            while (true)
            {
                postTitle = Console.ReadLine();
                if (!postTitle.IsNullOrEmpty())
                {
                    break;
                }
                Console.WriteLine("Post name cannot be null!");
            }

            Console.WriteLine("Enter your Post content:");
            while (true)
            {
                postContent = Console.ReadLine();
                if (!postContent.IsNullOrEmpty())
                {
                    break;
                }
                Console.WriteLine("Post content cannot be null!");
            }

            var post = new Post(postTitle, postContent, blogId);

            using (var db = new BlogContext())
            {
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        private static int SelectBlogId()
        {
            int validBlogId;

            DisplayBlogs();
            using (var db = new BlogContext())
            {
                while (true)
                {
                    try
                    {
                        validBlogId = int.Parse(Console.ReadLine());
                        var blog = db.Blogs.Where(blog => blog.BlogId == validBlogId).FirstOrDefault();
                        if (blog != null)
                        {
                            return validBlogId;
                        }
                        else
                        {
                            Console.WriteLine("Not a valid Blog Id!");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Not a valid Blog Id!");
                    }
                }
            }
        }
    }
}
