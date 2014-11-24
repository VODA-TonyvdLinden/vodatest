using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestProj.Classes
{
    public static class XMLSeriallizer
    {
        public static void Serialize<T>(T item, string filename)
        {
            using (XmlTextWriter xWriter = new XmlTextWriter(filename, Encoding.Unicode))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                serializer.Serialize(xWriter, item);
                xWriter.Close();
            }
        }

        public static T Deserialize<T>(string filename)
        {
            using (XmlReader xReader = XmlReader.Create(filename))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(xReader);
            }
        }
    }
}
