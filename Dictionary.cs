using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_56_Dictionary_C_
{
    public class Dictionary
    {
        private Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

        public void CreateDictionary()
        {
            dictionary.Clear();
            Console.WriteLine("Dictionary created.");
        }

        public void AddWordAndTranslation()
        {
            Console.Write("Enter the word: ");
            string word = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Invalid input. Word is required.");
                return;
            }

            Console.Write("Enter the translation(s) (comma-separated): ");
            string translationsInput = Console.ReadLine();
            List<string> translations = translationsInput.Split(',').Select(t => t.Trim()).ToList();

            if (!dictionary.ContainsKey(word))
            {
                dictionary[word] = new List<string>(translations);
            }
            else
            {
                foreach (var translation in translations)
                {
                    if (!dictionary[word].Contains(translation))
                    {
                        dictionary[word].Add(translation);
                    }
                }
            }

            Console.WriteLine("Word and translation added successfully.");
        }

        public void DeleteWordOrTranslation(string fileName)
        {
            Dictionary<string, List<string>> loadedDictionary = LoadDictionaryFromFile(fileName);

            if (loadedDictionary == null)
            {
                Console.WriteLine($"Dictionary in file '{fileName}' does not exist.");
                return;
            }

            Console.Write("Enter the word to delete: ");
            string wordToDelete = Console.ReadLine();

            if (loadedDictionary.Remove(wordToDelete))
            {
                Console.WriteLine("Word and translations deleted successfully.");
                SaveDictionaryToFile(fileName, loadedDictionary);
            }
            else
            {
                Console.WriteLine($"The word '{wordToDelete}' does not exist in the dictionary.");
            }
        }

        public void ReplaceWordOrTranslation()
        {
            Console.Write("Enter the word to replace: ");
            string wordToReplace = Console.ReadLine();

            if (dictionary.TryGetValue(wordToReplace, out var existingTranslations))
            {
                Console.Write("Enter the new word: ");
                string newWord = Console.ReadLine();

                Console.Write("Enter the new translation(s) (comma-separated): ");
                string[] newTranslations = Console.ReadLine().Split(',');

                dictionary.Remove(wordToReplace);
                dictionary[newWord] = new List<string>(newTranslations.Select(t => t.Trim()));

                Console.WriteLine("Word and translation replaced successfully.");
            }
            else
            {
                Console.WriteLine($"The word '{wordToReplace}' does not exist in the dictionary.");
            }
        }

        public void SearchTranslation(string wordToSearch, string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string[] lines = File.ReadAllLines(fileName);

                    foreach (var line in lines)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length >= 2)
                        {
                            string word = parts[0].Trim();
                            string translations = parts[1].Trim();
                            if (word.Equals(wordToSearch, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"Translations for '{wordToSearch}': {translations}");
                                return;
                            }
                        }
                    }

                    Console.WriteLine($"The word '{wordToSearch}' is not found in the dictionary.");
                }
                else
                {
                    Console.WriteLine($"The dictionary file '{fileName}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching for word: {ex.Message}");
            }
        }

        private Dictionary<string, List<string>> LoadDictionaryFromFile(string fileName)
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
        private void SaveDictionaryToFile(string fileName, Dictionary<string, List<string>> dictionary)
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
        public void SetDictionary(Dictionary<string, List<string>> newDictionary)
        {
            dictionary = newDictionary;
        }

        public void ReplaceWordOrTranslation(string wordToReplace, string newWord, List<string> newTranslations)
        {
            if (dictionary.ContainsKey(wordToReplace))
            {
                
                dictionary.Remove(wordToReplace);
                
                dictionary[newWord] = newTranslations;
            }
        }


        public void AddWordAndTranslation(string word, List<string> translations)
        {
            if (!dictionary.ContainsKey(word))
            {
                dictionary[word] = translations;
            }
            else
            {
                foreach (var translation in translations)
                {
                    if (!dictionary[word].Contains(translation))
                    {
                        dictionary[word].Add(translation);
                    }
                }
            }
        }


        public List<string> SearchTranslation(string word)
        {
            if (dictionary.ContainsKey(word))
            {
                return dictionary[word];
            }
            return new List<string>(); 
        }

        public Dictionary<string, List<string>> GetDictionary()
        {
            return dictionary;
        }

        public List<string> GetTranslations(string word)
        {
            if (dictionary.ContainsKey(word))
            {
                return dictionary[word];
            }
            else
            {
                return new List<string>();
            }
        }
    }
}
    