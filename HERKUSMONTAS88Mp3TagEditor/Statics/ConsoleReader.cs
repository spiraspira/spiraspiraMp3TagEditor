namespace HERKUSMONTAS88Mp3TagEditor.Statics;

public static class ConsoleReader
{
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