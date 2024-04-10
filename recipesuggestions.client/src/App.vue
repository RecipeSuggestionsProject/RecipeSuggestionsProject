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
            <router-view />
        </div>

        <main>
            <!-- Προσθήκη του component recipeSearch -->
            <RecipeSearch />
        </main>
    </div>
</template>

<script>
    import { ref, computed } from 'vue'
    import { createRouter, createWebHistory } from 'vue-router';

    
    import RecipeSearch from './components/homePage/recipeSearch.vue';

    export default {
        components: {
            RecipeSearch
        },
    };
    
</script>

<style scoped>
    * {
        padding: 0;
        margin: 0;
    }

    #app {
        height: 100vh;
        background-color: pink;
    }


    .webHeader {
        position: fixed;
        top: 0;
        width: 100%;
        z-index: 1000;
        background-color: rgb(133, 238, 201);
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
