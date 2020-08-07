using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    [Serializable]
    public class Status
    {
        [DataMember]
        public List<RssDatum> RssData { get; set; }

        public Status MargeStatus(Status status)
        {
            if(status == null)
            {
                return this;
            }
            RssData.AddRange(status.RssData);
            RssData = RssData.Distinct().ToList();
            return this;
        }
    }
}