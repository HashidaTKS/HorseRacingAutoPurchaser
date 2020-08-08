using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    public class BetResultStatus
    {
        /// <summary>
        /// ステータスのチェックが行われている時刻
        /// チェックをした時刻ではなく、ここまでのデータはチェック済みである、という意味
        /// </summary>
        [DataMember]
        public DateTime CheckedTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 馬連のステータス
        /// </summary>
        [DataMember]
        public BetResultStatusOfTicketType QuinellaBetStatus { get; set; } = new BetResultStatusOfTicketType();
    }

    [DataContract]

    public class BetResultStatusOfTicketType
    {
        [DataMember]
        public int CountOfContinuationLose { get; set; } = 0;
    }
}
