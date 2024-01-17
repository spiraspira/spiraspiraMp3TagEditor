namespace spiraspiraMp3TagEditor.Extensions;

/// <summary>
/// Contains extension methods for <see cref="List{T}"/> of <see cref="string"/>.
/// </summary>
public static class StringListExtension
{
	/// <summary>
	/// Converts <see cref="List{T}"/> of <see cref="string"/> expected to be
	/// file paths to <see cref="List{T}"/> of <see cref="Tuple"/> (<see cref="uint"/>, <see cref="string"/>)/>
	/// </summary>
	/// <param name="files">File paths by specified rules.</param>
	/// <param name="patterns">List of patterns.</param>
	/// <returns>List of numbers and titles.</returns>
	public static NumberTitleList ToNumberTitleList(this List<string> files, List<Regex> patterns)
	{
		return files
			.Select(Path.GetFileNameWithoutExtension)
			.Select(fileName => GetNumberTitle(fileName!, patterns))
			.ToList();

		NumberTitle GetNumberTitle(string fileName, List<Regex> patterns)
		{
			uint number = default;

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

			return new NumberTitle(number, title);

			Match? GetMatch(string input, List<Regex> patterns)
			{
				return patterns
					.Find(pattern => pattern.IsMatch(input))?
					.Match(input);
			}
		}
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