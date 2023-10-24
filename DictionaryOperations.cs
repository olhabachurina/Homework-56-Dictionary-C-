using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace Homework_56_Dictionary_C_;

public class DictionaryOperations
{
    private Dictionary dictionary;
    private FileDictionaryProvider fileProvider;
    private XmlDictionarySerializer xmlSerializer;

    public DictionaryOperations(Dictionary dictionary, FileDictionaryProvider fileProvider, XmlDictionarySerializer xmlSerializer)
    {
        this.dictionary = dictionary;
        this.fileProvider = fileProvider;
        this.xmlSerializer = xmlSerializer;
    }

    public void AddWordAndTranslation(string word, List<string> translations)
    {
        dictionary.AddWordAndTranslation(word, translations);
    }

    public void DeleteWord(string word)
    {
        dictionary.DeleteWordOrTranslation(word);
    }

    public List<string> SearchTranslation(string word)
    {
        return dictionary.SearchTranslation(word);
    }

    public void ReplaceWordOrTranslations(string wordToReplace, string newWord, List<string> newTranslations)
    {
        dictionary.ReplaceWordOrTranslation(wordToReplace, newWord, newTranslations);
    }

    internal void ExportDictionary(string fileName, Dictionary<string, List<string>> dictionary)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var kvp in dictionary)
                {
                    string word = kvp.Key;
                    List<string> translations = kvp.Value;
                    string translationsString = string.Join(", ", translations);
                    writer.WriteLine($"{word}: {translationsString}");
                }
                Console.WriteLine($"Dictionary exported to '{fileName}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting dictionary: {ex.Message}");
        }
    }

    public void SerializeDictionaryToXml(string fileName)
    {
        xmlSerializer.SerializeDictionaryToXml(fileName, dictionary.GetDictionary());
    }

    public void DeserializeDictionaryFromXml(string fileName)
    {
        var loadedDictionary = xmlSerializer.DeserializeDictionaryFromXml(fileName);
        if (loadedDictionary != null)
        {
            dictionary.SetDictionary(loadedDictionary);
        }
    }
}
