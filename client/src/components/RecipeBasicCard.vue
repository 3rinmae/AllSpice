<template>
  <div class="recipe-card p-3 row">
    <div class="col-12 d-flex justify-content-between">
      <p class="text-gb fw-bold p-1 m-0">{{ recipeProp.category }}</p>
      <p class="m-0 fs-4 text-gb text-end text-danger"> <i class="mdi mdi-heart-outline"></i> <i
          class="mdi mdi-heart"></i>
      </p>
    </div>
    <div class="col-12 d-flex align-items-end">
      <div class="text-gb row">
        <p class="fw-bold m-0 fs-4 col-12">{{ recipeProp.title }}</p>
        <p class="m-0 col-12">Creator: {{ recipeProp.creator.name }}</p>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from "../models/Recipe";
export default {
  props: { recipeProp: { type: Recipe, required: true } },
  setup(props) {
    return {
      recipeCoverImg: computed(() => `url(${props.recipeProp.img})`)
    }
  }
};
</script>


<style lang="scss" scoped>
.recipe-card {
  background-image: v-bind(recipeCoverImg);
  background-size: cover;
  border-radius: 8px;
  box-shadow: 4px 4px 11px rgba(0, 0, 0, 0.435);
  height: 35vh;
  width: 100%;
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
</style>