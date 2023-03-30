namespace Reposatory.DTOs
{
	public class OrderDTO
	{
		public class OrderItemDTO
		{
			public long Id { get; set; }
			public int Quantity { get; set; }
			public long ProductId { get; set; }
		}		

		public OrderItemDTO orderItem { get; set; }
        public long UserId { get; set; }

		public ICollection<OrderItemDTO> OrderItemsDTO { get; set; }

		public string Address { set; get; }

		public string AddressAr { set; get; }

		public decimal TotalPrice { get; set; }

		public int? Discount { get; set; }

		public string PaymentMethod { get; set; }

	}
}
