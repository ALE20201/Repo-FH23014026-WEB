using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PP4App.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public string TagName { get; set; } = "";

        public List<TitleTag> TitleTags { get; set; } = new();
    }
}
