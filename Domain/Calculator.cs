using System;
using System.Collections.Generic;
using System.Linq;
using HorseRacingAutoPurchaser.Utils;
using HorseRacingAutoPurchaser.Infrastructures;
using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Domain
{
    public class Calculator
    {
        public List<HorseDatum> HorseData { get; set; }

        public Calculator(List<HorseDatum> horseData)
        {
            HorseData = horseData;
        }

        public IEnumerable<OddsDatum> GetAllWinOdds()
        {
            return HorseData.Select(_ => new OddsDatum(new List<HorseDatum> { _ }, _.WinProbability > 0 ? 1 / _.WinProbability : Int32.MaxValue));
        }

        /// <summary>
        /// あるN連単の全組み合わせの理論値を返す
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public IEnumerable<OddsDatum> GetAllExactaOdds(int rank)
        {
            var allCombination = EnumerableUtils.GetPermutation(HorseData, rank, false);
            foreach(var combination in allCombination)
            {
                yield return GetExactaOdds(combination.ToList());
            }
        }

        /// <summary>
        /// あるN連複の全組み合わせの理論値を返す
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public IEnumerable<OddsDatum> GetAllQuinellaOdds(int rank)
        {
            var allCombination = EnumerableUtils.GetCombination(HorseData, rank, false);
            foreach (var combination in allCombination)
            {
                yield return GetQuinellaOdds(combination);
            }
        }

        /// <summary>
        /// あるN連複の全組み合わせの理論値を返す
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public IEnumerable<OddsDatum> GetAllWideOdds()
        {
            var allCombination = EnumerableUtils.GetCombination(HorseData, 2, false);
            foreach (var combination in allCombination)
            {
                yield return GetWideOdds(combination);
            }
        }


        /// <summary>
        /// N連単の理論値を計算する
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public OddsDatum GetExactaOdds(List<HorseDatum> horseData)
        {
            var expectedProbability = GetExactaProbability(horseData);
            var odds = expectedProbability > 0 ? 1 / expectedProbability : Int32.MaxValue;
            return new OddsDatum(horseData, odds);
        }

        private double GetExactaProbability(List<HorseDatum> horseData)
        {
            List<HorseDatum> winHorseList = new List<HorseDatum>();
            double expectedProbability = 1.0;
            foreach (var horseDatum in horseData)
            {
                //既に勝ち上がっている馬がいる上で、残りの馬の中で1番になる確率を計算する。（条件付き確率）
                var currentTotal = 1 - winHorseList.Sum(_ => _.WinProbability);
                var currentWinProbability = currentTotal > 0 ? horseDatum.WinProbability / currentTotal : 0;

                //既に勝ちあがっている馬が勝つ確率に乗算することで、条件付き確率から実際の発生確率に戻す
                expectedProbability *= currentWinProbability;
                winHorseList.Add(horseDatum);
            }
            return expectedProbability;
        }

        /// <summary>
        /// N連複の理論値を計算する
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public OddsDatum GetQuinellaOdds(List<HorseDatum> horseData)
        {
            var expectedProbability = 0.0;
            foreach(var exact in EnumerableUtils.GetPermutation(horseData, horseData.Count))
            {
                expectedProbability += GetExactaProbability(exact.ToList());
            }
            var odds = expectedProbability > 0 ? 1 / expectedProbability : Int32.MaxValue;
            return new OddsDatum(horseData, odds);
        }

        /// <summary>
        /// ワイドの理論値を計算する。
        /// </summary>
        /// <param name="horseData"></param>
        /// <returns></returns>
        public OddsDatum GetWideOdds(IEnumerable<HorseDatum> horseData)
        {
            var horseList = horseData.ToList();
            //ワイドの理論値は、三連単（複）のうち、指定の馬が含まれている全ての組み合わせの総和とする。
            var numberList = horseList.Select(_ => _.Number).ToList();
            var restHorseData = HorseData.Where(_ => !numberList.Contains(_.Number));
            var expectedProbability = 0.0;

            foreach (var restHorseDatum in restHorseData)
            {
                var odds = GetQuinellaOdds(horseList.Append(restHorseDatum).ToList()).Odds;
                expectedProbability += 1 / odds;
            }

            var totalOdds = expectedProbability > 0 ? 1 / expectedProbability : Int32.MaxValue;
            //シミュレーションしてみると、勝率が実測より1.3倍くらい良くでてしまっている。
            //実測に合わせてオッズを1.3倍しておく。
            //TODO: 原因調査
            return new OddsDatum(horseList, totalOdds * 1.3);
        }
    }
}
