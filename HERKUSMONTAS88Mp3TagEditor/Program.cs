#pragma warning disable SYSLIB1045

SharedEvents.Notify += Log;

//https://regex-generator.olafneumann.org
List<Regex> correctFileNamePatters = new()
{

	new(@"([0-9]+)\s(.+)"), //1 title.mp3
	new(@"([0-9]+)\.\s(.+)"), //1. title.mp3
	new(@"([0-9]+)-(.+)") //1-title.mp3
};

List<string>
	audioExtensions = new() { "mp3", "wav", "flac" },
	artworkExtensions = new() { "jpg", "png" };

const string
	artworkFileName = "cover";

string
	directoryPath = AppDomain.CurrentDomain.BaseDirectory;

List<string>
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

NumberTitleList
	numbersAndTitles = audioFiles.ToNumberTitleList(correctFileNamePatters);

List<uint>
	numbers = numbersAndTitles.Select(t => t.Item1).ToList();

List<string>
	titles = numbersAndTitles.Select(t => t.Item2).ToList();

List<TagLib.File>
	taggedAudioFiles = audioFiles.ToTaggedFiles();

taggedAudioFiles.SetAlbum(album);

taggedAudioFiles.SetArtist(artist);

taggedAudioFiles.SetArtwork(artwork);

taggedAudioFiles.SetGenre(genre);

taggedAudioFiles.SetNumbers(numbers);

taggedAudioFiles.SetTitles(titles);

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
	Console.ForegroundColor = isPositive switch
	{
		true => ConsoleColor.Green,
		false => ConsoleColor.Red,
		_ => Console.ForegroundColor
	};

	Console.WriteLine($"[{message}]");

	if (isPositive is not null)
	{
		Console.ForegroundColor = ConsoleColor.White;
	}
}