namespace HorseRacingAutoPurchaser
{
    public class BaseStatusRepository : BaseRepository<Status>
    {
        public BaseStatusRepository(string statusFilePath) : base(statusFilePath)
        {
        }
    }
}
