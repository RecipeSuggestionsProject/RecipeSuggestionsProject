<template>
    <div class="recipe">
        <div v-if="recipes.length === 0">Loading...</div>
        <ul v-else>
            <li v-for="(recipe, rIndex) in recipes" :key="recipe.id">
                <h2>{{ recipe.name }}</h2>
                <div class="recipe-details">
                    <div class="recipe-group">
                        <label>Description: </label>
                        <span>{{ recipe.description }}</span>
                    </div>
                    <div class="recipe-group">
                        <label>Portions: </label>
                        <span>{{ recipe.portions }}</span>
                    </div>
                    <div class="recipe-group">
                        <label>Duration: </label>
                        <span>{{ recipe.durationInMinutes }} minutes</span>
                    </div>
                    <div class="recipe-group">
                        <label>Ingredients: </label>
                        <ul>
                            <li v-for="(ingredientWithQuantity, iIndex) in recipe.ingredients" :key="iIndex">
                                <template v-if="ingredientWithQuantity && ingredientWithQuantity.ingredient">
                                    <h4>{{ ingredientWithQuantity.ingredient.name }}</h4>
                                    <div class="ingredient-group">
                                        <label>Category: </label>
                                        <span>{{ ingredientWithQuantity.ingredient.type }}</span>
                                    </div>
                                    <div class="ingredient-group">
                                        <label>Amount: </label>
                                        <span>
                                            {{ ingredientWithQuantity.quantity }} {{ ingredientWithQuantity.qauntityType }}
                                        </span>
                                    </div>
                                </template>
                                <template v-else>
                                    <span>Invalid Ingredient</span>
                                </template>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="buttons">
                    <button @click="navigateToRecipeEdit(recipe.id)" class="edit">Edit</button>
                    <button @click="confirmDelete(recipe.id)" class="delete">Delete</button>
                </div>
            </li>
        </ul>
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
        }
    }

    function navigateToRecipeEdit(id) {
        router.push('recipes/edit/' + id);
    }

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
</script>

<style scoped>
    .recipe {
        background-color: rgb(255, 229, 213);
    }

    .recipe-details {
        padding: 10px;
    }

    .recipe-group {
        margin-bottom: 10px;
    }

    .recipe-group label {
        font-weight: bold;
    }

    .buttons {
        margin-top: 20px;
    }

    button {
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

    .delete {
        color: rgb(255, 51, 51);
        background-color: #fff;
        border-color: rgb(255, 51, 51);
    }

    form {
        background-color: rgb(255, 229, 213);
    }
</style>
