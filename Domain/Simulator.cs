using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace HorseRacingAutoPurchaser
{
    public class Simulater
    {
        public static TotalResultOfBet Simulate(DateTime from, DateTime to, BetConfig betConfig, CancellationToken cancelToken, bool useOnlySavedData = false)
        {
            if (useOnlySavedData)
            {
                return SimulateInner(from, to, null, betConfig, cancelToken, useOnlySavedData);
            }
            using (var scraper = new Scraper())
            {
                return SimulateInner(from, to, scraper, betConfig, cancelToken, useOnlySavedData);
            }

        }

        public static TotalResultOfBet SimulateInner(DateTime from, DateTime to, Scraper scraper, BetConfig betConfig, CancellationToken cancelToken, bool useOnlySavedData = false)
        {

            if (to >= DateTime.Today)
            {
                //シミュレーション可能なのは昨日までの結果が確定しているデータとする。
                to = DateTime.Today.AddDays(-1);
            }

            if (!useOnlySavedData)
            {
                for (var date = from; date <= to; date = date.AddDays(1))
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        return null;
                    }
                    RaceResultManager.UpdateResultDataIfNeed(scraper, date, date);

                    //UpdateResultDataIfNeedの中でレース情報がなければ取得されているので、保存済みのもののみを対象にする。
                    foreach (var targetRace in RaceDataManager.GetRaceDataOfDay(date))
                    {
                        if (cancelToken.IsCancellationRequested)
                        {
                            return null;
                        }
                        if (targetRace.HoldingDatum.Region.RagionType == RegionType.Central)
                        {
                            if (!betConfig.ContainCentral())
                            {
                                continue;
                            }
                        }
                        if (targetRace.HoldingDatum.Region.RagionType == RegionType.Regional)
                        {
                            if (!betConfig.ContainRegional())
                            {
                                continue;
                            }
                        }
                        var actualRaceAndOddsData = new ActualRaceAndOddsData(targetRace);
                        var repo = actualRaceAndOddsData.GetRepository();
                        var savedData = repo.ReadAll();
                        if (savedData == null)
                        {
                            actualRaceAndOddsData.SetData(scraper);
                        }
                        else
                        {
                            continue;
                        }
                        repo.Store(actualRaceAndOddsData);
                    }
                }
            }

            var raceDataForComparisonManager = RaceDataForComparisonManager.Get(from, to).ToList();
            var outputRaceDataList = new List<RaceDataForComparison>();

            if (betConfig.ContainCentral())
            {
                outputRaceDataList.AddRange(raceDataForComparisonManager.Where(_ => _.RaceData.HoldingDatum.Region.RagionType == RegionType.Central));
            }
            if (betConfig.ContainRegional())
            {
                outputRaceDataList.AddRange(raceDataForComparisonManager.Where(_ => _.RaceData.HoldingDatum.Region.RagionType == RegionType.Regional));
            }

            var groupedOutputRaceDataList = outputRaceDataList.GroupBy(_ => _.RaceData.HoldingDatum.HeldDate);
            var totalResult = new List<DailyResultOfBet>();
            var betResultStatus = new BetResultStatus();
            foreach (var oneDayOutputRaceDataList in groupedOutputRaceDataList)
            {
                var resultList = new List<ResultOfBet>();
                foreach (var outputRaceData in oneDayOutputRaceDataList)
                {
                    var raceResultRepository = RaceResult.GetRepository(outputRaceData.RaceData);
                    var raceResult = raceResultRepository.ReadAll();
                    if (raceResult == null)
                    {
                        continue;
                    }

                    var betData = TicketSelector.SelectToBet(outputRaceData, betConfig, betResultStatus);
                    foreach (var betDatum in betData)
                    {
                        var simulationResultOfBet = new ResultOfBet(betDatum, raceResult);

                        var targetStatus = betResultStatus.GetTicketTypeStatus(betDatum.TicketType);
                        if (simulationResultOfBet.IsHit)
                        {
                            targetStatus.CountOfContinuationLose = 0;
                        }
                        else
                        {
                            targetStatus.CountOfContinuationLose += 1;
                        }

                        resultList.Add(simulationResultOfBet);
                    }
                }
                totalResult.Add(new DailyResultOfBet(resultList));
            }
            return new TotalResultOfBet(totalResult);
        }

    }

    public class TotalResultOfBet
    {
        public List<DailyResultOfBet> ResultOfDayList { get; set; }

        public double TotalBetMoney => ResultOfDayList.Sum(_ => _.TotalBetMoney);
        public double TotalPayBack => ResultOfDayList.Sum(_ => _.TotalPayBack);
        public double TotalProfit => ResultOfDayList.Sum(_ => _.TotalProfit);

        public TotalResultOfBet(List<DailyResultOfBet> totalResultOfDay)
        {
            ResultOfDayList = totalResultOfDay;
        }

        public IEnumerable<string> ToCsv()
        {
            return ResultOfDayList.SelectMany(_ => _.ResultOfBetList).Select(_ => _.ToCsv());
        }

        public static string GetCsvHeader()
        {
            return "開催日,開催地,レース番号,券種,馬,ベット額,払い戻し,利益,総ベット額,総払い戻し額,総利益,開始からの還元率,開始からの的中率";
        }

        private void OutputCsvToStream(StreamWriter streamWriter)
        {
            //Todo: Use Csv Helper
            streamWriter.WriteLine(GetCsvHeader());
            double currentTotalProfit = 0;
            double currentTotalBetMoney = 0;
            double currentTotalPayBack = 0;
            double currentReturnRate = 0;
            double hitCount = 0;
            double totalCount = 0;
            foreach (var result in ResultOfDayList.SelectMany(_ => _.ResultOfBetList))
            {
                currentTotalProfit += result.Profit;
                currentTotalBetMoney += result.BetMoney;
                currentTotalPayBack += result.PayBack;
                currentReturnRate = currentTotalBetMoney == 0 ? 0 : currentTotalPayBack / currentTotalBetMoney;
                if (result.IsHit)
                {
                    hitCount += 1;
                }
                totalCount += 1;
                var outputLine =
                    $"{result.BetDatum.RaceData.HoldingDatum.HeldDate.ToString("yyyy/MM/dd")}," +
                    $"{result.BetDatum.RaceData.HoldingDatum.Region.RegionName}," +
                    $"{result.BetDatum.RaceData.RaceNumber}," +
                    $"{result.BetDatum.TicketType.ToString()}," +
                    $"{string.Join(" - ", result.BetDatum.HorseNumList)}," +
                    $"{result.BetMoney}," +
                    $"{result.PayBack}," +
                    $"{result.Profit}," +
                    $"{currentTotalBetMoney}," +
                    $"{currentTotalPayBack}," +
                    $"{currentTotalProfit}," +
                    $"{currentReturnRate}," +
                    $"{hitCount / totalCount}";


                streamWriter.WriteLine(outputLine);
            }
        }

        public void OutputCsvToFile(string path)
        {
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            using (var streamWriter = new StreamWriter(stream))
            {
                OutputCsvToStream(streamWriter);
            }
        }
    }

    public class DailyResultOfBet
    {
        public double TotalBetMoney => ResultOfBetList.Sum(_ => _.BetMoney);
        public double TotalPayBack => ResultOfBetList.Sum(_ => _.PayBack);

        public double TotalProfit => ResultOfBetList.Sum(_ => _.Profit);

        public List<ResultOfBet> ResultOfBetList { get; set; }

        public DailyResultOfBet(List<ResultOfBet> resultOfBetList)
        {
            ResultOfBetList = resultOfBetList;
        }

        public IEnumerable<string> ToCsv()
        {
            return ResultOfBetList.Select(_ => _.ToCsv());
        }
    }

    public class ResultOfBet
    {
        public BetDatum BetDatum { get; set; }

        public RaceResult RaceResult { get; set; }

        public double BetMoney => BetDatum.BetMoney;

        public double PayBack { get; set; }

        public bool IsHit => PayBack > 0;

        public double Profit => PayBack - BetMoney;

        public ResultOfBet(BetDatum betDatum, RaceResult raceResult)
        {
            BetDatum = betDatum;
            RaceResult = raceResult;
            PayBack = GetPayBack();
        }

        private double GetPayBack()
        {
            var results = RaceResult.GetResultHorseAndPayoutOfTicket(BetDatum.TicketType);

            //Todo: 綺麗にする
            if (BetDatum.TicketType == TicketType.Quinella || BetDatum.TicketType == TicketType.Wide || BetDatum.TicketType == TicketType.Trio)
            {
                foreach (var result in results)
                {
                    //元々並びは同じはずだが、一応
                    if (result.Item1.OrderBy(_ => _).SequenceEqual(BetDatum.HorseNumList.OrderBy(_ => _)))
                    {
                        return result.Item2 * (BetMoney / 100);
                    }
                }
            }
            else
            {
                foreach (var result in results)
                {
                    if (result.Item1.SequenceEqual(BetDatum.HorseNumList))
                    {
                        return result.Item2 * (BetMoney / 100);
                    }
                }
            }
            return 0.0;
        }

        public static string GetCsvHeader()
        {
            return "開催日,開催地,レース番号,券種,馬,ベット額,払い戻し,利益";
        }

        public string ToCsv()
        {
            //Todo: Use Csv Helper
            return $"{BetDatum.RaceData.HoldingDatum.HeldDate.ToString("yyyy/MM/dd")},{BetDatum.RaceData.HoldingDatum.Region.RegionName},{BetDatum.RaceData.RaceNumber},{BetDatum.TicketType.ToString()},{string.Join(" - ", BetDatum.HorseNumList)},{BetMoney},{PayBack},{Profit}";
        }
    }
}

