using WBPortal.Api.Models;
using System.Collections.Generic;

namespace WBPortal.Api.Services
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProjectById(int id);
        IEnumerable<Project> GetProjectsByManager(int managerId);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(int id);
    }
}
