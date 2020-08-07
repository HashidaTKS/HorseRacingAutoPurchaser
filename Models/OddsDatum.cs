using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    [Serializable]
    public class OddsDatum
    {
        [DataMember]
        public List<HorseDatum> HorseData  { get; set; }

        [DataMember]
        public double Odds { get; set; }

        public OddsDatum(List<HorseDatum> horseData, double odds)
        {
            HorseData = horseData;
            Odds = odds;
        }


        public string GetHorsesString()
        {
            return string.Join(" - ", HorseData.Select(_ => _.Number));
        }

        public string GetOddsString()
        {
            return $"{Odds:F1}";
        }
    }
}
