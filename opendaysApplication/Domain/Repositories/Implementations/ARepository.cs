using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;

namespace Domain.Repositories.Implementations;

public abstract class ARepository<TEntity> : IRepository<TEntity> where TEntity:class //Komplexer Datentyp
{
    private readonly TdoTDbContext _dbContext;
    protected readonly DbSet<TEntity> _Set;

    public ARepository(TdoTDbContext context)
    {
        _dbContext = context;
        _Set = context.Set<TEntity>();
    }
    
    public TEntity Create(TEntity t)
    {
        _Set.Add(t);
        _dbContext.SaveChanges();
        return t;
    }

    public List<TEntity> CreateRange(List<TEntity> list)
    {
        _Set.AddRange(list);
        _dbContext.SaveChanges();
        return list;
    }

    public void Update(TEntity t)
    {
        _Set.Update(t);
        _dbContext.SaveChanges();
    }

    public void UpdateRange(List<TEntity> list)
    {
        _Set.UpdateRange(list);
        _dbContext.SaveChanges();
    }

    public TEntity? Read(int id) => _Set.Find(id);//select * from T where id == id;
    
    public TEntity? Read(string id) => _Set.Find(id);//select * from T where id == id;

    public List<TEntity> Read(Expression<Func<TEntity, bool>> filter) => _Set.Where(filter).ToList();

    public List<TEntity> Read(int start, int count) => _Set.Skip(start).Take(count).ToList();

    public List<TEntity> ReadAll() => _Set.ToList();

    public void Delete(TEntity t)
    {
        _Set.Remove(t);
        _dbContext.SaveChanges();
    }
}