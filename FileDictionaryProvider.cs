using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_56_Dictionary_C_
{
    public class FileDictionaryProvider
    {
        private string fileName;

        public FileDictionaryProvider(string fileName)
        {
            this.fileName = fileName;
        }

        public Dictionary<string, List<string>> LoadDictionaryFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    Dictionary<string, List<string>> loadedDictionary = new Dictionary<string, List<string>>();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            string word = parts[0].Trim();
                            List<string> translations = parts[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(t => t.Trim())
                                .ToList();
                            loadedDictionary[word] = translations;
                        }
                    }
                    return loadedDictionary;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dictionary from file: {ex.Message}");
                return null;
            }
        }

        public void SaveDictionaryToFile(Dictionary<string, List<string>> dictionary)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var word in dictionary.Keys.OrderBy(w => w))
                    {
                        string line = $"{word}: {string.Join(", ", dictionary[word])}";
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving dictionary to file: {ex.Message}");
            }
        }
        public void ExportDictionary(string fileName, Dictionary<string, List<string>> dictionary)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var entry in dictionary.OrderBy(e => e.Key))
                    {
                        string line = $"{entry.Key}: {string.Join(", ", entry.Value.OrderBy(t => t))}";
                        writer.WriteLine(line);
                    }

                    Console.WriteLine($"Dictionary exported successfully to '{fileName}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting dictionary: {ex.Message}");
            }
        }

        
    }
    
}