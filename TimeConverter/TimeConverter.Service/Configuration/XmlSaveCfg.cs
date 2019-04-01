using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TimeConverter.Service.Configuration
{
    public static class XmlSaveCfg
    {
        public static void Serialize(string fileName, Object obj,
            XmlSerializerNamespaces xmlSerializerNamespaces = null,
            Encoding encoding = null)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                Serialize((object)streamWriter, obj, xmlSerializerNamespaces, encoding);

                streamWriter.Dispose();
                streamWriter.Close();
            }
        }

        // Serializes the specificed Object and returns a string representation of the serialized object that
        // references the specified namespaces.
        public static string Serialize(Object obj,
            XmlSerializerNamespaces xmlSerializerNamespaces = null,
            Encoding encoding = null)
        {
            return Serialize((object)null, obj, xmlSerializerNamespaces, encoding);
        }

        // Serializes the specificed Object and writes the XML document to the specified Stream that
        // references the specified namespaces.
        public static void Serialize(Stream stream, Object obj,
            XmlSerializerNamespaces xmlSerializerNamespaces = null,
            Encoding encoding = null)
        {
            Serialize((object)stream, obj, xmlSerializerNamespaces, encoding);
        }

        // Serializes the specificed Object and writes the XML document to the specified TextWriter that
        // references the specified namespaces.
        public static void Serialize(TextWriter textWriter, Object obj,
            XmlSerializerNamespaces xmlSerializerNamespaces = null,
            Encoding encoding = null)
        {
            Serialize((object)textWriter, obj, xmlSerializerNamespaces, encoding);
        }

        // Serializes the specificed Object and writes the XML document to the specified XmlWriter that
        // references the specified namespaces.
        public static void Serialize(XmlWriter xmlWriter, Object obj,
            XmlSerializerNamespaces xmlSerializerNamespaces = null,
            Encoding encoding = null)
        {
            Serialize((object)xmlWriter, obj, xmlSerializerNamespaces, encoding);
        }

        // Serializes the specificed Object and writes the XML document to the specified writer Object that
        // references the specified namespaces.
        private static string Serialize(Object writerObj, Object obj,
            XmlSerializerNamespaces xmlSerializerNamespaces = null,
            Encoding encoding = null)
        {
            string serializedString = null;

            try
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());

                if (writerObj is Stream)
                {
                    var streamWriter = (Stream)writerObj;
                    serializer.Serialize(streamWriter, obj, xmlSerializerNamespaces);
                    streamWriter.Dispose();
                    streamWriter.Close();
                }
                else if (writerObj is TextWriter)
                {
                    var textWriter = (TextWriter)writerObj;
                    serializer.Serialize(textWriter, obj, xmlSerializerNamespaces);
                    textWriter.Dispose();
                    textWriter.Close();
                }
                else if (writerObj is XmlWriter)
                {
                    var xmlWriter = (XmlWriter)writerObj;
                    serializer.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
                    xmlWriter.Close();
                }
                else
                {
                    var sb = new StringBuilder();
                    using (var sw = new XmlSaveCfg.StringWriterWithEncoding(sb, encoding ?? Encoding.UTF8))
                    {
                        using (var tw = new XmlTextWriter(sw))
                        {
                            // Remove XML formatting
                            tw.Formatting = Formatting.None;

                            serializer.Serialize(tw, obj, xmlSerializerNamespaces);
                            tw.Close();
                        }

                        sw.Close();
                    }

                    serializedString = sb.ToString();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return serializedString;
        }

        // StringWriter class with encoding.
        private class StringWriterWithEncoding : StringWriter
        {
            // Private members .............................
            // NOTE: The encoding.  Default is UTF8
            private readonly Encoding encoding = Encoding.UTF8;

            // Properties ..................................
            public override Encoding Encoding
            {
                get
                {
                    return this.encoding;
                }
            }

            // Constructos ....................................
            public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
                : base(sb)
            {
                this.encoding = encoding;
            }
        }
    }
}
