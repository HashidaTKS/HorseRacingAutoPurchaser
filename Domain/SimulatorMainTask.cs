using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HorseRacingAutoPurchaser.Utils;
using HorseRacingAutoPurchaser.Infrastructures;
using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Domain
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
            LoggerWrapper.Info("Start SimulatorMainTask");
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

                    if (result == null)
                    {
                        return;
                    }

                    var (resultFilePath , configFilePath) = GetResultFilePaths(from, to);

                    result.OutputCsvToFile(resultFilePath);
                    var configRepo = new BetConfigRepository(configFilePath);
                    configRepo.Store(betConfig);
                }
                catch(Exception ex)
                {
                    LoggerWrapper.Error(ex);
                }
                finally
                {
                    LoggerWrapper.Info("End SimulatorMainTask");
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

        private static (string resultFilePath, string configFilePath) GetResultFilePaths(DateTime from, DateTime to)
        {
            var execTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var resultFileName = $"{execTime}-from-{from.ToString("yyyyMMdd")}-to-{to.ToString("yyyyMMdd")}_result.csv";
            var configFileName = $"{execTime}-betconfig.xml";

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return (Path.Combine(baseDir, "SimulationResult", resultFileName), Path.Combine(baseDir, "SimulationResult", configFileName));
        }
    }
}
