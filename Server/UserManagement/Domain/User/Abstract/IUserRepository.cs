namespace Server.UserManagement.Domain.Abstract;

public interface IUserRepository
{
    public Task<ulong> InsertAsync(UserModel user);
    
    public Task<UserModel?> GetByIdAsync(long id);
}