using jcAP.API.Enums;

namespace jcAP.API.Objects
{
    public class ToolsResponseItem
    {
        public Guid Id { get; set;  }

        public string Name { get; set; }

        public string Version { get; set; }

        public ToolCategory Category { get; set; }

        public string? InputSchema { get; set; }

        public string? OutputSchema { get; set; }

        public Uri? InvocationEndpoint { get; set; }

        public ToolStatus Status { get; set; }
    }
}
