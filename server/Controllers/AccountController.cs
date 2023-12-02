namespace AllSpice.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoriteService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritesService favoriteService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _favoriteService = favoriteService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("favorites")]
  public async Task<ActionResult<List<RecipeFavorite>>> GetRecipeFavoritesByAccountId()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<RecipeFavorite> recipeFavorites = _favoriteService.GetRecipeFavoritesByAccountId(userId);
      return Ok(recipeFavorites);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
