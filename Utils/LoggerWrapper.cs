using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRacingAutoPurchaser.Utils
{
    public static class LoggerWrapper
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Debug(string str)
        {
            _logger.Debug(str);
        }

        public static void Info(string str)
        {
            _logger.Info(str);
        }

        public static void Warn(string str)
        {
            _logger.Warn(str);
        }

        public static void Error(string str)
        {
            _logger.Error(str);
        }


    }
}

