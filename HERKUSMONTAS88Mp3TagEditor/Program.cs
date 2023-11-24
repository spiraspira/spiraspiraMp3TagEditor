SharedEvents.Notify += Log;

const string
	artworkFileName = "artwork";

string
	directoryPath = AppDomain.CurrentDomain.BaseDirectory;

List<string>
	audioExtensions = new() { "mp3", "wav", "flac" },
	artworkExtensions = new() { "jpg", "png" },
	audioFiles = GetFiles(directoryPath, audioExtensions);

string?
	artwork = GetArtworkFilePath(directoryPath, artworkFileName, artworkExtensions);

string
	artist = InputString("Artist: "),
	album = InputString("Album: "),
	genre = InputString("Genre: ");

uint
	year = InputUInt32("Year: ");

List<(uint, string)>
	numberTitleDictionary = new();

List<TagLib.File>
	taggedAudioFiles = new();

FilesToTuple(audioFiles, numberTitleDictionary);

FilesToTaggedFiles(audioFiles, ref taggedAudioFiles);

taggedAudioFiles.SetAlbum(album);

taggedAudioFiles.SetArtist(artist);

taggedAudioFiles.SetArtwork(artwork);

taggedAudioFiles.SetGenre(genre);

taggedAudioFiles.SetNumbers(numberTitleDictionary.Select(item1 => item1.Item1).ToList());

taggedAudioFiles.SetTitles(numberTitleDictionary.Select(item2 => item2.Item2).ToList());

taggedAudioFiles.SetYear(year);

taggedAudioFiles.SaveAll();

ApplicationExit();

return;

static void ApplicationExit()
{
	Log("Press any key to exit...");

	Console.ReadKey();

	Environment.Exit(0);
}

static string? GetArtworkFilePath(string directoryPath, string artworkFileName, List<string> artworkFileExtensions)
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
		Log($"Artwork file {artworkFileName}.{string.Join('/', artworkFileExtensions)} not found.", false);
	}

	return artworkFilePath;
}

static List<string> GetFiles(string directoryPath, List<string> extensions)
{
	var files = Directory
		.EnumerateFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly)
		.Where(file => extensions.Contains(Path.GetExtension(file).TrimStart('.').ToLowerInvariant()))
		.ToList();

	if (files.Count == 0)
	{
		Log($"Files with extensions {string.Join(' ', extensions)} not found.", false);

		ApplicationExit();
	}

	Log($"Files are detected ({files.Count}).", true);

	return files;
}

static void FilesToTuple(List<string> files, List<(uint, string)> dictionary)
{
	List<string> fileNames = files.Select(Path.GetFileNameWithoutExtension).ToList()!;

	foreach (var file in fileNames)
	{
		uint number = 0;

		string name = string.Empty;

		try
		{
			number = uint.Parse(file.Split()[0].Remove(file.Split()[0].IndexOf('.'), 1));

			name = file[(file.IndexOf(' ') + 1)..];
		}
		catch
		{
			try
			{
				number = uint.Parse(file.Split()[0]);

				name = file[(file.IndexOf(' ') + 1)..];
			}
			catch
			{
				Log($"Incorrect file name: {file}");
			}
		}
	
		dictionary.Add((number, name));
	}
}

static void FilesToTaggedFiles(List<string> files, ref List<TagLib.File> taggedFiles)
{
	taggedFiles = new();

	foreach (var file in files)
	{
		taggedFiles.Add(TagLib.File.Create(file));
	}
}

static string InputString(string message)
{
	string input;

	do
	{
		Console.Write(message);

		input = Console.ReadLine()!;
	} while (string.IsNullOrWhiteSpace(input));

	return input;
}

static uint InputUInt32(string message)
{
	uint input;

	do
	{
		Console.Write(message);
	} while (!uint.TryParse(Console.ReadLine(), out input));

	return input;
}

static void Log(string? message, bool? isPositive = null)
{
	switch (isPositive)
	{
		case true:
		{
			Console.ForegroundColor = ConsoleColor.Green;

			break;
		}

		case false:
		{
			Console.ForegroundColor = ConsoleColor.Red;

			break;
		}
	}

	Console.WriteLine($"[{message}]");

	Console.ForegroundColor = ConsoleColor.White;
}