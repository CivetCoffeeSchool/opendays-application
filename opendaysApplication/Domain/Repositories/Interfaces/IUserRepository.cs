using Model.Entities.Users;

namespace Domain.Repositories.Interfaces;

public interface IUserRepository:IRepository<AUser>
{
    AUser? GetByUsername(string username);
    AUser? GetByPersonId(string personId);

    public Admin? GetAdmins(string personId);
    public NormalUser? GetNormalUsers(string personId);
}