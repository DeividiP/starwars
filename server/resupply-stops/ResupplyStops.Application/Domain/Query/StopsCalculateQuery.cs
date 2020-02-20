namespace ResupplyStops.Application.Domain.Query
{
    public class ShipStopsCalculateQuery
    {
        public string ShipName { get; set; }
        public int Distance { get; set; }
        public int Stops { get; set; }
    }
}
