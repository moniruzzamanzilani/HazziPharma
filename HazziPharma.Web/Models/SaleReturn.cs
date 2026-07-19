using System.ComponentModel.DataAnnotations;

namespace HazziPharma.Web.Models
{
    public class SaleReturn
    {
        public int Id { get; set; }

        [Required]
        public string ReturnNo { get; set; } = "";

        [Required]
        public int SaleId { get; set; }

        public Sale? Sale { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public string? Remarks { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<SaleReturnDetail> SaleReturnDetails { get; set; }
            = new List<SaleReturnDetail>();
    }
}