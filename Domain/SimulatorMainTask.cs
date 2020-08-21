using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HorseRacingAutoPurchaser
{
    class SimulatorMainTask
    {
        public bool Running => CancellationTokenSource != null;

        private CancellationTokenSource CancellationTokenSource { get; set; }

        public void Run(DateTime from , DateTime to, bool useOnlySavedData)
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
                try
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        return;
                    }

                    var result = Simulater.Simulate(from, to, betConfig, cancelToken, useOnlySavedData);

                    var execTime = DateTime.Now.ToString("yyyyMMddhhmmss");
                    var resultFileName = $"{execTime}-from-{from.ToString("yyyyMMdd")}-to-{to.ToString("yyyyMMdd")}_result.csv";
                    var configFileName = $"{execTime}-betconfig.xml";

                    var resultFilePath = Path.Combine(".", "SimulationResult", resultFileName);
                    var configFilePath = Path.Combine(".", "SimulationResult", configFileName);

                    result.OutputCsvToFile(resultFilePath);
                    var configRepo = new BetConfigRepository(configFilePath);
                    configRepo.Store(betConfig);
                }
                finally
                {
                    CancellationTokenSource.Dispose();
                    CancellationTokenSource = null;
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
        }

        private void PurchaseIfNeed()
        {

        }
    }
}
