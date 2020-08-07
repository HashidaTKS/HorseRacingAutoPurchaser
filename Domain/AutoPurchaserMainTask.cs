using System;
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
            Task.Run(() =>
            {
                if (cancelToken.IsCancellationRequested)
                {
                    return;
                }
                using (var scraper = new Scraper())
                {
                    var loginConfig = (new LoginConfigRepository()).ReadAll();
                    var autoPurchaser = new AutoPurchaser(loginConfig);
                    var today = DateTime.Today;
                    var raceData = RaceDataManager.GetAndStoreRaceDataOfDay(today, scraper);
                    var targetRaceList = raceData;//.Where(_ => _.StartTime >= DateTime.Now && _.StartTime <= DateTime.Now.AddMinutes(5));
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
                                repo.Store(actualRaceAndOddsData);
                            }
                            else
                            {
                                //保存されているということは購入済みなのでスキップする
                                continue;
                            }

                            var raceDataForComparison = RaceDataForComparisonManager.Get(actualRaceAndOddsData);
                            var betInformationList = TicketSelector.SelectToBet(raceDataForComparison, 1);
                            autoPurchaser.Purchase(betInformationList);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            continue;
                        }
                    }
                }
                for (var i = 0; i < 30; i++)
                {
                    Thread.Sleep(1 * 1000);
                    if (cancelToken.IsCancellationRequested)
                    {
                        return;
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
    }
}