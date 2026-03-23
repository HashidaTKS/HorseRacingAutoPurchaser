using HorseRacingAutoPurchaser.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HorseRacingAutoPurchaser.Infrastructures;


namespace HorseRacingAutoPurchaser.Models
{
    [DataContract]
    public class BetInformation
    {
        [DataMember]
        public RaceData RaceData { get; set; }

        [DataMember]
        public List<BetDatum> BetData { get; set; }

        public BetInformation()
        {
        }


        public BetInformation(RaceData raceData, List<BetDatum> betData)
        {
            RaceData = raceData;
            BetData = betData;
        }

        public BetInformationRepository GetRepository()
        {
            return GetRepository(RaceData);
        }

        private static readonly ConcurrentDictionary<string, BetInformationRepository> _repositoryCache
            = new ConcurrentDictionary<string, BetInformationRepository>();

        public static BetInformationRepository GetRepository(RaceData raceData)
        {
            var path = Utility.GetBetInformationFilePath(raceData);
            return _repositoryCache.GetOrAdd(path, p => new BetInformationRepository(p));
        }
    }
}
