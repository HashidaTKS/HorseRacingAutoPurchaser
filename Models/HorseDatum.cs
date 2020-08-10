using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HorseRacingAutoPurchaser
{
    [DataContract]
    [Serializable]
    public class HorseDatum
    {
        [DataMember]
        public int Number { get; set; }

        public double WinProbability { get; set; }

        public string Name { get; set; }

        public string Jockey { get; set; }

        public HorseDatum(){
        }

        public HorseDatum(int number, double winProbablility, string name, string jockey) {
            Number = number;
            WinProbability = winProbablility;
            Name = name;
            Jockey = jockey;
        }

        public override string ToString()
        {
            return $"馬番[{Number}], 名前[{Name}], 騎手[{Jockey}]";
        }
    }
}
