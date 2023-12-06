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
                <textarea v-else v-model="editableInstructions.instructions" name="" id="" rows="13"
                  class="form-control">{{ activeRecipe.instructions }}</textarea>
              </div>
              <div v-if="activeRecipe.creatorId == accountId" class="">
                <button v-if="editable.edit == false" @click="editable.edit = !editable.edit" class="btn "
                  title="edit instructions" role="button" type="button">
                  <i class="mdi mdi-pencil"></i>
                </button>
                <button v-else @click="editable.edit = !editable.edit; saveEditInstructions()" class="btn" title="save">
                  <i class="mdi mdi-check"></i>
                </button>
              </div>
            </div>
          </div>
          <div class="col-5 p-2">
            <div class="bg-secondary rounded-top-2 fs-4 text-white text-center p-2">
              <p class="m-0">Ingredients</p>
            </div>
            <div class="ii-card rounded-bottom-2 p-2 ">
              <div>
                <span v-for=" ingredient  in  ingredients " :key="ingredient.id" class="fw-light">
                  <span v-if="ingredient.edit == false" class="d-flex align-items-center justify-content-between">
                    <li class="">{{ ingredient.quantity + " " + ingredient.name }}</li>
                    <div v-if="activeRecipe.creatorId == accountId" class="text-end">
                      <button @click="ingredient.edit = !ingredient.edit" class="btn text-end" title="edit ingredients"
                        role="button" type="button">
                        <i class="mdi mdi-pencil"></i>
                      </button>
                      <button @click="destroyIngredient(ingredient)" role="button" title="delete" class="btn">
                        <i class="mdi mdi-delete-outline"></i>
                      </button>
                    </div>
                  </span>
                  <span v-else class="d-flex">
                    <input type="text" v-model="ingredient.quantity" name="quantity" id="editIngredientQuantity"
                      class="form-control w-50">
                    <input type="text" v-model="ingredient.name" name="name" id="editIngredientName" class="form-control">
                    <button @click="saveEditIngredients(ingredient)" class="btn" title="save">
                      <i class="mdi mdi-check"></i>
                    </button>
                  </span>
                </span>
                <span class="d-flex">
                  <input type="text" v-model="editableIngredients.quantity" name="quantity" id="editIngredientQuantity"
                    class="form-control w-50">
                  <input type="text" v-model="editableIngredients.name" name="name" id="editIngredientName"
                    class="form-control">
                  <button @click="createIngredient()" class="btn" title="save">
                    <i class="mdi mdi-plus"></i>
                  </button>
                </span>
              </div>
            </div>
          </div>
        </section>
        <section class="row text-end">
          <div class="col-12 d-flex align-items-center justify-content-end">
            <span class="text-end">
              Published By: {{ activeRecipe.creator.name }}
            </span>
            <button v-if="activeRecipe.creatorId == accountId" @click="destroyRecipe()" class="btn fs-4" role="button"
              title="delete recipe"> <i class="mdi mdi-delete-outline"></i></button>
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
    const editableIng = ref({ edit: false })
    const editableInstructions = ref({})
    const editableIngredients = ref({})
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
      editableIng,
      editableInstructions,
      editableIngredients,
      activeRecipe: computed(() => AppState.activeRecipe),
      recipeCoverImg: computed(() => `url(${AppState.activeRecipe?.img})`),
      ingredients: computed(() => AppState.ingredients),
      myFavorites: computed(() => AppState.myFavorites),
      accountId: computed(() => AppState.account.id),

      async saveEditInstructions() {
        try {
          const updatedInstructions = editableInstructions.value;
          logger.log('editing instructions', updatedInstructions)
          await recipesService.saveEditInstructions(updatedInstructions);
        } catch (error) {
          logger.error(error);
          Pop.error(error);
        }
      },

      async destroyRecipe() {
        try {
          const yes = await Pop.confirm('Are you sure you would like to delete this recipe?')
          if (!yes) {
            return
          }
          await recipesService.destroyRecipe()
          Modal.getOrCreateInstance('#recipeDetailsModal').hide()
        } catch (error) {
          logger.error(error);
          Pop.error(error);
        }
      },

      async saveEditIngredients(ingredient) {
        try {
          await ingredientsService.saveEditIngredients(ingredient)
          ingredient.edit = !ingredient.edit
        } catch (error) {
        }
      },

      async destroyIngredient(ingredient) {
        try {
          const yes = await Pop.confirm('Are you sure you would like to delete this ingredient?')
          if (!yes) {
            return
          }
          await ingredientsService.destroyIngredient(ingredient)
        } catch (error) {
          logger.error(error);
          Pop.error(error);
        }
      },
      async createIngredient() {
        try {
          editableIngredients.value.recipeId = this.activeRecipe.id
          await ingredientsService.createIngredient(editableIngredients.value)
          editableIngredients.value = {}
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