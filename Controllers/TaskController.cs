using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTO_s;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                Priority = task.Priority
            };
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateNewTask(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Name = dto.Name,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted,
                DueDate = dto.DueDate,
                Priority = dto.Priority
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var taskDto = new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                Priority = task.Priority
            };

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, taskDto);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExistingTask(int id, CreateTaskDto dto)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null) return NotFound();

            existingTask.Name = dto.Name;
            existingTask.Description = dto.Description;
            existingTask.IsCompleted = dto.IsCompleted;
            existingTask.DueDate = dto.DueDate;
            existingTask.Priority = dto.Priority;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> SearchTasksByQuery(string? query, bool? isCompleted)
        {
            var tasks = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(query))
                tasks = tasks.Where(t => t.Name.Contains(query) || t.Description.Contains(query));

            if (isCompleted.HasValue)
                tasks = tasks.Where(t => t.IsCompleted == isCompleted.Value);

            var result = await tasks.ToListAsync();

            return result.Select(t => new TaskDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                DueDate = t.DueDate,
                Priority = t.Priority
            }).ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasksWithPaging(int page = 1, int pageSize = 10, string? sortBy = "Name")
        {
            var query = _context.Tasks.AsQueryable();

            query = sortBy switch
            {
                "DueDate" => query.OrderBy(t => t.DueDate),
                "Priority" => query.OrderBy(t => t.Priority),
                _ => query.OrderBy(t => t.Name)
            };

            var tasks = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                DueDate = t.DueDate,
                Priority = t.Priority
            }).ToList();
        }
    }
}