using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    public class OutputDatum
    {
        public List<int> HorseNumberList { get; }

        public OddsDatum ActualOdds { get; }

        public OddsDatum TheoretialOdds { get; }

        public double OddsRatio => ActualOdds.Odds / TheoretialOdds.Odds;

        public OutputDatum(OddsDatum actual, OddsDatum theoretical)
        {
            ActualOdds = actual;
            TheoretialOdds = theoretical;
            HorseNumberList = ActualOdds.HorseData.Select(_ => _.Number).ToList();
        }

        public string ToTableBodyString()
        {
            var style = GetBackgroundColorStyleString(OddsRatio);
            return $@"<tr><td>{ActualOdds.GetHorsesString()}</td><td>{ActualOdds.Odds:F1}</td><td>{TheoretialOdds.Odds:F1}</td><td {style}>{OddsRatio:F2}</td></tr>";
        }


        /// <summary>
        /// オッズ比に応じた背景色を取得する。1から着色し始め、2以上で最も赤くなる。
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        private static string GetBackgroundColorStyleString(double oddsRatio)
        {
            var coefficient = Math.Max(oddsRatio - 1, 0);
            var value = (int)Math.Max(255 * (1 - coefficient), 0);
            
            return value == 255 ? 
                "" : 
                $@" style=""background-color: rgb(255, {value}, {value});"" ";
            
        }
    }

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
            filterdTheoreticalRaceData.WideOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.WideOdds, theoreticalRaceData.WideOdds, TicketType.Wide).ToList();
            //馬連までで十分なので、その他は計算しない
            filterdTheoreticalRaceData.ExactaOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.ExactaOdds, theoreticalRaceData.ExactaOdds, TicketType.Exacta).ToList();
            filterdTheoreticalRaceData.TrifectaOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.TrifectaOdds, theoreticalRaceData.TrifectaOdds, TicketType.Trifecta).ToList();
            filterdTheoreticalRaceData.TrioOdds = EnumerableUtils.GetSameTicketOddsData(actualRaceData.TrioOdds, theoreticalRaceData.TrioOdds, TicketType.Trio).ToList();

            TheoreticalRaceData = filterdTheoreticalRaceData;
        }

        /// <summary>
        /// ヘッダーと本分を返す
        /// </summary>
        /// <returns></returns>
        public (string , string, string, DateTime) ToBlogString()
        {
            var stringBuilder = new StringBuilder();
            var raceData = ActualRaceData.BaseRaceData;
            var title = $"{raceData.HoldingDatum.HeldDate.ToString("yyyyMMdd")}-{raceData.HoldingDatum.Region.RegionName}-{raceData.RaceNumber}R";
            var linkText = $"{raceData.HoldingDatum.HeldDate.ToString("yyyyMMdd")}{raceData.HoldingDatum.Region.RegionId}{raceData.RaceNumber:D2}";
            //stringBuilder.Append($@"<h1>{header}</h1>");

            //単勝
            stringBuilder.Append($@"<h2>{Utility.TicketTypeToJraBetTypeString[TicketType.Win]}</h2>");
            var win = ActualRaceData.WinOdds.Zip(TheoreticalRaceData.WinOdds, (a, t) => new OutputDatum(a, t)).OrderBy(_ => _.HorseNumberList[0]);
            var winTable = GetOddsTable(win);
            stringBuilder.Append(winTable);

            stringBuilder.Append(GetTicketHtml(TicketType.Wide,
                "wide-tab",
                ActualRaceData.WideOdds.Zip(TheoreticalRaceData.WideOdds, (a, t) => new OutputDatum(a, t)).OrderBy(_ => _.HorseNumberList[0]).ThenBy(_ => _.HorseNumberList[1])));

            stringBuilder.Append(GetTicketHtml(TicketType.Quinella,
                "quinella-tab",
                ActualRaceData.QuinellaOdds.Zip(TheoreticalRaceData.QuinellaOdds, (a, t) => new OutputDatum(a, t)).OrderBy(_ => _.HorseNumberList[0]).ThenBy(_ => _.HorseNumberList[1])));

            stringBuilder.Append(GetTicketHtml(TicketType.Exacta,
                "exacta-tab",
                ActualRaceData.ExactaOdds.Zip(TheoreticalRaceData.ExactaOdds, (a, t) => new OutputDatum(a, t)).OrderBy(_ => _.HorseNumberList[0]).ThenBy(_ => _.HorseNumberList[1])));

            stringBuilder.Append(GetTicketHtml(TicketType.Trio,
                 "trio-tab",
                 ActualRaceData.TrioOdds.Zip(TheoreticalRaceData.TrioOdds, (a, t) => new OutputDatum(a, t)).OrderBy(_ => _.HorseNumberList[0]).ThenBy(_ => _.HorseNumberList[1]).ThenBy(_ => _.HorseNumberList[2])));

            stringBuilder.Append(GetTicketHtml(TicketType.Trifecta,
                "trifecta-tab",
                ActualRaceData.TrifectaOdds.Zip(TheoreticalRaceData.TrifectaOdds, (a, t) => new OutputDatum(a, t)).OrderBy(_ => _.HorseNumberList[0]).ThenBy(_ => _.HorseNumberList[1]).ThenBy(_ => _.HorseNumberList[2])));

            stringBuilder.Append($@"<p>※各タブのRはオッズ比1以上の上位100位ランキング</p>");

            return (linkText, title, stringBuilder.ToString(), raceData.StartTime);
        }

        private string GetTicketHtml(TicketType ticket, string tabNameBase, IEnumerable<OutputDatum> outputData)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($@"<h2>{Utility.TicketTypeToJraBetTypeString[ticket]}</h2>");
            stringBuilder.Append(@"<div class=""tab-wrap"">");
            var firstGroups = outputData.GroupBy(_ => _.HorseNumberList[0]);
            if (ticket == TicketType.Quinella || ticket == TicketType.Exacta || ticket == TicketType.Wide)
            {
                foreach (var firstGroup in firstGroups)
                {
                    var horseNum = firstGroup.Key;
                    var tabName = tabNameBase;
                    var table = GetOddsTable(firstGroup);
                    stringBuilder.Append(GetTabHtml(tabName, firstGroup.Key.ToString(), table));
                }
            }
            else if (ticket == TicketType.Trio || ticket == TicketType.Trifecta)
            {
                foreach (var firstGroup in firstGroups)
                {
                    var horseNum = firstGroup.Key;
                    var secondTabName = $"{tabNameBase}-{horseNum}";
                    var secondGroups = firstGroup.GroupBy(_ => _.HorseNumberList[1]);
                    var innerStringBuilder = new StringBuilder();
                    innerStringBuilder.Append(@"<div class=""tab-wrap"">");
                    foreach (var secondGroup in secondGroups)
                    {
                        var table = GetOddsTable(secondGroup);
                        innerStringBuilder.Append(GetTabHtml(secondTabName, secondGroup.Key.ToString(), table));
                    }
                    innerStringBuilder.Append(GetRankingTableTab(secondTabName, firstGroup, 100));
                    innerStringBuilder.Append("</div>");
                    stringBuilder.Append(GetTabHtml(tabNameBase, firstGroup.Key.ToString(), innerStringBuilder.ToString()));
                }
            }
            stringBuilder.Append(GetRankingTableTab(tabNameBase, outputData, 100));
            stringBuilder.Append("</div>");

            return stringBuilder.ToString();
        }

        public string GetTabHtml(string tabNameBase, string tabIndex, string contents, bool check = false)
        {
            return $@"<input id=""{tabNameBase}-{tabIndex}"" type=""radio"" name=""{tabNameBase}"" class=""tab-switch"" {(check ? @"checked=""checked"" " : "")} ><label class=""tab-label"" for=""{tabNameBase}-{tabIndex}"">{tabIndex}</label><div class=""tab-content"">{contents}</div>";
        }

        public string GetOddsTable(IEnumerable<OutputDatum> outputData)
        {
            var count = outputData.Count();
            var tableStringBuilder = new StringBuilder();
            if (count > 0)
            {
                tableStringBuilder.Append($@"<table>
                <tr><th>馬番号</th><th>実オッズ</th><th>予想オッズ</th><th>オッズ比</th></tr>");
                foreach (var outputDatum in outputData)
                {
                    tableStringBuilder.Append(outputDatum.ToTableBodyString());
                }
                tableStringBuilder.Append($@"</table>");
            }
            return tableStringBuilder.ToString();
        }

        public string GetRankingTableTab(string tabName, IEnumerable<OutputDatum> outputData, int rank)
        {
            var outputDataTopN = outputData.Where(_ => _.OddsRatio > 1).OrderByDescending(_ => _.OddsRatio).Take(rank);
            return GetTabHtml(tabName, "R", GetOddsTable(outputDataTopN), true);
        }
    }
}
