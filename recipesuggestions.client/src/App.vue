<template>
    <div id="app">
        <div class="sidebar">
            <!-- Προσθήκη του component recipeSearch -->
            <RecipeSearch />
        </div>
        <div class="main-content">
            <!-- Εδώ προστέθηκε το recipeSuggestions -->
            <RecipeSuggestions />

            <router-link to="/">Home</router-link>  |
            <router-link to="/recipes/add">Add Recipe</router-link>  |
            <router-link to="/recipes/edit">Edit Recipe</router-link>
            <router-view></router-view>
        </div>
    </div>
</template>

<script>
    import { ref, computed } from 'vue'
    import { createRouter, createWebHistory } from 'vue-router';

    import RecipeSearch from './components/homePage/recipeSearch.vue';
    import RecipeSuggestions from './components/RecipeSuggestions.vue';

    import TheWelcome from './components/TheWelcome.vue'
    import AddRecipe from './components/AddRecipe.vue'
    import EditRecipe from './components/EditRecipe.vue'

    export default {
        components: {
            RecipeSearch,
            RecipeSuggestions
        },
    };

    /*
    export default {
        components: {
            RecipeSuggestions,
            TheWelcome,
            AddRecipe,
            EditRecipe
        }
    };
    */


</script>

<style>
    .sidebar {
        width: 25%;
        float: left;
    }

    /* Νέα κλάση για το recipeSuggestions */
    .recipe-suggestions {
        width: 75%; /* Εδώ ορίζεται το πλάτος του recipeSuggestions */
        float: right; /* Αλλαγή του float από right σε left */
    }

    @media (max-width: 768px) {
        .sidebar,
        .main-content {
            width: 100%;
            float: none;
        }
    }

    body {
        background-color: grey;
    }
</style>
