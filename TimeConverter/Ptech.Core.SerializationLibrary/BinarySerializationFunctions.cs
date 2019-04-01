using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ptech.Core.SerializationLibrary
{

    /// <summary>
    /// Serialize/Deserialze an object to/from a Binary file 
    /// </summary>
    
    // BINARY Serializer
    // using System.Runtime.Serialization.Formatters.Binary
    // Incoming object needs the [Serializable] attribute

    public class BinarySerializationFunctions
    {
        public static void SaveBinObject(string iFilename, object objGraph)
        {
            // Usie Binary format for file
            BinaryFormatter binFormat = new BinaryFormatter();

            // Serialize
            using (Stream fStream = new FileStream(iFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
                fStream.Flush();
            }
        }

        public static object LoadBinObject(string iFilename)
        {
            object objGraph = null;

            // Usie Binary format to read file
            BinaryFormatter binFormat = new BinaryFormatter();

            // Read stream
            using (Stream fStream = File.OpenRead(iFilename))
            {
                objGraph = binFormat.Deserialize(fStream);
                fStream.Flush();
            }

            return objGraph;
        }

    }
}
