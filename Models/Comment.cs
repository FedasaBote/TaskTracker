using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApi.Models
{
  
        public class Comment
        {
            [Key]
            public int Id { get; set; }
            public string Text { get; set; }
             public DateTime CreatedAt { get; set; }

            public int PostId { get; set; }
            [JsonIgnore]
            public Post Post { get; set; }
        }
    
}
