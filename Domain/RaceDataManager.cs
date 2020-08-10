using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HorseRacingAutoPurchaser
{
    public class RaceDataManager
    {
        public static IEnumerable<RaceData> GetRaceDataOfDay(DateTime date)
        {
            var holdingInformationRepository = new HoldingInformationRepository();
            var currentHoldingInformation = holdingInformationRepository.ReadAll();

            var holdingData = new List<HoldingDatum>();
            try
            {
                holdingData = currentHoldingInformation.HoldingData.Where(_ => _.HeldDate.Date == date.Date).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                yield break;
            }

            foreach (var holdingDatum in holdingData)
            {
                if (holdingDatum == null || !holdingDatum.HasFullData)
                {
                    continue;
                }
                for (var raceNumber = 1; raceNumber <= holdingDatum.TotalRaceCount; raceNumber++)
                {
                    yield return new RaceData(holdingDatum, raceNumber);
                }
            }
        }

        public static IEnumerable<RaceData> GetAndStoreRaceDataOfDay(DateTime date, Scraper scraper)
        {
            var holdingInformationRepository = new HoldingInformationRepository();
            var currentHoldingInformation = holdingInformationRepository.ReadAll();

            if(currentHoldingInformation == null || !currentHoldingInformation.HoldingData.Any(_ => _.HeldDate.Date == date.Date))
            {
                try
                {
                    //別々に取得しなきゃいけないのおかしくね
                    var centralHoldingInformation = scraper.GetHoldingInformation(date, RegionType.Central);
                    var regionalHoldingInformation = scraper.GetHoldingInformation(date, RegionType.Regional);

                    currentHoldingInformation = currentHoldingInformation.MargeStatus(centralHoldingInformation).MargeStatus(regionalHoldingInformation);
                    new HoldingInformationRepository().Store(currentHoldingInformation);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    yield break;
                }
            }
         
            var holdingData = currentHoldingInformation.HoldingData.Where(_ => _.HeldDate.Date == date.Date).ToList();           

            foreach (var holdingDatum in holdingData)
            {
                if (holdingDatum == null || !holdingDatum.HasFullData)
                {
                    continue;
                }
                for (var raceNumber = 1; raceNumber <= holdingDatum.TotalRaceCount; raceNumber++)
                {
                    yield return new RaceData(holdingDatum, raceNumber);
                }
            }
        }

        public static RaceData GetRaceData(DateTime date, string regionName, int raceNumber)
        {
            var region = Utility.GetRegionFromName(regionName);
            if (string.IsNullOrEmpty(region.RegionName))
            {
                return null;
            }

            var holdingInformationRepository = new HoldingInformationRepository();
            var currentHoldingInformation = holdingInformationRepository.ReadAll();

            HoldingDatum holdingDatum;
            try
            {
                holdingDatum = currentHoldingInformation?.GetHoldingDatum(date, region.RegionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            if (holdingDatum == null || !holdingDatum.HasFullData)
            {
                return null;
            }

            return new RaceData(holdingDatum, raceNumber);
        }

        public static RaceData GetAndStoreRaceData(DateTime date, string regionName, int raceNumber, Scraper scraper)
        {
            var region = Utility.GetRegionFromName(regionName);
            if (string.IsNullOrEmpty(region.RegionName))
            {
                return null;
            }

            var holdingInformationRepository = new HoldingInformationRepository();

            var currentHoldingInformation = holdingInformationRepository.ReadAll();

            HoldingDatum holdingDatum;
            try
            {
                holdingDatum = currentHoldingInformation?.GetHoldingDatum(date, region.RegionId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            if (holdingDatum == null || !holdingDatum.HasFullData)
            {
                var heldRaseData = scraper.GetHoldingInformation(date, region.RagionType);
                if(heldRaseData == null)
                {
                    return null;
                }
                holdingDatum = heldRaseData.GetHoldingDatum(date, region.RegionId);

                //リポジトリを更新しておく
                heldRaseData.MargeStatus(currentHoldingInformation);
                holdingInformationRepository.Store(heldRaseData);
            }

            if (holdingDatum == null || !holdingDatum.HasFullData)
            {
                //リモートからデータを取得しても駄目だったので諦める。
                return null;
            }

            return new RaceData(holdingDatum, raceNumber);
        }

        /// <summary>
        /// タイトルから、レースの日にち、地域、レース番号を返す。先頭のbooleanは成否。
        /// </summary>
        /// <returns></returns>
        public static (bool, DateTime, string, int) GetRaceInfo(string fileName)
        {
            var regex = new Regex(@"(\d\d\d\d\d\d\d\d)_*(.*?)(\d+).*");
            var match = regex.Match(fileName);
            if (!match.Success)
            {
                return (false, DateTime.MinValue, "", 0);
            }
            var date = DateTime.ParseExact(match.Groups[1].Value, "yyyyMMdd", null);
            var region = match.Groups[2].Value;
            var raceNumber = int.Parse(match.Groups[3].Value);
            return (true, date, region, raceNumber);
        }
    }
}
