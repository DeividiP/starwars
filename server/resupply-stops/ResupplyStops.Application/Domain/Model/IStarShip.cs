namespace ResupplyStops.Application.Domain.Model
{
    public interface IStarShip
    {
        string Name { get; set; }
        int MGLT { get; set; }
        string Consumables { get; set; }

        int CalculateStops(int distance);
    }
}
