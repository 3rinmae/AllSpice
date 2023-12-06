import { AppState } from "../AppState"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {
  async getRecipes() {
    const res = await api.get('api/recipes')
    logger.log('get recipes', res.data)
    AppState.recipes = res.data.map((recipe) => new Recipe(recipe))
  }

  async saveEdit(updatedInstructions) {
    const recipeId = updatedInstructions.id
    const res = await api.put(`api/recipes/${recipeId}`, updatedInstructions)
    logger.log("recipe service update", res.data)
    AppState.activeRecipe = new Recipe(res.data)
  }

  async destroyRecipe() {
    const recipeId = AppState.activeRecipe.id
    const res = await api.delete(`api/recipes/${recipeId}`)
    logger.log('recipe deleted', res.data)
    AppState.recipes = AppState.recipes.filter()
  }
}

export const recipesService = new RecipesService()