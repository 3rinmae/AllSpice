namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class IngredientsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly IngredientsService _ingredientsService;

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      ingredientData.CreatorId = userInfo.Id;
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
      return Ok(ingredient);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}