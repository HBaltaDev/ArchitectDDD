namespace Server.UserManagement.Domain.Abstract;

public interface IUserService
{
    Task<string> SignInAsync(string email, string password);
    Task SignOut();

    public Task<UserModel> GetUserAsync(long id);
    
    public Task SignUpAsync(string name, string surname, string email, string phoneNumber, string password);
}