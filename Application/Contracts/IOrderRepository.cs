using Domain.Entities;

namespace Application.Contracts
{
	public interface IOrderRepository : IRepository<Order, long>
    {
        Task<Order> GetAllOrder(string userEmail,string status);
        Task<IEnumerable< Order>>GetAllUserOrders(string userEmail);
        Task<bool> CancelOrder(long orderid);
    }
}
