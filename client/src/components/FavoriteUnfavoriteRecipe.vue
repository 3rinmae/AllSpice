<template>
  <div class="">
    <span v-if="isFav" @click="unfavorite()" class="text-gb" role="button">
      <i class="fs-4 mdi mdi-heart"></i>
    </span>
    <span v-else @click="favorite()" class="text-gb" role="button">
      <i class="fs-4 mdi mdi-heart-outline"></i>
    </span>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { favoritesService } from "../services/FavoritesService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
export default {
  props: { id: { type: Number, required: true } },
  setup(props) {
    return {
      recipes: computed(() => AppState.recipes),
      myFavorites: computed(() => AppState.myFavorites),
      isFav: computed(() => AppState.myFavorites.find((recipe) => recipe.id == props.id)),
      async unfavorite() {
        try {
          await favoritesService.unfavorite(this.isFav)
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      },
      async favorite() {
        try {
          await favoritesService.favorite(props.id)
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.text-gb {
  background: rgba(8, 48, 61, 0.45);
  max-width: fit-content;
  height: fit-content;
  // max-height: fit-content;
  color: white;
  border-radius: 7px;
  box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(14.7px);
  -webkit-backdrop-filter: blur(14.7px);
}
</style>