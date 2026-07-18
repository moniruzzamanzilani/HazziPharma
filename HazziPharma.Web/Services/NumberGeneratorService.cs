using HazziPharma.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace HazziPharma.Web.Services
{
    public class NumberGeneratorService
    {
        private readonly HazziPharmaDbContext _context;

        public NumberGeneratorService(HazziPharmaDbContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateSaleNoAsync()
        {
            var saleNumbers = await _context.Sales
                .Where(x => !string.IsNullOrEmpty(x.SaleNo))
                .Select(x => x.SaleNo!)
                .ToListAsync();

            int maxNumber = saleNumbers
                .Select(x =>
                {
                    var digits = new string(x.Where(char.IsDigit).ToArray());
                    return int.TryParse(digits, out int n) ? n : 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            return $"SAL-{(maxNumber + 1):D6}";
        }
        public async Task<string> GenerateInvoiceNoAsync()
        {
            var InvoiceNumbers = await _context.Sales
                .Where(x => !string.IsNullOrEmpty(x.InvoiceNo))
                .Select(x => x.SaleNo!)
                .ToListAsync();

            int maxNumber = InvoiceNumbers
                .Select(x =>
                {
                    var digits = new string(x.Where(char.IsDigit).ToArray());
                    return int.TryParse(digits, out int n) ? n : 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            return $"INV-{(maxNumber + 1):D6}";
        }
        public async Task<string> GeneratePurchaseNoAsync()
        {
            var purchaseNumbers = await _context.Purchases
                .Where(x => !string.IsNullOrEmpty(x.PurchaseNo))
                .Select(x => x.PurchaseNo!)
                .ToListAsync();

            int maxNumber = purchaseNumbers
                .Select(x =>
                {
                    var digits = new string(x.Where(char.IsDigit).ToArray());
                    return int.TryParse(digits, out int n) ? n : 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            return $"PUR-{(maxNumber + 1):D6}";
        }
        public async Task<string> GeneratePurchaseInvoiceNoAsync()
        {
            var invoiceNumbers = await _context.Purchases
                .Where(x => !string.IsNullOrEmpty(x.InvoiceNo))
                .Select(x => x.InvoiceNo!)
                .ToListAsync();

            int maxNumber = invoiceNumbers
                .Select(x =>
                {
                    var digits = new string(x.Where(char.IsDigit).ToArray());
                    return int.TryParse(digits, out int n) ? n : 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            return $"PINV-{(maxNumber + 1):D6}";
        }
        public async Task<string> GeneratePurchaseReturnNoAsync()
        {
            var numbers = await _context.PurchaseReturns
                .Where(x => !string.IsNullOrEmpty(x.ReturnNo))
                .Select(x => x.ReturnNo!)
                .ToListAsync();

            int maxNumber = numbers
                .Select(x =>
                {
                    var digits = new string(x.Where(char.IsDigit).ToArray());
                    return int.TryParse(digits, out int n) ? n : 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            return $"PR-{(maxNumber + 1):D6}";
        }
    }
}