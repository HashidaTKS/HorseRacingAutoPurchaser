using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    /// <summary>
    /// 購入すべきチケットを選択する。
    /// </summary>
    public class TicketSelector
    {
        public static List<BetInformation> SelectToBet(RaceDataForComparison outputRaceData, int payBackRatio)
        {
            List<BetInformation> betInformation = new List<BetInformation>();
            betInformation.AddRange(SelectQuinellaTicket(outputRaceData).OrderByDescending(_ => _.OddsRatio).Take(1));

            betInformation.ForEach(_ => _.BetMoney *= payBackRatio);
            return betInformation;
        }

        public static IEnumerable<BetInformation> SelectWinTicket(RaceDataForComparison outputRaceData)
        {
            var actualOdds = outputRaceData.ActualRaceData.WinOdds;
            var theoreticalOdds = outputRaceData.TheoreticalRaceData.WinOdds;
            var count = actualOdds.Count;
            for (var i = 0; i < count; i++)
            {
                if (actualOdds[i] == null || theoreticalOdds[i] == null)
                {
                    continue;
                }
               
                if (GetOddsRatio(actualOdds[i].Odds, theoreticalOdds[i].Odds) >= 1.3 && theoreticalOdds[i].Odds > 5 && theoreticalOdds[i].Odds < 100)
                {
                    yield return new BetInformation(outputRaceData.RaceData, actualOdds[i].HorseData.Select(_ => _.Number).ToList(), 100, actualOdds[i].Odds, theoreticalOdds[i].Odds, TicketType.Win);
                }
                yield break;
            }
        }

        public static IEnumerable<BetInformation> SelectQuinellaTicket(RaceDataForComparison outputRaceData)
        {
            var actualOdds = outputRaceData.ActualRaceData.QuinellaOdds;
            var theoreticalOdds = outputRaceData.TheoreticalRaceData.QuinellaOdds;
            var count = actualOdds.Count;
            for (var i = 0; i < count; i++)
            {

                if (actualOdds[i] == null || theoreticalOdds[i] == null)
                {
                    continue;
                }
                if (GetOddsRatio(actualOdds[i].Odds, theoreticalOdds[i].Odds) >= 1.4 && theoreticalOdds[i].Odds > 5 && theoreticalOdds[i].Odds < 100)
                {
                    yield return new BetInformation(
                        outputRaceData.RaceData, 
                        actualOdds[i].HorseData.Select(_ => _.Number).ToList(), 
                        GetAdjustedBetMoney(10000, actualOdds[i].Odds), 
                        actualOdds[i].Odds, 
                        theoreticalOdds[i].Odds, 
                        TicketType.Quinella);
                }
            }
        }

        public static IEnumerable<BetInformation> SelectExactaTicket(RaceDataForComparison outputRaceData)
        {
            var actualOdds = outputRaceData.ActualRaceData.ExactaOdds;
            var theoreticalOdds = outputRaceData.TheoreticalRaceData.ExactaOdds;
            var count = actualOdds.Count;
            for (var i = 0; i < count; i++)
            {
                if (actualOdds[i] == null || theoreticalOdds[i] == null)
                {
                    continue;
                }
                if (GetOddsRatio(actualOdds[i].Odds, theoreticalOdds[i].Odds) >= 1.3 && actualOdds[i].Odds > 30)
                {
                    yield return new BetInformation(outputRaceData.RaceData, actualOdds[i].HorseData.Select(_ => _.Number).ToList(), GetAdjustedBetMoney(50000, theoreticalOdds[i].Odds), actualOdds[i].Odds, theoreticalOdds[i].Odds,TicketType.Exacta);
                }
            }
        }

        public static IEnumerable<BetInformation> SelectTrifectaTicket(RaceDataForComparison outputRaceData)
        {
            var actualOdds = outputRaceData.ActualRaceData.TrifectaOdds;
            var theoreticalOdds = outputRaceData.TheoreticalRaceData.TrifectaOdds;
            var count = actualOdds.Count;
            for (var i = 0; i < count; i++)
            {
                if (actualOdds[i] == null || theoreticalOdds[i] == null)
                {
                    continue;
                }

                if (GetOddsRatio(actualOdds[i].Odds, theoreticalOdds[i].Odds) >= 2.5 && actualOdds[i].Odds > 50)
                {
                    yield return new BetInformation(outputRaceData.RaceData, actualOdds[i].HorseData.Select(_ => _.Number).ToList(), 100, actualOdds[i].Odds, theoreticalOdds[i].Odds, TicketType.Trifecta);
                }
            }
        }

        public static IEnumerable<BetInformation> SelectTrioTicket(RaceDataForComparison outputRaceData)
        {
            var actualOdds = outputRaceData.ActualRaceData.TrioOdds;
            var theoreticalOdds = outputRaceData.TheoreticalRaceData.TrioOdds;
            var count = actualOdds.Count;
            for (var i = 0; i < count; i++)
            {
                if (actualOdds[i] == null || theoreticalOdds[i] == null)
                {
                    continue;
                }
                if (GetOddsRatio(actualOdds[i].Odds, theoreticalOdds[i].Odds) > 2.5 && actualOdds[i].Odds > 50)
                {
                    yield return new BetInformation(outputRaceData.RaceData, actualOdds[i].HorseData.Select(_ => _.Number).ToList(), 100, actualOdds[i].Odds, theoreticalOdds[i].Odds, TicketType.Trio);
                }
            }
        }

        private static double GetOddsRatio(double actual, double theoretical)
        {
            if (theoretical <= 0)
            {
                return 0;
            }
            return actual / theoretical;
        }

        /// <summary>
        /// 的中時の見積もり払戻額が指定金額以上になるようなベット額を取得する
        /// </summary>
        /// <returns></returns>
        private static int GetAdjustedBetMoney(double targetMoney, double odds)
        {
            if(odds <= 0)
            {
                //ありえないが
                return 100;
            }
            var ratio = targetMoney / (odds * 100);
            return (int)(ratio + 1) * 100;
        }
    }
}
