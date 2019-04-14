using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ptech.Core.SerializationLibrary
{
    /// <summary>
    /// Serialize/Deserialze an object to/from a XML file using DataContract
    /// </summary>
   
    // DATA CONTRACT SERIALIZATION.............................................
    // for this use on object the attributes Serializable, DataContract and 
    // DataMember otherwise wil not work. It requires to load as reference 
    // System.Runtime.Serialization 

    public class DataContractSerializationFunctions
    {
        /// <summary>
        /// Save an object into a file using Data Contract serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iFilename"></param>
        /// <param name="objGraph"></param>
        public static void SaveObjectByContract<T>(string iFilename, T objGraph)
        {
            // serializer object
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            // Serialize
            using (Stream fStream = new FileStream(iFilename,
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.WriteObject(fStream, objGraph);
                fStream.Flush();
            }
        }

        /// <summary>
        /// Loads an object from a file using Data contract serialization
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iFilename"></param>
        /// <returns></returns>
        public static object LoadObjectByContract<T>(string iFilename)
        {
            object objGraph = null;

            // serializer object
            DataContractSerializer deserilizer = new DataContractSerializer(typeof(T));

            // Read stream
            using (Stream fStream = File.OpenRead(iFilename))
            {
                objGraph = deserilizer.ReadObject(fStream);
                fStream.Flush();
            }

            return objGraph;
        }



    }
}
