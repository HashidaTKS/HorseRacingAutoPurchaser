using System;
using System.Collections.Generic;
using System.Text;

namespace HorseRacingAutoPurchaser.Models
{
    public class SimulationDatum
    {
        public RaceDataForComparison OutputRaceData { get; set; }
        public RaceResult RaceResult { get; set; }
        public List<BetInformation> BetInformationList { get; set; }

        public SimulationDatum(RaceDataForComparison outputRaceData, RaceResult raceResult, List<BetInformation> betInformationList)
        {
            OutputRaceData = outputRaceData;
            RaceResult = raceResult;
            BetInformationList = betInformationList;
        }
    }
}
