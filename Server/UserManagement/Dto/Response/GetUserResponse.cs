using Infrastructure.DtoBase.ResponseBase;
using Infrastructure.ModelBase;

namespace Server.UserManagement.Dto.Response;

public record GetUserResponse : ResponseBase
{
    public long Id { get; set; }
    
    public string Username { get; set; } = default!;
    
    public string Name { get; set; } = default!;
    
    public string Surname { get; set; } = default!;
    
    public string EmailAddress { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public bool? IsTfaActive { get; set; }
    
    public int? TfaType { get; set; }

    public int[]? RoleIdList { get; set; }

    public byte PasswordExpireDuration { get; set; }
    
    public RecordInfo? RecordInfo { get; set; }
}