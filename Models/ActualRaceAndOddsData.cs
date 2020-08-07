using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    [Serializable]
    public class ActualRaceAndOddsData : RaceAndOddsData
    {
        public ActualRaceAndOddsData(RaceData raceData) : base(raceData)
        {
        }

        public void SetData(Scraper scraper)
        {
            WinOdds = scraper.GetOdds(BaseRaceData, TicketType.Win).OrderBy(_ => _.Odds).ToList();
            QuinellaOdds = scraper.GetOdds(BaseRaceData, TicketType.Quinella).OrderBy(_ => _.Odds).ToList();
            //今は使わないので敢えて取得しない
            //ExactaOdds = scraper.GetOdds(RaceUrlInformation, TicketType.Exacta).OrderBy(_ => _.Odds).ToList();
            //TrifectaOdds = scraper.GetOdds(RaceUrlInformation, TicketType.Trifecta).OrderBy(_ => _.Odds).ToList();
            //TrioOdds = scraper.GetOdds(RaceUrlInformation, TicketType.Trio).OrderBy(_ => _.Odds).ToList();
            //LastUpadateUtcTime = DateTime.UtcNow;
        }

        public ActualRaceDataRepository GetRepository()
        {
            return GetRepository(BaseRaceData);
        }

        public static ActualRaceDataRepository GetRepository(RaceData raceData)
        {
            var path = Utility.GetRaceAndOddsDataFilePath(raceData, OddsType.Actual);
            return new ActualRaceDataRepository(path);
        }
    }
}
