using System.Collections.Generic;
using System.Linq;
using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Utils
{
    public static class EnumerableUtils
    {
        public static IEnumerable<OddsDatum> GetSameTicketOddsData(IEnumerable<OddsDatum> needs, IEnumerable<OddsDatum> froms, TicketType ticketType)
        {
            bool isOrdered = ticketType == TicketType.Win || ticketType == TicketType.Exacta || ticketType == TicketType.Trifecta;

            // froms を key → OddsDatum の辞書に変換して O(1) ルックアップを実現する
            // 順序依存チケット: カンマ区切り文字列をキーとして順序を保持
            // 順序非依存チケット: ソート済みのカンマ区切り文字列をキーとして順序を無視
            var fromDict = froms.ToDictionary(
                f => isOrdered
                    ? string.Join(",", f.HorseData.Select(x => x.Number))
                    : string.Join(",", f.HorseData.Select(x => x.Number).OrderBy(x => x)),
                f => f);

            foreach (var need in needs)
            {
                var key = isOrdered
                    ? string.Join(",", need.HorseData.Select(x => x.Number))
                    : string.Join(",", need.HorseData.Select(x => x.Number).OrderBy(x => x));

                fromDict.TryGetValue(key, out var matched);
                yield return matched;
            }
        }

        public static IEnumerable<IEnumerable<T>> GetContainedList<T>(IEnumerable<T> contained, IEnumerable<IEnumerable<T>> checkTargets)
        {
            foreach(var checkTarget in checkTargets){
                if (CombinationalContain(contained, checkTarget))
                {
                    yield return checkTarget;
                }
            }
        }

        public static bool CombinationalContain<T>(IEnumerable<T> contained , IEnumerable<T> checkTarget)
        {
            foreach (var elem in contained)
            {
                if (!checkTarget.Contains(elem))
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 順列を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetPermutation<T>(IEnumerable<T> items, int k, bool withRepetition = false)
        {
            if (items.Count() == 1)
            {
                yield return new List<T> { items.First() };
                yield break;
            }
            if (k == 1)
            {
                foreach (var item in items)
                    yield return new List<T> { item };
                yield break;
            }
            foreach (var item in items)
            {
                var leftside = new T[] { item };
                var unused = withRepetition ? items : items.Except(leftside);

                foreach (var rightside in GetPermutation(unused, k - 1, withRepetition))
                {
                    yield return leftside.Concat(rightside);
                }
            }
        }

        /// <summary>
        /// 組み合わせを取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="k"></param>
        /// <param name="withRepetition"></param>
        /// <returns></returns>
        public static IEnumerable<List<T>> GetCombination<T>(IEnumerable<T> items, int k, bool withRepetition = false)
        {
            if (k == 1)
            {
                foreach (var item in items)
                    yield return new List<T> { item };
                yield break;
            }
            foreach (var item in items)
            {
                var leftside = new T[] { item };

                // item よりも前のものを除く （順列と組み合わせの違い)
                // 重複を許さないので、unusedから item そのものも取り除く
                var unused = withRepetition ? items : items.SkipWhile(e => !e.Equals(item)).Skip(1).ToList();

                foreach (var rightside in GetCombination(unused, k - 1, withRepetition))
                {
                    yield return leftside.Concat(rightside).ToList();
                }
            }
        }
    }
}
