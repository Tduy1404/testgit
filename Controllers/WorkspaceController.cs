using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WorkspaceController : ControllerBase
{
    private readonly IWorkspaceService _workspaceService;

    public WorkspaceController(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetWorkspaces()
    {
        var workspaces = await _workspaceService.GetWorkspacesAsync();
        return Ok(workspaces);
    }

    [HttpPost]
    public async Task<ActionResult<Workspace>> CreateWorkspace(WorkspaceDto workspaceDto)
    {
        var workspace = await _workspaceService.AddWorkspaceAsync(workspaceDto);
        return CreatedAtAction(nameof(GetWorkspaces), new { id = workspace.Id }, workspace);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Workspace>> UpdateWorkspace(int id, WorkspaceDto workspaceDto)
    {
        var workspace = await _workspaceService.UpdateWorkspaceAsync(id, workspaceDto);
        return Ok(workspace);
    }

    // TODO: Implement the rest of the endpoints
    // GET /api/v1/workspaces/{id}
    // DELETE /api/v1/workspaces/{id}
}
