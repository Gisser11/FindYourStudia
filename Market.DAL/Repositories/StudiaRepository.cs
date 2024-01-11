using Market.DAL.Interfaces.IServices;
using Market.Domain.Entity;
using Market.Domain.ViewModels.StudiaViewModel;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories;

public class StudiaRepository : IStudiaRepository
{
    #region _dbContext
    private readonly ApplicationDbContext _db;
    
    public StudiaRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    #endregion

    #region StudiaMethods
    
    // сделать фронтенд. ext js отображение текущего автосервиса, вход, 
    // отображение ассортимента, редактирование и удаление ассортимента. 
    public async Task<bool> Create(Studia entity)
    {
        try
        {
            await _db.Studia.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"StudiaRepository [create ] - {ex.Message}");
            return false;
        }
    }
    
    public async Task<List<Studia>> Select()
    {
        return await _db.Studia.Include(x => x.Assortments).ToListAsync(); 
    }
    
    public async Task<bool> Delete(int id)
    {
        var entity = GetById(id);
        
        _db.Studia.Remove(entity);
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> Update(Studia entity)
    {
        _db.Studia.Update(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public Studia? GetById(int id)
    {
        return _db.Studia
            .Include(s => s.Assortments) 
            .FirstOrDefault(u => u.ManagerId == id);
    }

    
    #endregion
}