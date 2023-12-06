namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipesService _recipesService;
  private readonly IngredientsService _ingredientsService;

  public RecipesController(Auth0Provider auth0Provider, RecipesService recipesService, IngredientsService ingredientsService)
  {
    _auth0Provider = auth0Provider;
    _recipesService = recipesService;
    _ingredientsService = ingredientsService;
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.CreateRecipe(recipeData);
      return Ok(recipe);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetRecipes()
  {
    try
    {
      List<Recipe> recipes = _recipesService.GetRecipes();
      return Ok(recipes);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      {
        Recipe recipe = _recipesService.GetRecipeById(recipeId);
        return Ok(recipe);
      }
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [Authorize]
  [HttpPut("{recipeId}")]
  public ActionResult<Recipe> UpdateRecipe(int recipeId, [FromBody] Recipe recipeData)
  {
    try
    {
      Recipe recipe = _recipesService.UpdateRecipe(recipeId, recipeData);
      return Ok(recipe);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeId}")]
  public async Task<ActionResult<string>> DestroyRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      string Message = _recipesService.DestroyRecipe(recipeId, userId);
      return Ok(Message);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeId);
      return Ok(ingredients);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }


}
