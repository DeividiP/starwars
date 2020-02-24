using ResupplyStops.Application.Domain.Services;

namespace ResupplyStops.Application.Domain.Model
{
    public interface IStarShip
    {
        string Name { get; set; }
        string MGLT { get; set; }
        string Consumables { get; set; }
    }
}
