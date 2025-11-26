using jcAP.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace jcAP.API.Controllers
{
    public class AgentsController : BaseController
    {
        public AgentsController(ILogger<BaseController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Returns a list of Agents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return [];
        }
        /// <summary>
        /// Registers a new Agent
        /// </summary>
        [HttpPost]
        public void Create()
        {
        }

        /// <summary>
        /// Updates an existing Agent
        /// </summary>
        [HttpPatch]
        public void Update()
        {
        }

        /// <summary>
        /// Returns the metadata for a given agent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public object Get(Guid id)
        {
            return new object();
        }

        /// <summary>
        /// Deletes an Agent
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
