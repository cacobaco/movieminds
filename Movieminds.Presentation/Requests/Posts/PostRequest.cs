using System.ComponentModel.DataAnnotations;

namespace Movieminds.Presentation.Requests.Posts;

public class PostRequest
{
    [Required]
    public int? MovieId { get; set; }
    
    [Required]
    public string Content { get; set; }
}
