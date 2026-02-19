using Infrastructure.ModelBase;
using Server.UserManagement.Domain.ValueObjects;

namespace Server.UserManagement.Domain;

public class UserModel : ModelBase
{
    public long Id { get; set; }
    
    public string Username { get; set; }
    
    public string Name { get; set; }

    public string Surname { get; set; }
    
    public string Password { get; set; }
    
    public string EmailAddress { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string? LastSecondPassword { get; set; }

    public string? LastThirdPassword { get; set; }

    public TfaType? TfaType { get; set; }
    
    public bool? IsTfaActive { get; set; }
    
    public string? TfaSecret { get; set; }

    public int[] RoleIdList { get; set; }

    public DateTime? PasswordChangedDate { get; set; }

    public byte PasswordExpireDuration { get; set; }

    public bool IsOneTimePassword { get; set; }

    public DateTime? SessionExpireDuration { get; set; }
    
    public bool IsDeleted { get; set; }
}