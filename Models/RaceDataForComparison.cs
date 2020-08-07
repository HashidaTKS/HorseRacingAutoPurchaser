using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseRacingAutoPurchaser
{

    public class RaceDataForComparison
    {
        public RaceAndOddsData ActualRaceData { get; set; }

        /// <summary>
        /// 実オッズに対応する理論オッズ。各オッズの並びもActualraceDataと同じ。
        /// </summary>
        public RaceAndOddsData TheoreticalRaceData { get; set; }

        public RaceData RaceData => TheoreticalRaceData.BaseRaceData;

        public RaceDataForComparison(RaceAndOddsData actualRaceData, RaceAndOddsData theoreticalRaceData)
        {
            ActualRaceData = actualRaceData;

            var filterdTheoreticalRaceData = new TheoreticalRaceAndOddsData(theoreticalRaceData.BaseRaceData);
            filterdTheoreticalRaceData.WinOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.WinOdds, theoreticalRaceData.WinOdds, TicketType.Win).ToList();
            filterdTheoreticalRaceData.QuinellaOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.QuinellaOdds, theoreticalRaceData.QuinellaOdds, TicketType.Quinella).ToList();
            //馬連までで十分なので、その他は計算しない
            //filterdTheoreticalRaceData.ExactaOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.ExactaOdds, theoreticalRaceData.ExactaOdds, TicketType.Exacta).ToList();
            //filterdTheoreticalRaceData.TrifectaOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.TrifectaOdds, theoreticalRaceData.TrifectaOdds, TicketType.Trifecta).ToList();
            //filterdTheoreticalRaceData.TrioOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.TrioOdds, theoreticalRaceData.TrioOdds, TicketType.Trio).ToList();

            TheoreticalRaceData = filterdTheoreticalRaceData;
        }
    }
}
