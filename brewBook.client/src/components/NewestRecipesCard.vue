<template>
  <div class="card text-secondary bg-warning w-100">
    <div class="card-header">
      <h1 class="fs-2 text-center">
        <slot name="header"></slot>
      </h1>
    </div>
    <div class="card-body">
      <ul>
        <li
          class="fs-4 text-dark d-flex justify-content-between"
          v-for="r in recipes"
          :key="r?.id"
        >
          <div class="">
            <router-link
              :to="{ name: 'Recipe', params: { id: r.id } }"
              class="text-dark"
            >
              ~ {{ r?.title }}
            </router-link>
          </div>
          <div class="fs-5 text-secondary selectable">
            {{ r?.creator.name }}
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
export default {
  props: {
    recipes: {
      type: Array,
      default: []
    },
  },
  setup() {
    return {

    }
  }
}
</script>


<style lang="scss" scoped>
</style>