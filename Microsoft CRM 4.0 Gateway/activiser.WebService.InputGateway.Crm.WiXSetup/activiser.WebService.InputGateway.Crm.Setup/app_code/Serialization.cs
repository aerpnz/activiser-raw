using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace activiser.WebService
{
    sealed class Serialization
    {
        private Serialization() { }

        private static System.Globalization.CultureInfo ivCulture = System.Globalization.CultureInfo.InvariantCulture;

        public static string Serialize<T>(T item)
        {
            if (item == null) throw new ArgumentNullException("item");

            StringWriter sw = new StringWriter(ivCulture);
            XmlTextWriter xw = new XmlTextWriter(sw);
            xw.Formatting = Formatting.None;
            xw.QuoteChar = '\''; 
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            serializer.Serialize(xw, item);

            return sw.ToString();
        }

        public static T Deserialize<T>(string item)
        {
            if (string.IsNullOrEmpty(item)) throw new ArgumentNullException("item");

            StringReader sr = new StringReader(item);
            XmlTextReader xr = new XmlTextReader(sr);
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(xr);
        }
    }
}
