namespace ResupplyStops.Application.Domain.Services
{
    public interface IConsumableToHoursConvert
    {
        bool CanConvert(string consumable);
        int Convert(string consumable);
    }
}
