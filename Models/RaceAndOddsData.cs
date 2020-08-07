using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    [Serializable]
    public abstract class RaceAndOddsData
    {
        [DataMember]
        public RaceData BaseRaceData { get; set; }

        [DataMember]
        public List<OddsDatum> WinOdds { get; set; }

        [DataMember]
        public List<OddsDatum> ExactaOdds { get; set; }

        [DataMember]
        public List<OddsDatum> TrifectaOdds { get; set; }

        [DataMember]
        public List<OddsDatum> QuinellaOdds { get; set; }

        [DataMember]
        public List<OddsDatum> TrioOdds { get; set; }

        [DataMember]
        public DateTime LastUpadateUtcTime;

        public RaceAndOddsData(RaceData raceData)
        {
            BaseRaceData = raceData;
        }
    }
}
