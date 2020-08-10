using System;
using System.Collections.Generic;
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

        public void Run()
        {
            if (Running)
            {
                return;
            }
            CancellationTokenSource = new CancellationTokenSource();
            var cancelToken = CancellationTokenSource.Token;
            var betConfig = new BetConfigRepository().ReadAll();
            betConfig.WideBetConfig = betConfig.QuinellaBetConfig;
            Task.Run(() =>
            {
                if (cancelToken.IsCancellationRequested)
                {
                    return;
                }
                var from = new DateTime(2020, 1, 1);
                var to = new DateTime(2020, 7, 15);

                Simulater.Simulate(from, to, betConfig, cancelToken);
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
