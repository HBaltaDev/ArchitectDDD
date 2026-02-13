namespace Server.Infrastructure.ExectionHandling.Localization;

public interface IErrorLocalizer
{
    public string GetDescription(string description, string language);
}