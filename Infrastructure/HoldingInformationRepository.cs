using System;
using System.Linq;
using HorseRacingAutoPurchaser.Models;


namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class HoldingInformationRepository : BaseRepository<HoldingInformation> { 

        public HoldingInformationRepository() : base("holding_info.xml")
        {
        }

        public HoldingDatum ReadFromDateAndRegion(DateTime date , Region region)
        {
            lock (this)
            {
                return ReadAll()?.HoldingData?.FirstOrDefault(_ => _.HeldDate.Date == date.Date && _.Region.RegionId == region.RegionId);
            }
        }
    }
}
