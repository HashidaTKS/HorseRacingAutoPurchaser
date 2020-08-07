using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    public class BetConfig
    {
        [DataMember]
        public BetConfigForOneTicket QuinellaBetConfig { get; set; } = new BetConfigForOneTicket();
    }

    [DataContract]

    public class BetConfigForOneTicket
    {

        /// <summary>
        /// このオッズ比以上のチケットを購入する
        /// </summary>
        [DataMember]

        public double OddsRatio { get; set; } = 1.4;

        /// <summary>
        /// 最低払い戻し金額。払い戻しがこの金額以上になるようにオッズ額を修正する。
        /// </summary>
        [DataMember]
        public double MinimumPayBack { get; set; } = 5000;

        /// <summary>
        /// 購入しても良い最低オッズ
        /// </summary>
        [DataMember]
        public double MinimumOdds { get; set; } = 5;

        /// <summary>
        /// 購入しても良い最大オッズ
        /// </summary>
        [DataMember]
        public double MaximumOdds { get; set; } = 100;

        /// <summary>
        /// ココモ法を使うかどうか。
        /// </summary>
        [DataMember]
        public bool UseCocomo { get; set; } = true;

        /// <summary>
        /// ココモ法を使う場合に、何連敗ごとにオッズを増やすか
        /// </summary>
        [DataMember]
        public int CocomoThreshold { get; set; } = 50;

        /// <summary>
        /// ココモ法を使う場合に、許容可能な倍率（この倍率になったら加算をやめる）
        /// </summary>
        [DataMember]
        public int CocomoMaxMagnification { get; set; } = 10;
    }
}
