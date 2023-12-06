<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-4 d-flex py-3 px-4 filter-card justify-content-between">
        <p @click="getRecipes()" class="m-0" role="button" type="button">Home</p>
        <p @click="showMyRecipes()" class="m-0" role="button" type="button">My Recipes</p>
        <p @click="showMyFavorites()" class="m-0" role="button" type="button">Favorites</p>
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
import { computed, onMounted, watch } from "vue";
import { recipesService } from "../services/RecipesService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { AppState } from "../AppState";
import RecipeBasicCard from "../components/RecipeBasicCard.vue";
import { Recipe } from "../models/Recipe";

export default {
  setup() {
    // const wantsToSeeFavorites = ref({ edit: false })
    onMounted(() => {
      getRecipes();
    });
    // watch(); how would I use this to update when unfavoriting an item in favorites view?
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
      account: computed(() => AppState.account),
      getRecipes,
      async showMyFavorites() {
        try {
          await recipesService.showMyFavorites()
        } catch (error) {
          logger.error(error);
          Pop.error(error);
        }
      },
      async showMyRecipes() {
        try {
          await recipesService.showMyRecipes()
        } catch (error) {
          logger.error(error);
          Pop.error(error);
        }
      }
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
