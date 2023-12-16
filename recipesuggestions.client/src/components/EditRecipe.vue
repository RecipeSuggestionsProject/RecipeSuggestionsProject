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
                        <label for="ingredient_type_{{ingredient.name}}">Type:</label>
                        <input id="ingredient_type_{{ingredient.name}}" v-model="ingredient.type" />
                      </div>

                      <button type="button" @click="deleteIngredient(ingredient)">Delete Ingredient</button>
                    </li>
              </ul>

              <button type="button" @click="addIngredient">Add Ingredient</button>
          </div>

          <button>Update</button>
      </form>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue';

const recipeName = ref("");
const recipeDescription = ref("");
const recipePortions = ref(0);
const recipeDurationInMinutes = ref(0);

const ingredients = ref([]);

/*
κάπου δω θα γίνει prop για το id
const response = await fetch(`/api/Recipes/${recipeId.value}`);
const recipeData = await response.json();
recipeName.value = recipeData.name;
recipeDescription.value = recipeData.description;
recipePortions.value = recipeData.portions;
recipeDurationInMinutes.value = recipeData.durationInMinutes;
ingredients.value = [...recipeData.ingredients];
*/

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
    const response = await fetch(`/api/Recipes/${recipeId.value}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(recipe.value)
    });
    console.log("Recipe updated:", await response.json());
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
