using Microsoft.AspNetCore.Mvc;

using jcAP.API.Controllers.Base;
using jcAP.API.Objects.Tools;
using jcAP.API.Repositories;

namespace jcAP.API.Controllers
{
    public class ToolsController(ILogger<BaseController> logger, IHostEnvironment env, IToolRepository repo) : BaseController(logger, env)
    {
        private readonly IToolRepository _repo = repo;

        /// <summary>
        /// Returns a list of Tools (Name, Status and Capabilities)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolDto>>> Get(CancellationToken cancellationToken)
        {
            try
            {
                var items = await _repo.GetAllDtosAsync(cancellationToken);
                return ApiOk(items);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to fetch tools");
                return ApiError("Failed to fetch tools");
            }
        }

        /// <summary>
        /// Registers a new Tool
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ToolDto>> Create([FromBody] ToolDto dto, CancellationToken cancellationToken)
        {
            if (dto is null) return ApiValidationError(ModelState);
            try
            {
                var created = await _repo.CreateFromDtoAsync(dto, cancellationToken);
                return ApiCreated($"/api/tools/{created.Id}", created);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to create tool");
                return ApiError("Failed to create tool");
            }
        }

        /// <summary>
        /// Updates an existing Tool
        /// </summary>
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ToolDto dto, CancellationToken cancellationToken)
        {
            if (dto is null) return ApiValidationError(ModelState);
            try
            {
                await _repo.UpdateFromDtoAsync(id, dto, cancellationToken);
                return ApiOk();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to update tool");
                return ApiError("Failed to update tool");
            }
        }

        /// <summary>
        /// Returns the metadata for a given tool (excluding secrets)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ToolDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            var item = await _repo.GetDtoByIdAsync(id, cancellationToken);
            return item is null ? NotFound() : ApiOk(item);
        }

        /// <summary>
        /// Invokes the Tool
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("{id:guid}/invoke")]
        public async Task<ActionResult> Invoke(Guid id, CancellationToken cancellationToken)
        {
            // implementation detail: call other services or queue requests
            return ApiError("Invoke endpoint not implemented", 501);
        }
    }
}