using Infrastructure.ExceptionHandling;
using Server.Infrastructure.ExectionHandling.ErrorMessage;
using Server.UserManagement.Domain.Abstract;

namespace Server.UserManagement.Domain;

public class UserService(IUserRepository userRepository, IUserFactory userFactory) : IUserService
{
    public async Task<string> SignIn(string email, string password)
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

    public async Task<UserModel> GetUser(long id)
    {
        var user = await userRepository.GetById(id);

        return user ?? throw new ServiceException(ErrorDefinitions.UserNotFound);
    }
}