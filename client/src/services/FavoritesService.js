import { AppState } from "../AppState"
import { Favorite } from "../models/Favorite"
import { RecipeFavorite } from "../models/RecipeFavorite"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService {
  async getMyFavorites() {
    const res = await api.get('account/favorites')
    logger.log('my favorites', res.data)
    AppState.myFavorites = res.data.map((fav) => new RecipeFavorite(fav))
    logger.log('myFavorites in AppState', AppState.myFavorites)
  }

  async unfavorite(isFav) {
    const res = await api.delete(`api/favorites/${isFav.favoriteId}`)
    AppState.myFavorites = AppState.myFavorites.filter(recipeFavorite => recipeFavorite.favoriteId != isFav.favoriteId)
    // this.getMyFavorites()
    return res.data
  }

  async favorite(recipeId) {
    logger.log(recipeId)
    const res = await api.post('api/favorites', { recipeId })
    AppState.myFavorites.push(new RecipeFavorite(res.data))
    this.getMyFavorites()
  }
}

export const favoritesService = new FavoritesService()