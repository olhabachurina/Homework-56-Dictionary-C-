using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_56_Dictionary_C_
{
    public class ExportDictionaryHandler
    {
        internal void HandleExport(Dictionary<string, List<string>> dictionary, string fileName)
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
                }
                Console.WriteLine($"Dictionary exported to '{fileName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting dictionary: {ex.Message}");
            }
        }
    }
}
