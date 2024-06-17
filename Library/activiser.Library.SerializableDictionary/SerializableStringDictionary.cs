using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace activiser.Library
{
    [XmlRoot("stringDictionary")]
    public class SerializableStringDictionary 
        : Dictionary<string, string>, IXmlSerializable
    {
        //public void Add(string key, string value)
        //{
        //    this.Add(key, value);
        //}

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();
            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                string key = reader.ReadElementString("key");
                string value = reader.ReadElementString("value");
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement(); // read closing </stringDictionary>
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (string key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteElementString("key", key);
                writer.WriteElementString("value", this[key]);
                writer.WriteEndElement();
            }
        }
    }
}
