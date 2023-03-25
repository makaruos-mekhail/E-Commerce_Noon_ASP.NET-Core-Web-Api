namespace Domain.Entities
{
    public class Order
    {
        public long Id { get; set; }

		public bool IsClosed { get; set; }

		public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public int? Discount { get; set; }

		public virtual ICollection<Product> Product { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

    }
}
