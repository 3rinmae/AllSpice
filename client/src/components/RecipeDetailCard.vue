<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-4 m-0 p-0 recipe-img d-flex justify-content-end">
        <p class="text-gb p-2 fs-4">
          <i class="mdi mdi-heart-outline"></i>
        </p>
        <p class="text-gb p-2 fs-4">
          <i class="mdi mdi-heart"></i>
        </p>
      </div>
      <div class="col-8 py-4 px-2">
        <section class="row">
          <div class="col-12">
            <span class="fs-4 m-0 p-3 text-secondary">
              {{ activeRecipe.title }}
            </span>
            <span class="text-gb p-1">{{ activeRecipe.category }}</span>
            <p class="px-3 m-0">
              subtitle?
            </p>
          </div>
        </section>
        <section class="row justify-content-evenly align-content-between">
          <div class="col-5 p-2">
            <div class="bg-secondary rounded-top-2 fs-4 text-white text-center p-2">
              <p class="m-0">Instructions</p>
            </div>
            <div class="ii-card d-flex align-content-between flex-wrap justify-content-end rounded-bottom-2 p-2 d-block">
              <div>
                <span class="fw-light">{{ activeRecipe.instructions }}</span>
              </div>
              <div class="">
                <button class="btn " title="edit instructions" role="button" type="button">
                  <i class="mdi mdi-pencil"></i>
                </button>
              </div>
            </div>
          </div>
          <div class="col-5 p-2">
            <div class="bg-secondary rounded-top-2 fs-4 text-white text-center p-2">
              <p class="m-0">Ingredients</p>
            </div>
            <div class="ii-card d-flex align-content-between flex-wrap  rounded-bottom-2 p-2 d-block">
              <div>
                <span class="fw-light">
                  <li v-for="ingredient in ingredients" :key="ingredient.id">{{ ingredient.name }}</li>
                </span>
              </div>
              <div class="text-end">
                <button class="btn " title="edit instructions" role="button" type="button">
                  <i class="mdi mdi-pencil"></i>
                </button>
              </div>
            </div>
          </div>
        </section>
        <section class="row text-end">
          <div class="col-12">
            Published By: {{ activeRecipe.creator.name }}
          </div>
        </section>
      </div>
      <div class="col-4"></div>
    </section>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { ingredientsService } from "../services/IngredientsService";
export default {

  setup() {
    onMounted(() => {
      // getIngredientsByRecipeId();
    });
    async function getIngredientsByRecipeId() {
      try {
        await ingredientsService.getIngredientsByRecipeId(AppState.activeRecipe.id)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    };
    return {
      activeRecipe: computed(() => AppState.activeRecipe),
      recipeCoverImg: computed(() => `url(${AppState.activeRecipe.img})`),
      ingredients: computed(() => AppState.ingredients)
    }
  }
};
</script>


<style lang="scss" scoped>
.recipe-img {
  background-image: v-bind(recipeCoverImg);
  background-size: cover;
  background-position: center;
  height: 70vh;
}

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

.ii-card {
  height: 45vh;
  background-color: #14a2b8;
}
</style>