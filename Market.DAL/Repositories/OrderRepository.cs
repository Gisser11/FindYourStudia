using Market.DAL.Interfaces;
using Market.Domain.Entity;

namespace Market.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _db;
    
    public OrderRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<bool> Create(Order entity)
    {
        try
        {
            _db.Orders.Add(entity);
            foreach (var orderDetail in entity.OrderDetails)
            {
                _db.OrderDetails.Add(orderDetail); 
            }
            _db.SaveChanges();
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Task<List<Order>> Select()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Order entity)
    {
        throw new NotImplementedException();
    }
}