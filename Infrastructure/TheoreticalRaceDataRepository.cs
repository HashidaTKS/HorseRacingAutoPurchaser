using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class TheoreticalRaceDataRepository : BaseRepository<TheoreticalRaceAndOddsData>
    {
        public TheoreticalRaceDataRepository(string raceDataRepository) : base(raceDataRepository)
        {
        }
    }
}
