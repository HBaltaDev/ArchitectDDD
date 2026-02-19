namespace Server.UserManagement.Domain.Abstract;

public interface IUserRepository
{
    public Task<UserModel?> GetById(long id);
}