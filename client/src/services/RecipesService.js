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

  async showMyFavorites() {
    AppState.recipes = AppState.myFavorites.map((recipe) => new Recipe(recipe))
  }

  async getMyRecipes() {
    AppState.myRecipes = AppState.recipes.filter((recipe) => recipe.creatorId == AppState.account.id)
    logger.log('getting my recipes', AppState.myRecipes)
  }

  async showMyRecipes() {
    AppState.recipes = AppState.myRecipes.map((recipe) => new Recipe(recipe))
  }

  async saveEditInstructions(updatedInstructions) {
    const recipeId = updatedInstructions.id
    const res = await api.put(`api/recipes/${recipeId}`, updatedInstructions)
    logger.log("recipe service update", res.data)
    AppState.activeRecipe = new Recipe(res.data)
  }

  async destroyRecipe() {
    const recipeId = AppState.activeRecipe.id
    const res = await api.delete(`api/recipes/${recipeId}`)
    logger.log('recipe deleted', res.data)
    this.getRecipes()
  }

  async createNewRecipe(recipeData) {
    const res = await api.post('api/recipes', recipeData)
    logger.log('create recipe', res.data)
    AppState.recipes.unshift(new Recipe(res.data))
  }
}

export const recipesService = new RecipesService()