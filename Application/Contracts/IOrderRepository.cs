using Domain.Entities;

namespace Application.Contracts
{
	public interface IOrderRepository : IRepository<Order, long>
    {
        Task<IEnumerable<Order>> GetAllOrder();
        //Task<Order> AddOreder(OrderDTO order);


    }
}
