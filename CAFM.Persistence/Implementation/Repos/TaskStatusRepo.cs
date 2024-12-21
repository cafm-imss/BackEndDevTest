using CAFM.Application.Contracts.Repos;
using Microsoft.EntityFrameworkCore;

namespace CAFM.Persistence.Implementation.Repos
{
    internal class TaskStatusRepo : ITaskStatusRepo
    {
        private readonly CAFMDbContext _context;

        public TaskStatusRepo(CAFMDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsTaskStatusExist(int taskStatusId)
        => await _context.TaskStatues
            .AnyAsync(a => a.Id == taskStatusId);
    }
}
