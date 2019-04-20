using System;
using System.IO;
using System.Xml.Serialization;

namespace Ptech.Core.SerializationLibrary
{
    /// <summary>
    /// Serialize/Deserialze an object to/from a XML file
    /// </summary>

    // XML serializer
    // It needs System.Xml.Serialization libraries
    // Incoming object needs the [Serializable] attribute

    public class XmlSerializationFunctions
    {
        // XML serializer (generics) ----------------------------------------------------

        /// <summary>
        /// Save an object into a file using XML serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iFilename"></param>
        /// <param name="objGraph"></param>
        public static void SaveXmlGenericObject<T>(string iFilename, T objGraph)
        {
            using (Stream fStream = new FileStream(iFilename,
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Serializer object
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                // Serialzie
                xmlFormat.Serialize(fStream, objGraph);
            }
        }

        /// <summary>
        /// Loads an object from a file using XML serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iFilename"></param>
        /// <returns>object</returns>
        public static object LoadXmlGenericObject<T>(string iFilename)
        {
            object objGraph = new object();

            // Deerialize
            using (Stream fStream = new FileStream(iFilename, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                // Serializer object
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                // Deserialize
                objGraph = xmlFormat.Deserialize(fStream);
            }

            return objGraph;
        }


        // XML serializer (traditional, left here for learning purposes) ------------------------------------

        /// <summary>
        /// Save an object into a file using XML serialization
        /// </summary>
        /// <param name="iFilename"></param>
        /// <param name="objGraph"></param>
        /// <param name="objChildTypes"></param>
        public static void SaveXmlObject(string iFilename, object objGraph, Type[] objChildTypes)
        {
            // Get type of object 
            Type objType = objGraph.GetType();

            using (Stream fStream = new FileStream(iFilename,
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Serializer object
                XmlSerializer xmlFormat = new XmlSerializer(objType, objChildTypes);
                // Serialzie
                xmlFormat.Serialize(fStream, objGraph);
            }
        }

        /// <summary>
        /// Loads an object from a file using XML serialization
        /// </summary>
        /// <param name="iFilename"></param>
        /// <param name="objGraph"></param>
        /// <param name="objChildTypes"></param>
        /// <returns>object</returns>
        public static object LoadXmlObject(string iFilename, object objGraph, Type[] objChildTypes)
        {
            // Get type of object 
            Type objType = objGraph.GetType();

            // Deerialize
            using (Stream fStream = new FileStream(iFilename, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                // Serializer object
                XmlSerializer xmlFormat = new XmlSerializer(objType, objChildTypes);
                // Deserialize
                objGraph = xmlFormat.Deserialize(fStream);
            }

            return objGraph;
        }


    }
}
