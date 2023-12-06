
namespace AllSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repository;

  public RecipesService(RecipesRepository repository)
  {
    _repository = repository;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repository.CreateRecipe(recipeData);
    return recipe;
  }

  internal String DestroyRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId)
    {
      throw new Exception("Not your recipe to delete");
    }
    _repository.DestroyRecipe(recipeId);
    return $"{recipe.Title} has been deleted!";
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repository.GetRecipeById(recipeId);

    if (recipe == null)
    {
      throw new Exception($"Invalid id: {recipeId}");
    }
    return recipe;
  }

  internal List<Recipe> GetRecipes(string title)
  {
    if (title == null)
    {
      List<Recipe> recipes = _repository.GetRecipes();
      return recipes;
    }
    List<Recipe> recipesWithQuery = _repository.GetRecipesWithQuery(title);
    return recipesWithQuery;
  }

  internal Recipe UpdateRecipe(int recipeId, Recipe recipeData)
  {
    Recipe originalRecipe = GetRecipeById(recipeId);
    originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;
    originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;
    originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;
    originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;

    _repository.UpdateRecipe(originalRecipe);
    return originalRecipe;
  }
}