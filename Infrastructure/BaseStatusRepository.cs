using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class BaseStatusRepository : BaseRepository<Status>
    {
        public BaseStatusRepository(string statusFilePath) : base(statusFilePath)
        {
        }
    }
}
