<template>
    <div v-if="recipes.length === 0">Loading...</div>
    <ul v-else>
        <li v-for="(recipe, rIndex) in recipes">
            <h2>{{ recipe.name }}</h2>
            <div>
                <label for="recipe-description-{{rIndex}}">Description: </label>
                <span id="recipe-description-{{rIndex}}">{{ recipe.description }}</span>
            </div>
            <div>
                <label for="recipe-portions-{{rIndex}}">Portions: </label>
                <span id="recipe-portions-{{rIndex}}">{{ recipe.portions }}</span>
            </div>
            <div>
                <label for="recipe-duration-{{rIndex}}">Duration: </label>
                <span id="recipe-duration-{{rIndex}}">{{ recipe.durationInMinutes }} minutes</span>
            </div>
            <div>
                <label for="recipe-ingredients-{{rIndex}}">Ingredients: </label>
                <ul id="recipe-ingredients-{{rIndex}}">
                    <li v-for="(ingredientWithQuantity, iIndex) in recipe.ingredients">
                        <template v-if="ingredientWithQuantity && ingredientWithQuantity.ingredient">
                        <h4>{{ ingredientWithQuantity.ingredient.name }}</h4>
                        <div>
                            <label for="ingredient-type-{{iIndex}}">Category: </label>
                            <span id="ingredient-type-{{iIndex}}">{{ ingredientWithQuantity.ingredient.type }}</span>
                        </div>
                        <div>
                            <label for="ingredient-quantity-{{iIndex}}">Amount: </label>
                            <span id="ingredient-quantity{{iIndex}}">
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
            <button @click="navigateToRecipeEdit(recipe.id)">Edit</button>
            <button @click="deleteRecipe(recipe.id)">Delete</button>
        </li>
    </ul>
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

</script>