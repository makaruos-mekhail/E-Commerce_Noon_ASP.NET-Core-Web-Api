using Domain.Entities;

namespace Application.Contracts
{
	public interface IOrderRepository : IRepository<Order, long>
    {
        Task<IEnumerable<Order>> GetAllOrder(string userEmail,string status);
        //Task<Order> AddOreder(OrderDTO order);


    }
}
