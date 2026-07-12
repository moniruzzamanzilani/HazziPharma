using HazziPharma.Web.Models;

namespace HazziPharma.Web.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }

        public int TotalSuppliers { get; set; }

        public int TotalPurchases { get; set; }

        public int TotalSales { get; set; }

        public decimal TotalPurchaseAmount { get; set; }

        public decimal TotalSalesAmount { get; set; }

        public int LowStockCount { get; set; }

        public int ExpiredMedicineCount { get; set; }

        public int ExpiringSoonCount { get; set; }
        public int TodayPurchaseCount { get; set; }

        public int TodaySalesCount { get; set; }

        public decimal TodayPurchaseAmount { get; set; }

        public decimal TodaySalesAmount { get; set; }
       
        public int TotalExpenses { get; set; }

        public decimal TotalExpenseAmount { get; set; }

        public decimal TodayExpenseAmount { get; set; }
        public List<Purchase> RecentPurchases { get; set; } = new();

        public List<Sale> RecentSales { get; set; } = new();

        public List<Expense> RecentExpenses { get; set; } = new();
        public List<Product> LowStockProducts { get; set; } = new();
        public List<ProductSalesViewModel> TopSellingProducts { get; set; } = new();
        public List<PurchaseDetail> ExpiringSoonMedicines { get; set; } = new();
    }
}