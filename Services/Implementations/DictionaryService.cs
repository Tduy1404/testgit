using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class DictionaryService : IDictionaryService
{
    private readonly ApplicationDbContext _context;

    public DictionaryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DictionaryItem> CreateAsync(DictionaryItemDto item)
    {
        var dictionaryItem = new DictionaryItem
        {
            Word = item.Word,
            Definition = item.Definition,
            WorkspaceId = item.WorkspaceId,
            Workspace = new Workspace
            {
                Name = item.Workspace.Name,
                Description = "Default Description",
            },
        };

        _context.DictionaryItems.Add(dictionaryItem);
        await _context.SaveChangesAsync();
        return dictionaryItem;
    }

    public async Task<IEnumerable<DictionaryItem>> GetAllAsync()
    {
        return await _context.DictionaryItems.Include(d => d.Workspace).ToListAsync();
    }

    public async Task<DictionaryItem?> GetByIdAsync(int id)
    {
        return await _context
            .DictionaryItems.Include(d => d.Workspace)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<DictionaryItem?> UpdateAsync(int id, DictionaryItemDto item)
    {
        var existingItem = await _context.DictionaryItems.FindAsync(id);
        if (existingItem == null)
            return null;

        existingItem.Word = item.Word;
        existingItem.Definition = item.Definition;
        existingItem.WorkspaceId = item.WorkspaceId;

        await _context.SaveChangesAsync();
        return existingItem;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.DictionaryItems.FindAsync(id);
        if (item == null)
            return false;

        _context.DictionaryItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
