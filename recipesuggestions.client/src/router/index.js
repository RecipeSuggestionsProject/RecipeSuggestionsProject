import { createRouter, createWebHistory } from "vue-router";

import Home from '../app.vue'
import AddRecipe from '../components/AddRecipe.vue';
import EditRecipe from '../components/EditRecipe.vue';

//import RecipeSearch from '@/components/homePage/recipeSearch.vue';

const history = createWebHistory();

const routes = [
    { path: '/recipes/add', component: AddRecipe },
    { path: '/recipes/edit', component: EditRecipe },

    { path: '/', component: Home }
];

const router = createRouter({
    history,
    routes,
});

export default router;
