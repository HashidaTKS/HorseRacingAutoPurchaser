using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRacingAutoPurchaser
{
    public class BetResultStatusRepository : BaseRepository<BetResultStatus>
    {
        public BetResultStatusRepository() : base("bet_result_status.xml")
        {

        }
    }
}
