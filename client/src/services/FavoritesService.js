import { AppState } from "../AppState"
import { Favorite } from "../models/Favorite"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService {
  async getMyFavorites() {
    const res = await api.get(`api/account/favorites`)
    logger.log('my favorites', res.data)
    AppState.myFavorites = res.data.map((fav) => new Favorite(fav))
  }
}

export const favoritesService = new FavoritesService()