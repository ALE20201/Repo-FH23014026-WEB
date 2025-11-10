using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PP4App.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; } = "";

        public List<Title> Titles { get; set; } = new();
    }
}
