using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories;

public class AssortmentRepository : IAssortmentRepository
{
    private readonly ApplicationDbContext _db;

    public AssortmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<bool> Create(Assortment entity)
    {
        var studia = await _db.Studia.FindAsync(entity.StudiaId);

        if (studia == null)
            return false;
        
        await _db.Assortments.AddAsync(entity);
        await _db.SaveChangesAsync();
        
        return true;
    }

    public Task<List<Assortment>> Select()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(int id)
    {
        var result = await _db.Assortments.FirstOrDefaultAsync(assortment => assortment.Id == id);
        if (result != null)
        {
            _db.Assortments.Remove(result);
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }
    
    public async Task<bool> Update(Assortment entity)
    {
        _db.Assortments.Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Assortment>> GetAssortmentsWithKey(int id)
    {
        return _db.Assortments
            .Where(_ => _.StudiaId == id)
            .ToListAsync();
    }

    public async Task<Assortment> GetById(int id)
    {
        try
        {
            var response = await _db.Assortments.FirstOrDefaultAsync(assortment => assortment.Id == id);

            if (response != null)
            {
                return response;
            }

            response.Name = "Произошла ошибка";
            
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}