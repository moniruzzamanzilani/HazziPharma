using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class PurchaseReturnDetail
    {
        public int Id { get; set; }

        [Required]
        public int PurchaseReturnId { get; set; }

        public PurchaseReturn? PurchaseReturn { get; set; }

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