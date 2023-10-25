// See https://aka.ms/new-console-template for more information
using Homework_56_Dictionary_C_;

Dictionary dictionary = new Dictionary();
FileDictionaryProvider fileProvider = GetFileProvider();

XmlDictionarySerializer xmlSerializer = new XmlDictionarySerializer();
DictionaryOperations operations = new DictionaryOperations(dictionary, fileProvider, xmlSerializer);
List<string> translations;
string input = "Sample123";
string pattern = @"^[A-Za-z0-9]+$";

static FileDictionaryProvider GetFileProvider()
{
    return new FileDictionaryProvider("dictionary.txt");
}

while (true)
{
    Console.WriteLine("Dictionary Application Menu:");
    Console.WriteLine("1. Create Dictionary");
    Console.WriteLine("2. Add Word and Translation");
    Console.WriteLine("3. Replace Word or Translation");
    Console.WriteLine("4. Delete Word or Translation");
    Console.WriteLine("5. Search Translation");
    Console.WriteLine("6. Export Dictionary");
    Console.WriteLine("7. Exit");

    int choice = GetUserChoice(1, 7);
    int GetUserChoice(int min, int max)
    {
        int choice;
        while (true)
        {
            Console.Write($"Enter a number between {min} and {max}: ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= min && choice <= max)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        return choice;
    }
    switch (choice)
    {
        case 1:
            dictionary.CreateDictionary();
            Console.WriteLine("Dictionary created.");
            break;
        case 2:
            Console.Write("Enter the word: ");
            string word = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Invalid input. Word is required.");
                break;
            }
            Console.Write("Enter the translation(s) (comma-separated): ");
            string translationsInput = Console.ReadLine();
            translations = translationsInput.Split(',').Select(t => t.Trim()).ToList();
            operations.AddWordAndTranslation(word, translations);
            Console.WriteLine("Word and translation added successfully.");
            break;
        case 3:
            Console.Write("Enter the word to replace: ");
            string wordToReplace = Console.ReadLine();
            if (dictionary.GetTranslations(wordToReplace).Count() == 0)
            {
                Console.WriteLine($"The word '{wordToReplace}' does not exist in the dictionary.");
                break;
            }
            Console.Write("Enter the new word: ");
            string newWord = Console.ReadLine();
            Console.Write("Enter the new translation(s) (comma-separated): ");
            string newTranslationsInput = Console.ReadLine();
            List<string> newTranslations = newTranslationsInput.Split(',').Select(t => t.Trim()).ToList();
            operations.ReplaceWordOrTranslations(wordToReplace, newWord, newTranslations);
            Console.WriteLine("Word and translation replaced successfully.");
            break;
        case 4:
            Console.Write("Enter the word to delete: ");
            string wordToDelete = Console.ReadLine();
            if (dictionary.GetTranslations(wordToDelete).Count() == 0)
            {
                Console.WriteLine($"The word '{wordToDelete}' does not exist in the dictionary.");
            }
            else
            {
                operations.DeleteWord(wordToDelete);
                Console.WriteLine("Word and translations deleted successfully.");
            }
            break;
        case 5:
            Console.Write("Enter the word to search: ");
            string wordToSearch = Console.ReadLine();
            translations = operations.SearchTranslation(wordToSearch);
            if (translations.Count() > 0)
            {
                Console.WriteLine($"Translations for '{wordToSearch}': {string.Join(", ", translations)}");
            }
            else
            {
                Console.WriteLine($"The word '{wordToSearch}' is not found in the dictionary.");
            }
            break;
        case 6:
            Console.Write("Enter the filename to export the dictionary: ");
            string exportFileName = Console.ReadLine();
            operations.ExportDictionary(exportFileName, dictionary.GetDictionary());
            Console.WriteLine($"Dictionary exported to '{exportFileName}'.");
            break;
        case 7:
            Environment.Exit(0);
            break;
    }
    if (RegexHelper.IsMatch(input, pattern))
    {
        Console.WriteLine("Matches regular expression.");
    }
    else
    {
        Console.WriteLine("Does not match regular expression.");
    }
}