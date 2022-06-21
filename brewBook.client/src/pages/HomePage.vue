<template>
  <div class="container">
    <div class="row mt-5 justify-content-around">
      <div class="col-md-6">
        <NewestRecipesCard :recipes="newestFiveFilterRecipes">
          <template #header>Newest Filter Recipes</template>
        </NewestRecipesCard>
      </div>
      <div class="col-md-6">
        <NewestRecipesCard :recipes="newestFiveEspressoRecipes">
          <template #header>Newest Espresso Recipes</template>
        </NewestRecipesCard>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import { onMounted } from '@vue/runtime-core'
import { filterRecipesService } from '../services/FilterRecipesService.js'
import { espressoRecipesService } from '../services/EspressoRecipesService.js'
import Pop from '../utils/Pop.js'
export default {
  setup() {
    onMounted(async () => {
      try {
        await filterRecipesService.get()
        await espressoRecipesService.get()
      }
      catch (error) {
        console.error("[COULD_NOT_LOAD_RECIPES]", error.message);
        Pop.toast(error.message, "error");
      }
    })
    return {
      newestFiveFilterRecipes: computed(() => {
        let arr = []
        if (AppState.filterRecipes?.length >= 5) {
          for (let i = 0; i < 5; i++)
            arr.push(AppState.filterRecipes[i])
        } else {
          for (let i = 0; i < AppState.filterRecipes?.length; i++)
            arr.push(AppState.filterRecipes[i])
        }
        return arr
      }),
      newestFiveEspressoRecipes: computed(() => {
        let arr = []
        if (AppState.espressoRecipes?.length >= 5) {
          for (let i = 0; i < 5; i++)
            arr.push(AppState.espressoRecipes[i])
        } else {
          for (let i = 0; i < AppState.espressoRecipes?.length; i++)
            arr.push(AppState.espressoRecipes[i])
        }
        return arr
      })
    }
  }
}
</script>

<style scoped lang="scss">
</style>
