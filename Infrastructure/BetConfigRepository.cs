using System;
using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class BetConfigRepository: BaseRepository<BetConfig>
    {
        public BetConfigRepository() : base("bet_config.xml")
        {

        }

        public BetConfigRepository(string filePath) : base(filePath)
        {

        }
    }
}
