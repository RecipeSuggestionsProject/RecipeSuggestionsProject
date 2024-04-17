<template>
    <div class="edit-recipe-container">
        <h4>Edit Recipe</h4>
        <div v-if="recipes.length === 0">Loading...</div>
        <div v-else class="recipes-container">
            <div class="recipe-list">
                <button v-for="recipe in sortedRecipes" :key="recipe.id" @click="editRecipe(recipe)" class="recipe-item">
                    <h3>
                        {{ recipe.name }}
                    </h3>
                </button>
            </div>
            <div v-if="editing" class="edit-form-container">
                <form @submit.prevent="submitEditedRecipe" class="edit-form">
                    <div class="form-group">
                        <div class="recipe">
                            <div class="form-group">
                                <label for="recipe-name">Recipe Name: </label>
                                <input type="text" id="recipe-name" v-model="editedRecipe.name" required>
                            </div>
                            <div class="form-group">
                                <label for="recipe-portions">Servings: </label>
                                <input type="number" id="recipe-portions" v-model.number="editedRecipe.portions" min="1" required>
                            </div>
                            <div class="form-group">
                                <label for="recipe-durationinMinutes">Duration (minutes): </label>
                                <input type="number" id="recipe-durationInMinutes" v-model.number="editedRecipe.durationInMinutes" min="1" required>
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
                                <button type="button" @click="removeIngredient(index)" class="remove" :disabled="editedRecipe.ingredients.length <= 1">Remove</button>
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
                this.editedRecipe = { ...recipe };
                this.$router.push('/recipes/edit/' + recipe.id);
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
                this.editedRecipe.ingredients.splice(index, 1);
            },
            editNextRecipe() {
                this.editing = false;
                this.successMessage = '';
                const nextIndex = this.recipes.findIndex(recipe => recipe.id === this.editedRecipe.id) + 1;
                if (nextIndex < this.recipes.length) {
                    this.editRecipe(this.recipes[nextIndex]);
                }
            }
        }
    };
</script>

<style scoped>
    .edit-recipe-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        font-family: 'Montserrat', sans-serif;
        font-weight: 300;
        background-color: rgb(255, 229, 213);
    }

    .recipes-container {
        display: flex;
        justify-content: space-between;
        width: 100%;
        max-width: 800px;
        margin-top: 20px;
    }

    .recipe-list {
        width: 48%;
        overflow-y: auto;
    }

    .recipe-item h3 {
        font-family: 'Montserrat';
        font-weight: 300;
        margin: 0;
    }

    h2, h3 {
        font-family: 'Montserrat';
        font-weight: 500;
        margin-right: 20px;
    }

    h4 {
        font-family: 'Montserrat';
        font-size: 26px;
        margin-right: 20px;
        font-weight: 500;
    }

    .ingredient-input input {
        margin:6px;
    }

    .recipe-item {
        background-color: white;
        border: 1px solid #ccc;
        border-radius: 3px;
        border: none;
        color: #808080;
        font-size: 12px;
        margin: 6px;
        height: 25px;
        line-height: 0;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        padding: 12px;
        margin-bottom: 8px;
        cursor: pointer;
    }

        .recipe-item h3 {
            margin: 0;
        }

    .remove {
        color: rgb(255, 51, 51);
        background-color: #fff;
        border-color: rgb(255, 51, 51);
    }

    .edit-form-container {
        width: 45%;
        padding: 20px;
        border: 1px solid #ccc;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        background-color: #f9f9f9; /* Υπόβαθρο στο edt recipe */
    }

    .form-group {
        margin-bottom: 16px;
    }

    label {
        display: block;
        margin-bottom: 6px;
    }

    input[type="text"], /* Περίγραμμα πεδίων */
    input[type="number"], textarea {
        border: 1px solid #ccc;
        border-radius: 3px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        font-family: 'Montserrat';
        font-weight: 300;

    }


    .input-field {
        box-sizing: border-box;
    }

    textarea {
        width: 100%;
        height: 80px;
        padding: 8px;
        box-sizing: border-box;
    }

    .submit-button {
        background-color: #4caf50;
        color: white;
        border: none;
        padding: 10px 20px;
        border-radius: 4px;
        cursor: pointer;
    }

    button {
        background: white;
        border: 1px solid #ccc;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        border-radius: 3px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .success-message {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: #4caf50;
        color: white;
        padding: 1rem;
        border-radius: 5px;
        font-family: 'Montserrat';
        font-weight: 300;
    }
</style>
