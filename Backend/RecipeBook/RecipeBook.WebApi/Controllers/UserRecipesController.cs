using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.BLL.Extensions;
using RecipeBook.BLL.Models.DTOs;
using RecipeBook.BLL.Services.Interfaces;

namespace RecipeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserRecipesController : ControllerBase
{
    private readonly IUserRecipesService _service;

    public UserRecipesController(IUserRecipesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        int userId = HttpContext.GetUserId();
        return Ok(await _service.GetAllRecipesAsync(userId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipe(int id)
    {
        int userId = HttpContext.GetUserId();
        return Ok(await _service.GetRecipeAsync(userId, id));
    }

    [HttpPost]
    public async Task<IActionResult> AddRecipe([FromBody] RecipeCreateDTO dto)
    {
        int userId = HttpContext.GetUserId();
        var resultDTO = await _service.AddRecipeAsync(userId, dto);
        return CreatedAtAction(nameof(GetRecipe), new {id = resultDTO.Id}, resultDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeUpdateDTO dto)
    {
        int userId = HttpContext.GetUserId();
        var recipeDTO = await _service.UpdateRecipeAsync(userId, id, dto);
        return Ok(recipeDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        int userId = HttpContext.GetUserId();
        await _service.DeleteRecipeAsync(userId, id);
        return NoContent();
    }
}