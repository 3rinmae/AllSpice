



namespace AllSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
      INSERT INTO
      favorites(accountId, recipeId)
      VALUES(@AccountId, @RecipeId);
      
      SELECT
      fav.*,
      acc.*
      FROM favorites fav
      JOIN accounts acc ON fav.accountId = acc.id
      WHERE fav.id = LAST_INSERT_ID();";
    Favorite favorite = _db.Query<Favorite, Profile, Favorite>(sql, (favorite, profile) =>
    {
      favorite.AccountId = profile.Id;
      return favorite;
    }, favoriteData).FirstOrDefault();
    return favorite;
  }

  internal void DestroyFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";
    _db.Execute(sql, new { favoriteId });
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";
    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return favorite;
  }

  internal List<RecipeFavorite> GetRecipeFavoritesByAccountId(string userId)
  {
    string sql = @"
      SELECT
      fav.*,
      rec.*,
      acc.*
      FROM favorites fav
      JOIN recipes rec ON fav.recipeId = rec.id
      JOIN accounts acc on acc.id = rec.creatorId
      WHERE fav.accountId = @userId;";

    List<RecipeFavorite> recipeFavorites = _db.Query<Favorite, RecipeFavorite, Profile, RecipeFavorite>
    (sql, (favorite, recipeFavorites, profile) =>
    {
      recipeFavorites.FavoriteId = favorite.Id;
      recipeFavorites.AccountId = favorite.AccountId;
      recipeFavorites.CreatorId = profile.Id;
      return recipeFavorites;
    }, new { userId }).ToList();
    return recipeFavorites;
  }
}