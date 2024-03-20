using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId  { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public Post Post { get; set; }
    }
}
