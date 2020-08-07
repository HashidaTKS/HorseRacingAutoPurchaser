using System;
using System.Collections.Generic;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    public class BetInformation 
    {
        public RaceData RaceData { get; set; }
        public List<int> HorseNumList { get; set; }
        public int BetMoney { get; set; }
        public TicketType TicketType { get; set; }
        public double ActualOdds { get; set; }
        public double TheoreticalOdds { get; set; }
        public double OddsRatio => TheoreticalOdds > 0 ? ActualOdds / TheoreticalOdds : Int32.MaxValue;
        

        public BetInformation(RaceData raceData, List<int> horseNumList, int betYen, double actualOdds, double theoreticalOdds, TicketType ticketType)
        {
            RaceData = raceData;
            HorseNumList = horseNumList;
            BetMoney = betYen;
            ActualOdds = actualOdds;
            TheoreticalOdds = theoreticalOdds;
            TicketType = ticketType;
        }
    }
}
