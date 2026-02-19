using Infrastructure.ModelBase;
using Server.UserManagement.Domain.Abstract;

namespace Server.UserManagement.Domain;

public class UserFactory : IUserFactory
{
    public UserModel GenerateUserModelSignUpToDB(string name, string surname, string emailAddress, string phoneNumber, string password)
    {
        return new UserModel
        {
            Username = name + "@" + surname,
            Name = name,
            Surname = surname,
            EmailAddress = emailAddress,
            PhoneNumber = phoneNumber,
            Password = password,
            RoleIdList = new int[2],
            PasswordExpiration = 6,
            IsTFAActive = false,
            IsDeleted = false,
            RecordInfo = new RecordInfo()
        };
    }
}