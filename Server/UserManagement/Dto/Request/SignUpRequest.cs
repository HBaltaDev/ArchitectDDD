using Infrastructure.DtoBase.RequestBase;

namespace Server.UserManagement.Dto.Request;

public record SignUpRequest : RequestBase
{
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string EmailAddress { get; set; }
    
    public string Password { get; set; }
}