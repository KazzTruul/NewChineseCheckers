using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ProjectSearchTool
{
    class Program
    {
        private const string SearchFinishedMessage = "\nSearch finished!";
        private const string MatchFoundMessage = "Target phrase found in {0}!";
        private const string InputPhrasePromptMessage = "Please enter the phrase you wish to search for.";
        private const string PhraseInvalidMessage = "Cannot search for empty phrase!";
        private const string NumberOfHitsMessage = "A total of {0} matches for the phrase was found.";

        private static int matchesFound = 0;
        private static string targetPhrase;

        private static async Task Main(string[] args)
        {
            var projectRootPath = Path.GetFullPath($"{Directory.GetCurrentDirectory()}\\..\\..\\..\\..\\..\\..");

            while (string.IsNullOrEmpty(targetPhrase))
            {
                Console.WriteLine(InputPhrasePromptMessage);
                var submittedPhrase = Console.ReadLine();
                if (string.IsNullOrEmpty(submittedPhrase))
                {
                    Console.WriteLine(PhraseInvalidMessage);
                }
                targetPhrase = submittedPhrase;
            }

            await Task.Run(() => SearchDirectory(projectRootPath));

            Console.WriteLine(SearchFinishedMessage);
            Console.WriteLine(string.Format(NumberOfHitsMessage, matchesFound));
        }

        private static async Task SearchDirectory(string directory)
        {
            var directorySearches = new List<Task>();

            foreach (var subDirectory in Directory.GetDirectories(directory))
            {
                var info = new DirectoryInfo(subDirectory);
                if(info.Name == Assembly.GetExecutingAssembly().GetName().Name)
                {
                    Console.WriteLine($"Skipping directory {subDirectory}");
                    continue;
                }
                directorySearches.Add(Task.Run(() => SearchDirectory(subDirectory)));
            }

            await Task.WhenAll(directorySearches);

            var fileSearches = new List<Task>();

            foreach (var file in Directory.GetFiles(directory))
            {
                if (file.Equals(Assembly.GetExecutingAssembly().GetName().Name))
                {
                    continue;
                }
                fileSearches.Add(Task.Run(() => SearchFile(file)));
            }

            await Task.WhenAll(fileSearches);
        }

        private static async Task SearchFile(string file)
        {
            await Task.Run(() =>
            {
                var hits = Regex.Matches(File.ReadAllText(file), targetPhrase);
                foreach (var hit in hits)
                {
                    matchesFound++;
                    Console.WriteLine(string.Format(MatchFoundMessage, file));
                }
            });
        }
    }
}
