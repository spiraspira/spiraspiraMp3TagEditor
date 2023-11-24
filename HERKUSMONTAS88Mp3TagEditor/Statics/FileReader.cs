namespace HERKUSMONTAS88Mp3TagEditor.Statics;

public static class FileReader
{
	public static string? GetArtworkFilePath(string directoryPath, string artworkFileName, List<string> artworkFileExtensions)
	{
		string? artworkFilePath = null;

		foreach (var artworkFileExtension in artworkFileExtensions)
		{
			artworkFilePath = Directory
				.EnumerateFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
				.FirstOrDefault(file => Path.GetFileName(file).Equals($"{artworkFileName}.{artworkFileExtension}"));

			if (artworkFilePath is not null)
			{
				break;
			}
		}

		if (artworkFilePath is null)
		{
			SharedEvents.InvokeNotify($"Artwork file {artworkFileName}.{string.Join('/', artworkFileExtensions)} not found.", false);
		}

		return artworkFilePath;
	}

	public static List<string> GetFiles(string directoryPath, List<string> extensions)
	{
		var files = Directory
			.EnumerateFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
			.Where(file => extensions.Contains(Path.GetExtension(file).TrimStart('.').ToLowerInvariant()))
			.ToList();

		if (files.Count == 0)
		{
			SharedEvents.InvokeNotify($"Files with extensions {string.Join(' ', extensions)} not found.", false);
		}
		else
		{
			SharedEvents.InvokeNotify($"Files are detected ({files.Count}).", true);
		}

		return files;
	}
}