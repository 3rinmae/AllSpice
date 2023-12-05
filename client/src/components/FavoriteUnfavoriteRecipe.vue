<template>
  <div class="component">


  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { favoritesService } from "../services/FavoritesService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
export default {
  setup() {
    return {
      recipes: computed(() => AppState.recipes),
      myFavorites: computed(() => AppState.myFavorites),

      async favorite() {
        try {
          await favoritesService.getMyFavorites()
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped></style>