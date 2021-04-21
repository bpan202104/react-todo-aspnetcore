using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using todolist.api.DAL;
using todolist.api.Model;

namespace todolist.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController: ControllerBase
    {
        private readonly ILogger<TaskListController> _logger;
        private readonly ITaskQueries _taskQueries;

        public TaskListController(ILogger<TaskListController> logger, ITaskQueries taskQueries)
        {
            this._logger = logger;
            this._taskQueries = taskQueries;
        }
        
        // POST: api/TaskList
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoTaskListHeader newTaskList)
        {
            await _taskQueries.CreateTaskList(newTaskList);
            _logger.LogInformation("Adding task list {task}", JsonConvert.SerializeObject(newTaskList));
            return CreatedAtAction("Post", new { id = newTaskList.Id });
        }        
        
        // POST: api/TaskList
        [HttpGet("{id}", Name = "GetTaskList")]
        public async Task<TodoTaskList> Get(Guid id)
        {
            return await _taskQueries.GetTaskList(id);
        }      
        
        // GET: api/TaskList
        [HttpGet]
        public async Task<List<TodoTaskListHeader>> Get()
        {
            return (await _taskQueries.ListTasksLists()).ToList();
        }     
        // DELETE: api/TaskLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskQueries.DeleteTaskListById(id);
            return NoContent();
        }        
        
    }
}