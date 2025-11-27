using Microsoft.AspNetCore.Mvc;
using jcAP.API.Controllers.Base;

namespace jcAP.Api.Controllers
{
    public class SessionController : BaseController
    {
        public SessionController(ILogger<BaseController> logger, IHostEnvironment env) : base(logger, env)
        {
        }

        /// <summary>
        /// Start a new session for an agent (async). Returns 202 with session id and Location header.
        /// </summary>
        /// <param name="request">Session start payload (agentId, input, budget, pin versions).</param>
        /// <returns>202 Accepted with session id/location.</returns>
        [HttpPost]
        public async Task<IActionResult> StartSession([FromBody] SessionStartRequestDto request, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get current session state including budget usage and last step.
        /// </summary>
        /// <param name="sessionId">Session id GUID.</param>
        /// <returns>Session state DTO.</returns>
        [HttpGet("{sessionId:guid}")]
        public async Task<IActionResult> GetSession(Guid sessionId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submit an external event or request the agent take the next step.
        /// </summary>
        /// <param name="sessionId">Session id to continue.</param>
        /// <param name="stepRequest">Step request containing idempotency key and payload.</param>
        /// <returns>202 Accepted with step id/status.</returns>
        [HttpPost("{sessionId:guid}/steps")]
        public async Task<IActionResult> ContinueSession(Guid sessionId, [FromBody] StepRequestDto stepRequest, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Approve or reject a pending human approval for a session.
        /// </summary>
        /// <param name="sessionId">Session id awaiting approval.</param>
        /// <param name="approval">Approval decision and optional notes.</param>
        /// <returns>200 OK with updated session state.</returns>
        [HttpPost("{sessionId:guid}/approve")]
        public async Task<IActionResult> ApproveSession(Guid sessionId, [FromBody] ApprovalRequestDto approval, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cancel a running or queued session.
        /// </summary>
        /// <param name="sessionId">Session id to cancel.</param>
        /// <returns>204 No Content on success.</returns>
        [HttpPost("{sessionId:guid}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSession(Guid sessionId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a replayable trace for a session with per-step artifact references.
        /// </summary>
        /// <param name="sessionId">Session id to fetch trace for.</param>
        /// <returns>Trace DTO with ordered events and artifact refs.</returns>
        [HttpGet("{sessionId:guid}/trace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTrace(Guid sessionId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// List recent sessions optionally filtered by agent id; supports paging.
        /// </summary>
        /// <param name="agentId">Optional agent id to filter sessions.</param>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>Paged list of session summaries.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListSessions([FromQuery] Guid? agentId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get step status and artifacts for a specific step id.
        /// </summary>
        /// <param name="stepId">Step id GUID.</param>
        /// <returns>Step detail DTO.</returns>
        [HttpGet("steps/{stepId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStep(Guid stepId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Replay or retry a specific step (idempotent if idempotency key provided).
        /// </summary>
        /// <param name="stepId">Step id to replay.</param>
        /// <param name="replayRequest">Optional parameters controlling replay (force, simulate).</param>
        /// <returns>202 Accepted with new step id or status.</returns>
        [HttpPost("steps/{stepId:guid}/replay")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReplayStep(Guid stepId, [FromBody] StepReplayRequestDto replayRequest, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }

    // DTO placeholders
    public record SessionStartRequestDto(Guid AgentId, object? Input, int? BudgetTokens = null, string? PromptVersion = null);
    public record StepRequestDto(string? IdempotencyKey, object? Event);
    public record ApprovalRequestDto(bool Approved, string? ApproverId = null, string? Notes = null);
    public record StepReplayRequestDto(bool Force = false, bool Simulate = false);
}