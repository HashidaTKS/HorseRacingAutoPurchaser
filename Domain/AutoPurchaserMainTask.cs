using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HorseRacingAutoPurchaser
{
    public class AutoPurchaserMainTask
    {
        public bool Running => CancellationTokenSource != null;

        private CancellationTokenSource CancellationTokenSource { get; set; }

        public void Run()
        {
            if (Running)
            {
                return;
            }
            CancellationTokenSource = new CancellationTokenSource();
            var cancelToken = CancellationTokenSource.Token;
            var betConfig = new BetConfigRepository().ReadAll();
            Task.Run(() =>
            {                
                if (cancelToken.IsCancellationRequested)
                {
                    return;
                }
                var loginConfig = new LoginConfigRepository().ReadAll();
                var betResultStatusRepo = new BetResultStatusRepository();

                using (var scraper = new Scraper())
                using (var autoPurchaser = new AutoPurchaser(loginConfig))
                {
                    while (true)
                    {
                        var betResultStatus = betResultStatusRepo.ReadAll();
                        if (betResultStatus == null)
                        {
                            betResultStatus = new BetResultStatus();
                            betResultStatusRepo.Store(betResultStatus);
                        }
                        var today = DateTime.Today;
                        var raceData = RaceDataManager.GetAndStoreRaceDataOfDay(today, scraper);
                        var targetRaceList = new List<RaceData>();
                        if (betConfig.QuinellaBetConfig.PurchaseCentral)
                        {
                            targetRaceList.AddRange(raceData
                                .Where(_ => _.HoldingDatum.Region.RagionType == RegionType.Central)
                                .Where(_ => _.StartTime >= DateTime.Now && _.StartTime <= DateTime.Now.AddMinutes(5)));
                        }
                        if (betConfig.QuinellaBetConfig.PurchaseRegional)
                        {
                            targetRaceList.AddRange(raceData
                                //ばんえいは除く。
                                .Where(_ => _.HoldingDatum.Region.RagionType == RegionType.Regional && _.HoldingDatum.Region.RegionId != "65")
                                .Where(_ => _.StartTime >= DateTime.Now && _.StartTime <= DateTime.Now.AddMinutes(5)));
                        }

                        foreach (var targetRace in targetRaceList)
                        {
                            try
                            {
                                var actualRaceAndOddsData = new ActualRaceAndOddsData(targetRace);
                                var repo = actualRaceAndOddsData.GetRepository();
                                var savedData = repo.ReadAll();
                                if (savedData == null)
                                {
                                    actualRaceAndOddsData.SetData(scraper);
                                }
                                else
                                {
                                    //保存されているということは購入済みなのでスキップする
                                    continue;
                                }

                                var raceDataForComparison = RaceDataForComparisonManager.Get(actualRaceAndOddsData);
                                var betData = TicketSelector.SelectToBet(raceDataForComparison, betConfig, betResultStatus);

                                if (betData != null && betData.Count > 0)
                                {
                                    if (autoPurchaser.Purchase(betData))
                                    {
                                        var betInformation = new BetInformation(targetRace, betData);
                                        var betInfoRepo = betInformation.GetRepository();
                                        betInfoRepo.Store(betInformation);
                                    }
                                }

                                repo.Store(actualRaceAndOddsData);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                continue;
                            }
                        }

                        //必要なら結果データを更新しながら、過去のベットデータから未確認の結果を確認する。
                        //ただし最大でも一カ月前までしか遡らない。
                        //また、レースデータが存在している部分のみを対象とする。
                        var monthBefore = DateTime.Now.AddMonths(-1);
                        var statusCheckTargetTime = DateTime.Now.AddHours(-1);
                        var statusCheckStart = betResultStatus.CheckedTime > monthBefore ? betResultStatus.CheckedTime : monthBefore;


                        for (var date = statusCheckStart; date < statusCheckTargetTime; date = date.AddDays(1))
                        {
                            try
                            {
                                if (cancelToken.IsCancellationRequested)
                                {
                                    return;
                                }
                                foreach (var targetRace in RaceDataManager.GetRaceDataOfDay(date.Date))
                                {
                                    if (targetRace == null)
                                    {
                                        continue;
                                    }
                                    if (targetRace.StartTime > statusCheckTargetTime)
                                    {
                                        //本日のまだ確定していないデータの可能性があるので、スキップ
                                        continue;
                                    }
                                    if (targetRace.StartTime < statusCheckStart)
                                    {
                                        //確認済みなのでスキップ
                                        continue;
                                    }


                                    try
                                    {
                                        RaceResultManager.UpdateResultDataIfNeed(scraper, targetRace);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                        continue;
                                    }

                                    var betInformation = BetInformation.GetRepository(targetRace).ReadAll();
                                    if (betInformation == null)
                                    {
                                        continue;
                                    }

                                    var raceResult = RaceResult.GetRepository(targetRace).ReadAll();
                                    foreach (var betDatum in betInformation.BetData)
                                    {
                                        var resultOfBet = new ResultOfBet(betDatum, raceResult);
                                        switch (betDatum.TicketType)
                                        {
                                            case TicketType.Quinella:
                                                if (resultOfBet.IsHit)
                                                {
                                                    betResultStatus.QuinellaBetStatus.CountOfContinuationLose = 0;
                                                }
                                                else
                                                {
                                                    betResultStatus.QuinellaBetStatus.CountOfContinuationLose++;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                continue;
                            }
                        }

                        betResultStatus.CheckedTime = statusCheckTargetTime;
                        betResultStatusRepo.Store(betResultStatus);

                        for (var i = 0; i < 30; i++)
                        {
                            Thread.Sleep(1 * 1000);
                            if (cancelToken.IsCancellationRequested)
                            {
                                return;
                            }
                        }
                    }
                }

            }, cancelToken);
        }

        public void Stop()
        {
            if (!Running)
            {
                return;
            }
            CancellationTokenSource.Cancel();
            CancellationTokenSource = null;
        }

        private void PurchaseIfNeed()
        {

        }
    }
}