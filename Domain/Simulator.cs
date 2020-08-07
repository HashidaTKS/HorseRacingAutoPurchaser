using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    public class Simulater
    {
        private class Cocomo
        {
            private int Before { get; set; } = 1;

            public int GetNext(int current)
            {
                var next =  current + Before;
                Before = current;
                return next;
            }

            public void Reset()
            {
                Before = 1;
            }
        }

        public static TotalSimulationResult Simulate(DateTime from, DateTime to, bool useOnlySavedData = false)
        {
            if (useOnlySavedData)
            {
                return SimulateInner(from, to, null, useOnlySavedData);
            }
            using (var scraper = new Scraper())
            {
                return SimulateInner(from, to, scraper, useOnlySavedData);
            }

        }

        public static TotalSimulationResult SimulateInner(DateTime from, DateTime to, Scraper scraper, bool useOnlySavedData = false)
        {

            if (to >= DateTime.Today)
            {
                //シミュレーション可能なのは昨日までの結果が確定しているデータとする。
                to = DateTime.Today.AddDays(-1);
            }

            if (!useOnlySavedData)
            {
                //RaceResultManager.UpdateResultDataIfNeed(from, to);
            }

            var outputRaceDataList = RaceDataForComparisonManager.Get(from, to).ToList();
           
            var groupedOutputRaceDataList = outputRaceDataList.Where(_ => _.RaceData.HoldingDatum.Region.RagionType == RegionType.Central).GroupBy(_ => _.RaceData.HoldingDatum.HeldDate);
            //var groupedOutputRaceDataList = outputRaceDataList.GroupBy(_ => _.RaceUrlInformation.HoldingDatum.HeldDate);
            var totalResult = new List<SimulationResultOfDay>();
            var payBackRatio = 1;
            var loseCount = 0;
            var cocomo = new Cocomo();
            foreach (var oneDayOutputRaceDataList in groupedOutputRaceDataList)
            {
                var resultList = new List<SimulationResultOfBet>();
                foreach (var outputRaceData in oneDayOutputRaceDataList)
                {
                    var raceResultRepository = RaceResult.GetRepository(outputRaceData.RaceData);
                    var raceResult = raceResultRepository.ReadAll();
                    if (raceResult == null)
                    {
                        continue;
                    }

                    var betInformationList = TicketSelector.SelectToBet(outputRaceData, payBackRatio);
                    foreach (var betInformation in betInformationList)
                    {
                        var simulationResultOfBet = new SimulationResultOfBet(betInformation, raceResult);
                        if(simulationResultOfBet.IsHit)
                        {
                            cocomo.Reset();
                            payBackRatio = 1;
                        }
                        else
                        {
                            loseCount += 1;
                            if(loseCount % 50 == 0)
                            {
                                payBackRatio = cocomo.GetNext(payBackRatio);
                            }
                        }
                        resultList.Add(simulationResultOfBet);
                    }
                }
                totalResult.Add(new SimulationResultOfDay(resultList));
            }
            return new TotalSimulationResult(totalResult);
        }

    }

    public class TotalSimulationResult
    {
        public List<SimulationResultOfDay> ResultOfDayList { get; set; }

        public double TotalBetMoney => ResultOfDayList.Sum(_ => _.TotalBetMoney);
        public double TotalPayBack => ResultOfDayList.Sum(_ => _.TotalPayBack);
        public double TotalProfit => ResultOfDayList.Sum(_ => _.TotalProfit);

        public TotalSimulationResult(List<SimulationResultOfDay> totalResultOfDay)
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
                    $"{result.BetInformation.RaceData.HoldingDatum.HeldDate.ToString("yyyy/MM/dd")}," +
                    $"{result.BetInformation.RaceData.HoldingDatum.Region.RegionName}," +
                    $"{result.BetInformation.RaceData.RaceNumber}," +
                    $"{result.BetInformation.TicketType.ToString()}," +
                    $"{string.Join(" - ", result.BetInformation.HorseNumList)}," +
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

    public class SimulationResultOfDay
    {
        public double TotalBetMoney => ResultOfBetList.Sum(_ => _.BetMoney);
        public double TotalPayBack => ResultOfBetList.Sum(_ => _.PayBack);

        public double TotalProfit => ResultOfBetList.Sum(_ => _.Profit);

        public List<SimulationResultOfBet> ResultOfBetList { get; set; }

        public SimulationResultOfDay(List<SimulationResultOfBet> resultOfBetList)
        {
            ResultOfBetList = resultOfBetList;
        }

        public IEnumerable<string> ToCsv()
        {
            return ResultOfBetList.Select(_ => _.ToCsv());
        }
    }

    public class SimulationResultOfBet
    {
        public BetInformation BetInformation { get; set; }

        public RaceResult RaceResult { get; set; }

        public double BetMoney => BetInformation.BetMoney;

        public double PayBack { get; set; }

        public bool IsHit => PayBack > 0;

        public double Profit => PayBack - BetMoney;

        public SimulationResultOfBet(BetInformation betInformation, RaceResult raceResult)
        {
            BetInformation = betInformation;
            RaceResult = raceResult;
            SetPayBack();
        }

        private void SetPayBack()
        {
            var result = RaceResult.GetResultHorseAndPayoutOfTicket(BetInformation.TicketType);
            PayBack = result.Item1.SequenceEqual(BetInformation.HorseNumList) ?
                result.Item2 * (BetMoney / 100) :
                0.0;
        }

        public static string GetCsvHeader()
        {
            return "開催日,開催地,レース番号,券種,馬,ベット額,払い戻し,利益";
        }

        public string ToCsv()
        {
            //Todo: Use Csv Helper
            return $"{BetInformation.RaceData.HoldingDatum.HeldDate.ToString("yyyy/MM/dd")},{BetInformation.RaceData.HoldingDatum.Region.RegionName},{BetInformation.RaceData.RaceNumber},{BetInformation.TicketType.ToString()},{string.Join(" - ", BetInformation.HorseNumList)},{BetMoney},{PayBack},{Profit}";
        }
    }
}

