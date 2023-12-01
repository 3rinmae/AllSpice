
namespace AllSpice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
        INSERT INTO
        recipes(title, category, img, creatorId, instructions)
        VALUES(@Title, @Category, @Img, @CreatorId, @Instructions);
        
        SELECT
        rec.*,
        acc.*
        FROM recipes rec
        JOIN accounts acc ON rec.creatorId = acc.id
        WHERE rec.id = LAST_INSERT_ID();";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }, recipeData).FirstOrDefault();
    return recipe;
  }
}
