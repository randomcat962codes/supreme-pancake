namespace gui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void CompileData(object sender, RoutedEventArgs e)
	{
		// Gets the directory/file paths
		string editorContent = DirectoryInputText.Text;
		string[] paths = editorContent.Split(
			new string[] {"\r\n", "\n"},
			StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
		);

		Dictionary<string, string> output = new();
		long totalBytes = 0; 

		// A helper to convert the bytes to another readable format (gb, mb, etc.)
		private string BytesToReadable(int bytes)
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

		foreach (string directory in directories)
		{
			// Check if input is a file
			if (File.Exists(directory))
			{
				FileInfo fileInfo = new(directory);
				fileBytes = fileInfo.Length;

				totalBytes += fileBytes;
				
				string fileSize = BytesToReadable(fileBytes);
				output[directory] = fileSize;
			}
			// Check if input is a directory
			else if (Directory.Exists(directory))
			{
				DirectoryInfo dirInfo = new(directory);
				
				// Gets the file size and adds it to the collection.
				IEnumerable<FileInfo> files = dirInfo.EnumerateFiles("*", SearchOption.AllDirectories);
				directoryBytes = files.Sum(file => file.Length);

				totalBytes += directoryBytes;

				string directorySize = BytesToReadable(totalBytes);
				output[directory] = directorySize();
			}
			else
			{
				output[directory] = "There was an error compiling this directory/file";
			}
		}
	}
}
