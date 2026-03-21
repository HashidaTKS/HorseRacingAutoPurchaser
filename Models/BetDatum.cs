using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using HorseRacingAutoPurchaser.Utils;


namespace HorseRacingAutoPurchaser.Models
{
    [DataContract]
    public class BetDatum : IEquatable<BetDatum>
    {
        public RaceData RaceData { get; set; }

        [DataMember]
        public List<int> HorseNumList { get; set; }
        [DataMember]
        public int BetMoney { get; set; }
        [DataMember]
        public TicketType TicketType { get; set; }
        [DataMember]
        public double ActualOdds { get; set; }
        [DataMember]
        public double TheoreticalOdds { get; set; }

        public double OddsRatio => TheoreticalOdds > 0 ? ActualOdds / TheoreticalOdds : Int32.MaxValue;
        
        public BetDatum(RaceData raceData, List<int> horseNumList, int betYen, double actualOdds, double theoreticalOdds, TicketType ticketType)
        {
            RaceData = raceData;
            HorseNumList = horseNumList;
            BetMoney = betYen;
            ActualOdds = actualOdds;
            TheoreticalOdds = theoreticalOdds;
            TicketType = ticketType;
        }

        public bool Equals(BetDatum other)
        {
            if (other == null) return false;
            return TicketType == other.TicketType
                && HorseNumList != null
                && other.HorseNumList != null
                && HorseNumList.SequenceEqual(other.HorseNumList);
        }

        public override bool Equals(object obj) => Equals(obj as BetDatum);

        public override int GetHashCode()
        {
            var hash = TicketType.GetHashCode();
            if (HorseNumList != null)
            {
                foreach (var num in HorseNumList)
                {
                    hash = HashCode.Combine(hash, num);
                }
            }
            return hash;
        }
    }
}
