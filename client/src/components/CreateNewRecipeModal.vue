<template>
  <div class="">
    <p class=" bg-secondary fs-2 text-white px-4 py-2">New Recipe</p>
    <form @submit.prevent="createNewRecipe()" class="p-5">
      <div class="mb-3">
        <label for="Title" class="form-label">Title</label>
        <input v-model="editable.title" type="text" class="form-control" id="title" aria-describedby="title" />
      </div>
      <div class="mb-3">
        <label for="Category" class="form-label">Category</label>
        <input v-model="editable.category" type="text" class="form-control" id="category" aria-describedby="category" />
      </div>
      <div class="mb-3">
        <label for="Image Url" class="form-label">Image Url</label>
        <input v-model="editable.img" type="text" class="form-control" id="ImageUrl" aria-describedby="Image Url" />
      </div>
      <div class="mb-3">
        <label for="Instructions" class="form-label">Instructions</label>
        <input v-model="editable.instructions" type="text" class="form-control" id="instructions"
          aria-describedby="Instructions" />
      </div>
      <button type="submit" class="btn btn-primary">Submit</button>
    </form>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { recipesService } from "../services/RecipesService";
import { Modal } from "bootstrap";
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createNewRecipe() {
        try {
          const recipeData = editable.value;
          logger.log("creating recipe", recipeData)
          await recipesService.createNewRecipe(recipeData)
          Modal.getOrCreateInstance('#createNewRecipeModal').hide()
          editable.value = {}
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