using System;
using System.Linq;

namespace HorseRacingAutoPurchaser
{
    public class HoldingInformationRepository : BaseRepository<HoldingInformation> { 

        public HoldingInformationRepository() : base("holding_info.xml")
        {
        }

        public HoldingDatum ReadFromDateAndRegion(DateTime date , Region region)
        {
            lock (this)
            {
                return ReadAll()?.HoldingData?.FirstOrDefault(_ => _.HeldDate == date && _.Region.RegionId == region.RegionId);
            }
        }
    }
}
