namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly RecipesService _recipesService;

  public RecipesController(Auth0Provider auth0Provider, RecipesService recipesService)
  {
    _auth0Provider = auth0Provider;
    _recipesService = recipesService;
  }
}
