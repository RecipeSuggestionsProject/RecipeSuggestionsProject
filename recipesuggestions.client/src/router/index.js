import { createRouter, createWebHistory } from "vue-router";

import AddRecipe from '../components/AddRecipe.vue';
import EditRecipe from '../components/EditRecipe.vue';

import RecipeSuggestions from '@/components/homePage/RecipeSuggestions.vue';

const history = createWebHistory();

const routes = [
    { path: '/recipes/add', component: AddRecipe },
    { path: '/recipes/edit', component: EditRecipe },
    { path: '/RecipeSuggestions', component: RecipeSuggestions }
];

const router = createRouter({
    history,
    routes,
});

export default router;
