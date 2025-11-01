namespace spiraspiraMp3TagEditor.Extensions;

/// <summary>
/// Contains extension methods for <see cref="List{T}"/> of <see cref="TagLib.File"/>.
/// </summary>
public static class TagLibFileListExtension
{
	/// <summary>
	/// Writes <see cref="TagLib.File"/>s to file.
	/// </summary>
	/// <param name="files"><see cref="TagLib.File"/> list.</param>
	public static void SaveAll(this List<TagLib.File> files)
	{
		foreach (var file in files.ToList())
		{
			string fileName = Path.GetFileName(file.Name);

			try
			{
				file.Save();

				SharedEvents.InvokeNotify($"Tags for {fileName} are saved.", true);
			}
			catch (IOException)
			{
				var processes = FileUtil.WhoIsLocking(file.Name).Select(process => process.ProcessName).ToList();

				SharedEvents.InvokeNotify($"Couldn't save: {fileName} is locked by {string.Join(' ', processes)}.", false);
			}
		}
	}

	/// <summary>
	/// Sets Album property for a <see cref="List{T}"/> of <see cref="TagLib.File"/>.
	/// </summary>
	/// <param name="files"><see cref="TagLib.File"/> list.</param>
	/// <param name="album">Album name.</param>
	public static void SetAlbum(this List<TagLib.File> files, string album)
	{
		files.ToList().ForEach(f => f.Tag.Album = album);

		SharedEvents.InvokeNotify($"Album <{album}> is set.", true);
	}

	/// <summary>
	/// Sets single artwork for Pictures property for a <see cref="List{T}"/> of <see cref="TagLib.File"/>.
	/// </summary>
	/// <param name="files"><see cref="TagLib.File"/> list.</param>
	/// <param name="artworkFilePath">Path to image.</param>
	public static void SetArtwork(this List<TagLib.File> files, string? artworkFilePath)
	{
		if (artworkFilePath == null)
		{
			SharedEvents.InvokeNotify("Artwork is not set.", false);

			return;
		}

		files.ToList().ForEach(f => f.Tag.Pictures = new IPicture[] { new Picture(artworkFilePath) });

		SharedEvents.InvokeNotify("Artwork is set.", true);
	}

	/// <summary>
	/// Sets single <paramref name="artist"/> for a Performers property for a <see cref="List{T}"/> of <see cref="TagLib.File"/>.
	/// </summary>
	/// <param name="files"><see cref="TagLib.File"/> list.</param>
	/// <param name="artist">Artist name.</param>
	public static void SetArtist(this List<TagLib.File> files, string artist)
	{
		files.ToList().ForEach(f => f.Tag.Performers = new[] { artist });

		files.ToList().ForEach(f => f.Tag.AlbumArtists = new[] { artist });

		SharedEvents.InvokeNotify($"Artist <{artist}> is set.", true);
	}

	/// <summary>
	/// Sets single <paramref name="genre"/> for a Genres property for a <see cref="List{T}"/> of <see cref="TagLib.File"/>.
	/// </summary>
	/// <param name="files"><see cref="TagLib.File"/> list.</param>
	/// <param name="genre">Genre name.</param>
	public static void SetGenre(this List<TagLib.File> files, string genre)
	{
		files.ToList().ForEach(f => f.Tag.Genres = new[] { genre });

		SharedEvents.InvokeNotify($"Genre <{genre}> is set.", true);
	}

	/// <summary>
	/// Sets Number property for a <see cref="List{T}"/> of <see cref="TagLib.File"/>.
	/// </summary>
	/// <param name="files"><see cref="TagLib.File"/> list.</param>
	/// <param name="numbers">List of numbers, must be the same size as files.</param>
	/// <exception cref="ArgumentException">"Throws when <paramref name="files"/> isn't equal to <paramref name="numbers"/> count."</exception>
	public static void SetNumbers(this List<TagLib.File> files, List<uint> numbers)
	{
		if (files.ToList().Count != numbers.Count)
		{
			throw new ArgumentException($"{nameof(files)} count must be equal to {nameof(numbers)} count.");
		}

		for (int i = 0; i < numbers.Count; i++)
		{
			files[i].Tag.TrackCount = (uint)numbers.Count;

			files[i].Tag.Track = numbers[i];
		}

		SharedEvents.InvokeNotify("Numbers are set.", true);
	}

	/// <summary>
	/// Sets Title property for a <see cref="List{T}"/> of <see cref="TagLib.File"/>.
	/// </summary>
	/// <param name="files"><see cref="TagLib.File"/> list.</param>
	/// <param name="titles">List of titles, must be the same size as list of files.</param>
	/// <exception cref="ArgumentException">"Throws when <paramref name="files"/> isn't equal to <paramref name="titles"/> count."</exception>
	public static void SetTitles(this List<TagLib.File> files, List<string> titles)
	{
		if (files.ToList().Count != titles.Count)
		{
			throw new ArgumentException($"{nameof(files)} count must be equal to {nameof(titles)} count.");
		}

		for (int i = 0; i < titles.Count; i++)
		{
			files.ToList()[i].Tag.Title = titles[i];
		}

		SharedEvents.InvokeNotify("Titles are set.", true);
	}

	/// <summary>
	/// Sets Year property for a <see cref="List{T}"/> of <see cref="TagLib.File"/>.
	/// </summary>
	/// <param name="files"><see cref="TagLib.File"/> list.</param>
	/// <param name="year">Year.</param>
	public static void SetYear(this List<TagLib.File> files, uint year)
	{
		files.ToList().ForEach(f => f.Tag.Year = year);

		SharedEvents.InvokeNotify($"Year <{year}> is set.", true);
	}
}