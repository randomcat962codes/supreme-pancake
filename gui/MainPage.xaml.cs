namespace gui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	// Convert bytes to another readable format (gb, mb, etc.)
	private string BytesToReadable(long bytes)
	{
		const int KILOBYTE = 1000;
		const int MEGABYTE = 1000000;
		const int GIGABYTE = 1000000000;

		string result = "";

		if (bytes >= KILOBYTE && bytes < MEGABYTE) // Kilobyte
		{
			result = Convert.ToString(bytes/KILOBYTE) + " KB";
		}
		else if (bytes >= MEGABYTE && bytes < GIGABYTE) // Megabyte
		{
			result = Convert.ToString(bytes/MEGABYTE) + " MB";
		}
		else if (bytes >= GIGABYTE) // Gigabyte
		{
			result = Convert.ToString(bytes/GIGABYTE) + " GB";
		}
		else // Bytes
		{
			result = Convert.ToString(bytes) + " Bytes";
		}

		return result;
	}

	private void CompileData(object? sender, EventArgs e)
	{
		// Gets the directory/file paths
		string editorContent = DirectoryInputText.Text;
		string[] paths = editorContent.Split(
			new string[] {"\r\n", "\n"},
			StringSplitOptions.RemoveEmptyEntries
		);

		DirectoryInputText.Text = "";

		Dictionary<string, string> fileSizes = new Dictionary<string, string>(){};
		long totalBytes = 0L; 

		foreach (string directory in paths)
		{
			// Check if input is a file
			if (File.Exists(directory))
			{
				FileInfo fileInfo = new(directory);
				long fileBytes = fileInfo.Length;

				totalBytes += fileBytes;
				
				string fileSize = BytesToReadable(fileBytes);
				fileSizes[directory] = fileSize;
			}
			// Check if input is a directory
			else if (Directory.Exists(directory))
			{
				DirectoryInfo dirInfo = new(directory);
				
				// Gets the file size and adds it to the collection.
				IEnumerable<FileInfo> files = dirInfo.EnumerateFiles("*", SearchOption.AllDirectories);
				long directoryBytes = files.Sum(file => file.Length);

				totalBytes += directoryBytes;

				string directorySize = BytesToReadable(directoryBytes);
				fileSizes[directory] = directorySize;
			}
			else
			{
				fileSizes[directory] = "There was an error compiling this directory/file";
			}
		}
	}
}
