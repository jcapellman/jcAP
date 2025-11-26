using jcAP.API.Controllers.Base;
using jcAP.API.Objects.About;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace jcAP.API.Controllers
{
    public class AboutController : Base.BaseController
    {
        public AboutController(ILogger<BaseController> logger) : base(logger)
        {
        }
        /// <summary>
        /// Basic runtime metrics for quick diagnostics.
        /// GET /api/about/metrics
        /// </summary>
        [HttpGet("metrics")]
        public ActionResult Metrics()
        {
            try
            {
                var proc = Process.GetCurrentProcess();
                var metrics = new MetricsDto(
                    proc.Id,
                    proc.WorkingSet64,
                    proc.VirtualMemorySize64,
                    proc.PagedMemorySize64,
                    proc.Threads.Count,
                    proc.HandleCount,
                    proc.StartTime.ToUniversalTime(),
                    Environment.ProcessorCount,
                    DateTime.UtcNow
                );

                return ApiOk(metrics);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to collect metrics");
                return ApiError("Failed to collect metrics");
            }
        }
    }
}
