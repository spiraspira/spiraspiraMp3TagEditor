namespace HERKUSMONTAS88Mp3TagEditor.Statics;

/// <summary>
/// Contains methods that handle console input.
/// </summary>
public static class ConsoleReader
{
	/// <summary>
	/// Reads <see cref="string"/> value from the console, excluding null and empty input.
	/// </summary>
	/// <param name="message">Input message.</param>
	/// <returns><see cref="string"/> value (not null or empty).</returns>
	public static string ReadString(string message)
	{
		string input;

		do
		{
			Console.Write(message);

			input = Console.ReadLine()!;

			if (string.IsNullOrWhiteSpace(input))
			{
				SharedEvents.InvokeNotify("Write something.", false);

				continue;
			}

			break;
		} while (true);

		return input;
	}

	/// <summary>
	/// Reads <see cref="int"/> value from the console, excluding null, empty or incorrect input.
	/// </summary>
	/// <param name="message">Input message.</param>
	/// <returns><see cref="int"/> value</returns>
	public static uint ReadUInt32(string message)
	{
		uint input;

		do
		{
			Console.Write(message);

			if (!uint.TryParse(Console.ReadLine(), out input))
			{
				SharedEvents.InvokeNotify("Wrong input", false);

				continue;
			}

			break;
		} while (true);

		return input;
	}
}