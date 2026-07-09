using System.ComponentModel.DataAnnotations.Schema;

namespace HazziPharma.Web.Models
{
    public class SaleDetail
    {
        public int Id { get; set; }

        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public decimal SalePrice { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }

        public decimal SubTotal { get; set; }

        // Navigation
        [ForeignKey(nameof(SaleId))]
        public Sale Sale { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}