
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace HorseRacingAutoPurchaser
{
    public class Scraper : IDisposable
    {

        private readonly ChromeDriver Chrome;

        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                Chrome.Quit();
                Chrome.Dispose();
                disposed = true;
            }
        }

        public Scraper()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless", "disable-gpu");
            Chrome = new ChromeDriver(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), chromeOptions);
        }

        private IEnumerable<OddsDatum> GetFromWonTable(ReadOnlyCollection<IWebElement> trCollection)
        {
            var count = trCollection.Count;
            for (var i = 1; i < count; i++)
            {
                var tdList = trCollection[i].FindElements(By.TagName("td"));
                if (!int.TryParse(tdList[1].Text, out var horse))
                {
                    continue;
                }
                if (!double.TryParse(tdList.LastOrDefault().Text, out var odds))
                {
                    continue;
                }
                //ここでは馬番号だけが欲しいので、確率その他の情報は無視
                yield return new OddsDatum(new List<HorseDatum> { new HorseDatum(horse, -1, "", "") }, odds);
            }
        }

        private IEnumerable<OddsDatum> GetFromPopularTable(ReadOnlyCollection<IWebElement> trCollection, TicketType ticketType)
        {
            var rank = 1;
            if (ticketType == TicketType.Exacta || ticketType == TicketType.Quinella)
            {
                rank = 2;
            }
            else if (ticketType == TicketType.Trifecta || ticketType == TicketType.Trio)
            {
                rank = 3;
            }

            var count = trCollection.Count;
            var columnCount = trCollection[0].FindElements(By.TagName("th")).Count;
            var startIndexOfHorseNumber = columnCount - rank;
            for (var i = 2; i < count; i++)
            {
                var horseData = new List<HorseDatum>(rank);
                var tdList = trCollection[i].FindElements(By.TagName("td"));
                for (var j = 0; j < rank; j++)
                {
                    var index = startIndexOfHorseNumber + 2 * j;
                    if (!int.TryParse(tdList[index].Text, out var horse))
                    {
                        continue;
                    }
                    //ここでは馬番号だけが欲しいので、確率その他の情報は無視

                    horseData.Add(new HorseDatum(horse, -1, "", ""));
                }
                if (!double.TryParse(tdList[startIndexOfHorseNumber - 1].Text, out var odds))
                {
                    //パースに失敗する->出走取消
                    continue;
                }
                yield return new OddsDatum(horseData, odds);
            }
        }

        public List<OddsDatum> GetOdds(RaceData raceData, TicketType ticketType)
        {
            Chrome.Url = raceData.ToRaseOddsPageUrlString(ticketType);
            Thread.Sleep(300);

            var trCollection = Chrome.FindElementsByClassName("RaceOdds_HorseList_Table").FirstOrDefault()?.FindElements(By.TagName("tr"));
            if (trCollection == null)
            {
                return null;
            }

            if (ticketType == TicketType.Win)
            {
                return GetFromWonTable(trCollection).ToList();
            }
            else
            {
                return GetFromPopularTable(trCollection, ticketType).ToList();
            }

        }

        public RaceResult GetRaceResult(RaceData raceData)
        {
            try
            {
                Chrome.Url = raceData.ToRaceResultPageUrlString();
                Thread.Sleep(300);

                var firstPayoutTable = Chrome.FindElementsByClassName("Payout_Detail_Table").FirstOrDefault();
                var winHorseString = firstPayoutTable.FindElement(By.ClassName("Tansho")).FindElement(By.ClassName("Result")).Text;
                var winPayoutString = firstPayoutTable.FindElement(By.ClassName("Tansho")).FindElement(By.ClassName("Payout")).Text;
                var quinellaHorseString = firstPayoutTable.FindElement(By.ClassName("Umaren")).FindElement(By.ClassName("Result")).Text;
                var quinellaPayoutString = firstPayoutTable.FindElement(By.ClassName("Umaren")).FindElement(By.ClassName("Payout")).Text;

                var secondPayoutTable = Chrome.FindElementsByClassName("Payout_Detail_Table").Skip(1).FirstOrDefault();
                var extractHorseString = secondPayoutTable.FindElement(By.ClassName("Umatan")).FindElement(By.ClassName("Result")).Text;
                var extractPayoutString = secondPayoutTable.FindElement(By.ClassName("Umatan")).FindElement(By.ClassName("Payout")).Text;
                var trioHorseString = secondPayoutTable.FindElement(By.ClassName("Fuku3")).FindElement(By.ClassName("Result")).Text;
                var trioPayoutString = secondPayoutTable.FindElement(By.ClassName("Fuku3")).FindElement(By.ClassName("Payout")).Text;
                var trifectaHorseString = secondPayoutTable.FindElement(By.ClassName("Tan3")).FindElement(By.ClassName("Result")).Text;
                var trifectaPayoutString = secondPayoutTable.FindElement(By.ClassName("Tan3")).FindElement(By.ClassName("Payout")).Text;

                return new RaceResult(raceData)
                {
                    WinHorse = WinHorsesStringToList(winHorseString),
                    WinPayout = PayOutStringToDouble(winPayoutString),
                    QuinellaHorseList = WinHorsesStringToList(quinellaHorseString),
                    QuinellaPayout = PayOutStringToDouble(quinellaPayoutString),
                    ExtractHorseList = WinHorsesStringToList(extractHorseString),
                    ExtractPayout = PayOutStringToDouble(extractPayoutString),
                    TrioHorseList = WinHorsesStringToList(trioHorseString),
                    TrioPayout = PayOutStringToDouble(trioPayoutString),
                    TrifectaHorseList = WinHorsesStringToList(trifectaHorseString),
                    TrifectaPayout = PayOutStringToDouble(trifectaPayoutString),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Could not get result data.");
                return null;
            }
        }


        private List<int> WinHorsesStringToList(string winHorsesString)
        {
            //呼び出し元で例外をそのまま受け取りたいので、敢えてTryParseでなくParse
            return winHorsesString.Trim().Split(' ').Select(_ => int.Parse(_)).ToList();
        }

        private double PayOutStringToDouble(string payOutString)
        {
            //呼び出し元で例外をそのまま受け取りたいので、敢えてTryParseでなくParse
            return double.Parse(payOutString.Replace(",", "").Replace("円", "").Trim());
        }


        /// <summary>
        /// ある開催日のレース情報を一覧取得する。Numberは使わないが一応。
        /// </summary>
        /// <param name="kaisaiDateYYYYMMDD"></param>
        /// <param name="raceType"></param>
        /// <returns></returns>
        public HoldingInformation GetHoldingInformation(DateTime raceDate, RegionType raceType)
        {
            var url = raceType == RegionType.Regional ?
                $"https://nar.netkeiba.com/top/race_list.html?kaisai_date={raceDate.ToString("yyyyMMdd")}" :
                $"https://race.netkeiba.com/top/race_list.html?kaisai_date={raceDate.ToString("yyyyMMdd")}";

            //遅延評価にしたいが、using句の中でyieldして嬉しいことはないのでやめておく

            Console.WriteLine(url);

            try
            {
                Chrome.Url = url;
                //画面の切り替わり完了待ち
                Thread.Sleep(1000);

                var raceListDataListCollection = Chrome.FindElementByClassName("RaceList_Box").FindElements(By.ClassName("RaceList_DataList"));
                var count = raceListDataListCollection.Count;
                var regex = new Regex(@".*?(\d+)回 +(.*?) +(.*)日目.*");

                var raceUrlInfoList = new List<HoldingDatum>();
                for (var i = 0; i < count; i++)
                {
                    //.Textだと非表示のものが取得できない
                    var text = raceListDataListCollection[i].FindElement(By.ClassName("RaceList_DataTitle")).GetAttribute("textContent").Replace("\n", " ").Replace("\r", " ");
                    var match = regex.Match(text);
                    if (!match.Success)
                    {
                        continue;
                    }

                    if (!int.TryParse(match.Groups[1].Value, out var numberOfHeld))
                    {
                        continue;
                    }
                    var regionName = match.Groups[2].Value;
                    if (!int.TryParse(match.Groups[3].Value, out var numberOfDay))
                    {
                        continue;
                    }
                    var startTimeList = new List<DateTime>();
                    var horseCountList = new List<int>();
                    foreach (var item in raceListDataListCollection[i].FindElements(By.ClassName("RaceList_ItemContent")))
                    {
                        var textForData = item.FindElement(By.ClassName("RaceData")).GetAttribute("textContent").Replace("\r", "").Replace("\n", "");
                        var regexForData = new Regex(@".*?(\d\d):(\d\d).*?(\d+)頭.*?");
                        var matchForData = regexForData.Match(textForData);
                        if (!matchForData.Success)
                        {
                            continue;
                        }

                        if (!int.TryParse(matchForData.Groups[1].Value, out var hours))
                        {
                            continue;
                        }
                        if (!int.TryParse(matchForData.Groups[2].Value, out var minutes))
                        {
                            continue;
                        }
                        if (!int.TryParse(matchForData.Groups[3].Value, out var horseNumber))
                        {
                            continue;
                        }
                        var baseDate = raceDate.Date;
                        startTimeList.Add(baseDate.AddHours(hours).AddMinutes(minutes));
                        horseCountList.Add(horseNumber);
                    }

                    raceUrlInfoList.Add(new HoldingDatum(regionName, numberOfHeld, numberOfDay, raceDate, startTimeList, horseCountList));
                }
                return new HoldingInformation(raceUrlInfoList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Invalid Page");
                return null;
            }
        }
    }
}

