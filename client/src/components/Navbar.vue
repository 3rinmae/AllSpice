<template>
  <div class="container-fluid">
    <section class="row nav-coverImg m-4">
      <div class="col-12">
        <nav class="navbar d-flex flex-column navbar-expand-sm  px-3 row">
          <router-link class="navbar-brand d-block align-items-center text-center order-2 col-12" :to="{ name: 'Home' }">
            <div class="d-flex flex-column align-items-center text-center text-white">
              <p class="fs-1 m-0">All-Spice</p>
              <p class="fs-5 m-0">Cherish Your Family Recipes</p>
            </div>
          </router-link>
          <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
            aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button> -->
          <div class="d-flex align-items-center justify-content-between col-12 order-1">
            <button class=" btn btn-outline-white " type="button" title="add recipe" data-bs-toggle="modal"
              data-bs-target="#createNewRecipeModal"><i class="mdi mdi-folder-plus-outline"></i></button>
            <div class="d-flex align-items-center">
              <div class=" d-flex">
                <input type="text" class="form-control" placeholder="Search" aria-label="Search"
                  aria-describedby="search">
                <button class="btn btn-outline-white" type="button"><i class="mdi mdi-magnify"></i></button>
              </div>
              <div class=" d-flex " id="navbarText">
                <!-- <ul class="navbar-nav me-auto">
                  <li>
                    <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
                      About
                    </router-link>
                  </li>
                </ul> -->
                <!-- LOGIN COMPONENT HERE -->
                <Login />
              </div>
            </div>
          </div>
        </nav>
      </div>
      <div class="col-12">

      </div>
    </section>
  </div>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.nav-coverImg {
  background-image: url(src/assets/img/baking.jpg);
  background-size: cover;
  background-position: center;
  height: 222px;
  width: cover;
  border-radius: 8px;
  box-shadow: 4px 4px 22px rgba(0, 0, 0, 0.658);
}

/* @media screen and (min-width: 768px) {
  nav {
    height: 222px;
    background-image: url(src/assets/img/baking.jpg);
    background-position-x: left;
    background-attachment: fixed;
    background-size: cover;
    background-position: 50% 416%;
  }
} */
</style>
