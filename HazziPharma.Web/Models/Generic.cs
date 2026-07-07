using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class Generic
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;
    }
}