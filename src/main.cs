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

                long dirSize = 0;

                try
                {
                    // Gets the file size and adds it to the collection.
                    IEnumerable<FileInfo> files = dirInfo.EnumerateFiles("*", SearchOption.AllDirectories);
                    dirSize = files.Sum(file => file.Length);
                    dirCollection[input] = dirSize;
                    dirCount++;
                }
                catch (Exception err)
                {
                    Console.WriteLine($"\t*There was trouble searching the directory. The error will be listed below:\n{err.Message}");
                }
            }
        }

        Dictionary<string, long>.KeyCollection directories = dirCollection.Keys;
        const int KILOBYTE = 1000;
        const int MEGABYTE = 1000000;
        const int GIGABYTE = 1000000000;

        // Procceses the sizes to convert them to a readable format.
        Dictionary<string, string> results = new();
        long totalSize = 0; // The size of the collecions.
        string totalResult; // The end result that will be displayed.

        foreach (string dir in directories)
        {
            totalSize += dirCollection[dir];
        }
        if (totalSize >= KILOBYTE && totalSize < MEGABYTE) // KB
        {
            totalResult = Convert.ToString(totalSize / KILOBYTE) + " KB";
        }
        else if (totalSize >= MEGABYTE && totalSize < GIGABYTE) // MB
        {
            totalResult = Convert.ToString(totalSize / MEGABYTE) + " MB";
        }
        else if (totalSize >= GIGABYTE) // GB
        {
            totalResult = Convert.ToString(totalSize / GIGABYTE) + " GB";
        }
        else // Bytes
        {
            totalResult = Convert.ToString(totalSize) + " bytes";
        }

        foreach (string dir in directories)
        {
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
                results[dir] = Convert.ToString(size) + " bytes";
            }
        }

        Console.WriteLine("\nResults\n---------------------------"); // A line break.

        // Displays the size of the directories.
        Dictionary<string, string>.KeyCollection resultKeys = results.Keys;

        foreach (string dir in resultKeys)
        {
            Console.WriteLine($"{dir}: {results[dir]}");
        }

        Console.WriteLine($"\nTotal: {totalResult}");
    }
}