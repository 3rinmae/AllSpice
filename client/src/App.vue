<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="">
  </footer>

  <!-- <div class="modal fade" id="recipeDetailsModal" tabindex="-1" data-bs-keyboard="false" role="dialog"
      aria-labelledby="modalTitleId" aria-hidden="true">
      <div class="modal-dialog modal-dialog-top modal-dialog-scrollable modal-xl" role="document">
        <div class="modal-content">
          <div class="modal-body p-0" v-if="activeRecipe">
            <RecipeDetailCard />
          </div>
        </div>
      </div>
    </div> -->

  <div class="modal fade" id="recipeDetailsModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">

        <div class="modal-body p-0">
          <RecipeDetailCard />
        </div>

      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import RecipeDetailCard from "./components/RecipeDetailCard.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState),
      activeRecipe: computed(() => AppState.activeRecipe)
    }
  },
  components: { Navbar, RecipeDetailCard }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
