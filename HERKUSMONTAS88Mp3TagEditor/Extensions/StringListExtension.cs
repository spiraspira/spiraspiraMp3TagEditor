#pragma warning disable SYSLIB1045
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace HERKUSMONTAS88Mp3TagEditor.Extensions;

/// <summary>
/// Contains extension methods for <see cref="List{T}"/> of <see cref="string"/>.
/// </summary>
public static class StringListExtension
{
	public static readonly List<Regex> CorrectFileNamePatters = new()
	{
		
		new(@"([0-9]+)\s(.+)"), //1 title.mp3
		new(@"([0-9]+)\.\s(.+)"), //1. title.mp3
		new(@"([0-9]+)-(.+)"), //1-title.mp3
	};

	public static Match? GetMatch(string input, List<Regex> patterns)
	{
		foreach (var pattern in patterns)
		{
			if (pattern.IsMatch(input))
			{
				return pattern.Match(input);
			}
		}

		return null;
	}

	public static NumberTitle GetNumberTitle(string fileName, List<Regex> patterns)
	{
		uint number = 0;

		string title = string.Empty;

		var match = GetMatch(fileName, patterns);

		if (match is null)
		{
			SharedEvents.InvokeNotify($"Incorrect file name: {fileName}. Couldn't set number and title.", false);
		}
		else
		{
			number = uint.Parse(match.Groups[1].Value);

			title = match.Groups[2].Value;
		}

		return new(number, title);
	}

	/// <summary>
	/// Converts <see cref="List{T}"/> of <see cref="string"/> expected to be
	/// file paths to <see cref="List{T}"/> of <see cref="Tuple"/> (<see cref="uint"/>, <see cref="string"/>)/>
	/// </summary>
	/// <param name="files">File paths by specified rules.</param>
	/// <returns>List of numbers and titles.</returns>
	public static NumberTitleList ToNumberTitleList(this List<string> files)
	{
		var fileNames = files.Select(Path.GetFileNameWithoutExtension).ToList();

		return fileNames.Select(fileName => GetNumberTitle(fileName!, CorrectFileNamePatters)).ToList();
	}

	/// <summary>
	/// Converts a <see cref="List{T}"/> of <see cref="string"/> expected to be audio file paths to <see cref="List{T}"/> of <see cref="TagLib.File"/>.
	/// </summary>
	/// <param name="files">Audio file paths.</param>
	/// <returns><see cref="TagLib.File"/> list.</returns>
	public static List<TagLib.File> ToTaggedFiles(this List<string> files)
	{
		return files.Select(TagLib.File.Create).ToList();
	}
}