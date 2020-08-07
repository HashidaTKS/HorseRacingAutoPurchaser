using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    [Serializable]
    [DataContract]
    public class RaceResult
    {

        [DataMember]
        public RaceData RaceData { get; set; }

        [DataMember]
        public double WinPayout { get; set; }

        [DataMember]
        public double QuinellaPayout { get; set; }

        [DataMember]
        public double ExtractPayout { get; set; }

        [DataMember]
        public double TrioPayout { get; set; }

        [DataMember]
        public double TrifectaPayout { get; set; }

        [DataMember]
        public List<int> WinHorse { get; set; }

        [DataMember]
        public List<int> QuinellaHorseList { get; set; }

        [DataMember]
        public List<int> ExtractHorseList { get; set; }

        [DataMember]
        public List<int> TrioHorseList { get; set; }

        [DataMember]
        public List<int> TrifectaHorseList { get; set; }

        public RaceResult(RaceData raceData)
        {
            RaceData = raceData;
        }

        public (List<int>, double) GetResultHorseAndPayoutOfTicket(TicketType ticketType)
        {
            switch (ticketType)
            {
                default:
                case TicketType.Win:
                    return (WinHorse, WinPayout);
                case TicketType.Quinella:
                    return (QuinellaHorseList, QuinellaPayout);
                case TicketType.Exacta:
                    return (ExtractHorseList, ExtractPayout);
                case TicketType.Trio:
                    return (TrioHorseList, TrioPayout);
                case TicketType.Trifecta:
                    return (TrifectaHorseList, TrifectaPayout);
            }
        }

        public RaceResultRepository GetRepository()
        {
            return GetRepository(RaceData);
        }

        public static RaceResultRepository GetRepository(RaceData raceData)
        {
            var path = Path.Combine(
            "RaceData",
            raceData.HoldingDatum.HeldDate.ToString("yyyyMMdd"),
            "ResultData",
            $"{raceData.HoldingDatum.Region.RegionId}-{raceData.HoldingDatum.Region.RegionName}-{raceData.RaceNumber}.xml");
            return new RaceResultRepository(path);
        }
    }
}
