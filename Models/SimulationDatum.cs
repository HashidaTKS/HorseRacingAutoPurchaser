using System;
using System.Collections.Generic;
using System.Text;

namespace HorseRacingAutoPurchaser.Models
{
    public class SimulationDatum
    {
        public RaceDataForComparison OutputRaceData { get; set; }
        public RaceResult RaceResult { get; set; }
        public List<BetDatum> BetData { get; set; }

        public SimulationDatum(RaceDataForComparison outputRaceData, RaceResult raceResult, List<BetDatum> betData)
        {
            OutputRaceData = outputRaceData;
            RaceResult = raceResult;
            BetData = betData;
        }
    }
}
