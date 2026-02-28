namespace gui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		// Sets up the export grid
		var dataTable = new Grid
		{
			ColumnDefinitions = 
			{ 
				new ColumnDefinition(GridLength.Star), 
				new ColumnDefinition(120) 
			},
			RowDefinitions = 
			{
				new RowDefinition { Height = GridLength.Auto } // Header row
			},
			Padding = new Thickness(10),
			BackgroundColor = Colors.Gray
		};

		dataTable.Add(new Label { Text = "FILE PATH", FontAttributes = FontAttributes.Bold }, 0, 0);
		dataTable.Add(new Label { Text = "SIZE", FontAttributes = FontAttributes.Bold }, 1, 0);
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

	// This function compiles the data in the editor on the main page, and displays all the data in a table.
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

		string formattedTotal = BytesToReadable(totalBytes);

		// Display the data
		int currentRow = 1;
		string[] pathCollection = fileSizes.Keys.ToArray(); // Gets the keys from the fileSizes dictionary to be iterated over

		foreach (string path in pathCollection)
		{
			string size = fileSizes[path];

			// Create a new row
			dataTable.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

			// Create labels for the row
			var pathLabel = new Label { Text = path };
			var sizeLabel = new Label { Text = size };

			myTableGrid.Add(pathLabel, 0, currentRow);
    		myTableGrid.Add(sizeLabel, 1, currentRow);

			currentRow++;
		}
	}
}
