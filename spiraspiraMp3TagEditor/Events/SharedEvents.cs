namespace spiraspiraMp3TagEditor.Events;

/// <summary>
/// Contains event handlers used by multiple classes in the whole application.
/// </summary>
public static class SharedEvents
{
	/// <summary>
	/// Notifies with some message and it's mood (positive or negative).
	/// </summary>
	public static event Action<string, bool?>? Notify;

	/// <summary>
	/// Invokes <see cref="Notify"/>.
	/// </summary>
	/// <param name="message">Message text.</param>
	/// <param name="isPositive">Mood of the message (negative or positive).</param>
	public static void InvokeNotify(string message, bool? isPositive = null)
	{
		Notify?.Invoke(message, isPositive);
	}
}