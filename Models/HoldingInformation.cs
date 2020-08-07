using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    [Serializable]
    public class HoldingInformation
    {
        [DataMember]
        public List<HoldingDatum> HoldingData { get; set; }

        public HoldingInformation(List<HoldingDatum> holdingData)
        {
            HoldingData = holdingData;
        }

        public HoldingDatum GetHoldingDatum(DateTime date, string regionId)
        {
            return HoldingData.FirstOrDefault(_ => _.Region.RegionId == regionId && _.HeldDate.Date == date.Date);
        }

        public HoldingInformation MargeStatus(HoldingInformation holdingInformation)
        {
            if (holdingInformation == null)
            {
                return this;
            }
            HoldingData.AddRange(holdingInformation.HoldingData);
            HoldingData = HoldingData.Distinct().ToList();
            return this;
        }
    }
}
