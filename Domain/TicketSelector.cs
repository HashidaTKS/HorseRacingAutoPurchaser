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

        public static IEnumerable<BetDatum> SelectWinTicket(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betconfig, BetResultStatusOfTicketType betResultStatus)
        {
            return SelectTicketBase(raceDataForComparison, betconfig, betResultStatus, TicketType.Win);

        }

        public static IEnumerable<BetDatum> SelectWideTicket(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betconfig, BetResultStatusOfTicketType betResultStatus)
        {
            return SelectTicketBase(raceDataForComparison, betconfig, betResultStatus, TicketType.Wide);

        }

        public static IEnumerable<BetDatum> SelectQuinellaTicket(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betconfig, BetResultStatusOfTicketType betResultStatus)
        {
            return SelectTicketBase(raceDataForComparison, betconfig, betResultStatus, TicketType.Quinella);
        }

        public static IEnumerable<BetDatum> SelectExactaTicket(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betconfig, BetResultStatusOfTicketType betResultStatus)
        {
            return SelectTicketBase(raceDataForComparison, betconfig, betResultStatus, TicketType.Exacta);

        }

        public static IEnumerable<BetDatum> SelectTrifectaTicket(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betconfig, BetResultStatusOfTicketType betResultStatus)
        {
            return SelectTicketBase(raceDataForComparison, betconfig, betResultStatus, TicketType.Trifecta);

        }

        public static IEnumerable<BetDatum> SelectTrioTicket(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betconfig, BetResultStatusOfTicketType betResultStatus)
        {
            return SelectTicketBase(raceDataForComparison, betconfig, betResultStatus, TicketType.Trio);

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

        private static IEnumerable<BetDatum> SelectTicketBase(RaceDataForComparison raceDataForComparison, BetConfigForTicketType betConfigForTicketType, BetResultStatusOfTicketType betResultStatusOfTicketType, TicketType ticketType)
        {
            var actualOdds = raceDataForComparison.ActualRaceData.GetOddsOfTicketType(ticketType);
            var theoreticalOdds = raceDataForComparison.TheoreticalRaceData.GetOddsOfTicketType(ticketType);
            var count = actualOdds.Count;
            var cocomo = new Cocomo();
            for (var i = 0; i < count; i++)
            {

                if (actualOdds[i] == null || theoreticalOdds[i] == null)
                {
                    continue;
                }
                if (GetOddsRatio(actualOdds[i].Odds, theoreticalOdds[i].Odds) >= betConfigForTicketType.OddsRatio &&
                    theoreticalOdds[i].Odds >= betConfigForTicketType.MinimumOdds && theoreticalOdds[i].Odds < betConfigForTicketType.MaximumOdds)
                {
                    var betMoney = GetAdjustedBetMoney(betConfigForTicketType.MinimumPayBack, actualOdds[i].Odds);
                    if (betConfigForTicketType.UseCocomo)
                    {
                        betMoney *= cocomo.GetMagnification(betConfigForTicketType, betResultStatusOfTicketType);
                    }

                    yield return new BetDatum(
                        raceDataForComparison.RaceData,
                        actualOdds[i].HorseData.Select(_ => _.Number).ToList(),
                        betMoney,
                        actualOdds[i].Odds,
                        theoreticalOdds[i].Odds,
                        ticketType);
                }
            }
        }
    }
}
