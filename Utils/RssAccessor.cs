using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using HorseRacingAutoPurchaser.Models;


namespace HorseRacingAutoPurchaser.Utils
{
    public static class RssAccessor
    {
        public static IEnumerable<RssDatum> GetData(string url)
        {
            var element = XElement.Load(url);
            var channelElement = element.Element("channel");
            if (channelElement == null)
            {
                return Enumerable.Empty<RssDatum>();
            }

            return channelElement.Elements("item")
                .Where(_ => _.Element("title") != null && _.Element("link") != null && _.Element("description") != null)
                //後続の処理で少々困るので、現時点ではばんえいは無視する。
                .Select(_ => new RssDatum(_.Element("title").Value, _.Element("link").Value, _.Element("description").Value))
                .Where(_ => !_.Title.Contains("ばんえい"));
        }
    }
}
