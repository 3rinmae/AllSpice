


namespace AllSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repository;

  public IngredientsService(IngredientsRepository repository)
  {
    _repository = repository;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _repository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repository.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception($"Invalid id: {ingredientId}");
    }
    return ingredient;
  }

  internal string DestroyIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    if (ingredient.CreatorId != userId)
    {
      throw new Exception("This is not your ingredient to delete");
    }
    _repository.DestroyIngredient(ingredientId);
    return $"{ingredient.Name} has been deleted!";
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repository.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  internal Ingredient UpdateIngredient(int ingredientId, Ingredient ingredientData)
  {
    Ingredient originalIngredient = GetIngredientById(ingredientId);
    // originalIngredient.Name = ingredientData.Name ?? originalIngredient;
    originalIngredient.Quantity = ingredientData.Quantity ?? originalIngredient.Quantity;

    _repository.UpdateIngredient(originalIngredient);
    return originalIngredient;
  }
}