using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TimeConverter.Service.Configuration
{
    public static class XmlLoadCfg
    {
        public static T Deserialize<T>(Stream stream)
        {
            return Deserialize<T>((Object)stream);
        }

        public static T Deserialize<T>(TextReader textReader)
        {
            return Deserialize<T>((Object)textReader);
        }

        public static T Deserialize<T>(XmlReader xmlReader)
        {
            return Deserialize<T>((Object)xmlReader);
        }

        public static T Deserialize<T>(string xml)
        {
            T result;
            using (var stringReader = new StringReader(xml))
            {
                result = Deserialize<T>(stringReader);

                stringReader.Dispose();
                stringReader.Close();
            }

            return result;
        }

        public static T FileDeserialize<T>(string xmlFilename)
        {
            String xmlStr = File.ReadAllText(xmlFilename);
            T result = Deserialize<T>(xmlStr);

            return result;
        }

        private static T Deserialize<T>(Object reader)
        {
            Object obj = null;
            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                if (reader is Stream)
                {
                    Stream stream = (Stream)reader;
                    obj = (T)serializer.Deserialize(stream);
                    stream.Dispose();
                    stream.Close();
                }
                else if (reader is TextReader)
                {
                    TextReader textReader = (TextReader)reader;
                    obj = (T)serializer.Deserialize(textReader);
                    textReader.Dispose();
                    textReader.Close();
                }
                else if (reader is XmlReader)
                {
                    var xmlReader = (XmlReader)reader;
                    obj = (T)serializer.Deserialize(xmlReader);
                    xmlReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:" + ex.Message);
            }

            return (T)obj;
        }

    }
}
