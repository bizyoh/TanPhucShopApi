namespace TanPhucShopApi.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
