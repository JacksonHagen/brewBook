<template>
  <div class="container">
    <div class="row">
      <RecipeInfoCard />
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState.js'
import { onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop.js'
import { filterRecipesService } from '../services/FilterRecipesService.js'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await filterRecipesService.get(route.params.id)
      }
      catch (error) {
        console.error("[COULD_NOT_LOAD_RECIPE]", error.message);
        Pop.toast(error.message, "error");
      }
    })
    return {
			activeRecipe: computed(() => AppState.activeFilterRecipe)
    }
  }
}
</script>


<style lang="scss" scoped>
</style>