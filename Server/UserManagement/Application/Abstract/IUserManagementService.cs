using Infrastructure.Dto.Request;
using Infrastructure.DtoBase.ResponseBase;
using Server.UserManagement.Dto.Request;
using Server;

namespace Server.Applications.UserManagement.Abstract;

public interface IUserManagementService : IApplicationService
{
    Task<ResponseBase> SignInAsync(SignInRequest request);

    public Task<ResponseBase> SignUpAsync(SignUpRequest request);
    Task<ResponseBase> GetUserAsync(CommonIdRequest request);
}