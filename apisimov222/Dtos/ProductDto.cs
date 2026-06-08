namespace apisimov222.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = "";

        public string Article { get; set; } = "";

        public string Description { get; set; } = "";

        public decimal Amount { get; set; }

        public decimal Discount { get; set; }

        public decimal Count { get; set; }

        private string? Photo { get; set; }
    }
}
