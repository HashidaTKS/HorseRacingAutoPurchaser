using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    public class Cocomo
    {
        private int Before { get; set; } = 1;

        /// <summary>
        /// N回ココモを実行した場合の倍率を取得する
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private int GetMagnification(int n)
        {
            Reset();
            var current = 1;
            for (var i = 0; i < n; i++)
            {
                current = GetNext(current);
            }
            return current;
        }

        /// <summary>
        /// 次の倍率を取得する
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private int GetNext(int current)
        {
            var next = current + Before;
            Before = current;
            return next;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Reset()
        {
            Before = 1;
        }

        public int GetMagnification(BetConfigForTicketType betConfig, BetResultStatusOfTicketType betResultStatus)
        {
            var magnification = 1;
            if (betResultStatus.CountOfContinuationLose >= 1 && betConfig.CocomoThreshold > 0)
            {
                var division = betResultStatus.CountOfContinuationLose / betConfig.CocomoThreshold;
                magnification = Math.Min(GetMagnification(division), betConfig.CocomoMaxMagnification);
                
            }
            return magnification;
        }
    }

    /// <summary>
    /// 購入すべきチケットを選択する。
    /// </summary>
    public class TicketSelector
    {
        public static List<BetDatum> SelectToBet(RaceDataForComparison outputRaceData, BetConfig betconfig, BetResultStatus betResultStatus)
        {
            List<BetDatum> betData = new List<BetDatum>();
            betData.AddRange(SelectQuinellaTicket(outputRaceData, betconfig.QuinellaBetConfig, betResultStatus.QuinellaBetStatus).OrderByDescending(_ => _.OddsRatio).Take(betconfig.QuinellaBetConfig.MaxPurchaseCount));
            betData.AddRange(SelectWideTicket(outputRaceData, betconfig.WideBetConfig, betResultStatus.WideBetStatus).OrderByDescending(_ => _.OddsRatio).Take(betconfig.WideBetConfig.MaxPurchaseCount));
            return betData;
        }

        public static IEnumerable<BetDatum> SelectWinTicket(RaceDataForComparison outputRaceData)
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
                    yield return new BetDatum(outputRaceData.RaceData, actualOdds[i].HorseData.Select(_ => _.Number).ToList(), 100, actualOdds[i].Odds, theoreticalOdds[i].Odds, TicketType.Win);
                }
                yield break;
            }
        }

        public static IEnumerable<BetDatum> SelectWideTicket(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betconfig, BetResultStatusOfTicketType betResultStatus)
        {
            var actualOdds = raceDataForComparison.ActualRaceData.QuinellaOdds;
            var theoreticalOdds = raceDataForComparison.TheoreticalRaceData.QuinellaOdds;
            var count = actualOdds.Count;
            var cocomo = new Cocomo();
            for (var i = 0; i < count; i++)
            {

                if (actualOdds[i] == null || theoreticalOdds[i] == null)
                {
                    continue;
                }
                if (GetOddsRatio(actualOdds[i].Odds, theoreticalOdds[i].Odds) >= betconfig.OddsRatio &&
                    theoreticalOdds[i].Odds >= betconfig.MinimumOdds && theoreticalOdds[i].Odds < betconfig.MaximumOdds)
                {
                    var betMoney = GetAdjustedBetMoney(betconfig.MinimumPayBack, actualOdds[i].Odds);
                    if (betconfig.UseCocomo)
                    {
                        betMoney *= cocomo.GetMagnification(betconfig, betResultStatus);
                    }

                    yield return new BetDatum(
                        raceDataForComparison.RaceData,
                        actualOdds[i].HorseData.Select(_ => _.Number).ToList(),
                        betMoney,
                        actualOdds[i].Odds,
                        theoreticalOdds[i].Odds,
                        TicketType.Wide);
                }
            }
        }

        public static IEnumerable<BetDatum> SelectQuinellaTicket(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betconfig, BetResultStatusOfTicketType betResultStatus)
        {
            var actualOdds = raceDataForComparison.ActualRaceData.QuinellaOdds;
            var theoreticalOdds = raceDataForComparison.TheoreticalRaceData.QuinellaOdds;
            var count = actualOdds.Count;
            var cocomo = new Cocomo();
            for (var i = 0; i < count; i++)
            {

                if (actualOdds[i] == null || theoreticalOdds[i] == null)
                {
                    continue;
                }
                if (GetOddsRatio(actualOdds[i].Odds, theoreticalOdds[i].Odds) >= betconfig.OddsRatio &&
                    theoreticalOdds[i].Odds >= betconfig.MinimumOdds && theoreticalOdds[i].Odds < betconfig.MaximumOdds)
                {
                    var betMoney = GetAdjustedBetMoney(betconfig.MinimumPayBack, actualOdds[i].Odds);
                    if (betconfig.UseCocomo)
                    {
                        betMoney *= cocomo.GetMagnification(betconfig, betResultStatus);
                    }

                    yield return new BetDatum(
                        raceDataForComparison.RaceData,
                        actualOdds[i].HorseData.Select(_ => _.Number).ToList(),
                        betMoney,
                        actualOdds[i].Odds,
                        theoreticalOdds[i].Odds,
                        TicketType.Quinella);
                }
            }
        }

        public static IEnumerable<BetDatum> SelectExactaTicket(RaceDataForComparison outputRaceData)
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
                    yield return new BetDatum(outputRaceData.RaceData, actualOdds[i].HorseData.Select(_ => _.Number).ToList(), GetAdjustedBetMoney(50000, theoreticalOdds[i].Odds), actualOdds[i].Odds, theoreticalOdds[i].Odds, TicketType.Exacta);
                }
            }
        }

        public static IEnumerable<BetDatum> SelectTrifectaTicket(RaceDataForComparison outputRaceData)
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
                    yield return new BetDatum(outputRaceData.RaceData, actualOdds[i].HorseData.Select(_ => _.Number).ToList(), 100, actualOdds[i].Odds, theoreticalOdds[i].Odds, TicketType.Trifecta);
                }
            }
        }

        public static IEnumerable<BetDatum> SelectTrioTicket(RaceDataForComparison outputRaceData)
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
                    yield return new BetDatum(outputRaceData.RaceData, actualOdds[i].HorseData.Select(_ => _.Number).ToList(), 100, actualOdds[i].Odds, theoreticalOdds[i].Odds, TicketType.Trio);
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
            if (odds <= 0)
            {
                //ありえないが
                return 100;
            }
            var ratio = targetMoney / (odds * 100);
            return (int)(ratio + 1) * 100;
        }
    }
}
