namespace Domain.Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        public decimal TotalPay { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal Taxes { get; set; }
        public DateTime Date { get; set; }

        public ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
