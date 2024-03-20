using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public List<Comment> Comments { get; set; }
    }

}
