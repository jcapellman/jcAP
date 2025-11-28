using System;
using jcAP.API.Enums;

namespace jcAP.API.Entities
{
    public class ToolEntity : Base.BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public ToolCategory Category { get; set; }
        public string? InputSchema { get; set; }
        public string? OutputSchema { get; set; }
        
        public string? InvocationEndpoint { get; set; }
        public ToolStatus Status { get; set; }
    }
}