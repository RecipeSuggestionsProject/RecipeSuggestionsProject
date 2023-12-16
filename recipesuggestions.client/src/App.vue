<script setup>
import { ref, computed } from 'vue'

import RecipeSuggestions from './components/RecipeSuggestions.vue'
import TheWelcome from './components/TheWelcome.vue'
import AddRecipe from './components/AddRecipe.vue'
import EditRecipe from './components/EditRecipe.vue'

const routes = {
    "/": TheWelcome,
    '/recipes/add': AddRecipe,
    '/recipes/edit': EditRecipe
}

// Track URL fragment identifier in currentPath
const currentPath = ref(window.location.hash)
window.addEventListener('hashchange', () => {
    currentPath.value = window.location.hash
})

// Update current view when currentPath changes
const currentView = computed(() => {
    return routes[currentPath.value.slice(1) || "/"]
})

</script>

<template>
   

  <header>
    <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

    <nav>
        <ul>
            <li><a href="#/">Home</a></li>
            <li><a href="#/recipes/add">Add Recipe</a></li>
            <li><a href="#/recipes/edit">Edit Recipe</a></li>
        </ul>
    </nav>
  </header>

  

  <main>
    <component :is="currentView" />
  </main>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
