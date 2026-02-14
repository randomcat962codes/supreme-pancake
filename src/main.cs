using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the directories you want to be searched. Enter \"end\" when you are finished.");

        bool getDirs = true;
        int dirCount = 0; // Iterator
        Dictionary<string, long> dirCollection = new(); // Contains the directories with their respoctive sizes.
        while (getDirs)
        {
            Console.Write(Convert.ToString(dirCount + 1) + ": ");
            string input = Console.ReadLine();

            // Processes the input.
            if (input == "end")
            {
                getDirs = false;
            } 
            else
            {
                DirectoryInfo dirInfo = new(input);

                // Makes sure the listed directory exists.
                if (!dirInfo.Exists) 
                {
                    Console.WriteLine("The directory could not be found.");
                    continue;
                }

                long totalSize = 0;

                try
                {
                    // Gets the file size and adds it to the collection.
                    IEnumerable<FileInfo> files = dirInfo.EnumerateFiles("*", SearchOption.AllDirectories);
                    totalSize = files.Sum(file => file.Length);
                    dirCollection[input] = totalSize;
                    dirCount++;
                }
                catch (Exception err)
                {
                    Console.WriteLine($"\t*There was trouble searching the directory. The error will be listed below:\n{err.Message}");
                }
            }
        }

        Console.WriteLine("\nResults\n---------------------------"); //Creates a line break.

        // Displays the size of the directories.
        Dictionary<string, long>.KeyCollection directories = dirCollection.Keys;

        foreach (string dir in directories)
        {
            string size = Convert.ToString(dirCollection[dir]);
            Console.WriteLine($"{dir}: {size}");
        }
    }
}