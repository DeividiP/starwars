using System;

namespace ResupplyStops.Application.Domain.Command
{
    public class StarShipResupplyStopsCalculateCommand
    {
        public int Distance { get; set; }

        public void Validate()
        {
            if (Distance < 0)
            {
                throw new ArgumentOutOfRangeException("distance", "The distance must be greater than zero.");
            }
        }
    }
}
