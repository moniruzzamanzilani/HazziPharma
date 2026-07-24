using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }

        public Purchase? Purchase { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }
        public int RemainingQty { get; set; }

        public string? BatchNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        public decimal SubTotal { get; set; }
    }
}