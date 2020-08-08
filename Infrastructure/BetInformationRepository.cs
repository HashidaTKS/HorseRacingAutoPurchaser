using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRacingAutoPurchaser
{
    public class BetInformationRepository : BaseRepository<BetInformation>
    {
        public BetInformationRepository(string betInformationPath) : base(betInformationPath)
        {
        }
    }
}
