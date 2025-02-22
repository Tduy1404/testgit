using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces;

public interface IWorkspaceService
{
    Task<IEnumerable<Workspace>> GetWorkspacesAsync();
    Task<Workspace> AddWorkspaceAsync(WorkspaceDto workspace);
    Task<Workspace?> UpdateWorkspaceAsync(int id, WorkspaceDto workspace);
    // Task<bool> DeleteWorkspaceAsync(int id);
}
