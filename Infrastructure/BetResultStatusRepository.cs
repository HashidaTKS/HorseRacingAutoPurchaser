﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseRacingAutoPurchaser.Models;


namespace HorseRacingAutoPurchaser.Infrastructures
{
    public class BetResultStatusRepository : BaseRepository<BetResultStatus>
    {
        public BetResultStatusRepository() : base("bet_result_status.xml")
        {

        }
    }
}
