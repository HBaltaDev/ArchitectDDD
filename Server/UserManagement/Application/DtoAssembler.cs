using Server.Applications.UserManagement.Abstract;
using Server.UserManagement.Domain;
using Server.UserManagement.Domain.User;
using Server.UserManagement.Dto.Response;

namespace Server.Applications.UserManagement;

public class DtoAssembler : IDtoAssembler
{
    public SignInResponse ToSignInResponse(string email)
    {
        return new SignInResponse { Email = email };
    }
    
    public GetUserResponse ToGetUserResponse(UserModel user)
    {
        return new GetUserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            EmailAddress = user.EmailAddress,
            PhoneNumber = user.PhoneNumber,
            IsTfaActive = user.IsTfaActive,
            TfaType = user.TfaType.HasValue ? (int)user.TfaType.Value : null,
            RoleIdList = user.RoleIdList,
            PasswordExpireDuration = user.PasswordExpireDuration,
            RecordInfo = user.RecordInfo
        };
    }
}