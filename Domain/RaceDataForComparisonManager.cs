using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    public class RaceDataForComparisonManager
    {
        public static IEnumerable<RaceDataForComparison> Get(DateTime from, DateTime to)
        {
            for (var date = from; date <= to; date = date.AddDays(1))
            {
                if (date < from || date > to)
                {
                    continue;
                }
                var raceDataOfDay = RaceDataManager.GetRaceDataOfDay(date);
                foreach (var raceData in raceDataOfDay)
                {
                    if (raceData == null)
                    {
                        continue;
                    }
                    var actualRaceRepo = ActualRaceAndOddsData.GetRepository(raceData);
                    var actualRaceData = actualRaceRepo.ReadAll();

                    if (actualRaceData == null)
                    {
                        continue;
                    }
                    var statisticalWinOdds = new List<OddsDatum>();

                    foreach (var winOdds in actualRaceData.WinOdds)
                    {
                        if (winOdds == null)
                        {
                            continue;
                        }
                        statisticalWinOdds.Add(new OddsDatum(winOdds.HorseData, Utility.GetStatisticalOddsFromActualOdds(winOdds.Odds)));
                    }

                    var theoreticalRaceData = new TheoreticalRaceAndOddsData(raceData);
                    theoreticalRaceData.SetHorseDataFromWinOdds(statisticalWinOdds);
                    theoreticalRaceData.SetData();

                    if (actualRaceData == null)
                    {
                        continue;
                    }

                    yield return new RaceDataForComparison(actualRaceData, theoreticalRaceData);
                }
            }
        }

        public static RaceDataForComparison Get(ActualRaceAndOddsData actualRaceData)
        {
            var statisticalWinOdds = new List<OddsDatum>();
            foreach (var winOdds in actualRaceData.WinOdds)
            {
                if (winOdds == null)
                {
                    continue;
                }
                statisticalWinOdds.Add(new OddsDatum(winOdds.HorseData, Utility.GetStatisticalOddsFromActualOdds(winOdds.Odds)));
            }

            var theoreticalRaceData = new TheoreticalRaceAndOddsData(actualRaceData.BaseRaceData);
            theoreticalRaceData.SetHorseDataFromWinOdds(statisticalWinOdds);
            theoreticalRaceData.SetData();

            return new RaceDataForComparison(actualRaceData, theoreticalRaceData);
        }
    }
}
