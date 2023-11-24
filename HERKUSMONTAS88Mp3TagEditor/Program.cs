using HERKUSMONTAS88Mp3TagEditor.Statics;

SharedEvents.Notify += Log;

const string
	artworkFileName = "cover";

string
	directoryPath = AppDomain.CurrentDomain.BaseDirectory;

List<string>
	audioExtensions = new() { "mp3", "wav", "flac" },
	artworkExtensions = new() { "jpg", "png" },
	audioFiles = FileReader.GetFiles(directoryPath, audioExtensions);

if (audioFiles.Count == 0)
{
	ApplicationExit();
}

string?
	artwork = FileReader.GetArtworkFilePath(directoryPath, artworkFileName, artworkExtensions);

string
	artist = ConsoleReader.ReadString("Artist: "),
	album = ConsoleReader.ReadString("Album: "),
	genre = ConsoleReader.ReadString("Genre: ");

uint
	year = ConsoleReader.ReadUInt32("Year: ");

List<(uint, string)>
	numberTitleDictionary = audioFiles.ToNumberTitleList();

List<TagLib.File>
	taggedAudioFiles = audioFiles.ToTaggedFiles();

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