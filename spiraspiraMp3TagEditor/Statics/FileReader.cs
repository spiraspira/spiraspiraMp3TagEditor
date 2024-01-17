namespace spiraspiraMp3TagEditor.Statics;

/// <summary>
/// Contains methods that return files/directories data.
/// </summary>
public static class FileReader
{
	/// <summary>
	/// Gets file path of the first file in directory on <paramref name="directoryPath"/>
	/// matching <paramref name="artworkFileName"/> with one of <paramref name="artworkFileExtensions"/>.
	/// </summary>
	/// <param name="directoryPath">Directory path.</param>
	/// <param name="artworkFileName">Artwork file name, for example "cover".</param>
	/// <param name="artworkFileExtensions">Artwork possible extensions, for example { "png", "jpg" }.</param>
	/// <returns>Artwork file path. Returns null when no matches found.</returns>
	public static string? GetArtworkFilePath(string directoryPath, string artworkFileName, List<string> artworkFileExtensions)
	{
		string? artworkFilePath = null;

		foreach (var artworkFileExtension in artworkFileExtensions)
		{
			artworkFilePath = Directory
				.EnumerateFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
				.FirstOrDefault(file => Path
					.GetFileName(file)
					.Equals($"{artworkFileName}.{artworkFileExtension}"));

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

	/// <summary>
	/// Gets list of files with specific <paramref name="extensions"/>.
	/// </summary>
	/// <param name="directoryPath">Directory path.</param>
	/// <param name="extensions">Required extensions.</param>
	/// <returns>List of files found (can be empty).</returns>
	public static List<string> GetFiles(string directoryPath, List<string> extensions)
	{
		var files = Directory
			.EnumerateFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
			.Where(file => extensions.Contains(Path
				.GetExtension(file)
				.TrimStart('.')
				.ToLowerInvariant()))
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