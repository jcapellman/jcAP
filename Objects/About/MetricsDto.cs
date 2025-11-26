namespace jcAP.API.Objects.About
{
    public sealed record MetricsDto(
        int ProcessId,
        long WorkingSetBytes,
        long VirtualMemoryBytes,
        long PagedMemoryBytes,
        int ThreadCount,
        int HandleCount,
        DateTime StartTimeUtc,
        int ProcessorCount,
        DateTime UtcNow);
}
