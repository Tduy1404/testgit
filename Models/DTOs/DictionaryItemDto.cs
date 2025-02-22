namespace DotnetAPIProject.Models.DTOs;

public class DictionaryItemDto
{
    public required string Word { get; set; }
    public required string Definition { get; set; }
    public int WorkspaceId { get; set; }
    public required WorkspaceDto Workspace { get; set; }
}