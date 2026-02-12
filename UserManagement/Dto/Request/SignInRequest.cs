using NetStructre.Infrastructure.Dto.RequestBase;

namespace NetStructre.UserManagement.Dto.Request;

public record SignInRequest(string Email, string Password) : RequestBase;
