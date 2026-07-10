using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Sale Price")]
        public decimal SalePrice { get; set; }

        public int Stock { get; set; }

        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }
        public int? GenericId { get; set; }

        public int? CompanyId { get; set; }

        public int? CategoryId { get; set; }
        public Generic? Generic { get; set; }

        public Company? Company { get; set; }

        public Category? Category { get; set; }
        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
    }
}