namespace Notification.API.Entities;

public class Notification
{
    public enum LabelType
    {
        Information,
        Success,
        Warning,
        Error,
        Reminder
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string? LargeBody { get; set; }
    public string? SummaryText { get; set; }
    public LabelType Label { get; set; }
    public string? Url { get; set; }
}