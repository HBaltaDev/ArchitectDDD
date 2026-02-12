using NetStructre.Applications.UserManagement.Abstract;
using NetStructre.Infrastructure.ExectionHandling;
using NetStructre.UserManagement.Domain.Abstract;
using NetStructre.UserManagement.Dto.Request;
using StatusCodes = NetStructre.Infrastructure.ExectionHandling.StatusCodes;

namespace NetStructre.Applications.UserManagement;

public class UserManagementService(IUserService userService) : IUserManagementService
{
    public async Task<string> SignInAsync(SignInRequest request)
    {
        throw new ServiceException(StatusCodes.ALREADY_EXISTS, "Test");
        
        return await userService.SignIn(request.Email, request.Password);
    }
}