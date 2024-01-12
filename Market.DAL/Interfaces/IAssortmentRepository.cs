using Market.Domain.Entity;

namespace Market.DAL.Interfaces;

public interface IAssortmentRepository : IBaseRepository<Assortment>
{
    Task<List<Assortment>> GetAssortmentsWithKey(int id);

    Task<Assortment> GetById(int id);
}