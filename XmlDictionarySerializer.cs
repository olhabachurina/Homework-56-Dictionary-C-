using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

    namespace Homework_56_Dictionary_C_
    {
        public class XmlDictionarySerializer
        {
        public void SerializeDictionaryToXml(string fileName, Dictionary<string, List<string>> dictionary)
        {
                XmlSerializer serializer = new XmlSerializer(typeof(Dictionary<string, List<string>>));
                using (TextWriter writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, dictionary);
                }

                Console.WriteLine($"Dictionary serialized to '{fileName}'.");
            }

        public Dictionary<string, List<string>> DeserializeDictionaryFromXml(string fileName)
            {
                if (File.Exists(fileName))
                {
                    
                    XmlSerializer serializer = new XmlSerializer(typeof(Dictionary<string, List<string>>));
                    using (TextReader reader = new StreamReader(fileName))
                    {
                        return (Dictionary<string, List<string>>)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    Console.WriteLine($"The dictionary file '{fileName}' does not exist.");
                    return null; 
                }
            }
        }
    }