using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace adventure_through_the_knight.Utilities.Serializer
{
    public static class Serializer<T> where T : class, ISerializable
    {
        public static void Serialize(string filename, T objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public static T Deserialize(string fileName)
        {
            T objectToSerialize;
            Stream stream = File.Open(fileName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (T)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
