import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

function clearData() {
  AppState.ingredients = null
};
class IngredientsService {
  async getIngredientsByRecipeId(recipeId) {
    clearData();
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    logger.log('get ingredients by recipe id', res.data)
    AppState.ingredients = res.data.map((ingredient) => new Ingredient(ingredient))
  }

  async saveEditIngredients(ingredient) {
    const res = await api.put(`api/ingredients/${ingredient.id}`, ingredient)
    logger.log('save edit ingredient res.data', res.data)
  }

  async destroyIngredient(ingredient) {
    const res = await api.delete(`api/ingredients/${ingredient.id}`)
    logger.log('ingredient deleted', res.data)
  }

  async createIngredient(ingredient) {
    const res = await api.post('api/ingredients', ingredient)
    logger.log('create ingredient', res.data)
    AppState.ingredients.push(new Ingredient(res.data))
  }
}

export const ingredientsService = new IngredientsService()