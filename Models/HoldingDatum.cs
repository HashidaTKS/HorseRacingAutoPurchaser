﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    /// <summary>
    /// 大会の開催情報。場所、回数、日数、日時など。
    /// </summary>
    [DataContract]
    [Serializable]
    public class HoldingDatum : IEquatable<HoldingDatum>
    {
        [DataMember]
        public Region Region { get; set; }

        /// <summary>
        /// 開催回数
        /// </summary>
        [DataMember]
        public int NumberOfHeld { get; set; }

        /// <summary>
        /// 何日目か
        /// </summary>
        [DataMember]
        public int NumberOfDay { get; set; }
        /// <summary>
        /// 開催日
        /// </summary>
        [DataMember]
        public DateTime HeldDate { get; set; }

        [DataMember]
        public List<DateTime> StartTimeList { get; set; }

        [DataMember]
        public List<int> HorseCountList { get; set; }

        public int TotalRaceCount => StartTimeList.Count;

        public bool HasFullData => !string.IsNullOrEmpty(Region.RegionId) && NumberOfHeld > 0 && NumberOfDay > 0 && HeldDate > DateTime.MinValue && StartTimeList?.Count > 0;

        public HoldingDatum(Region region, int numberOfHeld, int numberOfDay, DateTime heldDate, List<DateTime> startTimeList, List<int> horseCountList)
        {
            Region = region;
            NumberOfHeld = numberOfHeld;
            NumberOfDay = numberOfDay;
            HeldDate = heldDate;
            StartTimeList = startTimeList;
            HorseCountList = horseCountList;
        }

        public HoldingDatum(string regionName,int numberOfHeld, int numberOfDay, DateTime heldDate, List<DateTime> startTimeList, List<int> horseCountList) 
            : this(Utility.GetRegionFromName(regionName), numberOfHeld, numberOfDay, heldDate, startTimeList, horseCountList)
        {
        }

        public bool Equals(HoldingDatum holdingDatum)
        {
            return Region.RegionId == holdingDatum.Region.RegionId && NumberOfHeld == holdingDatum.NumberOfHeld && NumberOfDay == holdingDatum.NumberOfDay && HeldDate.Date == holdingDatum.HeldDate.Date;
        }

        public override int GetHashCode()
        {
            return Region.GetHashCode() ^ NumberOfHeld.GetHashCode() ^ NumberOfDay.GetHashCode() ^ HeldDate.GetHashCode();
        }
    }
}
