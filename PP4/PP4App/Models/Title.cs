using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PP4App.Models
{
    public class Title
    {
        [Key]
        public int TitleId { get; set; }

        [Required]
        public string TitleName { get; set; } = "";

        [Required]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public List<TitleTag> TitleTags { get; set; } = new();
    }
}
