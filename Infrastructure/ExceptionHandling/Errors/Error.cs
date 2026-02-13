namespace Server.Infrastructure.ExectionHandling.ErrorMessage;

public sealed record Error(
    StatusCodes StatusCode,
    string Description
);