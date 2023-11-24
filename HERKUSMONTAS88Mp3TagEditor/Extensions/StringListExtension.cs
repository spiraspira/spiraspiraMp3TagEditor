namespace HERKUSMONTAS88Mp3TagEditor.Extensions;

public static class StringListExtension
{
	public static List<(uint, string)> ToNumberTitleList(this List<string> files)
	{
		var fileNames = files.Select(Path.GetFileNameWithoutExtension).ToList()!;

		List<(uint, string)> dictionary = new();

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

	public static List<TagLib.File> ToTaggedFiles(this List<string> files)
	{
		return files.Select(TagLib.File.Create).ToList();
	}
}