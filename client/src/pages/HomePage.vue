<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-4 d-flex py-3 px-4 filter-card justify-content-between">
        <p class="m-0">Home</p>
        <p class="m-0">My Recipes</p>
        <p @click="getMyFavorites()" class="m-0" role="button" type="button">Favorites</p>
      </div>
    </section>
    <section class="row">
      <div class="col-12 d-flex flex-wrap justify-content-center">
        <div class="row justify-content-center">
          <div v-for="recipe in recipes" :key="recipe.id" class="col-10 col-md-6 col-lg-4 p-3 p-md-5">
            <!-- {{ recipe }} -->
            <RecipeBasicCard :recipeProp="recipe" />
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from "vue";
import { recipesService } from "../services/RecipesService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { AppState } from "../AppState";
import RecipeBasicCard from "../components/RecipeBasicCard.vue";

export default {
  setup() {
    onMounted(() => {
      getRecipes();
    });
    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    return {
      recipes: computed(() => AppState.recipes),
      account: computed(() => AppState.account)
    };
  },
  components: { RecipeBasicCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.filter-card {
  background-color: white;
  border-radius: 8px;
  box-shadow: 4px 4px 11px rgba(0, 0, 0, 0.435);
}
</style>
