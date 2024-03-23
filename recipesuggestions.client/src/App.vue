<template>
    <div id="app">
        <div class="webHeader">

            <h1>Recipe Suggestions</h1>

            <nav>
                <ul>
                    <li><router-link to="/" class="link">Home</router-link></li>
                    <li><router-link to="/recipes/add" class="link">Add Recipe</router-link></li>
                    <li><router-link to="/recipes/edit" class="link">Edit Recipe</router-link></li>
                </ul>
            </nav>

        </div>


        <router-view></router-view>

        <main>
            <h2>blabla</h2>
        </main>
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

<style scoped>
    * {
        padding: 0;
        margin: 0;
    }

    body {
        background-color: pink;
    }

    .webHeader {
        background-color: rgba(31, 19, 19);
        width: 100%;
        position: fixed;
        text-align: center;
    }

        .webHeader h1 {
            color: white;
        }

        .webHeader nav ul {
            line-height: 50px;
            display: flex;
            list-style-type: none;
        }

        .webHeader nav ul li {
            margin-right: 20px;
        }

        .webHeader .link {
            color: white;
            text-decoration: none;
        }

        .webHeader .link:hover {
            text-decoration: underline;
        }



</style>
