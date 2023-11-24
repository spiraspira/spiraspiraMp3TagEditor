namespace HERKUSMONTAS88Mp3TagEditor.Events;

public static class SharedEvents
{
	public static event Action<string, bool?>? Notify;

	public static void InvokeNotify(string message, bool? isPositive = null)
	{
		Notify?.Invoke(message, isPositive);
	}
}