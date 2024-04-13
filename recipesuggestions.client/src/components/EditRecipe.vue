<template>
    <div class="edit-recipe-container">
        <h2>Edit Recipe</h2>
        <div v-if="recipes.length === 0">Loading...</div>
        <div v-else class="recipes-container">
            <div v-for="recipe in sortedRecipes" :key="recipe.id" class="recipe-item">
                <h3>
                    {{ recipe.name }}
                    <button @click="editRecipe(recipe)">Edit</button>
                </h3>
            </div>
        </div>
        <div v-if="editing" class="edit-form-container">
            <h3>Edit Recipe</h3>
            <form @submit.prevent="submitEditedRecipe" class="edit-form">
                <div class="form-group">
                    <div class="recipe">
                        <div class="form-group">
                            <label for="recipe-name">Recipe Name: </label>
                            <input type="text" id="recipe-name" v-model="editedRecipe.name" required>
                        </div>
                        <div class="form-group">
                            <label for="recipe-servings">Servings: </label>
                            <input type="number" id="recipe-servings" v-model.number="editedRecipe.servings" min="1" required>
                        </div>
                        <div class="form-group">
                            <label for="recipe-duration">Duration (minutes): </label>
                            <input type="number" id="recipe-duration" v-model.number="editedRecipe.duration" min="1" required>
                        </div>
                    </div>
                    <div class="form-group">
                        <h2>Ingredients:</h2>
                        <div v-for="(ingredient, index) in editedRecipe.ingredients" :key="index" class="ingredient-input">
                            <input type="text" v-model="ingredient.name" placeholder="Ingredient Name" required>
                            <input type="number" v-model="ingredient.quantity" min="1" placeholder="Quantity" required>
                            <select v-model="ingredient.quantityType" required>
                                <option disabled value="">Select unit</option>
                                <option value="pieces">pieces</option>
                                <option value="grams">grams</option>
                                <option value="tablespoons">tablespoons</option>
                                <option value="teaspoons">teaspoons</option>
                                <option value="cups">cups</option>
                            </select>
                            <span class="space"></span>
                            <input type="text" v-model="ingredient.category" placeholder="Ingredient Category" required>
                            <button type="button" @click="removeIngredient(index)">Remove</button>
                        </div>
                        <button type="button" @click="addIngredient">Add Ingredient</button>
                    </div>
                    <div class="form-group">
                        <h3>Description:</h3>
                        <textarea id="recipe-description" v-model="editedRecipe.description" required></textarea>
                    </div>
                </div>
                <button type="submit" class="submit-button">Submit</button>
            </form>
            <div v-if="successMessage" class="success-message">
                {{ successMessage }}
                <button @click="editNextRecipe">Edit Next Recipe</button>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                recipes: [],
                editing: false,
                editedRecipe: {
                    id: '',
                    name: '',
                    servings: '',
                    duration: '',
                    ingredients: [],
                    description: ''
                },
                successMessage: ''
            };
        },
        computed: {
            sortedRecipes() {
                return this.recipes.slice().sort((a, b) => a.name.localeCompare(b.name));
            }
        },
        mounted() {
            this.fetchRecipes();
        },
        methods: {
            async fetchRecipes() {
                const response = await fetch("/api/recipes");
                this.recipes = await response.json();
            },
            editRecipe(recipe) {
                this.editing = true;
                this.editedRecipe = { ...recipe }; // Create a copy of the recipe object
            },
            async submitEditedRecipe() {
                const response = await fetch(`/api/recipes/${this.editedRecipe.id}`, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(this.editedRecipe)
                });
                if (response.ok) {
                    this.successMessage = "Recipe updated successfully.";
                } else {
                    this.successMessage = "Failed to update recipe.";
                }
            },
            addIngredient() {
                this.editedRecipe.ingredients.push({ name: '', quantity: '', quantityType: '', category: '' });
                this.recipe.ingredients.push({ name: '', quantity: '', unit: '', category: '' });
            },
            removeIngredient(index) {
                this.recipe.ingredients.splice(index, 1);
            },
            editNextRecipe() {
                this.editing = false;
                this.successMessage = '';
                // Move to editing the next recipe if available
                const nextIndex = this.recipes.findIndex(recipe => recipe.id === this.editedRecipe.id) + 1;
                if (nextIndex < this.recipes.length) {
                    this.editRecipe(this.recipes[nextIndex]);
                }
            }
        }
    };
</script>

<style scoped>
    .edit-recipe-container { /* Γενική, */
        display: flex;
        flex-direction: row;
        font-family: 'Montserrat';
        font-weight: 300;
    }

    h2 {
        font-family: 'Montserrat';
        font-weight: 500;
        margin-right:20px;
    }

    .recipes-container {
        flex: 1;
    }

    .edit-form-container {
        flex:1;
        margin-left: 20px; /* Επιθυμητό περιθώριο μεταξύ της λίστας των συνταγών και της φόρμας επεξεργασίας */

        font-family: 'Montserrat';
        font-weight: 300;
    }

    .edit-form {
        /* Επιθυμητά στυλ για τη φόρμα επεξεργασίας */
    }

    .ingredient-input {
        display: flex;
        margin-bottom: 0.5rem;
        justify-content: center;
        align-items: center;
        color: #9c9c9c
    }


        .ingredient-input input {
            margin-right: 0.5rem;
            color: #808080;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

    .success-message {
        /* Επιθυμητά στυλ για το μήνυμα επιτυχίας */
    }

    button { /* Add ingredient, submit */
        display: inline_block;
        background: white;
        transition: all 200ms ease-in;
        border: 1px solid #ccc;
        border-radius: 3px;
        cursor: pointer;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        display: flex;
        flex-direction: column;
        align-items: center;
    }
</style>
