using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CompucareWard.Models.Converters
{
    public class XMLConverter<T> : JsonConverter<T>
        where T : class, new()
    {
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            writer.WriteValue(SerializeXml<T>(value, false));
        }

        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return DeserializeXml<T>((string)reader.Value);
        }

        public static TObject DeserializeXml<TObject>(string xml)
        {
            TObject deserialized = default(TObject);

            if (!String.IsNullOrEmpty(xml))
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    deserialized = (TObject)serializer.Deserialize(XmlReader.Create(sr));
                }

            return deserialized;
        }

        public static string SerializeXml<TObject>(object obj, bool encodeCommas)
        {
            string serialized = null;

            if (obj != null)
                using (StringWriter sw = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TObject));
                    serializer.Serialize(sw, obj);
                    sw.Flush();
                    serialized = sw.ToString();
                }

            if (encodeCommas)
                return serialized.Replace(",", "&#44;");
            else
                return serialized;
        }
    }
}
