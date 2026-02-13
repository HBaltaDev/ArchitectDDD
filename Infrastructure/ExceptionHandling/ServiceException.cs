using Server.Infrastructure.ExectionHandling;
using Server.Infrastructure.ExectionHandling.ErrorMessage;
using Server.Infrastructure.ExectionHandling.Localization;

namespace Infrastructure.ExceptionHandling;

public class ServiceException(Error error, string? language = null) : Exception
{ 
     private static IErrorLocalizer? _localizer;
     public static void Configure(IErrorLocalizer localizer) => _localizer = localizer;
     public StatusCodes StatusCode { get; } = error.StatusCode;
     public string Description { get; } = _localizer!.GetDescription(error.Description, language ?? "en");
} 