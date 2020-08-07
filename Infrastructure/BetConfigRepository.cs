using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRacingAutoPurchaser
{
    public class BetConfigRepository: BaseRepository<BetConfig>
    {
        public BetConfigRepository() : base("bet_config.xml")
        {

        }
    }
}
