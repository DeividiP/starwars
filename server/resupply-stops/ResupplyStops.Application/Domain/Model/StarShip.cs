using ResupplyStops.Application.Domain.Services;

namespace ResupplyStops.Application.Domain.Model
{
    public class StarShip : IStarShip
    {
        public string Name { get; set; }
        public string MGLT { get; set; }
        public string Consumables { get; set; }
    }
}
