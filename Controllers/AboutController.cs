using jcAP.API.Controllers.Base;
using jcAP.API.Objects.About;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace jcAP.API.Controllers
{
    public class AboutController(ILogger<BaseController> logger, IHostEnvironment env) : Base.BaseController(logger, env)
    {
        /// <summary>
        /// Basic application info (name, environment, version, uptime, links).
        /// GET /api/about
        /// </summary>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var entryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
                var assemblyName = entryAssembly.GetName();
                var productVersion = FileVersionInfo.GetVersionInfo(entryAssembly.Location).ProductVersion;
                var version = productVersion ?? assemblyName.Version?.ToString() ?? "unknown";

                var process = Process.GetCurrentProcess();
                var startTimeUtc = process.StartTime.ToUniversalTime();
                var uptime = DateTime.UtcNow - startTimeUtc;

                var about = new AboutDto
                {
                    ApplicationName = assemblyName.Name ?? "unknown",
                    Environment = _env.EnvironmentName,
                    Version = version,
                    StartTimeUtc = startTimeUtc,
                    Uptime = uptime,
                    InstanceId = Environment.MachineName,
                    Links = new
                    {
                        Self = $"{Request.Scheme}://{Request.Host}{Request.Path}",
                        Swagger = $"{Request.Scheme}://{Request.Host}/swagger/index.html"
                    }
                };

                return ApiOk(about);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to build about information");
                return ApiError("Failed to build about information");
            }
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
