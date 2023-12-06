<template>
  <div v-if="activeRecipe" class="container-fluid">
    <section class="row">
      <div class="col-4 m-0 pe-3 recipe-img d-flex justify-content-end">
        <FavoriteUnfavoriteRecipe :id="activeRecipe.id" />
      </div>
      <div class="col-8 pt-4 px-2">
        <section class="row">
          <div class="col-12 pb-3">
            <span class="fs-4 m-0 p-4 text-secondary">
              {{ activeRecipe.title }}
            </span>
            <span class="text-gb p-1">{{ activeRecipe.category }}</span>
          </div>
        </section>
        <section class="row justify-content-evenly align-content-between">
          <div class="col-5 p-2">
            <div class="bg-secondary rounded-top-2 fs-4 text-white text-center p-2">
              <p class="m-0">Instructions</p>
            </div>
            <div class="ii-card d-flex align-content-between flex-wrap justify-content-end rounded-bottom-2 p-2 d-block">
              <div>
                <span v-if="editable.edit == false" class="fw-light">{{ activeRecipe.instructions }}</span>
                <textarea v-else v-model="editableInstructions.instructions" name="" id="" rows="10"
                  class="form-control">{{ activeRecipe.instructions }}</textarea>
              </div>
              <div v-if="activeRecipe.creatorId == accountId" class="">
                <button v-if="editable.edit == false" @click="editable.edit = !editable.edit" class="btn "
                  title="edit instructions" role="button" type="button">
                  <i class="mdi mdi-pencil"></i>
                </button>
                <button v-else @click="editable.edit = !editable.edit; saveEdit()" class="btn">
                  <i class="mdi mdi-check"></i>
                </button>
              </div>
            </div>
          </div>
          <div class="col-5 p-2">
            <div class="bg-secondary rounded-top-2 fs-4 text-white text-center p-2">
              <p class="m-0">Ingredients</p>
            </div>
            <div class="ii-card d-flex align-content-between flex-wrap justify-content-end rounded-bottom-2 p-2 d-block">
              <div>
                <span v-if="ingredients" class="fw-light">
                  <li v-for=" ingredient  in  ingredients " :key="ingredient.id">{{ ingredient.quantity + " " +
                    ingredient.name }}</li>
                </span>
              </div>
              <div v-if="activeRecipe.creatorId == accountId" class="text-end">
                <button class="btn text-end" title="edit instructions" role="button" type="button">
                  <i class="mdi mdi-pencil"></i>
                </button>
              </div>
            </div>
          </div>
        </section>
        <section class="row text-end">
          <div class="col-12 d-flex align-items-center justify-content-between">
            <button @click="destroyRecipe()" class="btn fs-4" role="button" title="delete recipe"> <i
                class="mdi mdi-delete-outline"></i></button>
            <span class="">
              Published By: {{ activeRecipe.creator.name }}
            </span>
          </div>
        </section>
      </div>
      <div class="col-4"></div>
    </section>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect } from 'vue';
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { ingredientsService } from "../services/IngredientsService";
import FavoriteUnfavoriteRecipe from "./FavoriteUnfavoriteRecipe.vue";
import { recipesService } from "../services/RecipesService";
import { Modal } from "bootstrap";
export default {
  setup() {
    const editable = ref({ edit: false })
    const editableInstructions = ref({})
    watchEffect(() => {
      logger.log('watch effect working on recipe edit')
      editableInstructions.value = AppState.activeRecipe
    })
    onMounted(() => {
      // logger.log('recipe id', AppState.activeRecipe.id)
    });
    async function getIngredientsByRecipeId() {
      try {
        await ingredientsService.getIngredientsByRecipeId(AppState.activeRecipe.id);
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    ;
    return {
      editable,
      editableInstructions,
      activeRecipe: computed(() => AppState.activeRecipe),
      recipeCoverImg: computed(() => `url(${AppState.activeRecipe?.img})`),
      ingredients: computed(() => AppState.ingredients),
      myFavorites: computed(() => AppState.myFavorites),
      accountId: computed(() => AppState.account.id),

      async saveEdit() {
        try {
          const updatedInstructions = editableInstructions.value;
          logger.log('editing instructions', updatedInstructions)
          await recipesService.saveEdit(updatedInstructions);
        } catch (error) {
          logger.error(error);
          Pop.error(error);
        }
      },

      async destroyRecipe() {
        try {
          await recipesService.destroyRecipe()
          Modal.getOrCreateInstance('#recipeDetailsModal').hide()
        } catch (error) {
          logger.error(error);
          Pop.error(error);
        }
      }
    };
  },
  components: { FavoriteUnfavoriteRecipe }
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