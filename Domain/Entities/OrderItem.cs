namespace Domain.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }

        public decimal Price { get; set; }

        public string PaymentMethod { get; set; }

        public string Address { set; get; }

        public string AddressAr { set; get; }

        //public string UserId { get; set; }

        //public virtual User User { get; set; }

        public int OrderId { get; set; }

		public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }


    }
}
