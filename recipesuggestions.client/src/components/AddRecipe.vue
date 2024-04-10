<template>
    <div class="recipe-form">
        <h2>Create a Recipe </h2>
        <form @submit.prevent="submitRecipe">
            <div class="form-group">
                <label for="recipe-name">Recipe Name: </label>
                <input type="text" id="recipe-name" v-model="recipe.name" required>
            </div>
            <div class="form-group">
                <label for="recipe-servings">Servings: </label>
                <input type="number" id="recipe-servings" v-model.number="recipe.servings" min="1" required>
            </div>
            <div class="form-group">
                <label for="recipe-duration">Duration (minutes): </label>
                <input type="number" id="recipe-duration" v-model.number="recipe.duration" min="1" required>
            </div>
            <div class="form-group">
                <h3>Ingredients:</h3>
                <div v-for="(ingredient, index) in recipe.ingredients" :key="index" class="ingredient-input">
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
                <h4>Description:</h4>
                <textarea id="recipe-description" v-model="recipe.description" required></textarea>
            </div>
            <button type="submit" class="submit-button">Submit</button>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                recipe: {
                    name: '',
                    servings: 1,
                    duration: '',
                    ingredients: [{ name: '', quantity: '', unit: '', category: '' }],
                    description: '',
                }
            };
        },
        methods: {
            addIngredient() {
                this.recipe.ingredients.push({ name: '', quantity: '', unit: '', category: '' });
            },
            removeIngredient(index) {
                this.recipe.ingredients.splice(index, 1);
            },
            submitRecipe() {
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
                // Here you can submit the recipe data to your backend or perform any other action
                console.log(this.recipe);
            }
        }
    };
</script>

<style scoped>
    .form-group {
        margin-bottom: 1rem;
    }

    .ingredient-input {
        display: flex;
        margin-bottom: 0.5rem;
    }

    .ingredient-input input {
        margin-right: 0.5rem;
    }

    .recipe-form {
        height: 100vh; /* Κάνει τη φόρμα να καλύπτει ολόκληρη την οθόνη */
        display: flex;
        justify-content: center; /* Κεντράρει το περιεχόμενο οριζόντια */
        align-items: center; /* Κεντράρει το περιεχόμενο κατακόρυφα */
    }

    .recipe-form form {
        width: 90%; /* Προσαρμόζει το πλάτος της φόρμας */
        max-width: 600px; /* Ορίζει το μέγιστο πλάτος της φόρμας */
    }


    button {
        cursor: pointer;
    }

    .space {
        margin: 0 5px;
    }

    .submit-button {
        background-color: #4caf50;
        color: white;
        border: none;
        padding: 0.5rem 1rem;
        cursor: pointer;
        border-radius: 5px;
        transition: background-color 0.3s;
    }

    .submit-button:hover {
        background-color: #45a049;
    }

    .submit-button:active {
        background-color: #3e8e41;
    }
</style>
