using HorseRacingAutoPurchaser.Models;

namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class LoginConfigRepository : BaseRepository<LoginConfig>
    {
        public LoginConfigRepository() : base(@"login_config.xml")
        {
        }
    }
}
