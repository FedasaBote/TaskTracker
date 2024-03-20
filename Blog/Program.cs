// See https://aka.ms/new-console-template for more information


using Blog.Core;
using Blog.Data;

public class Program
{

    static void Main(string[] args)
    {
        using(var context = new BlogDbContext())
        {

            while (true)
            {
                Console.WriteLine("Select an option");
                Console.WriteLine("1.Show Posts");
                Console.WriteLine("2.Show Post Detail");
                Console.WriteLine("3.Create Post");
                Console.WriteLine("4.Update Post");
                Console.WriteLine("5.Delete Post");
                Console.WriteLine("6.Add Comment");
                Console.WriteLine("7.Update Comment");
                Console.WriteLine("8.Delete Comment");
                Console.WriteLine("9.Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                var postManager = new PostManager(context);
                var commentManager = new CommentManager(context);

                switch (choice)
                {
                    case "1":
                        var posts = postManager.GetAllPosts();
                        foreach (var post in posts)
                        {
                            Console.WriteLine($"{post.PostId} - {post.Title} - {post.Content}");
                        }
                        break;

                    case "2":
                       // Show Post Detail with comments
                       Console.Write("Enter Post Id: ");
                        var output = int.TryParse(Console.ReadLine(), out int postId);

                        if (output)
                        {
                            var post = postManager.GetPost(postId);
                            if (post != null)
                            {
                               var comments = commentManager.GetCommentsForPost(postId);
                               Console.WriteLine($"Post Id: {post.PostId} - {post.Title} - {post.Content}");

                                foreach (var comment in comments)
                                {
                                    Console.WriteLine($"Comment Id: {comment.CommentId} - {comment.Text}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Post Id");
                        }
                        break;

                    case "3":
                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter Content: ");
                        string content = Console.ReadLine();
                        try { 
                            postManager.CreatePost(title, content);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "4":
                        Console.Write("Enter Post Id: ");
                        var output1 = int.TryParse(Console.ReadLine(), out int postId1);
                        if (output1)
                        {
                            Console.Write("Enter Title: ");
                            string title1 = Console.ReadLine();
                            Console.Write("Enter Content: ");
                            string content1 = Console.ReadLine();
                            try
                            {
                                postManager.UpdatePost(postId1, title1, content1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Post Id");
                        }
                        break;

                    case "5":
                        Console.Write("Enter Post Id: ");
                        var output2 = int.TryParse(Console.ReadLine(), out int postId2);
                        if (output2)
                        {
                            try
                            {
                                postManager.DeletePost(postId2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Post Id");
                        }
                        break;

                    case "6":
                        Console.Write("Enter Post Id: ");
                        var output3 = int.TryParse(Console.ReadLine(), out int postId3);
                        if (output3)
                        {
                            Console.Write("Enter Comment: ");
                            string comment = Console.ReadLine();
                            try
                            {
                                commentManager.AddComment(postId3, comment);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Post Id");
                        }
                        break;

                    case "7":
                        Console.Write("Enter Comment Id: ");
                        var output4 = int.TryParse(Console.ReadLine(), out int commentId);
                        if (output4)
                        {
                            Console.Write("Enter Comment: ");
                            string comment = Console.ReadLine();
                            try
                            {
                                commentManager.UpdateComment(commentId, comment);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Comment Id");
                        }
                        break;

                    case "8":
                        Console.Write("Enter Comment Id: ");
                        var output5 = int.TryParse(Console.ReadLine(), out int commentId1);
                        if (output5)
                        {
                            try
                            {
                                commentManager.DeleteComment(commentId1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Comment Id");
                        }
                        break;

                    case "9":
                        return;
                }
            }
        }
    }
}

