using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DotnetAPIProject.Models;
using DotnetAPIProject.Services.Implementations;
using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces
{

    public interface IOxfordDictionaryService
    {
        Task<string> GetWordDefinitionAsync(string word);
    }

}