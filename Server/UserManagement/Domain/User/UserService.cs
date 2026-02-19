using Infrastructure.ExceptionHandling;
using Server.Infrastructure.ExectionHandling.ErrorMessage;
using Server.UserManagement.Domain.Abstract;

namespace Server.UserManagement.Domain;

public class UserService(IUserRepository userRepository, IUserFactory userFactory) : IUserService
{
    public async Task<string> SignInAsync(string email, string password)
    {
        if (email == "hbalta" && password == "12345")
        {
            return "success";
        }
        
        return"fail";
    }

    public Task SignOut()
    {
        throw new NotImplementedException();
    }

    public async Task<UserModel> GetUserAsync(long id)
    {
        var user = await userRepository.GetByIdAsync(id);

        return user ?? throw new ServiceException(ErrorDefinitions.UserNotFound);
    }

    public async Task SignUpAsync(string name, string surname, string email, string phoneNumber, string password)
    {
        var user = userFactory.GenerateUserModelSignUpToDB( name, surname, email, phoneNumber, password);
        
        await userRepository.InsertAsync(user);
    }
}