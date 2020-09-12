using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseRacingAutoPurchaser.Infrastructures;
using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Utils
{
    public class StatisticalOddsGetter
    {

        private static List<double> OoiPercentageList = new List<double>
        {
            31.9, 19.2, 11.8, 9.7, 8.9, 5.0, 4.7, 3.5, 1.7, 1.6, 1.2, 0.8, 0.8, 0.3, 0.5, 0,0,
        };

        private static List<double> FunabashiPercentageList = new List<double>
        {
            37.2, 19.3, 13.1, 9.9, 7.7, 5.0, 4.4, 2.7, 0.8, 0.8, 0, 0, 0, 0, 0, 0, 0,
        };

        private static List<double> KawasakiPercentageList = new List<double>
        {
            35.7, 18.5, 13.3, 10.0, 6.8, 5.7, 2.7, 3.3, 2.6, 1.8, 0.4, 0.2, 0.1, 0,
        };

        private static List<double> UrawaPercentageList = new List<double>
        {
            36.4, 18.4, 15.3, 8.8, 7.0, 5.4, 3.0, 3.4, 1.3, 1.2, 0.5, 0.0, 0.0, 0.0,
        };

        private static List<double> SonodaPercentageList = new List<double>
        {
            42.9, 22.7, 12.2, 7.4, 5.5, 3.2, 2.8, 1.4, 1.3, 0.5, 0.8, 0.0, 0.0, 0.0,
        };

        private static List<double> NagoyaPercentageList = new List<double>
        {
            52.9, 22.5, 10.6, 6.5, 4.0, 2.2, 1.0, 0.6, 0.8, 0.0, 0.0, 0.0, 0.0, 0.0,
        };

        private static List<double> DefaultPercentageList = new List<double>
        {
            32.8, 19.0, 13.3, 9.2, 7.5, 5.5, 3.8, 3.1, 2.2, 1.2, 1.3, 0.9, 0.7, 0.2, 0.2, 0.1, 0.0, 0.0
        };

        private static Dictionary<string, List<double>> Correspondance = new Dictionary<string, List<double>>
        {
            { "大井", OoiPercentageList },
            { "船橋", FunabashiPercentageList },
            { "川崎", KawasakiPercentageList },
            { "浦和", UrawaPercentageList },
            { "名古屋", NagoyaPercentageList },
            { "園田" , SonodaPercentageList }
        };

        public static IEnumerable<OddsDatum> Get(Region region, List<OddsDatum> actualWinOdds)
        {
            //競争除外などがあった場合。
            //正しいオッズが出せない恐れがあるので何もしない。
            if (actualWinOdds.Any(_ => _ == null))
            {
                return new List<OddsDatum>();
            }

            List<double> probabilityList;
            var sortedActualOdds = actualWinOdds.OrderBy(_ => _.Odds);

            //probabilityList = GetCorrectedProbabilityList(
            //    sortedActualOdds.Select((_, index) => Utility.GetStatisticalProbabilityFromActualOdds(_.Odds, index + 1)).ToList());
            //return actualWinOdds.Select((_, index) => new OddsDatum(_.HorseData, 1 / probabilityList[index])).ToList();

            if (region.RagionType == RegionType.Central)
            {
                probabilityList = GetCorrectedProbabilityList(
                    sortedActualOdds.Select((_, index) => Utility.GetStatisticalProbabilityFromActualOdds(_.Odds, index + 1)).ToList());
                return actualWinOdds.Select((_, index) => new OddsDatum(_.HorseData, 1 / probabilityList[index]));
            }

            if (region.RagionType == RegionType.Regional)
            {
                probabilityList = GetCorrectedProbabilityList(
                    actualWinOdds.Select(_ => Utility.GetStatisticalProbabilityFromActualOddsForRegional(_.Odds)).ToList());
                return actualWinOdds.Select((_, index) => new OddsDatum(_.HorseData, 1 / probabilityList[index]));
            }

            //今後地方競馬で地域ごとのデータを取得することがあれば以下を使う。(今は通らない）
            if (!Correspondance.TryGetValue(region.RegionName, out var probabilityParcentageList))
            {
                probabilityParcentageList = DefaultPercentageList;
            }

            probabilityList = GetCorrectedProbabilityList(
                actualWinOdds.Select((_, index) => GetProbabilityFromOddsList(probabilityParcentageList, index)).ToList());
            return actualWinOdds.Select((_, index) => new OddsDatum(_.HorseData, 1 / probabilityList[index]));

        }

        private static double GetProbabilityFromOddsList(List<double> probabilityParcentageLists, int index)
        {
            if (index >= probabilityParcentageLists.Count)
            {
                return 0.0;
            }
            return probabilityParcentageLists[index] / 100.0;
        }

        /// <summary>
        /// 全体の確率が1になるように調整する
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        private static List<double> GetCorrectedProbabilityList(List<double> from)
        {
            var total = from.Sum();
            var coefficient = 1 / total;

            return from.Select(_ => _ * coefficient).ToList();
        }
    }
}
