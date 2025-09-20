using WBPortal.Api.Data;
using WBPortal.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace WBPortal.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAllProjects() =>
            _context.Projects.Include(p => p.ProjectManager).Include(p => p.Feedbacks).ThenInclude(f => f.User).ToList();

        public Project GetProjectById(int id) =>
            _context.Projects.Include(p => p.ProjectManager).Include(p => p.Feedbacks).ThenInclude(f => f.User)
                            .FirstOrDefault(p => p.Id == id);

        public IEnumerable<Project> GetProjectsByManager(int managerId) =>
            _context.Projects.Include(p => p.ProjectManager).Include(p => p.Feedbacks).ThenInclude(f => f.User)
                             .Where(p => p.ProjectManagerId == managerId).ToList();

        public void CreateProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}
