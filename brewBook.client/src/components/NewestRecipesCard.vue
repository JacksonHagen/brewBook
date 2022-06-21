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
          <div class="text-dark selectable" @click="goToRecipe(r)">
            ~ {{ r?.title }}
          </div>
          <div class="fs-5 text-secondary">
            {{ r?.creator.name }}
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { useRouter } from 'vue-router'
export default {
  props: {
    recipes: {
      type: Array,
      default: []
    },
  },
  setup(props) {
    const router = useRouter()
    return {
      goToRecipe(recipe) {
        if (recipe.brewer) {
          router.push({ name: 'FilterRecipe', params: { id: recipe.id } })
        } else {
          router.push({ name: 'EspressoRecipe', params: { id: recipe.id } })
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>