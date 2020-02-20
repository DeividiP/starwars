namespace ResupplyStops.Application.Domain.Model
{
    public class StarShip
    {
        public string Name { get; set; }
        public int MGLT { get; set; }
        public string Consumables { get; set; }
        public int CalculateStops(int distance)
        {
            var mgltAutonomy = CalculateMGLTAutonomy();

            return distance / mgltAutonomy;
        }

        private int CalculateMGLTAutonomy()
        {
            var consumablesInHours = ConvertConsumablesPeriodToHours();

            return consumablesInHours * MGLT;
        }

        private int ConvertConsumablesPeriodToHours()
        {
            return 7 * 24;
        }
    }
}
