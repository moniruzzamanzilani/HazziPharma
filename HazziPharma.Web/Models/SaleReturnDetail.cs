using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class SaleReturnDetail
    {
        public int Id { get; set; }

        [Required]
        public int SaleReturnId { get; set; }

        public SaleReturn? SaleReturn { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Required]
        public decimal ReturnPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }
    }
}