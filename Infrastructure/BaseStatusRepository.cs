namespace HorseRacingAutoPurchaser
{
    public abstract class BaseStatusRepository : BaseRepository<Status>
    {
        public BaseStatusRepository(string statusFilePath) : base(statusFilePath)
        {
        }
    }
}
