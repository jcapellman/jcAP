using jcAP.API.Controllers.Base;
using jcAP.API.Objects;
using Microsoft.AspNetCore.Mvc;

namespace jcAP.API.Controllers
{

    public class ToolsController : BaseController
    {
        public ToolsController(ILogger<BaseController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Returns a list of Tools (Name, Status and Capabilities)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return [];
        }

        /// <summary>
        /// Registers a new Tool
        /// </summary>
        [HttpPost]
        public void Create()
        {

        }

        /// <summary>
        /// Updates an existing Tool
        /// </summary>
        [HttpPatch]
        public void Update()
        {

        }

        /// <summary>
        /// Returns the metadata for a given tool (excluding secrets)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ToolsResponseItem Get(Guid id)
        {
            return new ToolsResponseItem();
        }

        /// <summary>
        /// Invokes the Tool
        /// </summary>
        /// <param name="id"></param>
        [HttpPost("{id}")]
        public void Invoke(Guid id)
        {

        }
    }
}