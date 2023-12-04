<template>
  <div class="container">
    <section class="row">
      <div class="col-4 d-flex p-3">
        <p>Home</p>
        <p>My Recipes</p>
        <p>Favorites</p>
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
</style>
