namespace Server.UserManagement.Domain.Abstract;

public interface IUserFactory
{
    public UserModel GenerateUserModelSignUpToDB(string name, string surname, string emailAddress, string phoneNumber, string password);
}