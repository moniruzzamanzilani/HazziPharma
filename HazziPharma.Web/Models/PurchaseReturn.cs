using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class PurchaseReturn
    {
        public int Id { get; set; }

        [Required]
        public string ReturnNo { get; set; } = "";

        [Required]
        public int PurchaseId { get; set; }

        public Purchase? Purchase { get; set; }

        public DateTime ReturnDate { get; set; } = DateTime.Today;

        public decimal TotalAmount { get; set; }

        public string? Remarks { get; set; }

        public ICollection<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }
            = new List<PurchaseReturnDetail>();
    }
}