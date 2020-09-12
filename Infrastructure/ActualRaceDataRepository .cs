using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class ActualRaceDataRepository : BaseRepository<ActualRaceAndOddsData>
    {
        public ActualRaceDataRepository(string raceDataRepository) : base(raceDataRepository)
        {
        }
    }
}
