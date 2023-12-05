using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using TaskAngularProject.Models;

namespace TaskAngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskListContext _database;

        public TaskController(TaskListContext database)
        {
            _database = database;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var taskList = await _database.Tasks.ToListAsync();
            return Ok(taskList);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] Models.Task request)
        {
            await _database.Tasks.AddAsync(request);
            await _database.SaveChangesAsync();
            return Ok(request);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskDelete = await _database.Tasks.FindAsync(id);
            
            if(taskDelete == null)
            {
                return BadRequest("Task not found...");
            }

            _database.Tasks.Remove(taskDelete);
            await _database.SaveChangesAsync();
            return Ok();
        }
    }
}
