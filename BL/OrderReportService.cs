using Domains;
namespace BL
{
    public class OrderReportService
    {
        // i use an order business layer 
        private readonly Business<Order> _orderBL;
        // make it depend on it to link in Ctor
        public OrderReportService(Business<Order> order)
        {
            _orderBL = order;
        }

        public async Task<TotalSalesSummary> GetTotalSalesSummary()
        {
            try
            {
                Console.Clear();
                var orders = await _orderBL.GetAll();

                return new TotalSalesSummary
                {
                    TotalSales = orders.Sum(o => o.Price * o.Quantity),
                    OrdersCount = orders.Count()
                };

            }
            catch (BusinessLayerException ex)
            {
                throw;
                Console.WriteLine("Business Layer Exception: " + ex.Message);
            }
        }

        public async Task<List<SalesByCustomer>> GetSalesByCustomer()
        {
            try
            {
                Console.Clear();
                var orders = await _orderBL.GetAll();
                //LINQ statement
                return orders
                    .GroupBy(o => new { o.CustomerId, o.CustomerName })
                    .Select(g => new SalesByCustomer
                    {
                        CustomerId = g.Key.CustomerId,
                        CustomerName = g.Key.CustomerName,
                        OrderCount = g.Count(),
                        TotalRevenu = g.Sum(o => o.Price * o.Quantity)
                    })
                    .OrderByDescending(o => o.TotalRevenu)
                    .ToList();
            }
            catch (BusinessLayerException ex)
            {
                throw;
                Console.WriteLine($"Business Layer Exception: {ex.Message}");

            }
        }

        public async Task<List<SalesByItem>> GetSalesByItem()
        {
            try
            {
                Console.Clear();
                var orders = await _orderBL.GetAll();
                //LINQ statement
                return orders
                    .GroupBy(o => new { o.itemId, o.ItemName })
                    .Select(g => new SalesByItem
                    {
                        ItemId = g.Key.itemId,
                        ItemName = g.Key.ItemName,
                        TotalQuantity = g.Sum(o=>o.Quantity),
                        TotalRevenu = g.Sum(o => o.Price * o.Quantity)
                    })
                    .OrderByDescending(o => o.TotalRevenu)
                    .ToList();
            }
            catch (BusinessLayerException ex)
            {
                throw;
                Console.WriteLine($"Business Layer Exception: {ex.Message}");

            }
        }

        public async Task<List<SalesByCustomer>> GetTopSalesByCustomer(int top)
        {
            var report = await GetSalesByCustomer();
            //take() take the the number of items in the list to pick
            return report.Take(top).ToList();
        }

        public async Task<List<SalesByItem>> GetTopSalesByItem(int top)
        {
            var report = await GetSalesByItem();
            //take() take the the number of items in the list to pick
            return report.Take(top).ToList();
        }
    }
}
