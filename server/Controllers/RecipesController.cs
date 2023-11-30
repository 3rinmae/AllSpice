namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;

  public RecipesController(Auth0Provider auth0Provider)
  {
    _auth0Provider = auth0Provider
  }
}
