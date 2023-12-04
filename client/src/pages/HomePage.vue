<template>
  <div class="container">
    <section class="row justify-content-center">
      <div class="col-4 d-flex py-3 px-4 filter-card justify-content-between">
        <p class="m-0">Home</p>
        <p class="m-0">My Recipes</p>
        <p class="m-0">Favorites</p>
      </div>
    </section>
    <section class="row"></section>
  </div>
</template>

<script>
export default {
  setup() {
    return {

    }
  }
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
