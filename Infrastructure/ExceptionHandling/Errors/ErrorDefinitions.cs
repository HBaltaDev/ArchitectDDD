namespace Server.Infrastructure.ExectionHandling.ErrorMessage;

public static class ErrorDefinitions
{
    public static readonly Error UserNotFound =
        new(StatusCodes.NOT_FOUND, "USER_NOT_FOUND");
}