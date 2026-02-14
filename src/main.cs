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

        Dictionary<string, long>.KeyCollection directories = dirCollection.Keys;

        //Procceses the sizes to convert them to MG or GB if possible.
        Dictionary<string, string> results = new();

        foreach (string dir in directories)
        {
            // Constants for byte conversion.
            const int KILOBYTE = 1000;
            const int MEGABYTE = 1000000;
            const int GIGABYTE = 1000000000;

            long size = dirCollection[dir];
            
            if (size >= KILOBYTE && size < MEGABYTE) // KB
            {
                results[dir] = Convert.ToString(size / KILOBYTE) + " KB";
            }
            else if (size >= MEGABYTE && size < GIGABYTE) // MB
            {
                results[dir] = Convert.ToString(size / MEGABYTE) + " MB";
            }
            else if (size >= GIGABYTE) // GB
            {
                results[dir] = Convert.ToString(size / GIGABYTE) + " GB";
            }
            else // Byte
            {
                results[dir] = Convert.ToString(size) + " Bytes";
            }
        }

        Console.WriteLine("\nResults\n---------------------------"); //Creates a line break.

        // Displays the size of the directories.
        Dictionary<string, string>.KeyCollection resultKeys = results.Keys;

        foreach (string dir in resultKeys)
        {
            Console.WriteLine($"{dir}: {results[dir]}");
        }
    }
}