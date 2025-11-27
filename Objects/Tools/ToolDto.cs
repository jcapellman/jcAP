using jcAP.API.Enums;

namespace jcAP.API.Objects.Tools
{
    public sealed record ToolDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public ToolCategory Category { get; init; }
        public string? InputSchema { get; init; }
        public string? OutputSchema { get; init; }
        public Uri? InvocationEndpoint { get; init; }
        public ToolStatus Status { get; init; }
    }
}