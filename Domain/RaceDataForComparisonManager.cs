using HorseRacingAutoPurchaser.Utils;
using HorseRacingAutoPurchaser.Infrastructures;
using HorseRacingAutoPurchaser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HorseRacingAutoPurchaser.Domain
{
    public class RaceDataForComparisonManager
    {
        public static IEnumerable<RaceDataForComparison> Get(DateTime from, DateTime to)
        {
            // holding_info.xml をループ外で1回だけ読み込む（日付ループごとに再読み込みしない）
            var holdingInformation = new HoldingInformationRepository().ReadAll();
            if (holdingInformation == null)
            {
                yield break;
            }

            for (var date = from; date <= to; date = date.AddDays(1))
            {
                var holdingData = holdingInformation.HoldingData
                    .Where(h => h != null && h.HasFullData && h.HeldDate.Date == date.Date);

                foreach (var holdingDatum in holdingData)
                {
                    for (var raceNumber = 1; raceNumber <= holdingDatum.TotalRaceCount; raceNumber++)
                    {
                        var raceData = new RaceData(holdingDatum, raceNumber);
                        var actualRaceRepo = ActualRaceAndOddsData.GetRepository(raceData);
                        var actualRaceData = actualRaceRepo.ReadAll();

                        if (actualRaceData == null)
                        {
                            continue;
                        }
                        var statisticalWinOdds = StatisticalOddsGetter.Get(actualRaceData.BaseRaceData.HoldingDatum.Region, actualRaceData.WinOdds);
                        var theoreticalRaceData = new TheoreticalRaceAndOddsData(raceData);
                        theoreticalRaceData.SetHorseDataFromWinOdds(statisticalWinOdds);
                        theoreticalRaceData.SetData();

                        yield return new RaceDataForComparison(actualRaceData, theoreticalRaceData);
                    }
                }
            }
        }

        public static RaceDataForComparison Get(ActualRaceAndOddsData actualRaceData)
        {
            var statisticalWinOdds = StatisticalOddsGetter.Get(actualRaceData.BaseRaceData.HoldingDatum.Region, actualRaceData.WinOdds);
            var theoreticalRaceData = new TheoreticalRaceAndOddsData(actualRaceData.BaseRaceData);
            theoreticalRaceData.SetHorseDataFromWinOdds(statisticalWinOdds);
            theoreticalRaceData.SetData();

            return new RaceDataForComparison(actualRaceData, theoreticalRaceData);
        }
    }
}
