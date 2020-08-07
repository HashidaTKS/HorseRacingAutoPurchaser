namespace HorseRacingAutoPurchaser
{
    public class LoginConfigRepository : BaseRepository<LoginConfig>
    {
        public LoginConfigRepository() : base(@"login_config.xml")
        {
        }
    }
}
