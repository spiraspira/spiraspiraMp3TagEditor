namespace HERKUSMONTAS88Mp3TagEditor.Extensions;

public static class TagLibFileListExtension
{
	public static void SaveAll(this List<TagLib.File> files)
	{
		files.ToList().ForEach(f => f.Save());

		SharedEvents.InvokeNotify("Tags saved.", true);
	}

	public static void SetAlbum(this List<TagLib.File> files, string album)
	{
		files.ToList().ForEach(f => f.Tag.Album = album);

		SharedEvents.InvokeNotify($"Album <{album}> is set.", true);
	}

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

	public static void SetArtist(this List<TagLib.File> files, string artist)
	{
		files.ToList().ForEach(f => f.Tag.Performers = new[] { artist });

		SharedEvents.InvokeNotify($"Artist <{artist}> is set.", true);
	}

	public static void SetGenre(this List<TagLib.File> files, string genre)
	{
		files.ToList().ForEach(f => f.Tag.Genres = new[] { genre });

		SharedEvents.InvokeNotify($"Genre <{genre}> is set.", true);
	}

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

	public static void SetYear(this List<TagLib.File> files, uint year)
	{
		files.ToList().ForEach(f => f.Tag.Year = year);

		SharedEvents.InvokeNotify($"Year <{year}> is set.", true);
	}
}