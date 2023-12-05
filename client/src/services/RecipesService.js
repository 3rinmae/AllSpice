import { AppState } from "../AppState"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {
  async getRecipes() {
    const res = await api.get('api/recipes')
    // logger.log('get recipes', res.data)
    AppState.recipes = res.data.map((recipe) => new Recipe(recipe))
  }
}

export const recipesService = new RecipesService()