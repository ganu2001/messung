using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace XMPS2000.Core.Serializer
{
    public class SerializeDeserialize<T>
    {

        StringBuilder sbData;
        StringWriter swWriter;
        XmlDocument xDoc;
        XmlNodeReader xNodeReader;
        XmlSerializer xmlSerializer;

        public SerializeDeserialize()
        {
            sbData = new StringBuilder();
        }

        public string SerializeData(T data)
        {
            sbData.Clear();
            XmlSerializer tSerializer = new XmlSerializer(typeof(T));
            swWriter = new StringWriter(sbData);
            tSerializer.Serialize(swWriter, data);
            return sbData.ToString();
        }

        public T DeserializeData(string fileName)
        {
            xDoc = new XmlDocument();
            xDoc.Load(fileName);
            xNodeReader = new XmlNodeReader(xDoc.DocumentElement);
            xmlSerializer = new XmlSerializer(typeof(T));
            var tData = xmlSerializer.Deserialize(xNodeReader);
            T deserializedEmployee = (T)tData;
            return deserializedEmployee;
        }

    }
}
