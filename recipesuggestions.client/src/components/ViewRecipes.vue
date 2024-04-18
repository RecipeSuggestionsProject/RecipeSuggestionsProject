<template>
    <div class="recipe">
        <div v-if="recipes.length === 0">Loading...</div>
        <div v-else>
            <div class="recipe-container">
                <div v-for="(recipe, rIndex) in recipes" :key="recipe.id" class="recipe-details">
                    <h2>{{ recipe.name }}</h2>
                    <p><strong>Portions:</strong> {{ recipe.portions }}</p>
                    <p><strong>Duration:</strong> {{ recipe.durationInMinutes }} minutes</p>
                    <p><strong>Ingredients:</strong></p>
                    <ul class="ingredients-list">
                        <li v-for="(ingredientWithQuantity, iIndex) in recipe.ingredients" :key="iIndex">
                            <template v-if="ingredientWithQuantity && ingredientWithQuantity.ingredient">
                                <div>
                                    <p><strong>Name:</strong> {{ ingredientWithQuantity.ingredient.name }}</p>
                                    <p><strong>Category:</strong> {{ ingredientWithQuantity.ingredient.type }}</p>
                                    <p><strong>Amount:</strong> {{ ingredientWithQuantity.quantity }} {{ ingredientWithQuantity.qauntityType }}</p>
                                </div>
                            </template>
                            <template v-else>
                                <span>Invalid Ingredient</span>
                            </template>
                        </li>
                    </ul>
                    <div class="description-container">
                        <p class="description" :class="{ 'expanded': recipe.expanded }">{{ recipe.description }}</p>
                        <button @click="toggleDescription(recipe)" v-if="recipe.description.length > 100">
                            <span :style="{ color: 'black' }">{{ recipe.expanded ? 'Read less' : 'Read more' }}</span>
                        </button>
                    </div>
                    <div class="buttons">
                        <!--<button @click="navigateToRecipeEdit(recipe.id)" class="edit">Edit</button>-->
                        <button @click="confirmDelete(recipe.id)" class="delete">Delete</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, onMounted } from 'vue';
    import router from '../router';

    const recipes = ref([]);

    onMounted(() => {
        fetchRecipes();
    });

    async function fetchRecipes() {
        const response = await fetch("/api/recipes");

        if (response.ok) {
            recipes.value = await response.json();
            sortRecipes();
        }
    }

    function sortRecipes() {
        recipes.value.sort((a, b) => a.name.localeCompare(b.name));
    }

    /*
    function navigateToRecipeEdit(id) {
        router.push('recipes/edit/' + id);
    }
    */

    async function deleteRecipe(id) {
        await fetch('/api/recipes/' + id, {
            method: "DELETE"
        });
        window.location.reload();
    }

    function confirmDelete(id) {
        if (confirm("Are you sure you want to delete this recipe?")) {
            deleteRecipe(id);
        }
    }

    function toggleDescription(recipe) {
        recipe.expanded = !recipe.expanded;
    }
</script>

<style scoped>
    .recipe {
        background-color: rgb(255, 229, 213);
    }

    .recipe-container {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-between;
        margin: 20px;
       
    }

    .recipe-details {
        margin-top: 20px;
        width: 30%;
        margin-bottom: 20px;
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
        background-color: rgb(255, 240, 230);
        font-family: 'Montserrat';
        font-weight: 500;
    }

    span {
        font-family: 'Montserrat';
        font-weight: 300;
    }

    .ingredients-list {
        list-style-type: none;
        padding-left: 0;
    }

    .ingredients-list li {
        margin-bottom: 10px;
        border-bottom: 1px solid #ccc;
        padding-bottom: 10px;
    }

    .buttons {
        margin-top: 15px;
    }

    .delete {
        color: rgb(255, 51, 51);
        border-color: rgb(255, 51, 51);
        font-size: 15px; /* Προσαρμογή του μεγέθους του κουμπιού */
        padding: 3px 5px; /* Προσαρμογή του padding για να είναι πιο μικρό */
    }

    .buttons button {
        margin-bottom: 5px;
        display: inline-block;
        background: white;
        transition: all 200ms ease-in;
        border: 1px solid #ccc;
        border-radius: 3px;
        cursor: pointer;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        margin-right: 10px;
    }

    .description-container {
        position: relative;
    }

    .description {
        overflow: hidden;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-line-clamp: 3; /* Περιορισμός σε 3 γραμμές */
        -webkit-box-orient: vertical;
        position: relative;
        margin-bottom: 10px;
    }

    .description.expanded {
        -webkit-line-clamp: unset; /* Κατάργηση του περιορισμού */
    }

    
</style>
