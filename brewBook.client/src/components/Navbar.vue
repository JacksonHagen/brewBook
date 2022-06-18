<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto justify-content-between">
        <li class="flamenco">
          <router-link
            :to="{ name: 'About' }"
            class="btn text-light lighten-30 selectable text"
          >
            <h1>Filter Recipes</h1>
          </router-link>
        </li>
        <li class="flamenco">
          <router-link
            :to="{ name: 'About' }"
            class="btn text-light lighten-30 selectable text"
          >
            <h1>Espresso Recipes</h1>
          </router-link>
        </li>
        <li class="flamenco">
          <router-link
            :to="{ name: 'About' }"
            class="btn text-light lighten-30 selectable text"
          >
            <h1>Terms</h1>
          </router-link>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
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
</style>
