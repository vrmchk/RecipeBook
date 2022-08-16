using Microsoft.AspNetCore.Mvc;
using RecipeBook.BLL.Services.Interfaces;

namespace RecipeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly IRecipeSearchService _searchService;

    public SearchController(IRecipeSearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("{title}")]
    public async Task<IActionResult> Search(string title)
    {
        return Ok(await _searchService.Search(title));
    }
}