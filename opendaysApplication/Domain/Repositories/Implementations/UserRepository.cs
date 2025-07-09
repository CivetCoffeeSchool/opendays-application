using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;
using Model.Entities.Users;

namespace Domain.Repositories.Implementations;

public class UserRepository : ARepository<AUser>, IUserRepository
{
    private readonly TdoTDbContext _dbContext;

    public UserRepository(TdoTDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public AUser? GetByUsername(string username)
    {
        return _dbContext.Users
            .Include(u => u.Person)
            .FirstOrDefault(u => u.Username == username);
    }

    public AUser? GetByPersonId(string personId)
    {
        return _dbContext.Users
            .Include(u => u.Person)
            .FirstOrDefault(u => u.PersonId == personId);
    }

    public Admin? GetAdmins(string personId)
    {
        return _dbContext.Admins
            .Include(u => u.Person)
            .FirstOrDefault(u => u.PersonId == personId);
    }
    
    public NormalUser? GetNormalUsers(string personId)
    {
        return _dbContext.NormalUsers
            .Include(u => u.Person)
            .FirstOrDefault(u => u.PersonId == personId);
    }
    
    //TODO
    //change user to person to a 1-1 relationship
}