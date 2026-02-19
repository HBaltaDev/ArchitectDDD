using Infrastructure.Dto.Request;
using Infrastructure.DtoBase.ResponseBase;
using Infrastructure.ExceptionHandling;
using Server.Applications.UserManagement.Abstract;
using Server.Infrastructure.ExectionHandling.ErrorMessage;
using Server.Infrastructure.ExectionHandling.Localization;
using Server.UserManagement.Domain.Abstract;
using Server.UserManagement.Dto.Request;

namespace Server.UserManagement.Application;

public class UserManagementService(IUserService userService, IDtoAssembler dtoAssembler) : IUserManagementService
{
    public async Task<ResponseBase> SignInAsync(SignInRequest request)
    {
        //throw new ServiceException(StatusCodes.ALREADY_EXISTS, "Test");
        //throw new ServiceException(ErrorDefinitions.UserNotFound);
        
        var result = await userService.SignIn(request.Email, request.Password);
        
        return dtoAssembler.ToSignInResponse(result);
    }
    
    public async Task<ResponseBase> GetUserAsync(CommonIdRequest request)
    {
        var user = await userService.GetUser(request.Id);

        return dtoAssembler.ToGetUserResponse(user);
    }
    
    // public async Task<ResponseBase> GetUserList(GetListCommonRequest request)
    // {
    //     var (roleList, count) = await _roleService.GetRoleList(0, 10000);
    //
    //     var result = await _userService.GetUserList(request.PageNumber, request.PageSize);
    //
    //     return _dtoAssembler.ToGetUserListResponse(result, roleList);
    // }
}