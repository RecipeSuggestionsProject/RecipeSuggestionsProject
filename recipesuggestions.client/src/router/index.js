import { createRouter, createWebHistory } from "vue-router";
import AddRecipe from '../components/AddRecipe.vue';
import EditRecipe from '../components/EditRecipe.vue';

const history = createWebHistory();

const routes = [
    { path: '/recipes/add', component: AddRecipe },
    { path: '/recipes/edit', component: EditRecipe },
];

const router = createRouter({
    history,
    routes,
});

export default router;

