using Infrastructure.Dto.Response;
using Server.UserManagement.Domain;
using Server.UserManagement.Domain.User;
using Server.UserManagement.Dto.Request;
using Server.UserManagement.Dto.Response;

namespace Server.Applications.UserManagement.Abstract;

public interface IDtoAssembler
{
    public VoidResponse BuildVoidResponse();
    
    public SignInResponse ToSignInResponse(string email);

    public GetUserResponse ToGetUserResponse(UserModel user);
}