namespace jcAP.API.Objects.About
{
    public sealed record AboutDto
    {
        public string ApplicationName { get; init; } = string.Empty;
        public string Environment { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public DateTime StartTimeUtc { get; init; }
        public TimeSpan Uptime { get; init; }
        public string InstanceId { get; init; } = string.Empty;
        public object? Links { get; init; }
    }
}