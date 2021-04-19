using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using todolist.api.Model;
using todolist.api.Queries;

namespace todolist.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ILogger<TaskController> _logger;
        private readonly ITaskQueries _taskQueries;

        public TaskController(ILogger<TaskController> logger, ITaskQueries taskQueries)
        {
            this._logger = logger;
            this._taskQueries = taskQueries;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<List<TodoTask>> Get()
        {
            return (await _taskQueries.ListTasks()).ToList();
        }

        // GET: api/Task/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<TodoTask> Get(Guid id)
        {
            return await _taskQueries.GetById(id);
        }

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoTask newTask)
        {
            await _taskQueries.Create(newTask);
            _logger.LogInformation("Adding task {task}", JsonConvert.SerializeObject(newTask));
            return CreatedAtAction("Get", new { id = newTask.Id });
        }


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskQueries.DeleteById(id);
            return NoContent();
        }
    }
}
