using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using todolist.api.Model;

namespace todolist.api.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class TaskListController
    {
        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmptyTodoTaskList taskList)
        {
            await _taskQueries.Create(newTask);
            _logger.LogInformation("Adding task {task}", JsonConvert.SerializeObject(newTask));
            return CreatedAtAction("Get", new { id = newTask.Id });
        }        
    }
}