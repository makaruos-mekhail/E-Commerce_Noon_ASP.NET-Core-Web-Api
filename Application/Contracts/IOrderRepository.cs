using Domain.Entities;

namespace Application.Contracts
{
	public interface IOrderRepository : IRepository<Order, long>
    {
        Task<Order> GetAllOrder(string userEmail,string status);
    }
}
