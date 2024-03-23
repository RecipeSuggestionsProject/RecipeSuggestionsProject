<script setup>
import { ref, computed } from 'vue'


const ingredients = ref([])

const recipeName = ref("")
const recipeDescription = ref("")
const recipePortions = ref(0)
const recipeDurationInMinutes = ref(0)

const recipe = computed(() => {
    return {
        name: recipeName.value,
        description: recipeDescription.value,
        portions: recipePortions.value,
        durationInMinutes: recipeDurationInMinutes.value,
        ingredients: ingredients.value
    }
})

function addIngredient() {
    ingredients.value.push({
        name: "",
        quantity: 0,
        quantityType: "",
        type: ""
    })
}

function deleteIngredient(ingredientToDelete) {
    ingredients.value = ingredients.value.filter((ingredient) => ingredient !== ingredientToDelete)
}

async function postRecipe() {
    const response = await fetch("/api/Recipes", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(recipe.value)
    })
    console.log(await response.json());
}

</script>

<style>
    body {
        background-color: lightgrey;
    }
</style>

<template>

    <h1>Add A New Recipe </h1>

    <form @submit.prevent="postRecipe">
        <div class="field">
            <label for="recipe_name">Recipe Name :</label>
            <input id="recipe_name" v-model="recipeName" />
            
        </div>

        <div class="field">
            <label for="recipe_description">Description :</label>
            <textarea id="recipe_description" v-model="recipeDescription" />
        </div>

        <div class="field">
            <label for="recipe_portions">Portions :</label>
            <input id="recipe_portions" type="number" min="0" v-model="recipePortions" />
        </div>

        <div class="field">
            <label for="recipe_duration">Duration (minutes) :</label>
            <input id="recipe_duration" type="number" min="0" v-model="recipeDurationInMinutes" />
        </div>

        <div>
            <h4>Ingredients</h4>
            <ul>
                <li v-for="ingredient in ingredients">
                    <div class="field">
                        <label for="ingredient_name_{{ingredient.name}}">Name:</label>
                        <input id="ingredient_name_{{ingredient.name}}" v-model="ingredient.name" />
                    </div>

                    <div class="field">
                        <label for="ingredient_quantity_{{ingredient.name}}">Quantity:</label>
                        <input id="ingredient_quantity_{{ingredient.name}}" type="number" min="0" v-model="ingredient.quantity" />

                        <select v-model="ingredient.quantityType">
                            <option value="pieces">pieces</option>
                            <option value="grams">grams</option>
                            <option value="tablespoons">tablespoons</option>
                            <option value="teaspoons">teaspoons</option>
                            <option value="cups">cups</option>
                        </select>
                    </div>

                    <div class="field">
                        <label for="ingredient_type_{{ingredient.name}}">Type:</label>
                        <input id="ingredient_type_{{ingredient.name}}" v-model="ingredient.type" />
                    </div>

                    <button type="button" @click="deleteIngredient(ingredient)">Delete Ingredient</button>
                </li>
            </ul>

            <button type="button" @click="addIngredient">Add Ingredient</button>
        </div>

        <button>Submit</button>
    </form>
</template>

<style scoped>
    /*
    .field {
        display: flex;
        justify-content: space-between;
    }

    */

</style>