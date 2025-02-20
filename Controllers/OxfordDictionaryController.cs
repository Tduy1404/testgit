    using DotnetAPIProject.Models.Entities;
    using DotnetAPIProject.Services.Implementations;
    using DotnetAPIProject.Services.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    namespace DotnetAPIProject.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OxfordDictionaryController : ControllerBase
        {
            private readonly IOxfordDictionaryService _oxfordService;

            public OxfordDictionaryController(IOxfordDictionaryService oxfordService)
            {
                _oxfordService = oxfordService;
            }
     
        [HttpGet]
        public async Task<ActionResult<string>> GetDefinition(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return BadRequest("Word cannot be empty.");

            try
            {
                var definition = await _oxfordService.GetWordDefinitionAsync(word);
                return Ok(definition);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Error calling Oxford API: " + ex.Message);
            }
        }




    }
}
