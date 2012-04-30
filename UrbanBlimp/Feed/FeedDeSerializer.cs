using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;

namespace UrbanBlimp.Feed
{
    static class FeedDeSerializer
    {

        public static IEnumerable<Feed> DeSerializeMultiple(Stream content)
        {
            var array = JsonValue.Load(content);

            foreach (var value in array.Select(x => x.Value))
            {
                yield return DeSerializeFeed(value);
            }
        }
        public static Feed DeSerialize(Stream content)
        {
                return DeSerializeFeed(JsonValue.Load(content));
        }

        static Feed DeSerializeFeed(JsonValue value)
        {
            return new Feed
                       {
                           Url = value.StringValue("url"),
                           Id = value.StringValue("id"),
                           Template = DeSerializeTemplate(value["template"]),
                           FeedUrl = value.StringValue("feed_url"),
                           BroadCast = value.BoolValue("broadcast"),
                           LastChecked = value.DateValue("last_checked").Value
                       };
        }

        static Template DeSerializeTemplate(JsonValue value)
        {
            return new Template
                       {
                           FeedPayload = DeSerializePayload(value["aps"]),
                           Tags = value.ListValue("tags")
                       };
        }

        static FeedPayload DeSerializePayload(JsonValue value)
        {
            return new FeedPayload
                       {
                           Alert = value.StringValue("alert"),
                           Badge = value.IntValue("badge"),
                           Sound = value.StringValue("sound"),
                       };
        }
    }
}