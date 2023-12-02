namespace HERKUSMONTAS88Mp3TagEditor.Extensions;

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
	/// <returns>List of numbers and titles.</returns>
	public static NumbersTitlesList ToNumberTitleList(this List<string> files)
	{
		var fileNames = files.Select(Path.GetFileNameWithoutExtension).ToList()!;

		NumbersTitlesList dictionary = new();

		foreach (var file in fileNames)
		{
			uint number = 0;

			string name = string.Empty;

			try
			{
				number = uint.Parse(file!.Split()[0].Remove(file.Split()[0].IndexOf('.'), 1));

				name = file[(file.IndexOf(' ') + 1)..];
			}
			catch
			{
				try
				{
					number = uint.Parse(file!.Split()[0]);

					name = file[(file.IndexOf(' ') + 1)..];
				}
				catch
				{
					SharedEvents.InvokeNotify($"Incorrect file name: {file}", false);
				}
			}

			dictionary.Add((number, name));
		}

		return dictionary;
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