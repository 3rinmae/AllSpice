import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

function clearData() {
  AppState.ingredients = {}
};
class IngredientsService {
  async getIngredientsByRecipeId(recipeId) {
    clearData();
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    logger.log('get ingredients by recipe id', res.data)
    AppState.ingredients = res.data.map((ingredient) => new Ingredient(ingredient))
  }
}

export const ingredientsService = new IngredientsService()