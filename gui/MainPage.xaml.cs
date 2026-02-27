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
	}
}
