using System.ComponentModel.DataAnnotations;

namespace PP4App.Models
{
    public class TitleTag
    {
        [Key]
        public int TitleTagId { get; set; }

        [Required]
        public int TitleId { get; set; }
        public Title? Title { get; set; }

        [Required]
        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
