import { createRouter, createWebHistory } from "vue-router";

import AddRecipe from '../components/AddRecipe.vue';
import EditRecipe from '../components/EditRecipe.vue';
import ViewRecipes from "../components/ViewRecipes.vue";

import RecipeSearch from '@/components/homePage/recipeSearch.vue';


const history = createWebHistory();

const routes = [
    { path: '/recipes/add', component: AddRecipe },
    { path: '/recipes/edit/:id?', component: EditRecipe, props: true },
    { path: '/recipes', component: ViewRecipes },
    { path: '/', component: RecipeSearch }
];

const router = createRouter({
    history,
    routes,
});

export default router;
