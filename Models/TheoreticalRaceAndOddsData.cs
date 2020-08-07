using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    [Serializable]
    public class TheoreticalRaceAndOddsData : RaceAndOddsData
    {


        private List<HorseDatum> HorseData { get; set; }

        public TheoreticalRaceAndOddsData(RaceData raceData) : base(raceData)
        {
        }


        /// <summary>
        /// 期待オッズを計算する。
        /// これを呼ぶときには、HorseDataに理論値が入っている必要がある。
        /// </summary>
        public void SetData()
        {
            var calculator = new Calculator(HorseData);

            LastUpadateUtcTime = DateTime.UtcNow;
            WinOdds = calculator.GetAllWinOdds().OrderBy(_ => _.Odds).ToList();
            QuinellaOdds = calculator.GetAllQuinellaOdds(2).OrderBy(_ => _.Odds).ToList();
            //今は使わないので敢えて取得しない
            //ExactaOdds = calculator.GetAllExactaOdds(2).OrderBy(_ => _.Odds).ToList();
            //TrifectaOdds = calculator.GetAllExactaOdds(3).OrderBy(_ => _.Odds).ToList();
            //TrioOdds = calculator.GetAllQuinellaOdds(3).OrderBy(_ => _.Odds).ToList();
        }

        public void SetHorseDataFromWinOdds(List<OddsDatum> winOddsData)
        {
            var horceData = new List<HorseDatum>();
            foreach(var winOdds in winOddsData)
            {
                var horse = winOdds.HorseData?.FirstOrDefault();
                if(horse == null)
                {
                    continue;
                }
                horceData.Add(new HorseDatum(horse.Number, 1 / winOdds.Odds, horse.Name, horse.Jockey));
            }
            this.HorseData = horceData;
        }

        public bool HasData()
        {
            return !(WinOdds == null  || ExactaOdds == null || TrifectaOdds == null || QuinellaOdds == null || TrioOdds == null
                || WinOdds.Count == 0 || ExactaOdds.Count == 0 || TrifectaOdds.Count == 0 || QuinellaOdds.Count == 0 || TrioOdds.Count == 0 );
        }

        public TheoreticalRaceDataRepository GetRepository()
        {
            return GetRepository(BaseRaceData);
        }

        public static TheoreticalRaceDataRepository GetRepository(RaceData raceData)
        {
            var path = Utility.GetRaceAndOddsDataFilePath(raceData, OddsType.Theoretical);
            return new TheoreticalRaceDataRepository(path);
        }
    }
}
