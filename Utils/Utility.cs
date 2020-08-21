using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace HorseRacingAutoPurchaser
{
    /// <summary>
    /// クラスを作るまでもないユーティリティ
    /// </summary>
    public static class Utility
    {
        private static readonly List<Region> RegionInforomation = new List<Region>()
        {
            //パフォーマンスが悪そうなら辞書化する
            new Region("札幌", "01", RegionType.Central),
            new Region("函館", "02", RegionType.Central),
            new Region("福島", "03", RegionType.Central),
            new Region("新潟", "04", RegionType.Central),
            new Region("東京", "05", RegionType.Central),
            new Region("中山", "06", RegionType.Central),
            new Region("中京", "07", RegionType.Central),
            new Region("京都", "08", RegionType.Central),
            new Region("阪神", "09", RegionType.Central),
            new Region("小倉", "10", RegionType.Central),
            new Region("帯広", "65", RegionType.Regional),
            new Region("門別", "30", RegionType.Regional),
            new Region("盛岡", "35", RegionType.Regional),
            new Region("水沢", "36", RegionType.Regional),
            new Region("浦和", "42", RegionType.Regional),
            new Region("船橋", "43", RegionType.Regional),
            new Region("大井", "44", RegionType.Regional),
            new Region("川崎", "45", RegionType.Regional),
            new Region("金沢", "46", RegionType.Regional),
            new Region("笠松", "47", RegionType.Regional),
            new Region("名古屋", "48", RegionType.Regional),
            new Region("園田", "50", RegionType.Regional),
            new Region("姫路", "51", RegionType.Regional),
            new Region("高知", "54", RegionType.Regional),
            new Region("佐賀", "55", RegionType.Regional),
        };

        public static string GetRegionNameAtRakuten(Region region)
        {
            if (region.RegionId == "65")
            {
                return "帯広ば";
            }
            else
            {
                return region.RegionName;
            }
        }

        public static Region GetRegionFromName(string regionName)
        {
            //念のため部分一致にしておく。どうにかしたい。
            return RegionInforomation.FirstOrDefault(_ => regionName.Contains(_.RegionName));
        }

        public static Region GetRegionFromId(string id)
        {
            return RegionInforomation.FirstOrDefault(_ => id == _.RegionId);
        }

        public static RegionType GetRegionType(string regionName)
        {
            return RegionInforomation.FirstOrDefault(_ => regionName.Contains(_.RegionName)).RagionType;
        }

        public static Dictionary<TicketType, string> TicketTypeToUrlString = new Dictionary<TicketType, string>
        {
            { TicketType.Win , "type=b1" },
            { TicketType.Exacta , "type=b6" },
            { TicketType.Quinella , "type=b4" },
            { TicketType.Trifecta , "type=b8" },
            { TicketType.Trio , "type=b7" },
            { TicketType.Wide , "type=b5" },
        };

        public static string GetRaceAndOddsDataFilePath(RaceData raceInfo, OddsType oddsType)
        {
            return Path.Combine(
                "RaceData",
                raceInfo.HoldingDatum.HeldDate.ToString("yyyyMMdd"),
                oddsType.ToString(),
                $"{raceInfo.HoldingDatum.Region.RegionId}-{raceInfo.HoldingDatum.Region.RegionName}-{raceInfo.RaceNumber}.xml");
        }

        public static string GetBetInformationFilePath(RaceData raceInfo)
        {
            return Path.Combine(
                "RaceData",
                raceInfo.HoldingDatum.HeldDate.ToString("yyyyMMdd"),
                "BetInfo",
                $"{raceInfo.HoldingDatum.Region.RegionId}-{raceInfo.HoldingDatum.Region.RegionName}-{raceInfo.RaceNumber}.xml");
        }

        public static Dictionary<TicketType, string> TicketTypeToRakutenBetTypeString = new Dictionary<TicketType, string>
        {
            { TicketType.Win, "単勝"},
            { TicketType.Quinella, "馬複"},
            { TicketType.Exacta, "馬単"},
            { TicketType.Trio, "三連複"},
            { TicketType.Trifecta, "三連単"},
            { TicketType.Wide, "ワイド"},
        };

        public static Dictionary<TicketType, string> TicketTypeToJraBetTypeString = new Dictionary<TicketType, string>
        {
            { TicketType.Win, "単勝"},
            { TicketType.Quinella, "馬連"},
            { TicketType.Exacta, "馬単"},
            { TicketType.Trio, "三連複"},
            { TicketType.Trifecta, "三連単"},
            { TicketType.Wide, "ワイド"},
        };

        public static double GetStatisticalOddsFromActualOdds(double odds, int rank)
        {
            return 1.0 / GetStatisticalProbabilityFromActualOdds(odds, rank);
        }

        public static double GetStatisticalProbabilityFromActualOdds(double odds, int rank)
        {
            if (odds == 1)
            {
                return 1;
            }
            if (odds <= 1.1)
            {
                return 0.793;
            }
            if (odds <= 1.2)
            {
                return 0.673;
            }
            if (odds <= 1.3)
            {
                return 0.628;
            }
            if (odds <= 1.4)
            {
                return 0.556;
            }
            if (odds <= 1.5)
            {
                return 0.52;
            }
            if (odds <= 1.6)
            {
                return 0.499;
            }
            if (odds <= 1.7)
            {
                return 0.441;
            }
            if (odds <= 1.8)
            {
                return 0.432;
            }
            if (odds <= 1.9)
            {
                return 0.400;
            }
            if (odds <= 2.0)
            {
                return 0.368;
            }
            if (odds <= 2.1)
            {
                return 0.354;
            }
            if (odds <= 2.2)
            {
                return 0.334;
            }
            if (odds <= 2.3)
            {
                return 0.329;
            }
            if (odds <= 2.4)
            {
                return 0.328;
            }
            if (odds <= 2.5)
            {
                return 0.317;
            }
            if (odds <= 2.6)
            {
                return 0.301;
            }
            if (odds <= 2.7)
            {
                return 0.292;
            }
            if (odds <= 2.8)
            {
                return 0.272;
            }
            if (odds <= 2.9)
            {
                return 0.279;
            }
            if (odds <= 3.4)
            {
                return 0.24;
            }
            if (odds <= 3.9)
            {
                if (rank == 1)
                {
                    return 0.211;
                }
                if (rank == 2)
                {
                    return 0.222;
                }
                return 0.22;
            }
            if (odds <= 4.9)
            {
                if (rank == 1)
                {
                    return 0.171;
                }
                if (rank == 2)
                {
                    return 0.178;
                }

                return 0.175;
            }
            if (odds <= 5.9)
            {
                return 0.151;
            }
            if (odds <= 6.9)
            {
                return 0.125;
            }
            if (odds <= 7.9)
            {
                return 0.11;
            }
            if (odds <= 8.9)
            {
                return 0.096;
            }
            if (odds <= 9.9)
            {
                return 0.09;
            }
            if (odds <= 14.9)
            {
                return 0.072;
            }
            if (odds <= 19.9)
            {
                return 0.05;
            }
            if (odds <= 24.9)
            {
                return 0.037;
            }
            if (odds <= 29.9)
            {
                return 0.031;
            }
            if (odds <= 39.9)
            {
                return 0.024;
            }
            if (odds <= 49.9)
            {
                return 0.018;
            }
            if (odds <= 59.9)
            {
                return 0.014;
            }
            if (odds <= 69.9)
            {
                return 0.011;
            }
            if (odds <= 79.9)
            {
                return 0.011;
            }
            if (odds <= 89.9)
            {
                return 0.009;
            }
            if (odds <= 99.9)
            {
                return 0.006;
            }
            if (odds <= 150)
            {
                return 0.005;
            }
            if (odds <= 200)
            {
                return 0.003;
            }
            if (odds <= 500)
            {
                return 0.001;
            }
            //勝ったことがない。。。
            return 0;

            //以下は元々のデータ。
            if (odds < 3.5)
            {
                return 0.247;
            }
            if (odds < 4.0)
            {
                return 0.224;
            }
            if (odds < 5.0)
            {
                return 0.176;
            }
            if (odds < 7.0)
            {
                return 0.134;
            }
            if (odds < 10.0)
            {
                return 0.1;
            }
            if (odds < 15.0)
            {
                return 0.07;
            }
            if (odds < 20)
            {
                return 0.049;
            }
            if (odds < 30)
            {
                return 0.034;
            }
            if (odds < 50)
            {
                return 0.021;
            }
            if (odds < 100)
            {
                return 0.011;
            }
            return 0.003;
        }

        /// <summary>
        /// 統計データがないので、オッズが正しいものとして勝率がその80％になるようにする
        /// </summary>
        /// <param name="odds"></param>
        /// <returns></returns>
        public static double GetStatisticalProbabilityFromActualOddsForRegional(double odds)
        {
            return 0.8 / odds;
        }
    }
}
