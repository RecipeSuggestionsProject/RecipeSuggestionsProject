<template>
  <div>
      <h1>Edit Recipe</h1>

      <form @submit.prevent="updateRecipe">
          <div class="field">
              <label for="recipe_name">Name:</label>
              <input id="recipe_name" v-model="recipeName" />
          </div>

          <div class="field">
              <label for="recipe_description">Description:</label>
              <textarea id="recipe_description" v-model="recipeDescription" />
          </div>

          <div class="field">
              <label for="recipe_portions">Portions:</label>
              <input id="recipe_portions" type="number" min="0" v-model="recipePortions" />
          </div>

          <div class="field">
              <label for="recipe_duration">Duration (minutes):</label>
              <input id="recipe_duration" type="number" min="0" v-model="recipeDurationInMinutes" />
          </div>

          <div>
              <h4>Ingredients</h4>
              <ul>
                  <li v-for="(ingredient, index) in ingredients" :key="index">
                      <div class="field">
                          <label :for="'ingredient_name_' + index">Name:</label>
                          <input :id="'ingredient_name_' + index" v-model="ingredient.name" />
                      </div>

                      <div class="field">
                          <label :for="'ingredient_quantity_' + index">Quantity:</label>
                          <input :id="'ingredient_quantity_' + index" type="number" min="0" v-model="ingredient.quantity" />

                          <select v-model="ingredient.quantityType">
                              <option value="pieces">pieces</option>
                              <option value="grams">grams</option>
                              <option value="tablespoons">tablespoons</option>
                              <option value="teaspoons">teaspoons</option>
                              <option value="cups">cups</option>
                          </select>
                      </div>

                      <div class="field">
                          <label :for="'ingredient_type_' + index">Type:</label>
                          <input :id="'ingredient_type_' + index" v-model="ingredient.type" />
                      </div>

                      <button type="button" @click="deleteIngredient(index)">Delete Ingredient</button>
                  </li>
              </ul>

              <button type="button" @click="addIngredient">Add Ingredient</button>
          </div>

          <button>Update</button>
      </form>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';

const recipeId = ref(/* Set the recipe id here */);

// Mock data for testing purposes
const mockRecipeData = {
name: 'Test Recipe',
description: 'This is a test recipe',
portions: 4,
durationInMinutes: 30,
ingredients: [
  { name: 'Ingredient 1', quantity: 100, quantityType: 'grams', type: 'Type A' },
  { name: 'Ingredient 2', quantity: 2, quantityType: 'pieces', type: 'Type B' },
],
};

const recipeName = ref("");
const recipeDescription = ref("");
const recipePortions = ref(0);
const recipeDurationInMinutes = ref(0);
const ingredients = ref([]);

onMounted(() => {
// Fetch the recipe data from the server based on recipeId
// For now, use the mockRecipeData
recipeName.value = mockRecipeData.name;
recipeDescription.value = mockRecipeData.description;
recipePortions.value = mockRecipeData.portions;
recipeDurationInMinutes.value = mockRecipeData.durationInMinutes;
ingredients.value = [...mockRecipeData.ingredients];
});

const recipe = computed(() => {
return {
  name: recipeName.value,
  description: recipeDescription.value,
  portions: recipePortions.value,
  durationInMinutes: recipeDurationInMinutes.value,
  ingredients: ingredients.value,
};
});

function addIngredient() {
ingredients.value.push({
  name: "",
  quantity: 0,
  quantityType: "",
  type: ""
});
}

function deleteIngredient(index) {
ingredients.value.splice(index, 1);
}

async function updateRecipe() {
// Implement the logic to update the recipe on the server
console.log("Recipe updated:", recipe.value);
// Assume a successful update for now
}

</script>

<style scoped>
  .field {
      display: flex;
      justify-content: space-between;
      margin-bottom: 10px;
  }

  ul {
      list-style-type: none;
      padding: 0;
  }

  li {
      border: 1px solid #ccc;
      padding: 10px;
      margin-bottom: 10px;
  }
</style>
