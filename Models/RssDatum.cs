using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace HorseRacingAutoPurchaser.Models
{
    [DataContract]
    [Serializable]
    public class RssDatum : IEquatable<RssDatum>
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Link { get; set; }

        [IgnoreDataMember]
        public string Description { get; set; }

        public RssDatum(string title, string link, string description)
        {
            Title = title;
            Link = link;
            Description = description;
        }

        public bool Equals(RssDatum rssData)
        {
            return Title.Equals(rssData.Title);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        /// <summary>
        /// タイトルから、レースの日にち、地域、レース番号を返す。先頭のbooleanは成否。
        /// </summary>
        /// <returns></returns>
        public (bool, DateTime, string , int) GetRaceInfo()
        {
           var regex = new Regex(@"(\d\d\d\d\d\d\d\d) *(.*?)(\d+).*");
            var match = regex.Match(Title);
            if (!match.Success)
            {
                return (false, DateTime.MinValue, "", 0);
            }
            var date = DateTime.ParseExact(match.Groups[1].Value, "yyyyMMdd", null);
            var region = match.Groups[2].Value;
            var raceNumber = int.Parse(match.Groups[3].Value);
            return (true, date, region, raceNumber);
        } 
    }
}
