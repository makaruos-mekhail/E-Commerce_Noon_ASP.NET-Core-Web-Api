namespace Domain.Entities
{
    public class ProductReview
    {
        
        public long Id { get; set; }
        
        public string Review { get; set; }
        
        public string ReviewAr { get; set; }
        
        public byte Rate { get; set; }
        
        public long UserId { get; set; }

        public User User { get; set; }
        
        public long ProductId { get; set; }
        
        public Product Product { get; set; }
    }
}
