using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace HorseRacingAutoPurchaser
{
    public static class RssAccessor
    {
        public static IEnumerable<RssDatum> GetData(string url)
        {
            var element = XElement.Load(url);
            var channelElement = element.Element("channel");
            var elementItems = channelElement.Elements("item");

            return elementItems
                //後続の処理で少々困るので、現時点ではばんえいは無視する。
                .Select(_ => new RssDatum(_.Element("title").Value, _.Element("link").Value, _.Element("description").Value))
                .Where(_ => !_.Title.Contains("ばんえい"));
        }
    }
}
