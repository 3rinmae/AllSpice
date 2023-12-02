


namespace AllSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repository;

  public FavoritesService(FavoritesRepository repository)
  {
    _repository = repository;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    Favorite favorite = _repository.CreateFavorite(favoriteData);
    return favorite;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repository.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception($"Invalid id: {favoriteId}");
    }
    return favorite;
  }

  internal string DestroyFavorite(int favoriteId, string userId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != userId)
    {
      throw new Exception("This is not your favorite to delete");
    }
    _repository.DestroyFavorite(favoriteId);
    return "You have unfavorited this recipe";
  }

  internal List<RecipeFavorite> GetRecipeFavoritesByAccountId(string userId)
  {
    List<RecipeFavorite> recipeFavorites = _repository.GetRecipeFavoritesByAccountId(userId);
    return recipeFavorites;
  }
}