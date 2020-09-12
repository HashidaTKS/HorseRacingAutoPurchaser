using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class RaceResultRepository : BaseRepository<RaceResult>
    {
        public RaceResultRepository(string raceResultPath) : base(raceResultPath)
        {
        }
    }
}
