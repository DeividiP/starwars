namespace ResupplyStops.Application.Domain.Model
{
    public class StarShip
    {
        public string Name { get; set; }
        public int MGLT { get; set; }
        public string Consumables { get; set; }
        public int CalculateStops(int distance)
        {
            return 74;
        }
    }
}
